using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class UnUtilisateurVM : ObservableObject
    {

        private String _nameUtilisateur;
        private String _prenomUtilisateur;
        public int idEmployee { get; set; }
      

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

        public String PrenomUtilisateur
        {
            get { return _prenomUtilisateur; }
            set
            {
                if (value != _prenomUtilisateur)
                {
                    _prenomUtilisateur = value;
                    OnPropertyChanged("PrenomUtilisateur");
                }
            }
        }
    }
}
