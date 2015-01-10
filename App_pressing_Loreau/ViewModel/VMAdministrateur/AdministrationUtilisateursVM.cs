using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationUtilisateursVM : ObservableObject, IPageViewModel
    {
        private List<Utilisateur> _listeUtilisateurs;
        private String _nameUtilisateur;

        public String Name
        {
            get { return ""; }
        }

        public AdministrationUtilisateursVM()
        {
            initialisationUtilisateurs();   
        }

        #region properties and commands

        //public String NameUtilisateur
        //{
        //    get { return _nameUtilisateur; }
        //    set
        //    {
        //        if (value != _nameUtilisateur)
        //        {
        //            _nameUtilisateur = value;
        //            OnPropertyChanged("NameUtilisateur");
        //        }
        //    }
        //}
        public List<Utilisateur> ListeUtilisateurs
        {
            get { return _listeUtilisateurs; }
            set
            {
                _listeUtilisateurs = value;
                RaisePropertyChanged("ListeUtilisateurs");
            }
        }

        #endregion

        #region Methods
        public void initialisationUtilisateurs()
        {
            ListeUtilisateurs = new List<Utilisateur>();
            ListeUtilisateurs.Add(new Utilisateur() { NameUtilisateur = "Alexis" });
            ListeUtilisateurs.Add(new Utilisateur() { NameUtilisateur = "Huguette" });
            ListeUtilisateurs.Add(new Utilisateur() { NameUtilisateur = "Pierre" });
            ListeUtilisateurs.Add(new Utilisateur() { NameUtilisateur = "Pierre" });
            ListeUtilisateurs.Add(new Utilisateur() { NameUtilisateur = "Pierre" });

        }
        #endregion
    }


    class Utilisateur
    {
        public String NameUtilisateur { get; set; }
    }
}
