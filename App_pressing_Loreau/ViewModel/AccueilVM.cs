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
using App_pressing_Loreau.ViewModel;
using System.Drawing;



namespace App_pressing_Loreau
{
    public class AccueilVM : ObservableObject
    {
        #region Fields

        private IPageViewModel _accessUserControl;
        private bool _someConditionalProperty;
        Button b;
        private String User1 { get; set; }
        private String User2 { get; set; }
        private String User3 { get; set; }
        private String User4 { get; set; }
        private String User5 { get; set; }
        private String User6 { get; set; }


        #endregion

        #region Constructeur
        public AccueilVM()
        {
            User1 = "User1";
            User2 = "User2";
        }

        #endregion

        #region Properties / Commands


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
        public bool SomeConditionalProperty
        {
            get { return _someConditionalProperty; }
            set
            {
                //...

                OnPropertyChanged("SomeConditionalProperty");
                //Because Background is dependent on this property.
                OnPropertyChanged("Background");
            }
        }
        public Brush ButtonUserColor
        {
            get
            {
                return true ? Brushes.Red : Brushes.Pink;
            }
        }
        public ICommand Btn_user1_accueil
        {
            get { return new RelayCommand(
                p=> user1Action()); }
        }

        #region Command bouton menu
        // Button permettant la redirection vers la page Identification Client 
        public ICommand Btn_accueil_receptionClient
        { get { return new RelayCommand( p => identificationClientVM()); } }

        // Button permettant la redirection vers la page Restitution client 
        public ICommand Btn_accueil_renduArticles
        { get { return new RelayCommand( p=> restitutionArticleVM()); } }



        // Button permettant la redirection vers la page facture
        public ICommand Btn_accueil_facture
        { get { return new RelayCommand(p => factureVM()); } }


        // Button permettant la redirection vers la page client pro
        public ICommand Btn_accueil_client_pro
        { get { return new RelayCommand( p => clientproVM() ); } }

   

        // Button permettant la redirection vers la page impression 
        public ICommand Btn_accueil_impression
        { get { return new RelayCommand( p => impressionVM());} }



        // Button permettant la redirection vers la page Connexion administateur
        public ICommand Btn_accueil_administrateur
        { get  {  return new RelayCommand( p => administateurVM()); } }


        // Button permettant la redirection vers la page convoyeur
        public ICommand Btn_accueil_convoyeur
        { get{ return new RelayCommand( p => convoyeurVM());  }  }

        #endregion

        // Button permettant de revenir a la page preincipale 
        public ICommand Btn_accueil_image
        { get { return new RelayCommand(p => accueilVM());  } }
        #endregion

        #region Methods

        public void user1Action()
        {
            
            SomeConditionalProperty = true;
        }

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
            accessUserControl = new FactureVM();
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
        public void accueilVM()
        {
            accessUserControl = null;
        }
        #endregion

    }
}
