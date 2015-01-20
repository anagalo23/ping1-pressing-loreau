﻿using System;
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

        //Client client = new Client();
        Client client;
        //Client client = ClasseGlobale.client;

        //private String _txb_nouveauClient_nom;
        //private String _txb_nouveauClient_prenom;
        //private DateTime _txb_nouveauClient_date_naissance;
        //private int _txb_nouveauClient_idCleanway;
        //private int _txb_nouveauClient_numero;
        //private String _txb_nouveauClient_rue_voie;
        //private String _txb_nouveauClient_bp;
        //private String _txb_nouveauClient_ville;
        //private bool _ckb_nouveauClient_sms;
        //private String _txb_nouveauClient_portable;
        //private bool _ckb_nouveauClient_mail;
        //private String _txt_nouveauClient_mail;

        public static int index { get; private set; }
        #endregion

        #region Constructeur
        public NouveauClientVM()
        {
            ClasseGlobale.initializeClient();
            client = ClasseGlobale.client;
            client.type = 0;//Client particulier

        }
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

        public DateTime Txb_nouveauClient_date_naissance
        {
            get { return client.dateNaissance; }
            set
            {
                if (value != client.dateNaissance)
                {
                    client.dateNaissance = value;
                    OnPropertyChanged("Txb_nouveauClient_date_naissance");
                }
            }

        }


        public int Txb_nouveauClient_idCleanway
        {
            get { return client.idCleanWay; }
            set
            {
                if (value != client.idCleanWay)
                {
                    client.idCleanWay = value;
                    OnPropertyChanged("Txb_nouveauClient_idCleanway");
                }
            }
        }

        public int Txb_nouveauClient_numero
        {
            get { return Int32.Parse(client.adresse.numero); }
            set
            {
                if (value != Int32.Parse(client.adresse.numero))
                {
                    client.adresse.numero = String.Format("{0}", value);
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
            //ClasseGlobale.client = new Client();
            //client.nom = this._txb_nouveauClient_nom;
            //client.prenom = Txb_nouveauClient_prenom;
            //client.dateNaissance = Txb_nouveauClient_date_naissance;
            //client.idCleanWay = Txb_nouveauClient_idCleanway;
            //client.type = 0;
            //client.adresse = new Model.Adresse();
            //client.adresse.numero = Txb_nouveauClient_numero + "";
            //client.adresse.rue = Txb_nouveauClient_rue_voie;
            //client.adresse.codePostal = Txb_nouveauClient_bp;
            //client.adresse.ville = Txb_nouveauClient_ville;

            //client.contactSms = Ckb_nouveauClient_sms;
            //client.telmob = Txb_nouveauClient_portable;
            //client.contactMail = Ckb_nouveauClient_mail;
            //client.email = Txb_nouveauClient_mail;
            

            index= ClientDAO.insertClient(client);

            if (index == 0)
            {
                MessageBox.Show("Problème d'enregistrement du client dans la base de données");
            }

        }
        #endregion

    }
}
