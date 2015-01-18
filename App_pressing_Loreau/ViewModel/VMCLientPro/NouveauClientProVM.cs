using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;
using System.Windows.Input;
using System.Windows;

namespace App_pressing_Loreau.ViewModel
{
    class NouveauClientProVM:ObservableObject, IPageViewModel
    {
        #region Variables locales

        Client client = new Client();

        private String _txb_nouveauClientPro_nomSociete;

        private int _txb_nouveauClientPro_numeroAdresse;
        private String _txb_nouveauClientPro_rue_voie;
        private String _txb_nouveauClientPro_bp;
        private String _txb_nouveauClientPro_ville;
       
        private String _txb_nouveauClientPro_numeroFixe;
        private String _txb_nouveauClientPro_numeroPortable;
        private String _txb_nouveauClientPro_email;

        public static int index { get; private set; }
        #endregion



        public String Name
        {
            get { return " "; }
        }


        #region Properties / Commands
        public String Txb_nouveauClientPro_nomSociete
        {
            get { return _txb_nouveauClientPro_nomSociete; }
            set
            {
                if (value != _txb_nouveauClientPro_nomSociete)
                {
                    _txb_nouveauClientPro_nomSociete = value;
                    OnPropertyChanged("Txb_nouveauClientPro_nomSociete");
                }
            }
        }


        public int Txb_nouveauClientPro_numeroAdresse
        {
            get { return _txb_nouveauClientPro_numeroAdresse; }
            set
            {
                if (value != _txb_nouveauClientPro_numeroAdresse)
                {
                    _txb_nouveauClientPro_numeroAdresse = value;
                    OnPropertyChanged("Txb_nouveauClientPro_numeroAdresse");
                }
            }
        }

        public String Txb_nouveauClientPro_rue_voie
        {
            get { return _txb_nouveauClientPro_rue_voie; }
            set
            {
                if (value != _txb_nouveauClientPro_rue_voie)
                {
                    _txb_nouveauClientPro_rue_voie = value;
                    OnPropertyChanged("Txb_nouveauClientPro_rue_voie");
                }
            }
        }

        public String Txb_nouveauClientPro_bp
        {
            get { return _txb_nouveauClientPro_bp; }
            set
            {
                if (value != _txb_nouveauClientPro_bp)
                {
                    _txb_nouveauClientPro_bp = value;
                    OnPropertyChanged("Txb_nouveauClientPro_bp");
                }
            }
        }

        public String Txb_nouveauClientPro_ville
        {
            get { return _txb_nouveauClientPro_ville; }
            set
            {
                if (value != Txb_nouveauClientPro_ville)
                {
                    Txb_nouveauClientPro_ville = value;
                    OnPropertyChanged("Txb_nouveauClientPro_ville");

                }
            }
        }

        public String Txb_nouveauClientPro_numeroFixe
        {
            get { return _txb_nouveauClientPro_numeroFixe; }
            set
            {
                if (value != _txb_nouveauClientPro_numeroFixe)
                {
                    _txb_nouveauClientPro_numeroFixe = value;
                    OnPropertyChanged("Txb_nouveauClientPro_numeroFixe");
                }
            }
        }

        public String Txb_nouveauClientPro_numeroPortable
        {
            get { return _txb_nouveauClientPro_numeroPortable; }
            set
            {
                if (value != _txb_nouveauClientPro_numeroPortable)
                {
                    _txb_nouveauClientPro_numeroPortable = value;
                    OnPropertyChanged("Txb_nouveauClientPro_numeroPortable");
                }
            }
        }


        public String Txb_nouveauClientPro_email
        {
            get { return _txb_nouveauClientPro_email; }
            set
            {
                if (value != _txb_nouveauClientPro_email)
                {
                    _txb_nouveauClientPro_email = value;
                    OnPropertyChanged("Txb_nouveauClientPro_email");
                }
            }
        }


        public ICommand Btn_nouveauClientPro_enregistrer
        {
            get
            {
                return new RelayCommand(
                    p => enregisterClient(),
                    p => Txb_nouveauClientPro_nomSociete != null );
            }
        }
        #endregion


        #region Méthodes
        public void enregisterClient()
        {
            client.nom = this.Txb_nouveauClientPro_nomSociete;
            client.prenom = this.Txb_nouveauClientPro_nomSociete;
            client.type = 1;
            client.adresse = new Model.Adresse();
            client.adresse.numero = Txb_nouveauClientPro_numeroAdresse + "";
            client.adresse.rue = Txb_nouveauClientPro_rue_voie;
            client.adresse.codePostal = Txb_nouveauClientPro_bp;
            client.adresse.ville = Txb_nouveauClientPro_ville;

            client.telfix = Txb_nouveauClientPro_numeroFixe;
            client.telmob = Txb_nouveauClientPro_numeroPortable;
           
            client.email = Txb_nouveauClientPro_email;
            

            index = ClientDAO.insertClient(client);

            if (index != 0)
            {
                MessageBox.Show("alexis te dis : Bonjour");
            }

        }
        #endregion
    }
}
