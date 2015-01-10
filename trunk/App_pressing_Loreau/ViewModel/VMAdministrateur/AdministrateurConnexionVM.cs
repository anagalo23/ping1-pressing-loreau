using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;
using System.Windows;



namespace App_pressing_Loreau.ViewModel
{
    class AdministrateurConnexionVM : ObservableObject, IPageViewModel
    {
        AccueilVM acVM = new AccueilVM();
        private IPageViewModel _accessUserControl;

        public String Name
        {
            get { return ""; }
        }

        public ICommand Btn_identificationAdmin_connectee
        {
            get
            {
                return new RelayCommand(
                      p => suivant());
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
                 
                    _accessUserControl = value;
                    OnPropertyChanged("accessUserControl");
                }
            }
        }




        public void suivant()
        {
            accessUserControl=null;
            accessUserControl = new PageAdministrateurVM();
        }
    }
}
