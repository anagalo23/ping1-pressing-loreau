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

        public String txb_nouveauClient_numero
        {
            get { return _txt_nouveauClient_numero; }
            set
            {
                if (value != _txt_nouveauClient_numero)
                {
                    _txt_nouveauClient_numero = value;
                    OnPropertyChanged("txb_nouveauClient_numero");
                }
            }
        }

        public String txb_nouveauClient_rue_voie
        {
            get { return _txt_nouveauClient_rue_voie; }
            set
            {
                if (value != _txt_nouveauClient_rue_voie)
                {
                    _txt_nouveauClient_rue_voie = value;
                    OnPropertyChanged("txb_nouveauClient_rue_voie");
                }
            }
        }

        public String txb_nouveauClient_bp
        {
            get { return _txt_nouveauClient_bp; }
            set
            {
                if (value != _txt_nouveauClient_bp)
                {
                    _txt_nouveauClient_bp = value;
                    OnPropertyChanged("txb_nouveauClient_bp");
                }
            }
        }

        public String txb_nouveauClient_ville
        {
            get { return _txt_nouveauClient_ville; }
            set
            {
                if (value != _txt_nouveauClient_ville)
                {
                    _txt_nouveauClient_ville = value;
                    OnPropertyChanged("txb_nouveauClient_ville");
                }
            }
        }
        public String txb_nouveauClient_portable
        {
            get { return _txt_nouveauClient_portable; }
            set
            {
                if (value != _txt_nouveauClient_portable)
                {
                    _txt_nouveauClient_portable = value;
                    OnPropertyChanged("txb_nouveauClient_portable");
                }
            }
        }

        public String txb_nouveauClient_mail
        {
            get { return _txt_nouveauClient_mail; }
            set
            {
                if (value != _txt_nouveauClient_mail)
                {
                    _txt_nouveauClient_mail = value;
                    OnPropertyChanged("txb_nouveauClient_mail");
                }
            }
        }
        #endregion
    }
}
