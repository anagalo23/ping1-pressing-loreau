using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace App_pressing_Loreau.ViewModel
{
    /// <summary> Classe Edite Articles
    /// Cette classe perme de modifier, de supprimer et d ajouter un type d'article dans un departement
    /// </summary>
    class AdministrationArticlesDepartementsVM : ObservableObject
    {
        #region Attributs

        private ComboDepart _selected_adminArt_ChoixDepart;
        private ComboDepart _selected_adminArt_ChoixDepartDetele;
        private ComboArticles _selected_adminArt_ChoixArticlesDelete;

        private DelegateCommand<AdministrationArticlesDepartementsVM> _supprimerArticles;
        private DelegateCommand<AdministrationArticlesDepartementsVM> _modifierArticles;

        private List<ComboArticles> _listeArticlesDelete;

        ComboDepart comboDepart = new ComboDepart();
        ComboArticles comboArt = new ComboArticles();
        public TypeArticle typeArticle;

        //Modifier articles
        private String _txb_adminArt_modifTypeNom;
        private float _txb_adminArt_modifTypeTTC;
        private float _txb_adminArt_modifTypeTVA;
        private float _txb_adminArt_modifTypeEncombrement;
        private TypeArticle typeModif = new TypeArticle();

        #endregion

        #region Constructeur
        public AdministrationArticlesDepartementsVM()
        {
            ListeDepartementChoix = comboDepart.ListeDep();

            ListeDepartementChoixDelete = comboDepart.ListeDep();
            typeArticle = new TypeArticle();

            Txb_adminArt_modifTypeEncombrement = new float();
            Txb_adminArt_modifTypeTVA = new float();
            Txb_adminArt_modifTypeTTC = new float();
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

        public float Txt_adminArt_TauxTVA
        {
            get { return this.typeArticle.TVA; }
            set
            {
                this.typeArticle.TVA = value;
                OnPropertyChanged("Txt_adminArt_TauxTVA");

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


        public ICommand Btn_adminArt_AjoutArticle
        {
            get
            {
                return new RelayCommand(p => addArticles(),
                    p => Selected_adminArt_ChoixDepart != null & Txt_adminArt_TypeArt != null
                    & Txt_adminArt_PrixTTC != 0 & Txt_adminArt_TauxTVA != 0);
            }
        }
        #endregion


        #region Supprimer /Modifier  article


        public ICommand Btn_ValiderDepartement
        {
            get { return new RelayCommand(p => getListeArticles()); }

        }
        public List<ComboDepart> ListeDepartementChoixDelete { get; set; }
        public List<ComboArticles> ListeArticlesDelete
        {
            get { return _listeArticlesDelete; }
            set
            {
                _listeArticlesDelete = value;
                RaisePropertyChanged("ListeArticlesDelete");
            }
        }
        public DelegateCommand<AdministrationArticlesDepartementsVM> SupprimerArticles
        {
            get
            {
                return this._supprimerArticles ?? (this._supprimerArticles = new DelegateCommand<AdministrationArticlesDepartementsVM>(
                                                                       this.ExecuteDeleteArticles,
                                                                       (arg) => true));
            }
        }

        public DelegateCommand<AdministrationArticlesDepartementsVM> ModifierArticles
        {
            get
            {
                return this._modifierArticles ?? (this._modifierArticles = new DelegateCommand<AdministrationArticlesDepartementsVM>(
                                                                       this.ExecuteModifArticle,
                                                                       (arg) => true));
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
                    RaisePropertyChanged("Selected_adminArt_ChoixDepartDetele");
                }
            }
        }

        public ComboArticles Selected_adminArt_ChoixArticlesDelete
        {
            get { return _selected_adminArt_ChoixArticlesDelete; }
            set
            {
                _selected_adminArt_ChoixArticlesDelete = value;
                OnPropertyChanged("Selected_adminArt_ChoixArticlesDelete");

            }
        }


        #region Modifier Articles

        public ICommand Btn_adminArt_ModifTypeArt
        {
            get
            {
                return new RelayCommand(p => modifArticle()
                    );
            }
        }
        public String Txb_adminArt_modifTypeNom
        {
            get { return _txb_adminArt_modifTypeNom; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _txb_adminArt_modifTypeNom = value;
                    OnPropertyChanged("Txb_adminArt_modifTypeNom");
                }
            }
        }
        public float Txb_adminArt_modifTypeTTC
        {
            get { return _txb_adminArt_modifTypeTTC; }
            set
            {
                if (value != _txb_adminArt_modifTypeTTC)
                {
                    _txb_adminArt_modifTypeTTC = value;
                    OnPropertyChanged("Txb_adminArt_modifTypeTTC");
                }
            }

        }
        public float Txb_adminArt_modifTypeTVA
        {
            get { return _txb_adminArt_modifTypeTVA; }
            set
            {
                if (value != _txb_adminArt_modifTypeTVA)
                {
                    _txb_adminArt_modifTypeTVA = value;
                    OnPropertyChanged("Txb_adminArt_modifTypeTVA");
                }
            }

        }
        public float Txb_adminArt_modifTypeEncombrement
        {
            get { return _txb_adminArt_modifTypeEncombrement; }
            set
            {
                if (value != _txb_adminArt_modifTypeEncombrement)
                {
                    _txb_adminArt_modifTypeEncombrement = value;
                    OnPropertyChanged("Txb_adminArt_modifTypeEncombrement");
                }
            }

        }

        #endregion
        #endregion
        #endregion

        #region Methods
        private void ExecuteDeleteArticles(AdministrationArticlesDepartementsVM obj)
        {
            if (obj.Selected_adminArt_ChoixArticlesDelete != null &
                obj.Selected_adminArt_ChoixArticlesDelete.cbbDepId == obj.Selected_adminArt_ChoixDepartDetele.cbbDepId)
            {
                //MessageBox.Show(obj.Selected_adminArt_ChoixArticlesDelete.NameArticles);
                TypeArticle artType = TypeArticleDAO.selectTypesById(obj.Selected_adminArt_ChoixArticlesDelete.cbbArtId);

                if (artType != null)
                {
                    int ind = TypeArticleDAO.deleteType(artType);
                    if (ind != 0)
                    {
                        MessageBox.Show("l'article " + obj.Selected_adminArt_ChoixArticlesDelete.NameArticles + " est supprimé");
                        initializeFieldsDelete();
                    }
                }
            }
            else
            {
                MessageBox.Show("Choissez un departement ou un article de ce departement");
            }

        }
        private void ExecuteModifArticle(AdministrationArticlesDepartementsVM obj)
        {
            if (obj.Selected_adminArt_ChoixArticlesDelete != null &
                obj.Selected_adminArt_ChoixArticlesDelete.cbbDepId == obj.Selected_adminArt_ChoixDepartDetele.cbbDepId)
            {
                typeModif = TypeArticleDAO.selectTypesById(obj.Selected_adminArt_ChoixArticlesDelete.cbbArtId);
                if (typeModif != null)
                {
                    Txb_adminArt_modifTypeNom = typeModif.nom;
                    Txb_adminArt_modifTypeTTC = typeModif.TTC;
                    Txb_adminArt_modifTypeTVA = typeModif.TVA;
                    Txb_adminArt_modifTypeEncombrement = typeModif.encombrement;

                }

            }
            else
            {
                MessageBox.Show("Choissez un departement ou un article de ce departement");
            }

        }
        private void getListeArticles()
        {
            if (Selected_adminArt_ChoixDepartDetele != null)
            {
                ListeArticlesDelete = comboArt.ListeArt(this._selected_adminArt_ChoixDepartDetele.cbbDepId);

            }
        }

        private void addArticles()
        {
            Departement dep = new Departement(Selected_adminArt_ChoixDepart.cbbDepId, Selected_adminArt_ChoixDepart.NameDepart);
            TypeArticle TArt = new TypeArticle(typeArticle.nom, typeArticle.encombrement, typeArticle.TVA, typeArticle.TTC, dep);
            //MessageBox.Show(TArt.nom);

            int index = TypeArticleDAO.insertType(TArt);
            if (index != 0)
            {

                MessageBox.Show("Enregistrement " + TArt.nom + " reussi");

                initializeFieldsAdd();
            }
        }

        private void modifArticle()
        {
            //Departement dep = new Departement(S.cbbDepId, Selected_adminArt_ChoixDepart.NameDepart);
            if (typeModif != null)
            {
                TypeArticle TArtModif = new TypeArticle(typeModif.id, Txb_adminArt_modifTypeNom, Txb_adminArt_modifTypeEncombrement, Txb_adminArt_modifTypeTVA, Txb_adminArt_modifTypeTTC, typeModif.departement);
                int index = TypeArticleDAO.updateType(TArtModif);
                if (index != 0)
                {

                    MessageBox.Show("Modification " + TArtModif.nom + " reussie");
                    initializeFieldsModif();
                }
            }

        }


        private void initializeFieldsAdd()
        {
            Selected_adminArt_ChoixDepart = null;
            Txt_adminArt_TypeArt = null;
            Txt_adminArt_PrixTTC = 0;
            Txt_adminArt_TauxTVA = 0;
            Txt_adminArt_Encombrement = 0;
        }
        private void initializeFieldsDelete()
        {
            Selected_adminArt_ChoixDepartDetele = null;
            Selected_adminArt_ChoixArticlesDelete = null;
        }

        private void initializeFieldsModif()
        {
            Txb_adminArt_modifTypeNom = null;
            Txb_adminArt_modifTypeTTC = 0;
            Txb_adminArt_modifTypeTVA = 0;
            Txb_adminArt_modifTypeEncombrement = 0;
            typeModif = null;
        }

        #endregion
    }
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
