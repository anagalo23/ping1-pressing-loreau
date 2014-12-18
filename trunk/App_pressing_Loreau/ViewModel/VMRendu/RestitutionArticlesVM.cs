using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;

using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Collections.ObjectModel;

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// ViewModel pour la vue RestitutionArticles.xaml
    /// 
    /// </summary>
    class RestitutionArticlesVM : ObservableObject, IPageViewModel
    {
        #region Attributs 
        Departement dep;

        private int _txb_restitutionArticles_idFactures;
        private string _txb_restitutionArticles_choix;

        ChoixBox choixbox = new ChoixBox();
        private ChoixBox _selected_restitutionClient_choix_theme;
        public List<ChoixBox> Cbb_restitutionClient_choix_theme { get; set; }

        public CommandeConcernantRA_DATA _contentCommandeConcernant;
        public CommandeConcernantRA_DATA _dp_content_affiche_select;

        #endregion
        public String Name
        {
            get { return ""; }

        }
        #region Constructeur 
        public RestitutionArticlesVM()
        {
            Cbb_restitutionClient_choix_theme = choixbox.ListeChamp();
        }


        #endregion


        #region Propriétés et commandes
        public int Txb_restitutionArticles_idFactures
        {
            get { return _txb_restitutionArticles_idFactures; }
            set
            {
                _txb_restitutionArticles_idFactures = value;
                OnPropertyChanged("Txb_restitutionArticles_idFactures");
            }
        }

        public String Txb_restitutionArticles_choix
        {
            get { return _txb_restitutionArticles_choix; }
            set
            {
                _txb_restitutionArticles_choix = value;
                OnPropertyChanged("Txb_restitutionArticles_choix");
            }

        }
        

        public ChoixBox Selected_restitutionClient_choix_theme
        {
            get { return _selected_restitutionClient_choix_theme; }
            set
            {
                _selected_restitutionClient_choix_theme = value;
                RaisePropertyChanged("Selected_restitutionClient_choix_theme");
            }
        }
        
        public ICommand Btn_restitutionArticles_ok
        {
            get
            {
                return new RelayCommand(
                    p => ContenuDeLaCommande(),
                    p => Txb_restitutionArticles_idFactures > 0);
            }

        }

        public ICommand Btn_restitutionArticles_valider
        {
            get { return new RelayCommand(
                p => ContenuDeLaRecherche(),
                p=> Txb_restitutionArticles_choix!=null);
            }
        }

        public CommandeConcernantRA_DATA ContentCommandeConcernant
        {
            get { return _contentCommandeConcernant; }
            set
            {
                if (value != _contentCommandeConcernant)
                {
                    _contentCommandeConcernant = value;
                    OnPropertyChanged("ContentCommandeConcernant");
                }
            }

        }
       
        public CommandeConcernantRA_DATA Dp_content_affiche_select
        {
            get { return _dp_content_affiche_select; }
            set
            {
                _dp_content_affiche_select = value;
                OnPropertyChanged("Dp_content_affiche_select");
            }
        }

        #endregion

        #region Méthodes
        public void ContenuDeLaCommande()
        {
            dep = (Departement)DepartementDAO.selectDepartementById(2);
            CommandeConcernantRA_DATA ccd = new CommandeConcernantRA_DATA();
            ccd.Label_restitutionArticles_departement = "repassage";
            ccd.Label_restitutionArticles_type = "Pantalon";
            ccd.Label_restitutionArticles_name = Txb_restitutionArticles_idFactures.ToString();
            ContentCommandeConcernant = ccd;
        }

        public void ContenuDeLaRecherche()
        {
            CommandeConcernantRA_DATA cdata = new CommandeConcernantRA_DATA();
            cdata.Label_restitutionArticles_departement = "repassage";
            cdata.Label_restitutionArticles_type = "Pantalon";
            cdata.Label_restitutionArticles_name = Txb_restitutionArticles_choix.ToString();

            Dp_content_affiche_select = cdata;

        }

        #endregion


        #region Classe

        public class ChoixBox
        {
            public String NameCbb { get; set; }
            public int cbbId { get; set; }

            public List<ChoixBox> ListeChamp()
            {
                List<ChoixBox> lstCb = new List<ChoixBox>();

                lstCb.Add(new ChoixBox() { cbbId=1, NameCbb="Nom" });
                lstCb.Add(new ChoixBox() { cbbId = 2, NameCbb = "Prenom" });
                lstCb.Add(new ChoixBox() { cbbId = 3, NameCbb = "IdCleanWay" });


                return lstCb;
            }
        }



        #endregion



    }
}
