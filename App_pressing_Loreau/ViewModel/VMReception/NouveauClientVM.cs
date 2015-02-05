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
using App_pressing_Loreau.Data;


namespace App_pressing_Loreau.ViewModel
{
    class NouveauClientVM : ObservableObject, IPageViewModel
    {

        #region Variables locales

        public static int index { get; private set; }
        #endregion

        #region Constructeur
        public NouveauClientVM()
        {
            ClasseGlobale.initializeClient();
            //ClasseGlobale.client = ClasseGlobale.client;
            ClasseGlobale.Client.type = 0;//Client particulier

        }
        #endregion

        public String Name
        {
            get { return " "; }
        }


        #region Properties / Commands

        public String Txb_nouveauClient_nom
        {
            get { return ClasseGlobale.Client.nom; }
            set
            {
                if (value != ClasseGlobale.Client.nom)
                {
                    ClasseGlobale.Client.nom = value;
                    OnPropertyChanged("Txb_nouveauClient_nom");
                }
            }
        }

        public String Txb_nouveauClient_prenom
        {
            get { return ClasseGlobale.Client.prenom; }
            set
            {
                if (value != ClasseGlobale.Client.prenom)
                {
                    ClasseGlobale.Client.prenom = value;
                    OnPropertyChanged("Txb_nouveauClient_prenom");
                }
            }
        }

        public String Txb_nouveauClient_date_naissance
        {
            get { return ClasseGlobale.Client.dateNaissance; }
            set
            {
                if (value != ClasseGlobale.Client.dateNaissance)
                {
                    ClasseGlobale.Client.dateNaissance = value;
                    OnPropertyChanged("Txb_nouveauClient_date_naissance");
                }
            }

        }


        public int Txb_nouveauClient_idCleanway
        {
            get { return ClasseGlobale.Client.idCleanWay; }
            set
            {
                if (value != ClasseGlobale.Client.idCleanWay)
                {
                    ClasseGlobale.Client.idCleanWay = value;
                    OnPropertyChanged("Txb_nouveauClient_idCleanway");
                }
            }
        }

        public String Txb_nouveauClient_numero
        {
            get
            {
                try
                {
                    return ClasseGlobale.Client.adresse.numero;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            set
            {
                if (value != ClasseGlobale.Client.adresse.numero)
                {
                    ClasseGlobale.Client.adresse.numero=value;
                    OnPropertyChanged("Txb_nouveauClient_numero");
                }
            }
        }

        public String Txb_nouveauClient_rue_voie
        {
            get { return ClasseGlobale.Client.adresse.rue; }
            set
            {
                if (value != ClasseGlobale.Client.adresse.rue)
                {
                    ClasseGlobale.Client.adresse.rue = value;
                    OnPropertyChanged("Txb_nouveauClient_rue_voie");
                }
            }
        }

        public String Txb_nouveauClient_bp
        {
            get { return ClasseGlobale.Client.adresse.codePostal; }
            set
            {
                if (value != ClasseGlobale.Client.adresse.codePostal)
                {
                    ClasseGlobale.Client.adresse.codePostal = value;
                    OnPropertyChanged("Txb_nouveauClient_bp");
                }
            }
        }

        public String Txb_nouveauClient_ville
        {
            get { return ClasseGlobale.Client.adresse.ville; }
            set
            {
                if (value != ClasseGlobale.Client.adresse.ville)
                {
                    ClasseGlobale.Client.adresse.ville = value;
                    OnPropertyChanged("Txb_nouveauClient_ville");

                }
            }
        }

        public bool Ckb_nouveauClient_sms
        {
            get { return ClasseGlobale.Client.contactSms; }
            set
            {
                if (value != ClasseGlobale.Client.contactSms)
                {
                    ClasseGlobale.Client.contactSms = value;
                    OnPropertyChanged("Ckb_nouveauClient_sms");
                }
            }
        }
        public String Txb_nouveauClient_portable
        {
            get { return ClasseGlobale.Client.telmob; }
            set
            {
                if (value != ClasseGlobale.Client.telmob)
                {
                    ClasseGlobale.Client.telmob = value;
                    OnPropertyChanged("Txb_nouveauClient_portable");
                }
            }
        }


        public bool Ckb_nouveauClient_mail
        {
            get { return ClasseGlobale.Client.contactMail; }
            set
            {
                if (value != ClasseGlobale.Client.contactMail)
                {
                    ClasseGlobale.Client.contactMail = value;
                    OnPropertyChanged("Ckb_nouveauClient_mail");
                }
            }
        }

        public String Txb_nouveauClient_mail
        {
            get { return ClasseGlobale.Client.email; }
            set
            {
                if (value != ClasseGlobale.Client.email)
                {
                    ClasseGlobale.Client.email = value;
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
                    p => Txb_nouveauClient_nom != null & Txb_nouveauClient_prenom != null);
            }
        }



        #endregion


        #region Méthodes

        public void enregisterClient()
        {
            index = 0;
            if (ClasseGlobale.Client != null)
            {
                index = ClientDAO.insertClient(ClasseGlobale.Client);
            }

            if (index != 0)
            {
                MessageBox.Show("Nouveau client enregistré avec succès");
                Client client = ClientDAO.lastClient();
                //if ()
                if (client == null)
                {
                    MessageBox.Show("Problème de récupération du dernier ClasseGlobale.client");
                }
                else
                {
                    ClasseGlobale.Client = client;
                }
                
            }
            else
            {
                MessageBox.Show("Problème d'enregistrement du ClasseGlobale.client dans la base de données");
            }
            Bdd.deconnexion();
        }

        #endregion

    }
}
