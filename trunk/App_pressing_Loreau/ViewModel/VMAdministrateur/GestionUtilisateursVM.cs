using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;


namespace App_pressing_Loreau.ViewModel
{
    class GestionnUtilisateursVM : ObservableObject, IPageViewModel
    {
        #region attributs

        List<Employe> listEmploye = null;
        private List<UnUtilisateurVM> _listeUtilisateurs;
        private String _txb_Utilisateur_Name;

        private List<Employe> listeEmployer;
        private Employe employee;

        private DelegateCommand<UnUtilisateurVM> _deleteUtilisateurs;
        #endregion
        public String Name
        {
            get { return ""; }
        }

        public GestionnUtilisateursVM()
        {
            ListeUtilisateurs = new List<UnUtilisateurVM>();
            listeEmployer = (List<Employe>)EmployeDAO.selectEmployes();

            ajouterUser();
   
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


        public ICommand AjouterUnUtilisateur
        {
            get
            {
                this._txb_Utilisateur_Name = null;
               
                return  new RelayCommand(
                p=>ajouterUser()); }

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

        public void ajouterUser()
        {
          
            employee = new Employe(this._txb_Utilisateur_Name,this._txb_Utilisateur_Name);
            if (employee != null)
            {
                EmployeDAO.insertEmploye(employee);
            }
            for (int i = 0; i < listeEmployer.Count; i++)
            {
                ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = listeEmployer[i].nom, idEmployee = listeEmployer[i].id });
            }
        }
    
        private void ExecuteDeleteUser(UnUtilisateurVM obj)
        {
            employee = (Employe)EmployeDAO.selectEmployeById(obj.idEmployee);
            EmployeDAO.deleteEmploye(employee);
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
