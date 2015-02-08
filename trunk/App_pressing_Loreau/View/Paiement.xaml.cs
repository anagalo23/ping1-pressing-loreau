using App_pressing_Loreau.Data;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour Paiement.xaml
    /// </summary>
    public partial class Paiement : UserControl
    {
        public Paiement()
        {
            InitializeComponent();
            DataContext = new PaiementVM();
        }

        private void btn_paiment_valider_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ClasseGlobale.reste_a_payer == 0)
            {


                ListePaiement listeDeMontantParMoyenPaiement = ClasseGlobale.listeDeMontantParMoyenPaiement;

                //Récupération des articles de la commande, du client, et du paiement et enregistrement en bdd

                //int i = 0, j = 0, k = 0;
                Client client = ClasseGlobale.Client;

                //***Enregistrement en base de données***

                //Enregistrement de la commande

                if (listeDeMontantParMoyenPaiement != null)
                {

                    ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;

                    List<Article> ListeSelectArt = ClasseGlobale._rendreArticlesSelectionnes;
                    //Ajouter ??????????????????????????????????????????????????????????????????????????????????????????????????????????
                    if (cmdDetail != null)
                    {

                        Commande cmd = new Commande(DateTime.Now, true, ClasseGlobale.remise, client);
                        CommandeDAO.insertCommande(cmd);
                        cmd = CommandeDAO.lastCommande();

                        //Enregistrement des articles
                        foreach (ArticlesVM artVM in cmdDetail)
                        {
                            ArticleDAO.insertArticle(artVM.getArticle(cmd.id));
                        }

                        //Enregistrement du/des paiement(s)
                        Payement paiement;
                        ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;
                        foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
                        {
                            paiement = new Payement(DateTime.Now, listeDeMontantParMoyenPaiement[monMoyenDePaiement], monMoyenDePaiement, cmd.id);
                            PayementDAO.insertPaiement(paiement);
                        }

                        //Mise à jour de la table convoyeur
                        foreach (PlaceConvoyeur place in ClasseGlobale.PlacesLibres.getList())
                        {
                            PlaceConvoyeurDAO.updatePlaceConvoyeur(place);
                        }


                        if (ClasseGlobale.Client.nom == "")
                        {
                            MessageBox.Show("problème avec le client, il est null");
                        }

                        Commande cmdTota = CommandeDAO.selectCommandeById(cmd.id, true, true, true);
                      
                        try
                        {
                            RecuPaiement rp = new RecuPaiement(cmdTota);
                            rp.printRecu();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impression refusée");
                        }
                        finally
                        {
                            //initialise tout
                            ClasseGlobale.INITIALIZE_ALL();
                            Bdd.deconnexion();
                        }


                        //FactureExcel fe = new FactureExcel(CommandeDAO.selectCommandeById(cmd.id, true, true, true));
                        //fe.printFacture();
                    }
                    else if (ListeSelectArt != null)
                    {
                        Commande comdRendu = ClasseGlobale._renduCommande;

                        foreach (Article art in ListeSelectArt)
                        {
                            Article artAdd = new Article(art.id, art.photo, art.commentaire, true, art.TVA, art.TTC, art.type, null, comdRendu.id);
                            artAdd.date_rendu = DateTime.Now;
                            ArticleDAO.updateArticle(art);
                        }


                        //Enregistrement du/des paiement(s)
                        Payement paiement;
                        ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;
                        foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
                        {
                            paiement = new Payement(DateTime.Now, listeDeMontantParMoyenPaiement[monMoyenDePaiement], monMoyenDePaiement, comdRendu.id);
                            PayementDAO.insertPaiement(paiement);
                        }

                        Commande cmdTota = CommandeDAO.selectCommandeById(comdRendu.id, true, true, true);

                        try
                        {
                            RecuPaiement rp = new RecuPaiement(cmdTota);
                            rp.printRecu();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impression refusée");
                        }
                        finally
                        {
                            //initialise tout
                            ClasseGlobale.INITIALIZE_ALL();
                            Bdd.deconnexion();
                        }
                    }

                    //Accueil page2Obj = new Accueil(); //Create object of Page2
                    //page2Obj.Show(); //Show page2
                    //this.Close();

                }
                else
                {
                    MessageBox.Show("Toute la commande n'a pas été payée. Veuillez s'il vous plait compléter l'intégralité du paiement.");
                }






                dp.Children.Clear();
                //On retourne à l'accueil
                //AccueilVM ac = new AccueilVM();
                //MessageBox.Show("salut");
                //try
                //{

                //    dp.Children.Add(new Accueil());
                //}
                //catch (InvalidOperationException ioe)
                //{

                //}

                //ac.Btn_receptionColor = Brushes.Teal;
            }
            else
            {
                MessageBox.Show("La commande n'est pas complétée, il reste " + ClasseGlobale.reste_a_payer + " à payer");
            }
        }
    }
}
