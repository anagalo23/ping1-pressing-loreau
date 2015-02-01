using App_pressing_Loreau.Helper;
using App_pressing_Loreau.ViewModel;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;




namespace App_pressing_Loreau
{
    /// <summary>
    /// ViewModel pour la classe Accueil.xaml
    /// </summary>
    public class AccueilVM : ObservableObject, IPageViewModel
    {
        #region Attributs

        private IPageViewModel _accessUserControl;
        private List<CategoryItem> _listeUser;


        private Brush _btn_receptionColor;
        private Brush _btn_renduColor;
        private Brush _btn_factureColor;
        private Brush _btn_impressionColor;
        private Brush _btn_clientProColor;
        private Brush _btn_administrateurColor;
        private Brush _btn_convoyeurColor;



        private List<Employe> listeEmployer;

        #endregion

        #region Constructeur
        public AccueilVM()
        {
            _listeUser = new List<CategoryItem>();

            UtilisateurListe();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._contentDetailCommande = null;

        }

        #endregion

        #region Propriétés et Commandes


        public IPageViewModel accessUserControl
        {
            get { return _accessUserControl; }
            set
            {
                if (_accessUserControl != value)
                {
                    _accessUserControl = value;
                    OnPropertyChanged("accessUserControl");
                }
            }
        }

        public List<CategoryItem> ListeUser
        {
            get { return this._listeUser; }
            set
            {
                this._listeUser = value;
                OnPropertyChanged("ListeUser");
            }
        }

        ICommand lesUtilisateurs;
        public ICommand LesUtilisateurs
        {
            get
            {
                foreach (CategoryItem utilisateur in _listeUser)
                {
                    utilisateur.ButtonUserBackground = Brushes.Teal;
                }

                return lesUtilisateurs ?? (lesUtilisateurs = new RelayCommand(ClickSurUtilisateur));
            }

        }


        public Brush Btn_receptionColor
        {
            get { return _btn_receptionColor; }
            set
            {
                _btn_receptionColor = value;
                RaisePropertyChanged("Btn_receptionColor");

            }
        }
        public Brush Btn_renduColor
        {
            get { return _btn_renduColor; }
            set
            {
                _btn_renduColor = value;
                RaisePropertyChanged("Btn_renduColor");
            }
        }
        public Brush Btn_factureColor
        {
            get { return _btn_factureColor; }
            set
            {
                _btn_factureColor = value;
                RaisePropertyChanged("Btn_factureColor");
            }
        }
        public Brush Btn_convoyeurColor
        {
            get { return _btn_convoyeurColor; }
            set
            {
                _btn_convoyeurColor = value;
                RaisePropertyChanged("Btn_convoyeurColor");
            }
        }
        public Brush Btn_clientProColor
        {
            get { return _btn_clientProColor; }
            set
            {
                _btn_clientProColor = value;
                RaisePropertyChanged("Btn_clientProColor");
            }
        }
        public Brush Btn_impressionColor
        {
            get { return _btn_impressionColor; }
            set
            {
                _btn_impressionColor = value;
                RaisePropertyChanged("Btn_impressionColor");
            }
        }
        public Brush Btn_administrateurColor
        {
            get { return _btn_administrateurColor; }
            set
            {
                _btn_administrateurColor = value;
                RaisePropertyChanged("Btn_administrateurColor");
            }
        }





        #region Command bouton menu
        // Button permettant la redirection vers la page Identification Client 
        public ICommand Btn_accueil_receptionClient
        { get { return new RelayCommand(p => identificationClientVM()); } }

        // Button permettant la redirection vers la page Restitution client 
        public ICommand Btn_accueil_renduArticles
        { get { return new RelayCommand(p => restitutionArticleVM()); } }



        // Button permettant la redirection vers la page facture
        public ICommand Btn_accueil_facture
        { get { return new RelayCommand(p => factureVM()); } }


        // Button permettant la redirection vers la page client pro
        public ICommand Btn_accueil_client_pro
        { get { return new RelayCommand(p => clientproVM()); } }



        // Button permettant la redirection vers la page impression 
        public ICommand Btn_accueil_impression
        { get { return new RelayCommand(p => impressionVM()); } }



        // Button permettant la redirection vers la page Connexion administateur
        public ICommand Btn_accueil_administrateur
        { get { return new RelayCommand(p => administateurVM()); } }


        // Button permettant la redirection vers la page convoyeur
        public ICommand Btn_accueil_convoyeur
        { get { return new RelayCommand(p => convoyeurVM()); } }

        #endregion

        // Button permettant de revenir a la page preincipale 
        public ICommand Btn_accueil_image
        { get { return new RelayCommand(p => accueilVM()); } }

        #endregion

        #region Méthodes


        #region Methodes Button menu
        //Methodes des redirection vers le ViewModel de l'Identification client
        public void identificationClientVM()
        {
            Btn_receptionColor = Brushes.IndianRed;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            accessUserControl = new IdentificationClientVM();//Problème avec le datacontext... la vm est liée via data context => ajouter view à dockpanel
            //new IdentificationClient();
        }
        //Methodes des redirection vers le ViewModel de l'restitution client
        public void restitutionArticleVM()
        {
            accessUserControl = new RestitutionArticlesVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.IndianRed;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

        }

        public void factureVM()
        {
            accessUserControl = new FactureVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.IndianRed;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

        }

        public void clientproVM()
        {
            accessUserControl = new ClientPROVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.IndianRed;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

        }
        public void impressionVM()
        {
            accessUserControl = new ImpressionVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.IndianRed;
            Btn_administrateurColor = Brushes.Teal;

        }

        public void administateurVM()
        {
            accessUserControl = new AdministrateurConnexionVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.IndianRed;

        }

        public void convoyeurVM()
        {
            accessUserControl = new ConvoyeurVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.IndianRed;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

        }

        #endregion


        public void ClickSurUtilisateur(object User)
        {
            Button clickedbutton = User as Button;
            if (clickedbutton != null)
            {
                clickedbutton.Background = Brushes.Red;
            }
        }
        public void UtilisateurListe()
        {
            ClasseGlobale.listeEmployes = (List<Employe>)EmployeDAO.selectEmployes();
            if (ClasseGlobale.listeEmployes != null)
            {


                foreach (Employe em in ClasseGlobale.listeEmployes)
                {
                    _listeUser.Add(new CategoryItem() { ButtonUserContent = em.nom, ButtonUserTag = em.id, ButtonUserBackground = Brushes.Teal });
                }
            }
            else
            {
                _listeUser.Add(new CategoryItem() { ButtonUserContent = "David", ButtonUserBackground = Brushes.Teal });

            }



        }
        public void accueilVM()
        {
            listeEmployer = null;
            listeEmployer = (List<Employe>)EmployeDAO.selectEmployes();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            foreach (CategoryItem utilisateur in _listeUser)
            {
                utilisateur.ButtonUserBackground = Brushes.Teal;
            }

            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._contentDetailCommande = null;

         
            accessUserControl = null;
        }
        #endregion

        #region Class
        public class CategoryItem
        {
            public string ButtonUserContent { get; set; }

            public int ButtonUserTag { get; set; }

            public Brush ButtonUserBackground { get; set; }

        }
        #endregion


        public string Name
        {
            get { return ""; }
        }
    }
}
