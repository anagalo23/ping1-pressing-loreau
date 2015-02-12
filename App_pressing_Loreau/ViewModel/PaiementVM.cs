using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using App_pressing_Loreau.Model;
using App_pressing_Loreau.View;
using Microsoft.Practices.Prism.Commands;

//using System.Windows.Controls.Button;
//using System.Windows.Forms;

namespace App_pressing_Loreau.ViewModel
{
    class PaiementVM : ObservableObject
    {
        #region Attributs

        //public static int commandePayee = 0;
        private String _label_paiement_prixHT;
        private String _label_paiement_prixTTC;
        private float _txb_paiement_montantRemise;
        private float[] checkRemise;
        private String _label_paiement_montant;//Prix ttc - remise

        //Correspond à la valeur du testbox prix à payer (pour 1 et seulement 1 moyen de paiement)
        private float _txb_paiement_montantParMoyenPaiement;

        //Prends en param : le moyen de paiement et le montant correspondant à ce moyen de paiement

        private ListePaiement listeDeMontantParMoyenPaiement = new ListePaiement();
        private String _mode_de_paiement;

        private List<GetPaiement> _itemsMontantParMoyenPaiement;

        //private ObservableCollection<PaiementListeVM> _contenuListePaiement;

        private float _reste_a_payer;


        private DelegateCommand<PaiementListeVM> _deletePaiement;

        //private bool erreurDeManip;
        //Payement paiement;
        #endregion

        #region constructeur

        public PaiementVM()
        {
            lePaiement();
            _txb_paiement_montantParMoyenPaiement = new float();

            _itemsMontantParMoyenPaiement = new List<GetPaiement>();

            //Txb_paiement_montantParMoyenPaiement = Label_NouvelleCommande_prixTotal;
            Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
            Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
            ClasseGlobale.reste_a_payer = Reste_a_payer;
            _mode_de_paiement = "";
            ClasseGlobale.initializeContenuListePaiement();
            checkRemise = new float[2] { 0F, 0F };

        }

        #endregion

        #region Accesseurs et mutateurs

        //public String Reste_a_payer_String
        //{
        //    get { return _reste_a_payer.ToString(); }
        //    set
        //    {
        //        _reste_a_payer = float.Parse(value);
        //        OnPropertyChanged("Reste_a_payer_String");
        //    }
        //}


        //public List<GetPaiement> ItemsMontantParMoyenPaiement
        //{
        //    get { return _itemsMontantParMoyenPaiement; }
        //    set
        //    {
        //        _itemsMontantParMoyenPaiement = value;
        //        OnPropertyChanged("ItemsMontantParMoyenPaiement");
        //    }
        //}


        public String Label_paiement_prixHT
        {
            get { return _label_paiement_prixHT; }
            set
            {
                if (value != _label_paiement_prixHT)
                {
                    _label_paiement_prixHT = value;
                    OnPropertyChanged("Label_paiement_prixHT");
                }
            }
        }

        public String Mode_de_paiement
        {
            get { return _mode_de_paiement; }
            set
            {
                if (value != _mode_de_paiement)
                {
                    _mode_de_paiement = value;
                    OnPropertyChanged("Mode_de_paiement");
                }
            }
        }

        public float Reste_a_payer
        {
            get { return _reste_a_payer; }
            set
            {
                if (value != _reste_a_payer)
                {
                    _reste_a_payer = value;
                    OnPropertyChanged("Reste_a_payer");
                }
            }
        }

        public String Label_paiement_prixTTC
        {
            get { return _label_paiement_prixTTC; }
            set
            {
                if (value != _label_paiement_prixTTC)
                {
                    _label_paiement_prixTTC = value;
                    OnPropertyChanged("Label_paiement_prixTTC");
                }
            }
        }

        public String Label_paiement_montant
        {
            get { return _label_paiement_montant; }
            set
            {
                if (value != _label_paiement_montant)
                {
                    _label_paiement_montant = value;
                    OnPropertyChanged("Label_paiement_montant");
                }
            }
        }



        public float Txb_paiement_montantRemise
        {
            get { return _txb_paiement_montantRemise; }
            set
            {
                if (value != _txb_paiement_montantRemise)
                {
                    _txb_paiement_montantRemise = value;
                    OnPropertyChanged("Txb_paiement_montantRemise");
                    //OnPropertyChanged("Txb_paiement_montantParMoyenPaiement");
                }
            }
        }

        public float Txb_paiement_montantParMoyenPaiement
        {
            get { return _txb_paiement_montantParMoyenPaiement; }
            set
            {
                if (value != _txb_paiement_montantParMoyenPaiement)
                {
                    _txb_paiement_montantParMoyenPaiement = value;
                    OnPropertyChanged("Txb_paiement_montantParMoyenPaiement");
                }
            }
        }

        //public ListePaiement MontantParMoyenPaiement
        //{
        // get { return listeDeMontantParMoyenPaiement; }
        // set
        // {
        // if (value != listeDeMontantParMoyenPaiement)
        // {
        // listeDeMontantParMoyenPaiement = value;
        // OnPropertyChanged("MontantParMoyenPaiement");
        // }
        // }
        //}


        #endregion

        #region Commandes

        #region Bouton valider commande
        public ICommand Btn_valider_paiement_commande
        {
            get
            {
                return new RelayCommand(
                    p => ValidationPaiement(),
                    p => ClasseGlobale.Client != null
                    );
            }
        }


        #endregion

        #region Bouton mode de paiement

        //Gère les boutons de mode de paiement
        ICommand btn_paiement;
        public ICommand Btn_paiement
        {
            get
            {
                //if (ClasseGlobale.Client.type == 1)
                return btn_paiement ?? (btn_paiement = new RelayCommand(getModeDePaiement));
                //return null;
            }
        }
        private void getModeDePaiement(object button)
        {
            System.Windows.Controls.Button clickedbutton = button as System.Windows.Controls.Button;
            //Si je ne suis pas un client pro je ne peux pas cliquer sur virement
            if (ClasseGlobale.Client.type == 0 && clickedbutton.Tag.ToString() == "VirementBancaire")
            {
                MessageBox.Show("Les virements bancaires sont résevés aux clients professionnels");
            }
            else
            {
                _mode_de_paiement = clickedbutton.Tag.ToString();
            }
            
        }
        #endregion

        #region Bouton valider le montant par mode de paiement ou valider la remise
        //Gère les boutons de mode de paiement
        ICommand btn_paiement_valider;
        public ICommand Btn_paiement_valider
        {
            get { return btn_paiement_valider ?? (btn_paiement_valider = new RelayCommand(completeLaListeDesModesDePaiement)); }
        }
        private void completeLaListeDesModesDePaiement(object button)
        {
            Button clickedbutton = button as Button;
            if (clickedbutton != null)
            {
                //Si j'ai sélectionné un mode de paiement et que la remise n'a pas bougée
                if (Mode_de_paiement != "" && checkRemise[0] == Txb_paiement_montantRemise)
                {
                    applyModeDePaiement();
                }

                //Si je n'ai pas sélectionné de mode de paiement mais que la remise à changée, j'applique la remise
                else if (Mode_de_paiement == "" && checkRemise[0] != Txb_paiement_montantRemise)
                {
                    applyRemise();
                }
                else
                {
                    //Récupération de la valeur saisie pour ce paiement
                    float valeur_desiree = Txb_paiement_montantParMoyenPaiement;
                    //MessageBox.Show("Veuillez d'abord appliquer votre remise, puis le paiement.\nInfo : ");
                    applyRemise();

                    Txb_paiement_montantParMoyenPaiement = valeur_desiree;

                    applyModeDePaiement();

                }

            }


        }

        #endregion

        #region
        public DelegateCommand<PaiementListeVM> DeletePaiement
        {
            get
            {
                return this._deletePaiement ?? (this._deletePaiement = new DelegateCommand<PaiementListeVM>(
                                                                       this.ExecuteDeletePaiement,
                                                                       (arg) => true));
            }
        }

        private void ExecuteDeletePaiement(PaiementListeVM obj)
        {
            //MessageBox.Show("mode de paiement : "+obj.ModeDePaiement+"\nMontant : "+obj.Montant);

            //listeDeMontantParMoyenPaiement[Mode_de_paiement] = Txb_paiement_montantParMoyenPaiement;

            //Actualisation des champs de texte
            Reste_a_payer += float.Parse(obj.Montant);

            //Suppression des champs de la liste
            listeDeMontantParMoyenPaiement.dico.Remove(obj.ModeDePaiement);

            //Attributio de la nouvelle valeur au champ de texte
            Txb_paiement_montantParMoyenPaiement = Reste_a_payer;

            //Initialisation -> Permet que la valeur bindée (en classe globale, soit modifiée aussi)
            InitialiseLaListeDePaiement();
        }

        #endregion


        #region ObservableCollection
        public ObservableCollection<PaiementListeVM> ContenuListePaiement
        {
            get
            {
                if (ClasseGlobale._contenuListePaiement == null)//ClasseGlobale.
                {
                    ClasseGlobale.initializeContenuListePaiement();
                    //_contenuListePaiement = new ObservableCollection<PaiementListeVM>();
                }
                return ClasseGlobale._contenuListePaiement;
            }

            set
            {
                if (value != null)
                {
                    ClasseGlobale._contenuListePaiement = value;
                    //_contenuListePaiement = value;
                    OnPropertyChanged("ContenuListePaiement");
                }
            }
        }
        #endregion

        #endregion

        #region Methods
        //initialisation de la page de paiement
        public void lePaiement()
        {
            //On récupère la classe globale contenant les articles et on calcul le prix
            ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;


            float prixHT = 0;
            float prixTTC = 0;
            float prixTTCrendu = 0;
            float prixHTrendu = 0;
            //Si je viens de la page de création de la commande
            if (cmdDetail != null)
            {
                try
                {
                    foreach (ArticlesVM art in cmdDetail)
                    {
                        //prixTTC += art.typeArticle.TTC;
                        //prixHT += art.typeArticle.TTC * (1 - art.typeArticle.TVA / 100);

                        prixTTC = (float)((decimal)prixTTC + (decimal)art.typeArticle.TTC);
                        prixHT = (float)((decimal)prixHT + (decimal)art.typeArticle.TTC * (1 - (decimal)art.typeArticle.TVA / 100));
                        prixHT = (float)Math.Round((decimal)prixHT + (decimal)art.typeArticle.TTC * (1 - (decimal)art.typeArticle.TVA / 100), 2, MidpointRounding.AwayFromZero);
                        
                    }
                    Label_paiement_prixHT = prixHT + " €";
                    Label_paiement_prixTTC = prixTTC + " €";
                    Label_paiement_montant = prixTTC - Txb_paiement_montantRemise + " €";
                }
                catch (Exception )
                {
                    //Inscription en log
                }
            }
            //Si je viens de la page de sélection des articles à payer
            else if (ClasseGlobale._rendreArticlesSelectionnes != null)
            {
                try
                {
                    foreach (Article artic in ClasseGlobale._rendreArticlesSelectionnes)
                    {
                        prixTTCrendu = (float)((decimal)prixTTCrendu + (decimal)artic.TTC);
                        prixHTrendu = (float)((decimal)prixHTrendu + (decimal)artic.TTC * (1 - (decimal)artic.TVA / 100));
                    }
                    Label_paiement_prixHT = prixHTrendu - prixHT + " €";
                    Label_paiement_prixTTC = prixTTCrendu - prixTTC + " €";
                    Label_paiement_montant = prixTTCrendu - prixTTC - Txb_paiement_montantRemise + " €";
                }
                catch (Exception e)
                {
                    //Inscription en log
                }
            }
            //else if (ClasseGlobale._renduCommandeClientPro != null)
            //{
            //    try
            //    {
            //        foreach (Article artic in ClasseGlobale._renduCommandeClientPro.listArticles)
            //        {
            //            prixTTCrendu = (float)((decimal)prixTTCrendu + (decimal)artic.TTC);
            //            prixHTrendu = (float)((decimal)prixHTrendu + (decimal)artic.TTC * (1 - (decimal)artic.TVA / 100));
            //        }
            //        Label_paiement_prixHT = prixHTrendu - prixHT + " €";
            //        Label_paiement_prixTTC = prixTTCrendu - prixTTC + " €";
            //        Label_paiement_montant = prixTTCrendu - prixTTC - Txb_paiement_montantRemise + " €";
            //    }
            //    catch (Exception e)
            //    {
            //        //Inscription en log
            //    }
            //}


        }

        private void ValidationPaiement()
        {
            if (ClasseGlobale.Client != null)
            {
                //Récupération des articles de la commande, du client, et du paiement et enregistrement en bdd

                Client client = ClasseGlobale.Client;

                //Validation du paiement

                if (Reste_a_payer == 0)
                {
                    //Récupère la liste des articles, il y'en a qu'une seule qui soit initialisée. L'autre est nulle
                    ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;
                    List<Article> ListeSelectArt = ClasseGlobale._rendreArticlesSelectionnes;

                    //Si je viens de l'écran de nouvelle commande
                    if (cmdDetail != null)
                    {
                        Commande cmd = new Commande(DateTime.Now, true, Txb_paiement_montantRemise, client);
                        CommandeDAO.insertCommande(cmd);
                        cmd = CommandeDAO.selectCommandeById(CommandeDAO.lastCommande().id, false, true, false);

                        //Enregistrement des articles
                        foreach (ArticlesVM artVM in cmdDetail)
                        {
                            Article art = artVM.getArticle(cmd.id);
                            art.date_payee = DateTime.Now;
                            ArticleDAO.insertArticle(art);
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



                        Commande cmdTota = CommandeDAO.selectCommandeById(cmd.id, true, true, true);

                        //MessageBox.Show("La commande " + cmdTota.id + " à été enregistrée avec succès");
                        try
                        {
                            RecuPaiement rp = new RecuPaiement(cmdTota);
                            rp.printRecu(); 
                            rp.printRecu();

                            if (cmdTota.listArticles != null)
                            {
                                TicketVetement ticketVetement = new TicketVetement(cmdTota);
                                ticketVetement.printAllArticleCmd();
                            }
                            else
                                MessageBox.Show("La commande ne contient pas d'articles");

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impression refusée");
                        }
                        finally
                        {
                            //initialise tout
                            ClasseGlobale.SET_ALL_NULL();
                        }


                        //FactureExcel fe = new FactureExcel(CommandeDAO.selectCommandeById(cmd.id, true, true, true));
                        //fe.printFacture();
                    }
                    //Si je viens de l'écran de rendu des articles
                    else if (ListeSelectArt != null)
                    {
                        Commande comdRendu = ClasseGlobale._renduCommande;

                        foreach (Article art in ListeSelectArt)
                        {
                            //Mise à jour de la place convoyeur
                            //1 - dans la table convoyeur : on soustrait l'encombrement
                            //2 - dans la table article : id convoyeur devient nul

                            art.convoyeur.encombrement = (float)((decimal)art.convoyeur.encombrement - (decimal)art.type.encombrement);
                            //Si un article est à la même place, il faut modifier sa place convoyeur pour qu'elle corresponde au changement appliqué
                            //Permet la mise à jour correcte de la table convoyeur
                            foreach (Article art2 in ListeSelectArt)
                            {
                                //Si j'ai un autre article au même emplacement convoyeur
                                if (art2.convoyeur.id == art.convoyeur.id && art2.id != art.id)
                                {
                                    //Je lui attribut le bon encombrement
                                    art2.convoyeur.encombrement = art.convoyeur.encombrement;
                                }
                            }

                            PlaceConvoyeurDAO.updatePlaceConvoyeur(art.convoyeur);

                            Article artAdd = new Article(art.id, art.photo, art.commentaire, true, art.TVA, art.TTC, art.type, null, comdRendu.id);
                            artAdd.date_rendu = DateTime.Now;
                            artAdd.date_payee = DateTime.Now;

                            ArticleDAO.updateArticle(artAdd);


                        }


                        //Enregistrement du/des paiement(s)
                        Payement paiement;
                        ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;
                        foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
                        {
                            paiement = new Payement(DateTime.Now, listeDeMontantParMoyenPaiement[monMoyenDePaiement], monMoyenDePaiement, comdRendu.id);
                            PayementDAO.insertPaiement(paiement);
                        }



                        //Mise à jour de la commande
                        comdRendu = CommandeDAO.selectCommandeById(comdRendu.id, true, true, true);

                        //Vérification du paiement
                        //1 - Je calcule le montant total de la commande
                        //2 - Je calcule le montant payé total
                        decimal prixTotalDeLaCommande = 0;
                        foreach (Article article in comdRendu.listArticles)
                        {
                            prixTotalDeLaCommande += (decimal)article.TTC;
                        }


                        decimal prixPayeTotal = 0;
                        if (comdRendu.listPayements.Count > 0)
                        {
                            foreach (Payement paiementEffectue in comdRendu.listPayements)
                            {
                                prixPayeTotal += (decimal)paiementEffectue.montant;
                            }
                        }


                        decimal resteAPayer = prixTotalDeLaCommande - prixPayeTotal - (decimal)Txb_paiement_montantRemise;
                        if (resteAPayer == 0)
                        {
                            //Mise à jour de la commande, le champ cmd_payee passe à 1
                            comdRendu.payee = true;
                            comdRendu.date_rendu = DateTime.Now;

                        }
                        else
                        {
                            MessageBox.Show("Un reste à payer de " + resteAPayer);
                        }
                        if (Txb_paiement_montantRemise != 0)
                        {
                            comdRendu.remise = Txb_paiement_montantRemise;
                        }
                        if (Txb_paiement_montantRemise != 0 || resteAPayer == 0)
                        {
                            CommandeDAO.updateCommande(comdRendu);
                        }



                        Commande cmdTota = CommandeDAO.selectCommandeById(comdRendu.id, true, true, true);

                        try
                        {
                            RecuPaiement rp = new RecuPaiement(cmdTota);
                            rp.printRecu();
                            rp.printRecu();

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impression refusée");
                        }
                        finally
                        {
                            //initialise tout
                            ClasseGlobale.SET_ALL_NULL();
                        }
                    }
                    //Si je ne  viens pas des pages précedentes
                    //Mes liste sont vides => paiement déjà effectué

                    else
                    {
                        MessageBox.Show("La commande à été correctement enregistrée, cliquez sur le bouton home pour retourner à l'accueil");
                    }
                    //Accueil page2Obj = new Accueil(); //Create object of Page2
                    //page2Obj.Show(); //Show page2
                    //this.Close();

                }
                else
                {
                    MessageBox.Show("Toute la commande n'a pas été payée. Veuillez s'il vous plait compléter l'intégralité du paiement.");
                }

            }
            else
            {
                MessageBox.Show("La commande à été correctement enregistrée, cliquez sur le bouton home pour retourner à l'accueil");
            }

        }

        private void applyModeDePaiement()
        {
            //On vérifie qu'un mode de paiement a bien été sélectionné
            if (Mode_de_paiement != "")
            {
                //On vérifie que le montant est positif
                if (Txb_paiement_montantParMoyenPaiement >= 0)
                {
                    //S'il n'a rien mis dans le text box, on ne fait rien et on le signale à l'utilisateur
                    if (Txb_paiement_montantParMoyenPaiement != 0)
                    {
                        //On vérifie que le montant désiré est inférieur ou égal au reste à payer
                        if (Txb_paiement_montantParMoyenPaiement <= Reste_a_payer)
                        {
                            //Vérifie s'il n'y a pas de problème de paiement cleanway
                            //Lorsqu'une commande est payée en cleanway, elle n'est payée qu'en cleanway
                            if (CleanWayOK())
                            {
                                //Ajout de mon montant et du mode de paiement dans le dico
                                listeDeMontantParMoyenPaiement[Mode_de_paiement] = Txb_paiement_montantParMoyenPaiement;

                                InitialiseLaListeDePaiement();

                                //Je met dans le text box le nouveau reste à payer. Le mélange decimal/float permet de ne pas avoir de problème de précision
                                //comme 15.56668879 ou autre
                                Txb_paiement_montantParMoyenPaiement = (float)((decimal)Reste_a_payer - (decimal)Txb_paiement_montantParMoyenPaiement);

                                //Redéfinition du reste à payer
                                Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
                                ClasseGlobale.reste_a_payer = Reste_a_payer;
                                //MessageBox.Show("ma remise vaut : " + Txb_paiement_montantRemise + "\nprix du paiement : " + Txb_paiement_montantParMoyenPaiement + "\n Reste_a_payer : " + Reste_a_payer);
                                Mode_de_paiement = "";
                            }
                        }
                        else
                        {
                            //Redéfinition du contenu du text box
                            Txb_paiement_montantParMoyenPaiement = Reste_a_payer;
                            MessageBox.Show("Problème lors de la validation de ce paiement.\nLa somme désirée est supérieure à la somme due.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Paiement de 0 € refusé. Veuillez renseigner la valeur désirée du paiement.");
                    }
                }
                else
                {
                    //Redéfinition du contenu du text box
                    Txb_paiement_montantParMoyenPaiement = Reste_a_payer;
                    MessageBox.Show("Vous ne pouvez pas saisir des montants négatifs");
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un mode de paiement");
            }



        }

        private bool CleanWayOK()
        {
            //Si je viens 
            //de l'écran rendu et que 
            //tout les articles sont sélectionnés alors je peux payer en clean way
            List<Article> ListeSelectArt = ClasseGlobale._rendreArticlesSelectionnes;

            if (ListeSelectArt != null)//Je viens de l'écran de rendu donc toute la commande doit être sélectionnée
            {
                if (ClasseGlobale._renduCommande != null)
                {
                    if (ClasseGlobale._renduCommande.listArticles.Count == ListeSelectArt.Count || Mode_de_paiement != "CleanWay")
                    {
                        //Si tous les articles sont sélectionnés je vérifie la possibilité cleanway
                        return checkUnicitePaiementCleanway();
                    }

                    //Sinon le paiemnt par cleanway n'est pas possible
                    MessageBox.Show("Possibilité de payer par cleanway annulée, il faut sélectionner tous les articles de la commande");
                    return false;
                }
                else
                {
                    MessageBox.Show("La commande à rendre n'est pas retrouvée.\n Problème logiciel grave, contactez l'administrateur");
                    return false;
                }

            }
            return checkUnicitePaiementCleanway();



        }

        private bool checkUnicitePaiementCleanway()
        {
            //Si je désire payer en CleanWay, je vérifie que ma liste de paiement ne contient pas de paiement autre que cleanway
            //Sinon, on vérifie qu'aucun paiement cleanway n'a été effectué
            if (Mode_de_paiement == "CleanWay")
            {
                //Si mon seul paiement est en cleanWay ou si j'en ai pas return true
                if (listeDeMontantParMoyenPaiement.dico.Count == 1 && listeDeMontantParMoyenPaiement.dico.Keys.Contains("CleanWay")
                    ||
                    listeDeMontantParMoyenPaiement.dico.Count == 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Vous ne pouvez pas ajouter de paiement par cleanway, supprimez d'abord les autres paiements");
                    return false;
                }
            }
            else
            {
                if (listeDeMontantParMoyenPaiement.dico.Keys.Contains("CleanWay"))
                {
                    MessageBox.Show("Vous ne pouvez pas ajouter de paiement autre que cleanway.");
                    return false;
                }
                else
                {
                    //Aucun paiement cleanway => roulez jeunesse
                    return true;
                }


            }
        }

        private void InitialiseLaListeDePaiement()
        {
            //On récupère tous les modes de paiements
            System.Collections.Generic.ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;

            //On crèe la liste que l'on référencera plus tard dans la classe globale
            ObservableCollection<PaiementListeVM> contenuListePaiementTampon = new ObservableCollection<PaiementListeVM>();

            //initialisation de la liste de paiement en globale
            ClasseGlobale.initializeContenuListePaiement();

            //Reconstruction à partir du dico mis à jour
            foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
            {
                contenuListePaiementTampon.Add(new PaiementListeVM()
                {
                    ModeDePaiement = monMoyenDePaiement,
                    Montant = listeDeMontantParMoyenPaiement[monMoyenDePaiement].ToString()
                });
            }

            //On donne une nouvelle référence à notre liste globale de client checkRemise
            ContenuListePaiement = contenuListePaiementTampon;
            ClasseGlobale.listeDeMontantParMoyenPaiement = listeDeMontantParMoyenPaiement;
        }

        private void applyRemise()
        {
            //enlève la remise précédente et applique la nouvelle remise
            checkRemise[1] = checkRemise[0];//met le montant de l'ancienne à la place de la nouvelle
            checkRemise[0] = Txb_paiement_montantRemise;
            Txb_paiement_montantParMoyenPaiement = Reste_a_payer - checkRemise[0] + checkRemise[1];
            Reste_a_payer = Txb_paiement_montantParMoyenPaiement;

            //Réinitialise l'ancienne valeur
            checkRemise[1] = 0;
            ClasseGlobale.remise = checkRemise[0];
        }

        #endregion

    }

    #region classes internes
    public class GetPaiement
    {
        /// <summary>
        /// 
        /// </summary>
        public String Label_nomModePaiement { get; set; }

        public float Label_MontantPayerParMode { get; set; }
    }

    class ListePaiement
    {

        private readonly IDictionary<string, float> maListeDePaiement = new Dictionary<string, float>();

        public IDictionary<string, float> dico
        {
            get
            {
                return maListeDePaiement;
            }
        }

        public float this[string key]
        {
            get
            {
                return maListeDePaiement[key];
            }

            // Je mets la nouvelle valeur pour ce mode de paiement, attention le calcul (- et + une certaine somme) se fait à l'extérieur
            set
            {
                //maListeDePaiement[key] = value;
                if (maListeDePaiement.ContainsKey(key))
                {
                    maListeDePaiement[key] += value;
                }
                else
                {
                    maListeDePaiement.Add(key, value);
                }

            }
        }


    }

    #endregion
}