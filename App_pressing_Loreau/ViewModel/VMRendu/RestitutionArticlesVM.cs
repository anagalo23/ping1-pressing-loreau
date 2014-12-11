using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;

namespace App_pressing_Loreau.View
{
    class RestitutionArticlesVM : ObservableObject, IPageViewModel
    {

        private int _txb_restitutionArticles_idFactures;
        public String Name
        {
            get { return ""; }
          
        }



        public int Txb_restitutionArticles_idFactures
        {
            get { return _txb_restitutionArticles_idFactures;}
            set
            {
                _txb_restitutionArticles_idFactures = value;
                OnPropertyChanged("Txb_restitutionArticles_idFactures");
            }
        }


        public ICommand Btn_restitutionArticles_ok
        {
            get { return new RelayCommand(
                p=>but(),
                p=> Txb_restitutionArticles_idFactures>0 ); }

        }


        public void but()
        {

        }
    }
}
