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
        #endregion

        #region Constructeurs
        public UnClientPROVM()
        {

        }
        #endregion

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
            
       
    }
}
