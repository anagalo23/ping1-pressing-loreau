using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// Classe Administration commande VM
    /// </summary>
    class AdministrationCommandesVM : ObservableObject
    {
        #region Attributes
        private List<ItemCommand> _listeCommandeEnCours;
        private Brush _colorCommande;
        #endregion

        #region Constructor
        public AdministrationCommandesVM()
        {
            AfficherCommandeEnCours();
        }
        #endregion

        #region Properties and commands

        public List<ItemCommand> ListeCommandeEnCours
        {
            get { return _listeCommandeEnCours; }
            set
            {
                if (value != _listeCommandeEnCours)
                {
                    _listeCommandeEnCours = value;
                    OnPropertyChanged("ListeCommandeEnCours");
                }
            }
        }

        public Brush ColorCommande
        {
            get { return _colorCommande; }
            set
            {
                _colorCommande = value;
                OnPropertyChanged("ColorCommande");
            }
        }
        #endregion

        #region Methods

        private void AfficherCommandeEnCours()
        {
            ListeCommandeEnCours = new List<ItemCommand>();
            List<Commande> listeCommande = CommandeDAO.selectCommandes(true, true, true);
            //bool commandeContientEncoreDesArticlesARendre = false;

            foreach (Commande com in listeCommande)
            {
               
                    String etat = null;
                    if (com.payee == true)
                    {
                        etat = "payée";
                    }
                    else etat = "non payée";


                    int duree = NombreDeMois(com.date, DateTime.Now);



                    //Comptabiliser le prix a payer
                    float prixP = 0;
                    float prixA = 0;

                    foreach (Payement pay in com.listPayements)
                    {
                        prixP = (float)((decimal)prixP + (decimal)pay.montant);
                    }

                    foreach (Article art in com.listArticles)
                    {
                        prixA = (float)((decimal)prixA + (decimal)art.TTC);

                    }

                    foreach (Article art in com.listArticles)
                    {
                        //Dès lors qu'un article est à rendre => j'ajoute à la liste
                        if (art.ifRendu == false)
                        {
                            //commandeContientEncoreDesArticlesARendre = true;

                            ListeCommandeEnCours.Add(new ItemCommand()
                            {
                                Label_AdminCom_Client = com.client.prenom,
                                Label_AdminCom_ref = "Ref:   " + com.id,
                                Label_AdminCom_DateEnregistrement = "Date recu:  " + com.date,
                                Label_AdminCom_EtatPaiement = "Etat paiement: " + etat,
                                Label_AdminCom_PrixRestant = "Prix à payer: " + (float)((decimal)prixA - (decimal)prixP),
                                Label_AdminCom_Duree = duree + " Mois",
                                colorDuree = duree >= 3 ? Brushes.Red : Brushes.Transparent
                            });

                            break;
                        }

                }
            }
        }

        private int NombreDeMois(DateTime DateDebut, DateTime DateFin)
        {
            int mois = 0;// init à 0 car on va compter
            if (DateDebut.Year != DateFin.Year)
            {
                // traitement première année
                for (int compteur = DateDebut.Month; compteur <= 12; ++compteur)
                    mois++;
                // traitement des années pleines
                mois += (DateFin.Year - (DateDebut.Year + 1)) * 12;
                // traitement dernière année
                for (int compteur = 1; compteur <= DateFin.Month; ++compteur)
                    mois++;
            }
            else
                for (int compteur = DateDebut.Month; compteur < DateFin.Month; ++compteur)
                    mois++;
            return mois;
        }

        #endregion

        #region Class

        public class ItemCommand
        {
            public String Label_AdminCom_ref { get; set; }
            public String Label_AdminCom_EtatPaiement { get; set; }
            public String Label_AdminCom_DateEnregistrement { get; set; }
            public String Label_AdminCom_PrixRestant { get; set; }
            public String Label_AdminCom_Client { get; set; }
            public String Label_AdminCom_Duree { get; set; }
            public Brush colorDuree { get; set; }

        }
        #endregion

    }
}
