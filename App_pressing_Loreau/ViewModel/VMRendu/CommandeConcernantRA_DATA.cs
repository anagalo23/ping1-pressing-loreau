using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{

    /// <summary>
    /// Listes des commandes d'un client
    /// Cette classe permettra de constituer les articles a afficher par client 
    /// </summary>


    class CommandeConcernantRA_DATA : ObservableObject
    {
        #region Attributs
        private int _label_restitutionArticles_Reference;
        private String _label_restitutionArticles_Name;
        private String _label_restitutionArticles_DateCommande;



        #endregion

        #region Constructeur

        public CommandeConcernantRA_DATA()
        {

        }
        #endregion

        #region Properties and commands
        #region recherche client
        public String ContentButtonClientRA { get; set; }
        public int TagButtonClientRA { get; set; }
        public Client clt;


        public String Label_restitutionArticles_NomClient
        {
            get { return this.clt.nom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.clt.nom = value;
                    OnPropertyChanged("Label_restitutionArticles_NomClient");
                }
            }
        }


        public String Label_restitutionArticles_PrenomClient
        {
            get { return this.clt.prenom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.clt.prenom = value;
                    OnPropertyChanged("Label_restitutionArticles_PrenomClient");
                }
            }
        }
        #endregion

        #region Contenu Commande du client

        public Commande commande;
        private String nom;
        public int Label_restitutionArticles_Reference
        {
            get { return _label_restitutionArticles_Reference; }
            set
            {
                if (value != _label_restitutionArticles_Reference)
                {
                    _label_restitutionArticles_Reference = value;
                    OnPropertyChanged("Label_restitutionArticles_Reference");
                }
            }
        }


        public String Label_restitutionArticles_nomDuClient
        {
            get { return _label_restitutionArticles_Name; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _label_restitutionArticles_Name = value;
                    OnPropertyChanged("Label_restitutionArticles_nomDuClient");
                }
            }
        }

        public String Label_restitutionArticles_DateCommande
        {
            get { return _label_restitutionArticles_DateCommande; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _label_restitutionArticles_DateCommande = value;
                    OnPropertyChanged("Label_restitutionArticles_DateCommande");
                }
            }
        }


        //public ObservableCollection<ArticlesRestitutionVM> ListeArticlesRestitution
        //{
        //    get
        //    {
        //        return this._listeArticlesRestitution ??
        //            (this._listeArticlesRestitution = new ObservableCollection<ArticlesRestitutionVM>());
        //    }

        //    set
        //    {
        //        if (value != null)
        //        {
        //            this._listeArticlesRestitution = value;
        //            OnPropertyChanged("ListeArticlesRestitution");
        //        }
        //    }
        //}
        #endregion

        #endregion
    }
}
