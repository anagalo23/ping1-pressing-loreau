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
    class NouveauClientProVM : ObservableObject
    {
        #region Variables locales

        Client client = new Client();

        //private String _txb_nouveauClientPro_nomSociete;

        //private int _txb_nouveauClientPro_numeroAdresse;
        //private String _txb_nouveauClientPro_rue_voie;
        //private String _txb_nouveauClientPro_bp;
        //private String _txb_nouveauClientPro_ville;

        //private String _txb_nouveauClientPro_numeroFixe;
        //private String _txb_nouveauClientPro_numeroPortable;
        //private String _txb_nouveauClientPro_email;

        public static int index { get; private set; }
        #endregion




        #region Properties / Commands
        public String Txb_nouveauClientPro_nomSociete
        {
            get { return client.nom; }
            set
            {
                if (value != client.nom)
                {
                    client.nom = value;
                    OnPropertyChanged("Txb_nouveauClientPro_nomSociete");
                }
            }
        }


        public int Txb_nouveauClientPro_numeroAdresse
        {
            get
            {
                try
                {
                    return Int32.Parse(client.adresse.numero);
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            set
            {
                //if (client.adresse.numero != "")
                //{
                //    if (value != Int32.Parse(client.adresse.numero))
                //    {
                        client.adresse.numero = String.Format("{0}", value);
                        OnPropertyChanged("Txb_nouveauClientPro_numeroAdresse");
                //    }
                //}
                //else
                //{

                //}

            }
        }

        public String Txb_nouveauClientPro_rue_voie
        {
            get { return client.adresse.rue; }
            set
            {
                if (value != client.adresse.rue)
                {
                    client.adresse.rue = value;
                    OnPropertyChanged("Txb_nouveauClientPro_rue_voie");
                }
            }
        }

        public String Txb_nouveauClientPro_bp
        {
            get { return client.adresse.codePostal; }
            set
            {
                if (value != client.adresse.codePostal)
                {
                    client.adresse.codePostal = value;
                    OnPropertyChanged("Txb_nouveauClientPro_bp");
                }
            }
        }

        public String Txb_nouveauClientPro_ville
        {
            get { return client.adresse.ville; }
            set
            {
                if (value != client.adresse.ville)
                {
                    client.adresse.ville = value;
                    OnPropertyChanged("Txb_nouveauClientPro_ville");

                }
            }
        }

        public String Txb_nouveauClientPro_numeroFixe
        {
            get { return client.telfix; }
            set
            {
                if (value != client.telfix)
                {
                    client.telfix = value;
                    OnPropertyChanged("Txb_nouveauClientPro_numeroFixe");
                }
            }
        }

        public String Txb_nouveauClientPro_numeroPortable
        {
            get { return client.telmob; }
            set
            {
                if (value != client.telmob)
                {
                    client.telmob = value;
                    OnPropertyChanged("Txb_nouveauClientPro_numeroPortable");
                }
            }
        }


        public String Txb_nouveauClientPro_email
        {
            get { return client.email; }
            set
            {
                if (value != client.email)
                {
                    client.email = value;
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
                    p => Txb_nouveauClientPro_nomSociete != null);
            }
        }
        #endregion


        #region Méthodes
        public void enregisterClient()
        {
            //client.nom = Txb_nouveauClientPro_nomSociete;
            client.prenom = Txb_nouveauClientPro_nomSociete;
            client.type = 1;
            //client.dateNaissance = null;
            //client.adresse = new Model.Adresse();
            //client.adresse.numero = Txb_nouveauClientPro_numeroAdresse + "";
            //client.adresse.rue = Txb_nouveauClientPro_rue_voie;
            //client.adresse.codePostal = Txb_nouveauClientPro_bp;
            //client.adresse.ville = Txb_nouveauClientPro_ville;

            // client.telfix = Txb_nouveauClientPro_numeroFixe;
            //client.telmob = Txb_nouveauClientPro_numeroPortable;
            //client.email = Txb_nouveauClientPro_email;

            index = 0;
            index = ClientDAO.insertClient(client);

            if (index != 0)
            {
                MessageBox.Show("Client pro enregistré");
            }
            else
            {
                MessageBox.Show("Client pro non enregistré");

            }

        }
        #endregion
    }
}
