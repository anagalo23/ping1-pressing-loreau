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
        private string _txb_nouveauClient_date_naissance;

        #region Variables locales

        //public static int index { get; private set; }

        private Client _client;
        private bool dateDeNaissanceObligatoire;

        #endregion

        #region Constructeur
        public NouveauClientVM()
        {
            //ClasseGlobale.initializeClient();
            //ClasseGlobale.client = ClasseGlobale.client;
            //ClasseGlobale.Client.type = 0;//Client particulier
            Client = new Client();
            dateDeNaissanceObligatoire = false;

        }
        #endregion

        public String Name
        {
            get { return " "; }
        }


        #region Properties / Commands

        public Client Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }
        public String Txb_nouveauClient_nom
        {
            get { return Client.nom; }
            set
            {
                if (value != Client.nom)
                {
                    Client.nom = value;
                    OnPropertyChanged("Txb_nouveauClient_nom");
                }
            }
        }

        public String Txb_nouveauClient_prenom
        {
            get { return Client.prenom; }
            set
            {
                if (value != Client.prenom)
                {
                    Client.prenom = value;
                    OnPropertyChanged("Txb_nouveauClient_prenom");
                }
            }
        }

        public String Txb_nouveauClient_date_naissance
        {
            get { return _txb_nouveauClient_date_naissance; }//ClasseGlobale.Client.dateNaissance.ToString().Split(' ')[0]
            set
            {
                if (value != _txb_nouveauClient_date_naissance)//ClasseGlobale.Client.dateNaissance.ToString()
                {
                    //ClasseGlobale.Client.dateNaissance = value;
                    _txb_nouveauClient_date_naissance = value;
                    //MessageBox.Show("Value changed : " + _txb_nouveauClient_date_naissance);
                    OnPropertyChanged("Txb_nouveauClient_date_naissance");
                }
            }

        }


        public int Txb_nouveauClient_idCleanway
        {
            get { return Client.idCleanWay; }
            set
            {
                if (value != Client.idCleanWay)
                {
                    Client.idCleanWay = value;
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
                    return Client.adresse.numero;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            set
            {
                if (value != Client.adresse.numero)
                {
                    Client.adresse.numero=value;
                    OnPropertyChanged("Txb_nouveauClient_numero");
                }
            }
        }

        public String Txb_nouveauClient_rue_voie
        {
            get { return Client.adresse.rue; }
            set
            {
                if (value != Client.adresse.rue)
                {
                    Client.adresse.rue = value;
                    OnPropertyChanged("Txb_nouveauClient_rue_voie");
                }
            }
        }

        public String Txb_nouveauClient_bp
        {
            get { return Client.adresse.codePostal; }
            set
            {
                if (value != Client.adresse.codePostal)
                {
                    Client.adresse.codePostal = value;
                    OnPropertyChanged("Txb_nouveauClient_bp");
                }
            }
        }

        public String Txb_nouveauClient_ville
        {
            get { return Client.adresse.ville; }
            set
            {
                if (value != Client.adresse.ville)
                {
                    Client.adresse.ville = value;
                    OnPropertyChanged("Txb_nouveauClient_ville");

                }
            }
        }

        public bool Ckb_nouveauClient_sms
        {
            get { return Client.contactSms; }
            set
            {
                if (value != Client.contactSms)
                {
                    Client.contactSms = value;
                    OnPropertyChanged("Ckb_nouveauClient_sms");
                }
            }
        }
        public String Txb_nouveauClient_portable
        {
            get { return Client.telmob; }
            set
            {
                if (value != Client.telmob)
                {
                    Client.telmob = value;
                    OnPropertyChanged("Txb_nouveauClient_portable");
                }
            }
        }


        public bool Ckb_nouveauClient_mail
        {
            get { return Client.contactMail; }
            set
            {
                if (value != Client.contactMail)
                {
                    Client.contactMail = value;
                    OnPropertyChanged("Ckb_nouveauClient_mail");
                }
            }
        }

        public String Txb_nouveauClient_mail
        {
            get { return Client.email; }
            set
            {
                if (value != Client.email)
                {
                    Client.email = value;
                    OnPropertyChanged("Txb_nouveauClient_mail");
                }
            }
        }


        public ICommand BtnNouveauClientEnregistrer
        {
            get
            {
                return new RelayCommand(
                    p => enregisterClient());
            }
        }



        #endregion


        #region Méthodes

        public void enregisterClient()
        {
            bool check = false;
            if (Client != null)
            {
                if (Client.nom!="" && Client.prenom!="")
                {
                    //Conversion du champ de texte date de naissance en datetime
                    if (_txb_nouveauClient_date_naissance != null || dateDeNaissanceObligatoire == true)
                    {
                        //Vérification du format de la date
                        try
                        {
                            if (_txb_nouveauClient_date_naissance.Split('/').Length != 3)
                            {
                                check = true;
                                MessageBox.Show("La date que vous avez saisie n'est pas du bon format.\n" +
                                    "\nAssurez-vous que la date saisie respecte le format suivant : jour/mois/année");
                            }
                            else//Si le format de la date est le bon
                            {
                                try
                                {
                                    Client.dateNaissance = DateTime.Parse(_txb_nouveauClient_date_naissance);
                                }
                                catch (Exception e)
                                {
                                    if (dateDeNaissanceObligatoire == true)
                                    {
                                        MessageBox.Show("Vous devez enregistrer la date de naissance du client, un autre client portant le même nom et prénom " +
                                            "existe déjà en base de données");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Problème de parse de la date de naissance.\n" + e.ToString());
                                    }

                                }
                            }
                        }
                        catch (Exception e)
                        {
                            check = true;
                            MessageBox.Show("La date que vous avez saisie n'est pas du bon format.\n" +
                                    "\nAssurez-vous que la date saisie respecte le format suivant : jour/mois/année");
                        }

                        
                    }

                    //Vérification de l'existence du nom et du prénom en base de données
                    if (ClientDAO.verificationNomEtPrenom(Client.nom, Client.prenom) && dateDeNaissanceObligatoire == false && check == false)
                    {
                        dateDeNaissanceObligatoire = true;
                        MessageBox.Show("Un client portant le même nom et prénom existe déjà en BDD.\n"+
                            "Vous devez renseigner la date de naissance");
                    }
                    else
                    {
                        if (check == false)//Si je n'ai pas rencontré de problème au niveau de la date de naissance
                        {
                            if (ClientDAO.insertClient(Client) == 1)
                            {
                                Client client = ClientDAO.lastClient();
                                Bdd.deconnexion();
                                if (client == null)
                                {
                                    MessageBox.Show("Problème de récupération du dernier client en BDD");
                                }
                                else
                                {
                                    ClasseGlobale.Client = client;
                                    MessageBox.Show("Nouveau client enregistré avec succès.\nCliquez sur Nouvelle commande pour accéder à l'écran suivant");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Problème d'enregistrement du client dans la base de données");
                            }
                        }
                    }
                }
                else
                {
                    String message = "Assurez-vous d'avoir bien renseigné : ";
                    if (Client.nom == "")
                    {
                        message += "\n\t-\tle nom;";
                    }
                    if (Client.prenom == "")
                    {
                        message += "\n\t-\tle prenom;";
                    }
                    MessageBox.Show(message);
                }

            }
            else
            {
                MessageBox.Show("Le client n'a pas été initialisée, cette erreur logiciel n'est pas censée arriver. Cf code NouveauClientVM.cs l~341");
            }

        }

        #endregion

    }
}
