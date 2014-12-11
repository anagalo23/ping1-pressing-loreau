using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;


namespace App_pressing_Loreau.View
{
    class NouveauClientVM : ObservableObject, IPageViewModel
    {

        private ICommand _clientAjoute;


        //private int _clientName;


        public String Name 
        {
            get { return "Nouveau Client"; }
        }


        #region Properties/Commands

        //public int ClientName
        //{
        //    get { return _clientName; }
        //    set
        //    {
        //        if (value != _clientName)
        //        {
        //            _clientName = value;
        //            OnPropertyChanged("ProductId");
        //        }
        //    }
        //}


        public ICommand ajoutNouveauClient
        {
            get
            {
                if (_clientAjoute == null)
                {
                    _clientAjoute = new RelayCommand(
                        param => ajoutClient()
                    );
                }
                return _clientAjoute;
            }
        }

        #endregion


        #region Methods

        private void ajoutClient()
        {
            //Implémentation du code pour l'enregistrement en base de données
            NouveauClientData clientData
        }


        #endregion


    }
}
