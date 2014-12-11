using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;

using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.View
{
    class RestitutionArticlesVM : ObservableObject, IPageViewModel
    {
        Departement dep ;

        private int _txb_restitutionArticles_idFactures;
        public CommandeConcernantRA_DATA _contentCommandeConcernant;
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

        public CommandeConcernantRA_DATA ContentCommandeConcernant
        {
            get { return _contentCommandeConcernant; }
            set
            {
                if (value != _contentCommandeConcernant)
                {
                     _contentCommandeConcernant=value ;
                    OnPropertyChanged("ContentCommandeConcernant");
                }
            }

        }
        public void but()
        {
            dep = (Departement)DepartementDAO.selectDepartementById(2);
            CommandeConcernantRA_DATA ccd = new CommandeConcernantRA_DATA();
            ccd.Label_restitutionArticles_departement = "repassage";
            ccd.Label_restitutionArticles_type = "Pantalon";
            ccd.Label_restitutionArticles_name = Txb_restitutionArticles_idFactures.ToString();
            ContentCommandeConcernant = ccd;
        }
    }
}
