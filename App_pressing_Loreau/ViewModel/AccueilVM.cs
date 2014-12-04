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
        private ICommand _btn_identificationAdmin_connecte;

        #endregion

        #region Constructeur
        public AccueilVM()
        {

        }

        #endregion

        #region Properties / Commands

        #region Commands / Action

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


            else if (lang.ToString().Equals("btn_identificationAdmin_connecte"))
            {
                //accessUserControl = null;
                //accessUserControl = new PageAdministrateurVM();
                MessageBox.Show("Alexis");

            }


                //Bouton Accueil Image
            else if (lang.ToString().Equals("btn_accueil_image"))
            {
                accessUserControl = null;
            }
        }

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

        public ICommand btn_identificationAdmin_connecte
        {
            set
            {
                accessUserControl = new PageAdministrateurVM();
            }

        }

        #endregion

        #region Methods



        #endregion

    }
}
