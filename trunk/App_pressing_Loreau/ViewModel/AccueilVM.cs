using App_pressing_Loreau.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using App_pressing_Loreau;
using App_pressing_Loreau.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;




namespace App_pressing_Loreau
{
    /// <summary>
    /// ViewModel pour la classe Accueil.xaml
    /// </summary>
    public class AccueilVM : ObservableObject
    {
        #region Attributs

        private IPageViewModel _accessUserControl;
        private List<CategoryItem> _listeUser;

        #endregion

        #region Constructeur
        public AccueilVM()
        {
            _listeUser = new List<CategoryItem>();

            UtilisateurListe();
        }

        #endregion

        #region Propriétés et Commandes


        public IPageViewModel accessUserControl
        {
            get
            {
                return _accessUserControl;
            }
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
            get { return lesUtilisateurs ?? (lesUtilisateurs = new RelayCommand(ClickSurUtilisateur)); }
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
            accessUserControl = new IdentificationClientVM();
        }
        //Methodes des redirection vers le ViewModel de l'restitution client
        public void restitutionArticleVM()
        {
            accessUserControl = new RestitutionArticlesVM();
        }

        public void factureVM()
        {
            accessUserControl = new FactureVM();
        }

        public void clientproVM()
        {
            accessUserControl = new ClientPROVM();
        }
        public void impressionVM()
        {
            accessUserControl = new ImpressionVM();
        }

        public void administateurVM()
        {
            accessUserControl = new AdministrateurConnexionVM();
        }

        public void convoyeurVM()
        {
            accessUserControl = new ConvoyeurVM();
        }

        #endregion


        public void ClickSurUtilisateur(object User)
        {
            Button clickedbutton = User as Button;
           
            if (clickedbutton != null)
            {

                //_listeUser[0].ButtonUserBackground = Brushes.Blue;
                //_listeUser[1].ButtonUserBackground = Brushes.Blue;
                //_listeUser[2].ButtonUserBackground = Brushes.Blue;
                //_listeUser[3].ButtonUserBackground = Brushes.Blue;
                //_listeUser[4].ButtonUserBackground = Brushes.Blue;
                clickedbutton.Background= Brushes.Red;

            }
            else //if (clickedbutton != null & clickedbutton.Tag.ToString().Equals(ListeUser[1].ButtonUserTag))
            {
                //_listeUser[0].ButtonUserBackground = Brushes.Blue;
                //_listeUser[1].ButtonUserBackground = Brushes.Red;
                //_listeUser[2].ButtonUserBackground = Brushes.Blue;
                //_listeUser[3].ButtonUserBackground = Brushes.Blue;
                //_listeUser[4].ButtonUserBackground = Brushes.Blue;
                clickedbutton.Background = Brushes.Blue;

            }
   

        }
        public void UtilisateurListe()
        {
            

            _listeUser.Add(new CategoryItem() { ButtonUserContent="Faye" , ButtonUserBackground=Brushes.Teal ,ButtonUserTag="Faye" });
            _listeUser.Add(new CategoryItem() { ButtonUserContent="Fofou", ButtonUserBackground=Brushes.Teal,ButtonUserTag="Fofou"});
            _listeUser.Add(new CategoryItem() { ButtonUserContent="Nagalo",ButtonUserBackground=Brushes.Teal, ButtonUserTag="Nagalo"});
            _listeUser.Add(new CategoryItem() { ButtonUserContent = "Pollet", ButtonUserBackground = Brushes.Teal, ButtonUserTag = "Pollet" });
            _listeUser.Add(new CategoryItem() { ButtonUserContent = "Taquet", ButtonUserBackground = Brushes.Teal, ButtonUserTag = "Taquet" });
            _listeUser.Add(null);

        }
        public void accueilVM()
        {
            accessUserControl = null;
        }
        #endregion




        #region Class
        public class CategoryItem
        {
            public string ButtonUserContent { get; set; }

            public string ButtonUserTag { get; set; }

            public Brush ButtonUserBackground { get; set; }


        }
        #endregion

    }
}
