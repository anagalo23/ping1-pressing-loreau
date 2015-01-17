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
using Microsoft.Practices.Prism.Commands;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationUtilisateursVM : ObservableObject, IPageViewModel
    {
        #region attributs

        List<Employe> listEmploye = null;
        private List<UnUtilisateurVM> _listeUtilisateurs;
        private String _txb_Utilisateur_Name;

        private DelegateCommand<UnUtilisateurVM> _deleteUtilisateurs;
        #endregion
        public String Name
        {
            get { return ""; }
        }

        public AdministrationUtilisateursVM()
        {
            ListeUtilisateurs = new List<UnUtilisateurVM>();
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


        public List<UnUtilisateurVM> ListeUtilisateurs
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



        public DelegateCommand<UnUtilisateurVM> DeleteUtilisateurs
        {
            get
            {
                return this._deleteUtilisateurs ?? (this._deleteUtilisateurs = new DelegateCommand<UnUtilisateurVM>(
                                                                       this.ExecuteDeleteUser,
                                                                       (arg) => true));
            }
        }
        #endregion

        #region Methods

        public void ajouterUser(object button)
        {
            Button clickedbutton = button as Button;
            if (clickedbutton != null)
            {
                this._listeUtilisateurs.Add(new UnUtilisateurVM()
                {
                    NameUtilisateur = this._txb_Utilisateur_Name
                });
            }
        }
        public void initialisationUtilisateurs()
        {
          //listEmploye= (List<Employe>) EmployeDAO.
            ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = "Alexis" });
            ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = "Huguette" });
            ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = "Pierre" });
            ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = "Pierre" });
            ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = "Pierre" });

        }

        private void ExecuteDeleteUser(UnUtilisateurVM obj)
        {
            if (this._listeUtilisateurs.Contains(obj))
            {
                this._listeUtilisateurs.Remove(obj);
            }
        }
        #endregion
    }


    class Utilisateur
    {
        public String NameUtilisateur { get; set; }
        public int idUser { get; set; }
    }
}
