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
        //private ICommand _changePageCommand;
        private IPageViewModel _accessUserControl;
        private List<IPageViewModel> _ViewModels;


        #endregion

        #region Constructeur 
        public AccueilVM()
        {
            ViewModels.Add(new IdentificationClientVM());
            ViewModels.Add(new RestitutionArticlesVM());
            ViewModels.Add(new FactureVM());
            ViewModels.Add(new ConvoyeurVM());
            ViewModels.Add(new ClientPROVM());
            ViewModels.Add(new ImpressionVM());
            ViewModels.Add(new AdministrateurConnexionVM());
            ViewModels.Add(new PageAdministrateurVM());
            //ViewModels.Add(new NouveauClientVM());
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
                accessUserControl = ViewModels[0];

            }
            //Bouton Recu
            else if (lang.ToString().Equals("btn_accueil_renduArticles"))
            {
                accessUserControl = ViewModels[1];

                //MessageBox.Show("Bonjour NAGALO", "Bonjour");
            }
             // Bouton Facture
            else if (lang.ToString().Equals("btn_accueil_facture"))
            {
                accessUserControl = ViewModels[2];
            }
                // Bouton Convoyeur
            else if (lang.ToString().Equals("btn_accueil_convoyeur"))
            {
                accessUserControl = ViewModels[3];

            }
                //Bouton Client Pro
            else if (lang.ToString().Equals("btn_accueil_client_pro")) 
            {
                accessUserControl = ViewModels[4];

            }
                //Bouton Impression
            else if (lang.ToString().Equals("btn_accueil_impression"))
            {
                accessUserControl = ViewModels[5];

            }
                //Bouton Administrateur
            else if (lang.ToString().Equals("btn_accueil_administrateur"))
            {
                accessUserControl = ViewModels[6];

            }
            else if (lang.ToString().Equals("btn_identificationAdmin_connecte"))
            {
                accessUserControl = ViewModels[7];

            }
            else if (lang.ToString().Equals("btnIdentifiantNouveauClient"))
            {
                //accessUserControl = ViewModels[3];
            }

                //Bouton Accueil Image
            else if (lang.ToString().Equals("btn_accueil_image"))
            {
                accessUserControl = null;
            }
        }
       
#endregion 


        public List<IPageViewModel> ViewModels
        {
            get
            {
                if (_ViewModels == null)
                    _ViewModels = new List<IPageViewModel>();

                return _ViewModels;
            }
        }

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
                    _accessUserControl= value;
                    OnPropertyChanged("accessUserControl");
                }
            }
        }


      
        #endregion

        #region Methods


        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!ViewModels.Contains(viewModel))
                ViewModels.Add(viewModel);

            accessUserControl = ViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

     
        #endregion

    }
}
