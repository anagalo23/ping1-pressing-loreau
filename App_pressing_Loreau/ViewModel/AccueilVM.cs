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


namespace App_pressing_Loreau
{
    public class AccueilVM : ObservableObject
    {
        #region Fields

        private IPageViewModel _accessUserControl;

        #endregion

        #region Constructeur
        public AccueilVM()
        {

        }

        #endregion

        #region Properties / Commands

        #region Commands / Action
/*
        ICommand onCollectionChangeCommand;
        public ICommand OnCollectionChangeCommand
        {
            get { return onCollectionChangeCommand ?? (onCollectionChangeCommand = new RelayCommand(OnCollectionChange)); }
        }
        private void OnCollectionChange(object lang)
        {
            //Bouton reception
            if (lang.ToString().Equals("btn_accueil_receptionClient"))
            {
                accessUserControl = new IdentificationClientVM();

            }
            //Bouton Recu
            else if (lang.ToString().Equals("btn_accueil_renduArticles"))
            {
                accessUserControl = new RestitutionArticlesVM();

                //MessageBox.Show("Bonjour NAGALO", "Bonjour");
            }
            // Bouton Facture
            else if (lang.ToString().Equals("btn_accueil_facture"))
            {
                accessUserControl = new FactureVM();
            }
            // Bouton Convoyeur
            else if (lang.ToString().Equals("btn_accueil_convoyeur"))
            {
                accessUserControl = new ConvoyeurVM();

            }
            //Bouton Client Pro
            else if (lang.ToString().Equals("btn_accueil_client_pro"))
            {
                accessUserControl = new ClientPROVM();

            }
            //Bouton Impression
            else if (lang.ToString().Equals("btn_accueil_impression"))
            {
                accessUserControl = new ImpressionVM();
                
            }
            //Bouton Administrateur
            else if (lang.ToString().Equals("btn_accueil_administrateur"))
            {
                accessUserControl = new AdministrateurConnexionVM();


            }

                
                //Bouton Accueil Image
            else if (lang.ToString().Equals("btn_accueil_image"))
            {
                accessUserControl = null;
            }
        }

 * */
        #endregion


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


        // Button permettant de revenir a la page preincipale 
        public ICommand Btn_accueil_image
        { get { return new RelayCommand(p => accueilVM());  } }
        #endregion

        #region Methods

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

        public void accueilVM()
        {
            accessUserControl = null;
        }
        #endregion

    }
}
