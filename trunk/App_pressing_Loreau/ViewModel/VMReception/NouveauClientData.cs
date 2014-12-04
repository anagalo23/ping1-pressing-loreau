using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class NouveauClientData : ObservableObject
    {
        #region Variables

        private String _txt_nouveauClient_nom;
        private String _txt_nouveauClient_prenom;
        private String _txt_nouveauClient_date_naissance;
        private String _txt_nouveauClient_idCleanway;
        private String _txt_nouveauClient_numero;
        private String _txt_nouveauClient_rue_voie;
        private String _txt_nouveauClient_bp;
        private String _txt_nouveauClient_ville;
        private String _txt_nouveauClient_portable;
        private String _txt_nouveauClient_mail;
        #endregion


        #region Properties
        public String txb_nouveauClient_nom
        {
            get { return _txt_nouveauClient_nom; }
            set
            {
                if (value != _txt_nouveauClient_nom)
                {
                    _txt_nouveauClient_nom = value;
                    OnPropertyChanged("txb_nouveauClient_nom");
                }
            }
        }

        public String txb_nouveauClient_prenom
        {
            get { return _txt_nouveauClient_prenom; }
            set
            {
                if (value != _txt_nouveauClient_prenom)
                {
                    _txt_nouveauClient_prenom = value;
                    OnPropertyChanged("txb_nouveauClient_prenom");
                }
            }
           }

        public String txb_nouveauClient_date_naissance
        {
            get { return _txt_nouveauClient_date_naissance; }
            set
            {
                if (value != _txt_nouveauClient_date_naissance)
                {
                    _txt_nouveauClient_date_naissance = value;
                    OnPropertyChanged("txb_nouveauClient_date_naissance");
                }
            }
        }

        public String txb_nouveauClient_idCleanway
        {
            get { return _txt_nouveauClient_idCleanway; }
            set
            {
                if (value != _txt_nouveauClient_idCleanway)
                {
                    _txt_nouveauClient_idCleanway = value;
                    OnPropertyChanged("txb_nouveauClient_idCleanway");
                }
            }
        }
        #endregion
    }
}
