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

        //List<Employe> listEmploye = null;
        private List<UnUtilisateurVM> _listeUtilisateurs;
        private String _txb_Utilisateur_Name;

        private List<Employe> listeEmploye=null;
        //private Employe employee;

        private DelegateCommand<UnUtilisateurVM> _deleteUtilisateurs;
        #endregion
        public String Name
        {
            get { return ""; }
        }

        public GestionnUtilisateursVM()
        {
           
            //ajouterUser();
            ListeEmployee();
   
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

        private DelegateCommand<GestionnUtilisateursVM> _ajouterUnUtilisateur;
        public DelegateCommand<GestionnUtilisateursVM> AjouterUnUtilisateur
        {
            get
            {
                return this._ajouterUnUtilisateur ?? (this._ajouterUnUtilisateur = new DelegateCommand<GestionnUtilisateursVM>(ajouterUser,
                    (arg) => true));
            }
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


        private void ListeEmployee()
        {
            ListeUtilisateurs = new List<UnUtilisateurVM>();

            List<Employe> employees = (List<Employe>)EmployeDAO.selectEmployes();
            foreach (Employe em in employees)
            {
                ListeUtilisateurs.Add(new UnUtilisateurVM() { NameUtilisateur = em.nom, idEmployee = em.id });
            }

        }
        public void ajouterUser(GestionnUtilisateursVM  obj)
        {
            //listeEmploye = (List<Employe>)EmployeDAO.selectEmployes();
            Employe employee = new Employe(obj._txb_Utilisateur_Name, obj._txb_Utilisateur_Name);
            if (employee != null)
            {
                EmployeDAO.insertEmploye(employee);
                ListeEmployee();
            }
           
        }
    
        private void ExecuteDeleteUser(UnUtilisateurVM obj)
        {
            Employe employee = (Employe)EmployeDAO.selectEmployeById(obj.idEmployee);
            EmployeDAO.deleteEmploye(employee);
            ListeEmployee();

        }
        #endregion
    }


    class Utilisateur
    {
        public String NameUtilisateur { get; set; }
        public int idUser { get; set; }
    }
}
