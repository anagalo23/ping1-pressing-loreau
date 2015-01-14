using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;
using System.Windows.Controls;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationUtilisateursVM : ObservableObject, IPageViewModel
    {
        #region attributs

        List<Employe> listEmploye = null;
        private List<Utilisateur> _listeUtilisateurs;
        private String _txb_Utilisateur_Name;
        #endregion
        public String Name
        {
            get { return ""; }
        }

        public AdministrationUtilisateursVM()
        {
            ListeUtilisateurs = new List<Utilisateur>();
            initialisationUtilisateurs();   
        }

        #region properties and commands

        public String Txb_Utilisateur_Name
        {
            get { return _txb_Utilisateur_Name; }
            set
            {
                if (value != _txb_Utilisateur_Name)
                {
                    _txb_Utilisateur_Name = value;
                    OnPropertyChanged("Txb_Utilisateur_Name");
                }
            }
        }
        public List<Utilisateur> ListeUtilisateurs
        {
            get { return _listeUtilisateurs; }
            set
            {
                _listeUtilisateurs = value;
                RaisePropertyChanged("ListeUtilisateurs");
            }
        }

        ICommand addUser;
        public ICommand AddUser
        {
            get { return addUser ?? (addUser = new RelayCommand(ajouterUser)); }

        }

        //public ICommand AddUser
        //{
        //    get { return new RelayCommand(
        //        p=>ajouterUser(),
        //        p=>Txb_Utilisateur_Name!=null); }

        //}
        #endregion

        #region Methods

        public void ajouterUser(object button)
        {
            Button clickedbutton = button as Button;
            if (clickedbutton != null)
            {
                this._listeUtilisateurs.Add(new Utilisateur()
                {
                    NameUtilisateur = this._txb_Utilisateur_Name
                });
            }
        }
        public void initialisationUtilisateurs()
        {
          //listEmploye= (List<Employe>) EmployeDAO.
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
