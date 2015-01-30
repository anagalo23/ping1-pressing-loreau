using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationArticlesDepartementsVM : ObservableObject
    {
        #region Attributs

        private ComboDepart _selected_adminArt_ChoixDepart;
        private ComboDepart _selected_adminArt_ChoixDepartDetele;
        private ComboArticles _selected_adminArt_ChoixArticlesDelete;

        private List<ComboArticles> _listeArticlesDelete;

        ComboDepart comboDepart = new ComboDepart();
        ComboArticles comboArt = new ComboArticles();

        public TypeArticle typeArticle;

        #endregion

        #region Constructeur
        public AdministrationArticlesDepartementsVM()
        {
            ListeDepartementChoix = comboDepart.ListeDep();

            ListeDepartementChoixDelete = comboDepart.ListeDep();

            getArticles();
           
        }
        #endregion

        #region Properties and commands

        #region Ajout articles
       
        public List<ComboDepart> ListeDepartementChoix { get; set; }

        public ComboDepart Selected_adminArt_ChoixDepart
        {
            get { return _selected_adminArt_ChoixDepart; }
            set
            {
                if (value != _selected_adminArt_ChoixDepart)
                {
                    _selected_adminArt_ChoixDepart = value;
                    RaisePropertyChanged("Selected_adminArt_ChoixDepart");
                }

            }
        }
        public String Txt_adminArt_TypeArt
        {
            get { return this.typeArticle.nom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.typeArticle.nom = value;
                    OnPropertyChanged("Txt_adminArt_TypeArt");
                }
            }
        }

        public float Txt_adminArt_PrixTTC
        {
            get { return this.typeArticle.TTC; }
            set
            {
                this.typeArticle.TTC = value;
                OnPropertyChanged("Txt_adminArt_PrixTTC");
            }
        }

        public float Txt_adminArt_Encombrement
        {
            get { return this.typeArticle.encombrement; }
            set
            {
                this.typeArticle.encombrement = value;
                OnPropertyChanged("Txt_adminArt_Encombrement");
            }
        }
        #endregion


        #region Supprimer article

       
        public List<ComboDepart> ListeDepartementChoixDelete{get;set;}
         public List<ComboArticles> ListeArticlesDelete 
        {
            get { return _listeArticlesDelete; }
            set
            {
                _listeArticlesDelete = value;
                RaisePropertyChanged("ListeArticlesDelete");
            }
        }

        public ComboDepart Selected_adminArt_ChoixDepartDetele
        {
            get { return _selected_adminArt_ChoixDepartDetele; }
            set
            {
                if (value != _selected_adminArt_ChoixDepartDetele)
                {
                    _selected_adminArt_ChoixDepartDetele = value;
                    //getArticles();
                    RaisePropertyChanged("Selected_adminArt_ChoixDepartDetele");
                }
            }
        }

        #endregion
        #endregion

        #region Methods

        public TypeArticle getTypeArticle()
        {
            Departement dep = new Departement(Selected_adminArt_ChoixDepart.cbbDepId, Selected_adminArt_ChoixDepart.NameDepart);

            TypeArticle tArticle = new TypeArticle(Txt_adminArt_TypeArt, Txt_adminArt_Encombrement, typeArticle.TVA, Txt_adminArt_PrixTTC, dep);

            return tArticle;
        }

        private void getArticles()
        {
            if (Selected_adminArt_ChoixDepartDetele!=null)
            {
                ListeArticlesDelete = comboArt.ListeArt(this._selected_adminArt_ChoixDepartDetele.cbbDepId);

            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedDep(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));

            if (propertyName == "Selected_adminArt_ChoixDepartDetele")
                getArticles();
        }
        #endregion


        #region Class

        public class ComboDepart
        {
            public String NameDepart { get; set; }
            public int cbbDepId { get; set; }

            List<Departement> depart = (List<Departement>)DepartementDAO.selectDepartements();
            public List<ComboDepart> ListeDep()
            {
                List<ComboDepart> listDep = new List<ComboDepart>();

                foreach (Departement cc in depart)
                {
                    listDep.Add(new ComboDepart() { NameDepart = cc.nom, cbbDepId = cc.id });
                }

                return listDep;
            }
        }

        public class ComboArticles
        {
            public String NameArticles { get; set; }
            public int cbbArtId { get; set; }
            public int cbbDepId { get; set; }

            public List<ComboArticles> ListeArt(int idDep)
            {
                List<TypeArticle> articles = (List<TypeArticle>)TypeArticleDAO.selectTypeByDepId(idDep);
                List<ComboArticles> listArt = new List<ComboArticles>();

                foreach (TypeArticle ta in articles)
                {
                    if (ta.departement.id == idDep)
                    {
                        listArt.Add(new ComboArticles() { cbbArtId = ta.id, cbbDepId = idDep, NameArticles = ta.nom });

                    }
                }
                return listArt;
            }


        }


        #endregion
    }
}
