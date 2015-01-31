using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.ViewModel;
using System.Collections.ObjectModel;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;

namespace App_pressing_Loreau.Helper
{
    class ClasseGlobale
    {
        

        //*********************************************************************************GESTION DE LA LISTE DE COMMANDE
        public static ObservableCollection<ArticlesVM> _contentDetailCommande { get; set; }

        public static void initializeContentDetailCommande()
        {
            _contentDetailCommande = new ObservableCollection<ArticlesVM>();
        }

        //*********************************************************************************GESTION DU CLIENT EN COURS
        private static Client _client;
        public static Client Client
        {
            get
            {
                if (_client == null)
                {
                    initializeClient();
                }
                return _client;
            }
            set
            {
                _client = value;
            }
        }


        public static void initializeClient()
        {
            Client = new Client();
        }

        

        //*******************************************************************************GESTION DE LA LISTE DES EMPLOYES

        public static List<Employe> listeEmployes { get; set; }
        public static void getAllEmployee()
        {
            listeEmployes= (List<Employe>)EmployeDAO.selectEmployes();
        }


        //*********************************************************************************GESTION DE LA LISTE DES PAIEMENTS DESIRES
        public static ObservableCollection<PaiementListeVM> _contenuListePaiement;
        internal static void initializeContenuListePaiement()
        {
            _contenuListePaiement = new ObservableCollection<PaiementListeVM>();
        }

        //*********************************************************************************GESTION DES RENDU COMMANDE

        public static CommandeConcernantRA_DATA _renduCommande { get; set; }

        public static CommandeConcernantRA_DATA _rendreArticlesSelectionnes { get; set; }
    }
}
