using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;


namespace App_pressing_Loreau.ViewModel
{
    class NouveauClientVM : ObservableObject, IPageViewModel
    {

        #region Variables locales

        Client client = new Client();
        private String _txb_nouveauClient_nom;
        private String _txb_nouveauClient_prenom;
        private DateTime _txb_nouveauClient_date_naissance;
        private int _txb_nouveauClient_idCleanway;
        private int _txb_nouveauClient_numero;
        private String _txb_nouveauClient_rue_voie;
        private String _txb_nouveauClient_bp;
        private String _txb_nouveauClient_ville;
        private bool _ckb_nouveauClient_sms;
        private String _txb_nouveauClient_portable;
        private bool _ckb_nouveauClient_mail;
        private String _txt_nouveauClient_mail;

        public static int index { get; private set; }
        #endregion



        public String Name
        {
            get { return " "; }
        }


        #region Properties / Commands
        public String Txb_nouveauClient_nom
        {
            get { return _txb_nouveauClient_nom; }
            set
            {
                if (value != _txb_nouveauClient_nom)
                {
                    _txb_nouveauClient_nom = value;
                    OnPropertyChanged("Txb_nouveauClient_nom");
                }
            }
        }

        public String Txb_nouveauClient_prenom
        {
            get { return _txb_nouveauClient_prenom; }
            set
            {
                if (value != _txb_nouveauClient_prenom)
                {
                    _txb_nouveauClient_prenom = value;
                    OnPropertyChanged("Txb_nouveauClient_prenom");
                }
            }
        }

        public DateTime Txb_nouveauClient_date_naissance
        {
            get { return _txb_nouveauClient_date_naissance; }
            set
            {
                if (value != _txb_nouveauClient_date_naissance)
                {
                    _txb_nouveauClient_date_naissance = value;
                    OnPropertyChanged("Txb_nouveauClient_date_naissance");
                }
            }

        }


        public int Txb_nouveauClient_idCleanway
        {
            get { return _txb_nouveauClient_idCleanway; }
            set
            {
                if (value != _txb_nouveauClient_idCleanway)
                {
                    _txb_nouveauClient_idCleanway = value;
                    OnPropertyChanged("Txb_nouveauClient_idCleanway");
                }
            }
        }

        public int Txb_nouveauClient_numero
        {
            get { return _txb_nouveauClient_numero; }
            set
            {
                if (value != _txb_nouveauClient_numero)
                {
                    _txb_nouveauClient_numero = value;
                    OnPropertyChanged("Txb_nouveauClient_numero");
                }
            }
        }

        public String Txb_nouveauClient_rue_voie
        {
            get { return _txb_nouveauClient_rue_voie; }
            set
            {
                if (value != _txb_nouveauClient_rue_voie)
                {
                    _txb_nouveauClient_rue_voie = value;
                    OnPropertyChanged("Txb_nouveauClient_rue_voie");
                }
            }
        }

        public String Txb_nouveauClient_bp
        {
            get { return _txb_nouveauClient_bp; }
            set
            {
                if (value != _txb_nouveauClient_bp)
                {
                    _txb_nouveauClient_bp = value;
                    OnPropertyChanged("Txb_nouveauClient_bp");
                }
            }
        }

        public String Txb_nouveauClient_ville
        {
            get { return _txb_nouveauClient_ville; }
            set
            {
                if (value != _txb_nouveauClient_ville)
                {
                    _txb_nouveauClient_ville = value;
                    OnPropertyChanged("Txb_nouveauClient_ville");

                }
            }
        }

        public bool Ckb_nouveauClient_sms
        {
            get { return _ckb_nouveauClient_sms; }
            set
            {
                if (value != _ckb_nouveauClient_sms)
                {
                    _ckb_nouveauClient_sms = value;
                    OnPropertyChanged("Ckb_nouveauClient_sms");
                }
            }
        }
        public String Txb_nouveauClient_portable
        {
            get { return _txb_nouveauClient_portable; }
            set
            {
                if (value != _txb_nouveauClient_portable)
                {
                    _txb_nouveauClient_portable = value;
                    OnPropertyChanged("Txb_nouveauClient_portable");
                }
            }
        }


        public bool Ckb_nouveauClient_mail
        {
            get { return _ckb_nouveauClient_mail; }
            set
            {
                if (value != _ckb_nouveauClient_mail)
                {
                    _ckb_nouveauClient_mail = value;
                    OnPropertyChanged("Ckb_nouveauClient_mail");
                }
            }
        }

        public String Txb_nouveauClient_mail
        {
            get { return _txt_nouveauClient_mail; }
            set
            {
                if (value != _txt_nouveauClient_mail)
                {
                    _txt_nouveauClient_mail = value;
                    OnPropertyChanged("Txb_nouveauClient_mail");
                }
            }
        }


        public ICommand BtnNouveauClientEnregistrer
        {
            get
            {
                return new RelayCommand(
                    p => enregisterClient(),
                    p=> Txb_nouveauClient_nom!=null & Txb_nouveauClient_prenom!=null);
            }
        }
        #endregion


        #region Méthodes
        public void enregisterClient()
        {
            client.nom = this._txb_nouveauClient_nom;
            client.prenom = Txb_nouveauClient_prenom;
            client.dateNaissance = Txb_nouveauClient_date_naissance;
            client.idCleanWay = Txb_nouveauClient_idCleanway;
            client.type = 0;
            client.adresse = new Model.Adresse();
            client.adresse.numero = Txb_nouveauClient_numero + "";
            client.adresse.rue = Txb_nouveauClient_rue_voie;
            client.adresse.codePostal = Txb_nouveauClient_bp;
            client.adresse.ville = Txb_nouveauClient_ville;

            client.contactSms = Ckb_nouveauClient_sms;
            client.telmob = Txb_nouveauClient_portable;
            client.contactMail = Ckb_nouveauClient_mail;
            client.email = Txb_nouveauClient_mail;
            //index = 1;

            index= ClientDAO.insertClient(client);

            if (index == 0)
            {
                MessageBox.Show("alexis te dis : Bonjour");
            }

        }
        #endregion

    }
}
