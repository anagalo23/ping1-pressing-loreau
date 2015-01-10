using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;


namespace App_pressing_Loreau.ViewModel
{
    class NouveauClientVM : ObservableObject, IPageViewModel
    {

        #region Variables locales

        Client client;
        #endregion



        public String Name
        {
            get { return " "; }
        }


        #region Properties / Commands
        public String Txb_nouveauClient_nom
        {
            get { return client.nom; }
            set
            {
                if (value != client.nom)
                {
                    client.nom = value;
                    OnPropertyChanged("Txb_nouveauClient_nom");
                }
            }
        }

        public String Txb_nouveauClient_prenom
        {
            get { return client.prenom; }
            set
            {
                if (value != client.prenom)
                {
                    client.prenom = value;
                    OnPropertyChanged("Txb_nouveauClient_prenom");
                }
            }
        }

        public String Txb_nouveauClient_date_naissance
        {
            get { return client.dateNaissance.ToString(); }
            set
            {
                if (value != client.dateNaissance.ToString())
                {
                    client.dateNaissance = DateTime.Parse(value);
                    OnPropertyChanged("Txb_nouveauClient_date_naissance");
                }
            }

        }


        public String Txb_nouveauClient_idCleanway
        {
            get { return client.idCleanWay.ToString(); }
            set
            {
                if (value != client.idCleanWay.ToString())
                {
                    client.idCleanWay = Int16.Parse(value);
                    OnPropertyChanged("Txb_nouveauClient_idCleanway");
                }
            }
        }

        public String Txb_nouveauClient_numero
        {
            get { return client.adresse.numero; }
            set
            {
                if (value != client.adresse.numero)
                {
                    client.adresse.numero = value;
                    OnPropertyChanged("Txb_nouveauClient_numero");
                }
            }
        }

        public String Txb_nouveauClient_rue_voie
        {
            get { return client.adresse.rue; }
            set
            {
                if (value != client.adresse.rue)
                {
                    client.adresse.rue = value;
                    OnPropertyChanged("Txb_nouveauClient_rue_voie");
                }
            }
        }

        public String Txb_nouveauClient_bp
        {
            get { return client.adresse.codePostal; }
            set
            {
                if (value != client.adresse.codePostal)
                {
                    client.adresse.codePostal = value;
                    OnPropertyChanged("Txb_nouveauClient_bp");
                }
            }
        }

        public String Txb_nouveauClient_ville
        {
            get { return client.adresse.ville; }
            set
            {
                if (value != client.adresse.ville)
                {
                    client.adresse.ville = value;
                    OnPropertyChanged("Txb_nouveauClient_ville");

                }
            }
        }




        public bool Ckb_nouveauClient_sms
        {
            get { return client.contactSms; }
            set
            {
                if (value != client.contactSms)
                {
                    client.contactSms = value;
                    OnPropertyChanged("Ckb_nouveauClient_sms");
                }
            }
        }
        public String Txb_nouveauClient_portable
        {
            get { return client.telmob; }
            set
            {
                if (value != client.telmob)
                {
                    client.telmob = value;
                    OnPropertyChanged("Txb_nouveauClient_portable");
                }
            }
        }


        public bool Ckb_nouveauClient_mail
        {
            get { return client.contactMail; }
            set
            {
                if (value != client.contactMail)
                {
                    client.contactMail = value;
                    OnPropertyChanged("Ckb_nouveauClient_mail");
                }
            }
        }

        public String Txb_nouveauClient_mail
        {
            get { return client.email; }
            set
            {
                if (value != client.email)
                {
                    client.email = value;
                    OnPropertyChanged("Txb_nouveauClient_mail");
                }
            }
        }


        public ICommand Btn_nouveauClient_enregistrer
        {
            get
            {
                return new RelayCommand(
                    p => enregisterClient(),
                    p => Txb_nouveauClient_nom != null & Txb_nouveauClient_prenom != null);
            }
        }
        #endregion


        #region Méthodes
        public void enregisterClient()
        {
            ClientDAO.insertClient(client);
        }
        #endregion

    }
}
