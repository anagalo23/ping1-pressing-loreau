using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class IdentificationClientData : ObservableObject
    {
        #region Variables

        private String _txt_identificationClient_nom;
        private String _txt_identificationClient_prenom;
        private String _txt_identificationClient_portable;
        private String _txt_identificationClient_adresse;
        private String _txt_identificationClient_date_naissance;
        
        #endregion


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

        public String txb_identificationClient_prenom
        {
            get { return _txt_identificationClient_prenom; }
            set
            {
                if (value != _txt_identificationClient_prenom)
                {
                    _txt_identificationClient_prenom = value;
                    OnPropertyChanged("txb_identificationClient_prenom");
                }
            }
        }

        public String txb_identificationClient_portable
        {
            get { return _txt_identificationClient_portable; }
            set
            {
                if (value != _txt_identificationClient_portable)
                {
                    _txt_identificationClient_portable = value;
                    OnPropertyChanged("txb_identificationClient_portable");
                }
            }
        }

        public String txb_identificationClient_adresse
        {
            get { return _txt_identificationClient_adresse; }
            set
            {
                if (value != _txt_identificationClient_adresse)
                {
                    _txt_identificationClient_adresse = value;
                    OnPropertyChanged("txb_identificationClient_adresse");
                }
            }
        }

        public String txb_identificationClient_date_naissance
        {
            get { return _txt_identificationClient_date_naissance; }
            set
            {
                if (value != _txt_identificationClient_date_naissance)
                {
                    _txt_identificationClient_date_naissance = value;
                    OnPropertyChanged("txb_identificationClient_date_naissance");
                }
            }
        }
        #endregion

     
    }
}
