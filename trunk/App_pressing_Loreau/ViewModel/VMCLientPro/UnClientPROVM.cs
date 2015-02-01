using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class UnClientPROVM : ObservableObject
    {
        #region Attributs 

        //private String _nomSociete_clientPro;
        private String _nombreCommande_clientPro;

        public Client clt;
        public Commande commande;
        #endregion

        #region Constructeurs
        public UnClientPROVM()
        {

        }
        #endregion

        #region Properties and commands

        #region affichages client pro
        public String NomSociete_clientPro
        {
            get { return this.clt.nom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.clt.nom = value;
                    OnPropertyChanged("NomSociete_clientPro");
                }
            }
        }

        public String NombreCommande_clientPro
        {
            get { return _nombreCommande_clientPro; }
            set
            {
                if (value != _nombreCommande_clientPro)
                {
                    _nombreCommande_clientPro = value;
                    OnPropertyChanged("NombreCommande_clientPro");

                }

            }
        }
        #endregion

        #region Details commande client pro

        public String Label_datail_nomSociete
        {
            get { return commande.client.nom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    commande.client.nom = value;
                    OnPropertyChanged("Label_datail_nomSociete");
                }
            }
        }


        public String Label_Detail_dateReception
        {
            get { return commande.date.ToString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    String date = commande.date.ToString();
                    date = value;
                    OnPropertyChanged("Label_Detail_dateReception");
                }
            }
        }
        #endregion
        #endregion

    }
}
