using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace App_pressing_Loreau.ViewModel
{
    class AdministrationClientVM : ObservableObject
    {
        #region Attributs

        private ComboTheme _selected_administrationClient_choix_theme;

        private DelegateCommand<AdministrationClientVM> _supprimerClient;
        private DelegateCommand<AdministrationClientVM> _modifierClient;

        public List<RechercheClient> _listeRechercheClient;

        Client choixClient = new Client();
        Client clientModif = new Client();
        public List<ComboTheme> Cbb_administrationClient_choix_theme { get; set; }

        ComboTheme comboTheme = new ComboTheme();
        private String _txb_administrationClient_choix;
        private String _label_adminIdentClient_choix;
        //Modifier client

        private string _txb_adminClient_modifNumAdresse;
        private String _txb_adminClient_modifNameAdresse;
        private String _txb_adminClient_modifBP;
        private String _txb_adminClient_modifVille;

        private String _txb_adminClient_modifTypeTelephone;
        private int _txb_adminClient_modifTypeIdCleanway;


        #endregion

        #region constructeur
        public AdministrationClientVM()
        {
            Cbb_AdministrationClient_choix_theme = comboTheme.ListeChamp();
            Txb_adminClient_modifTypeIdCleanway = new Int32();
            //Txb_adminClient_modifNumAdresse = new Int32();
        }
        #endregion

        #region Properties and commands

        //Modification client , parametres

        #region modifier client

        public string Txb_adminClient_modifNumAdresse
        {
            get { return _txb_adminClient_modifNumAdresse; }
            set
            {
                if (value != _txb_adminClient_modifNumAdresse)
                {
                    _txb_adminClient_modifNumAdresse = value;
                    OnPropertyChanged("Txb_adminClient_modifNumAdresse");
                }
            }

        }
        public String Txb_adminClient_modifNameAdresse
        {
            get { return _txb_adminClient_modifNameAdresse; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _txb_adminClient_modifNameAdresse = value;
                    OnPropertyChanged("Txb_adminClient_modifNameAdresse");
                }
            }
        }
        public String Txb_adminClient_modifBP
        {
            get { return _txb_adminClient_modifBP; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _txb_adminClient_modifBP = value;
                    OnPropertyChanged("Txb_adminClient_modifBP");
                }
            }
        }

        public String Txb_adminClient_modifVille
        {
            get { return _txb_adminClient_modifVille; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _txb_adminClient_modifVille = value;
                    OnPropertyChanged("Txb_adminClient_modifVille");
                }
            }
        }
        public String Txb_adminClient_modifTypeTelephone
        {
            get { return _txb_adminClient_modifTypeTelephone; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _txb_adminClient_modifTypeTelephone = value;
                    OnPropertyChanged("Txb_adminClient_modifTypeTelephone");
                }
            }
        }
        public int Txb_adminClient_modifTypeIdCleanway
        {
            get { return _txb_adminClient_modifTypeIdCleanway; }
            set
            {
                if (value != _txb_adminClient_modifTypeIdCleanway)
                {
                    _txb_adminClient_modifTypeIdCleanway = value;
                    OnPropertyChanged("Txb_adminClient_modifTypeIdCleanway");
                }
            }
        }

        public ICommand Btn_adminArt_ModifTypeArt
        {
            get { return new RelayCommand(p => EnregistrerModifClient()); }
        }
        #endregion

        public List<ComboTheme> Cbb_AdministrationClient_choix_theme { get; set; }

        public ComboTheme Selected_administrationClient_choix_theme
        {
            get { return _selected_administrationClient_choix_theme; }
            set
            {
                _selected_administrationClient_choix_theme = value;
                OnPropertyChanged("Selected_administrationClient_choix_theme");
            }
        }

        public String Txb_administrationClient_choix
        {
            get { return _txb_administrationClient_choix; }
            set
            {
                _txb_administrationClient_choix = value;
                OnPropertyChanged("Txb_administrationClient_choix");
            }
        }

        public String Label_adminIdentClient_choix
        {
            get { return _label_adminIdentClient_choix; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _label_adminIdentClient_choix = value;
                    OnPropertyChanged("Label_adminIdentClient_choix");
                }
            }
        }
        public List<RechercheClient> ListeRechercheClient
        {
            get { return _listeRechercheClient; }
            set
            {
                _listeRechercheClient = value;
                OnPropertyChanged("ListeRechercheClient");
            }
        }

        public ICommand Btn_administrationClient_valider
        {
            get { return new RelayCommand(c => getChoix(), p => Txb_administrationClient_choix!=null); }
        }

        private DelegateCommand<RechercheClient> getButtonRecherche;
        public DelegateCommand<RechercheClient> GetButtonRecherche
        {
            get { return this.getButtonRecherche??(getButtonRecherche=new DelegateCommand<RechercheClient>(
                this.ExecuteGetClient,
                (arg)=>true)); }
        }

        //private DelegateCommand<AdministrationClientVM> _supprimerClient;
        public DelegateCommand<AdministrationClientVM> SupprimerClient
        {
            get { return this._supprimerClient ?? (this._supprimerClient = new DelegateCommand<AdministrationClientVM>(
                this.ExecuteDeleteClient,
                (arg)=>true));
            }
        }

        public DelegateCommand<AdministrationClientVM> ModifierClient
        {
            get
            {
                return this._modifierClient ?? (this._modifierClient = new DelegateCommand<AdministrationClientVM>(
                    this.ExecuteModifClient,
                    (arg) => true));
            }
        }
        #endregion

        #region methods

        private void EnregistrerModifClient()
        {

            //clientModif.id = this.choixClient.id;
            clientModif = ClientDAO.selectClientById(this.choixClient.id,false, false,false);

            clientModif.adresse.numero = Txb_adminClient_modifNumAdresse;
            clientModif.adresse.rue = Txb_adminClient_modifNameAdresse;
            clientModif.adresse.codePostal = Txb_adminClient_modifBP;
            clientModif.adresse.ville = _txb_adminClient_modifVille;

            clientModif.telmob = _txb_adminClient_modifTypeTelephone;
            clientModif.idCleanWay = Txb_adminClient_modifTypeIdCleanway;

            int x = ClientDAO.updateClient(clientModif);
            if (x != 0)
            {
                MessageBox.Show("Modification de l'adresse \n de " + clientModif.nom + " " + clientModif.prenom + " effectué");
            }
        }
        private void ExecuteDeleteClient(AdministrationClientVM obj)
        {
            if (choixClient != null)
            {
                int y = ClientDAO.deleteClient(obj.choixClient);
                if (y != 0)
                {
                    MessageBox.Show("Client supprimé");
                }
            }
        }

        private void ExecuteModifClient(AdministrationClientVM obj)
        {
            if (obj.choixClient != null)
            {
                Txb_adminClient_modifNumAdresse = obj.choixClient.adresse.numero;
                Txb_adminClient_modifNameAdresse = obj.choixClient.adresse.rue;
                Txb_adminClient_modifVille = obj.choixClient.adresse.ville;
                Txb_adminClient_modifBP = obj.choixClient.adresse.codePostal;

                Txb_adminClient_modifTypeTelephone = obj.choixClient.telmob;
                Txb_adminClient_modifTypeIdCleanway = obj.choixClient.idCleanWay;
            }

        }


        private void ExecuteGetClient(RechercheClient obj)
        {
            if (obj.client != null)
            {
                choixClient = obj.client;
                Label_adminIdentClient_choix = "Choix = " + choixClient.nom + " " + choixClient.prenom;
            }

        }

        public void getChoix()
        {
            //MessageBox.Show(Txb_administrationClient_choix);
            ListeRechercheClient = new List<RechercheClient>();
            List<Client> resultat = null;
            if (Selected_administrationClient_choix_theme != null)
            {


                if (Selected_administrationClient_choix_theme.NameCbb.Equals("Nom"))
                {
                    resultat = ClientDAO.seekClients(Txb_administrationClient_choix, null, null, 0);
                }
                else if (Selected_administrationClient_choix_theme.NameCbb.Equals("Prenom"))
                {
                    resultat = ClientDAO.seekClients(null, Txb_administrationClient_choix, null, 0);
                }
                else resultat = null;

                if (resultat != null)
                {
                    foreach (Client clt in resultat)
                    {
                        ListeRechercheClient.Add(new RechercheClient()
                        {
                           client=clt
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Pas de resultat ");
                }
            }
            else
            {
                MessageBox.Show("Choisissez un élement ");
            }
        }
        #endregion
    }


    #region Class

    public class ComboTheme
    {

        public int cbID { get; set; }
        public String NameCbb { get; set; }

        public List<ComboTheme> ListeChamp()
        {
            List<ComboTheme> lstCb = new List<ComboTheme>();

            lstCb.Add(new ComboTheme() { cbID = 1, NameCbb = "Nom" });
            lstCb.Add(new ComboTheme() { cbID = 2, NameCbb = "Prenom" });

            return lstCb;
        }
    }

    class RechercheClient : ObservableObject
    {
        public Client client = new Client();

       // public int TagButtonClientRA { get; set; }
        public String Label_administrationClient_NomClient
        {
            get { return client.nom; }
            set { client.nom = value;
            OnPropertyChanged("Label_administrationClient_NomClient");
            }
        }

        public String Label_administrationClient_PrenomClient
        {
            get { return client.prenom; }
            set
            {
                client.prenom = value;
                OnPropertyChanged("Label_administrationClient_PrenomClient");
            }
        }


        //public Client getClient()
        //{
        //    //Client c = ClientDAO.seekClients(Label_administrationClient_NomClient, Label_administrationClient_PrenomClient, null, 0);
      
        //    return c;
        //}
    }
    #endregion
}
