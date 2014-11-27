using App_pressing_Loreau.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using App_pressing_Loreau;
using System.Windows;
using App_pressing_Loreau.View;

namespace App_pressing_Loreau
{
   public class AccueilVM : ObservableObject
    {
        #region Fields
        private ICommand _changePageCommand;
        Accueil ac;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        #region Constructeur 
        public AccueilVM () 
        {
           // PageViewModels.Add(this);
            PageViewModels.Add(new IdentificationClientVM());
          

        }

        #endregion

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }


        ICommand onCollectionChangeCommand;
        public ICommand OnCollectionChangeCommand
        {
            get { return onCollectionChangeCommand ?? (onCollectionChangeCommand = new RelayCommand(OnCollectionChange)); }
        }
        private void OnCollectionChange(object lang)
        {
            ac = new Accueil();
            if (lang.ToString().Equals("btn_accueil_reception"))
            {
                CurrentPageViewModel = PageViewModels[0];
                
               //ac.dpanel.Children.Clear();
               //ac.dpanel.Children.Add(new IdentificationClient());
            }
        }
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

    }


   public class CategoryItem
   {
       public string ButtonContent { get; set; }
       public string ButtonTag { get; set; }
   }

}
