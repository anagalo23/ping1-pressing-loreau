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
    class UnClientPROVM : ObservableObject, IPageViewModel
    {
        #region Attributs 

        private String _nomSociete_clientPro;
        private String _nombreCommande_clientPro;

        #endregion

        public String NomSociete_clientPro
        {
            get { return _nomSociete_clientPro; }
            set
            {
                if (value != _nomSociete_clientPro)
                {
                    _nomSociete_clientPro = value;
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
            
        public String Name
        {
            get { return ""; }
        }
    }
}
