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
using System.IO;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Media.Animation;




namespace App_pressing_Loreau
{
    /// <summary>
    /// ViewModel pour la classe Accueil.xaml
    /// Quelque soit le bouton de l'accueil qui est cliqué (header) le set est appelé puis le getter deux fois
    /// </summary>
    public class AccueilVM : ObservableObject
    {
        #region Attributs

        private IPageViewModel _accessUserControl;
        private List<CategoryItem> _listeUser;
        private String _label_Accueil_NomUser;

        private Brush _btn_receptionColor;
        private Brush _btn_renduColor;
        private Brush _btn_factureColor;
        private Brush _btn_impressionColor;
        private Brush _btn_clientProColor;
        private Brush _btn_administrateurColor;
        private Brush _btn_convoyeurColor;

        private Brush _colorUserConnect;

        ICommand lesUtilisateurs;
        private DelegateCommand<AccueilVM> _btn_accueil_image;

        #endregion

        #region Constructeur
        public AccueilVM()
        {

            UtilisateurListe();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            ColorUserConnect = Brushes.Red;

            Label_Accueil_NomUser = "Choisissez un employé";
        }

        #endregion

        #region Propriétés et Commandes


        public String Label_Accueil_NomUser
        {
            get { return _label_Accueil_NomUser; }
            set
            {

                _label_Accueil_NomUser = value;
                OnPropertyChanged("Label_Accueil_NomUser");

            }
        }
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

        public ICommand LesUtilisateurs
        {
            get
            {
                return lesUtilisateurs ?? (lesUtilisateurs = new RelayCommand(ClickSurUtilisateur));
            }

        }

        public Brush Btn_receptionColor
        {
            get
            {
                return _btn_receptionColor;
            }
            set
            {
                _btn_receptionColor = value;
                ClasseGlobale.SET_ALL_NULL();
                RaisePropertyChanged("Btn_receptionColor");

            }
        }
        public Brush Btn_renduColor
        {
            get { return _btn_renduColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_renduColor = value;
                RaisePropertyChanged("Btn_renduColor");
            }
        }
        public Brush Btn_factureColor
        {
            get { return _btn_factureColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_factureColor = value;
                RaisePropertyChanged("Btn_factureColor");
            }
        }
        public Brush Btn_convoyeurColor
        {
            get { return _btn_convoyeurColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_convoyeurColor = value;
                RaisePropertyChanged("Btn_convoyeurColor");
            }
        }
        public Brush Btn_clientProColor
        {
            get { return _btn_clientProColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_clientProColor = value;
                RaisePropertyChanged("Btn_clientProColor");
            }
        }
        public Brush Btn_impressionColor
        {
            get { return _btn_impressionColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_impressionColor = value;
                RaisePropertyChanged("Btn_impressionColor");
            }
        }
        public Brush Btn_administrateurColor
        {
            get { return _btn_administrateurColor; }
            set
            {
                ClasseGlobale.SET_ALL_NULL();
                _btn_administrateurColor = value;
                RaisePropertyChanged("Btn_administrateurColor");
            }
        }

        public Brush ColorUserConnect
        {
            get { return _colorUserConnect; }
            set
            {
                _colorUserConnect = value;
                OnPropertyChanged("ColorUserConnect");
            }
        }




        #region Command bouton menu
        // Button permettant la redirection vers la page Identification Client 
        public ICommand Btn_accueil_receptionClient
        {
            get
            {
                return new RelayCommand(p => identificationClientVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }

        // Button permettant la redirection vers la page Restitution client 
        public ICommand Btn_accueil_renduArticles
        {
            get
            {
                return new RelayCommand(p => restitutionArticleVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }



        // Button permettant la redirection vers la page facture
        public ICommand Btn_accueil_facture
        {
            get
            {
                return new RelayCommand(p => factureVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }


        // Button permettant la redirection vers la page client pro
        public ICommand Btn_accueil_client_pro
        {
            get
            {
                return new RelayCommand(p => clientproVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }



        // Button permettant la redirection vers la page impression 
        public ICommand Btn_accueil_impression
        {
            get
            {
                return new RelayCommand(p => impressionVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }



        // Button permettant la redirection vers la page Connexion administateur
        public ICommand Btn_accueil_administrateur
        {
            get
            {
                return new RelayCommand(p => administateurVM(),
                    p => ClasseGlobale.employeeEnCours != null );
            }
        }


        // Button permettant la redirection vers la page convoyeur
        public ICommand Btn_accueil_convoyeur
        {
            get
            {
                return new RelayCommand(p => convoyeurVM(),
                    p => ClasseGlobale.employeeEnCours != null);
            }
        }

        #endregion

        // Button permettant de revenir a la page preincipale 
        public DelegateCommand<AccueilVM> Btn_accueil_image
        {
            get
            {
                return this._btn_accueil_image ?? (this._btn_accueil_image = new DelegateCommand<AccueilVM>(accueilVM,
                    (arg) => true));
            }
        }


        #endregion

        #region Méthodes


        #region Methodes Button menu
        //Methodes des redirection vers le ViewModel de l'Identification client
        public void identificationClientVM()
        {
            Btn_receptionColor = Brushes.Crimson;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            accessUserControl = new IdentificationClientVM();//Problème avec le datacontext... la vm est liée via data context => ajouter view à dockpanel

        }
        //Methodes des redirection vers le ViewModel de l restitution client
        public void restitutionArticleVM()
        {
            accessUserControl = new RestitutionArticlesVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Crimson;
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
            Btn_factureColor = Brushes.Crimson;
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
            Btn_clientProColor = Brushes.Crimson;
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
            Btn_impressionColor = Brushes.Crimson;
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
            Btn_administrateurColor = Brushes.Crimson;

        }

        public void convoyeurVM()
        {
            accessUserControl = new ConvoyeurVM();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Crimson;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

        }

        #endregion

        // Action sur button utilisateur
        public void ClickSurUtilisateur(object User)
        {
            Button clickedbutton = User as Button;

            //Action qui suit si le button utilisateur est actionné
            if (clickedbutton != null)
            {
                //Si un utilisateur est en cours 
                if (ClasseGlobale.employeeEnCours != null)
                {
                    ColorUserConnect = Brushes.Orange;

                    //Un processus est en cours 
                    if (accessUserControl != null)
                    {
                        
                        MessageBox.Show("Un utilisateur est en cours d'utilisation \n Un processus est en cours");

                    }
                    else
                    {
                        MessageBox.Show("Un utilisateur est en cours d'utilisation");

                    }
                }
                else if (accessUserControl != null)
                {
                    MessageBox.Show("Un processus est en cours");

                }
                    //Changement d utilisateur
                else
                {
                    try
                    {
                        ClasseGlobale.employeeEnCours = EmployeDAO.selectEmployeById(Int32.Parse(clickedbutton.Tag.ToString()));
                        Label_Accueil_NomUser = "Utilisateur : " + ClasseGlobale.employeeEnCours.nom + " " + ClasseGlobale.employeeEnCours.prenom;
                        ColorUserConnect = Brushes.Green;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("" + e);

                    }
                    

                }

            }
        }
        public void UtilisateurListe()
        {
            try
            {

          

            ListeUser = new List<CategoryItem>();

            List<Employe> emp = (List<Employe>)EmployeDAO.selectEmployes();
            if (emp != null)
            {
                foreach (Employe em in emp)
                {
                    ListeUser.Add(new CategoryItem() { ButtonUserContent = em.prenom, ButtonUserTag = em.id, ButtonUserBackground = Brushes.Teal});
                }
            }
            else
            {
                ListeUser.Add(new CategoryItem() { ButtonUserContent = "Erreur", ButtonUserBackground = Brushes.Teal });
            }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur chargement utilisateur" + e);
            }
        }




        public void accueilVM(AccueilVM obj)
        {

            obj.UtilisateurListe();

            Btn_receptionColor = Brushes.Teal;
            Btn_renduColor = Brushes.Teal;
            Btn_factureColor = Brushes.Teal;
            Btn_clientProColor = Brushes.Teal;
            Btn_convoyeurColor = Brushes.Teal;
            Btn_impressionColor = Brushes.Teal;
            Btn_administrateurColor = Brushes.Teal;

            ClasseGlobale.SET_ALL_NULL();


            obj.accessUserControl = null;
            ClasseGlobale.employeeEnCours = null;
            Label_Accueil_NomUser = "Choisissez un employé";
            ColorUserConnect = Brushes.Red;
        }
        #endregion
    }
    #region Class
    public class CategoryItem
    {
        public string ButtonUserContent { get; set; }

        public int ButtonUserTag { get; set; }

        public Brush ButtonUserBackground { get; set; }

    }
    #endregion

}
