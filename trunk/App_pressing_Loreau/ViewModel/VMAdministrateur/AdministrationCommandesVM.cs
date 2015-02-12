using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationCommandesVM : ObservableObject
    {
        #region Attributes
        private List<ItemCommand> _listeCommandeEnCours;
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
        #endregion

        #region Methods

        private void AfficherCommandeEnCours()
        {
            ListeCommandeEnCours = new List<ItemCommand>();
            List<Commande> listeCommande = CommandeDAO.selectCommandes(true, true, true);
            //bool commandeContientEncoreDesArticlesARendre = false;

            foreach (Commande com in listeCommande)
            {
                if (com.date_rendu != null)
                {
                    String etat = null;
                    if (com.payee == true)
                    {
                        etat = "payée";
                    }
                    else etat = "non payée";

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
                                Label_AdminCom_PrixRestant = "Prix à payer: " + (float)((decimal)prixA - (decimal)prixP)
                            });


                            break;
                        }
                    }
                    
                }
            }
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


        }
        #endregion

    }
}
