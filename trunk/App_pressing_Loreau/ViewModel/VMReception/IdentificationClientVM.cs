using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientVM : ObservableObject, IPageViewModel
    {

        #region Variables

        private String _txt_identificationClient_nom;
        private IdentificationClientData _resultatRecherche;

        private ICommand _btnIdentifiantNouveauClient;
        #endregion

        public string Name
        {
            get { return ""; }
        }

        public IdentificationClientVM(){
          
        }

         #region Properties 

        public String txb_identificationClient_nom
        {
            get { return _txt_identificationClient_nom; }
            set
            {
                if (value != _txt_identificationClient_nom)
                {
                    _txt_identificationClient_nom = value;
                    OnPropertyChanged("txb_identificationClient_nom");
                }
            }
        }


        public IdentificationClientData resultatRecherche
        {
            get { return _resultatRecherche; }
            set
            {
                if (value != _resultatRecherche)
                {
                    _resultatRecherche = value;
                    OnPropertyChanged("resultatRecherche");
                }
            }
        }

        public ICommand btnIdentifiantNouveauClient
        {
            get
            {
                if (_btnIdentifiantNouveauClient != null)
                {
                    _btnIdentifiantNouveauClient = new RelayCommand(
                        p=>resultatName());

                }
                return _btnIdentifiantNouveauClient;
            }

        }
         #endregion


        #region methodes
        private void resultatName()
        {
            IdentificationClientData icd = new IdentificationClientData();
            icd.txb_identificationClient_nom= txb_identificationClient_nom;
            resultatRecherche = icd;
        }
        #endregion



        //Suite 
    }
}
