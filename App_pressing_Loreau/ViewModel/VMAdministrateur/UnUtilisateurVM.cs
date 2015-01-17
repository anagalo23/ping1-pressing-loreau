using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class UnUtilisateurVM : ObservableObject, IPageViewModel
    {

        private String _nameUtilisateur;
        public string Name
        {
            get { return ""; }
        }


        public String NameUtilisateur
        {
            get { return _nameUtilisateur; }
            set
            {
                if (value != _nameUtilisateur)
                {
                    _nameUtilisateur = value;
                    OnPropertyChanged("NameUtilisateur");
                }
            }
        }
    }
}
