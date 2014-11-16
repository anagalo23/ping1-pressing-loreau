using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using App_pressing_Loreau.View;


namespace App_pressing_Loreau.Controler
{
    class AffichageAccueil : INotifyPropertyChanged
    {
        

        
        public  AffichageAccueil()
        {
          //  OnCollectionChange("accueil_reception");
        }

        ICommand onButtonClickCommand;
        public ICommand OnButtonClickCommand
        {
            get { return onButtonClickCommand ?? (onButtonClickCommand = new RelayCommand(btn_banniereaccueil)); }
        }

        private void btn_banniereaccueil(Object button)
        {
            Button button_reception = button as Button;
            if (button_reception!=null)
            {
                Accueil accueil = new Accueil();
               accueil.DP.Children.Clear();
               accueil.DP.Children.Add(new IdentificationClient());

                MessageBox.Show("msg");
            }
            //MessageBox.Show("msg");
        }

        private void OnCollectionChange(object lang)
        {
            CategoryItem item1 = new CategoryItem();

            if (lang.ToString().Equals("accueil_reception"))
            {
                Accueil accueil = new Accueil();
                accueil.DP.Children.Clear();
                accueil.DP.Children.Add(new IdentificationClient());
            }

        }


        public class CategoryItem
        {
            public string ButtonContent { get; set; }
            public string ButtonTag { get; set; }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

     
    }

}

