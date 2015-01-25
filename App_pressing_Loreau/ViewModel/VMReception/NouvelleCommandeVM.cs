using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using App_pressing_Loreau.Data;


namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// ViewModel pour la vue NouvelleCommande.xaml
    /// </summary>
    class NouvelleCommandeVM : ObservableObject, IPageViewModel
    {


        #region Attributs
        private List<CategoryItem> _listeDepartement;
        private List<CategoryItem> _listeArticles;
        private List<Departement> listeDepartementDTO = null;
        private List<TypeArticle> articlesByDep = null;
        private TypeArticle typeArticleDTO = null;

        private float _label_NouvelleCommande_prixTotal;
        public float prixTotal { get; private set; }

        private DelegateCommand<ArticlesVM> _deleteArticles;
        private List<Article> lArticles;

        #endregion


        public String Name
        {
            get { return ""; }
        }

        #region Constructeur
        public NouvelleCommandeVM()
        {

            lArticles = new List<Article>();

            defileDepartementPrecedente();
            //deselectButtons();
            prixTotal = 0;
            if (ContentDetailCommande != null)
            {
                for (int i = 0; i < ContentDetailCommande.Count; i++)
                {
                    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.TTC);
                }

            }
            
        }

        #endregion


        #region Proprietés et Commandes



        //gestion du choix des articles

        #region Bouton departement
        //Permet de faire réagir le bouton
        ICommand onButtonClickCommand;
        public ICommand OnButtonClickCommand
        {
            get { return onButtonClickCommand ?? (onButtonClickCommand = new RelayCommand(Contenudepartement)); }
        }

        //Ajouter un article a la commande 
        ICommand listesArticlesCommandes;
        public ICommand ListesArticlesCommandes
        {
            get { return listesArticlesCommandes ?? (listesArticlesCommandes = new RelayCommand(AjouterArticles)); }

        }


        public ICommand CommandeSuivante
        {
            get
            {
                return new RelayCommand(p => defileDepartementSuivante(),
                    p => ListeDepartements.Count != 4);
            }
        }

        public ICommand CommandePrecedente
        {
            get
            {
                return new RelayCommand(p => defileDepartementPrecedente(),
                    p => ListeDepartements.Count != 5);
            }
        }

        public List<CategoryItem> ListeDepartements
        {
            get
            {
                return _listeDepartement;
            }
            set
            {
                _listeDepartement = value;
                RaisePropertyChanged("ListeDepartements");
            }
        }

        public List<CategoryItem> ListeArticles
        {
            get
            {
                return _listeArticles;
            }
            set
            {
                _listeArticles = value;
                RaisePropertyChanged("ListeArticles");
            }
        }
        #endregion


        #region contenu commande

        public float Label_NouvelleCommande_prixTotal
        {
            get { return _label_NouvelleCommande_prixTotal; }
            set
            {
                if (value != _label_NouvelleCommande_prixTotal)
                {
                    _label_NouvelleCommande_prixTotal = value;
                    OnPropertyChanged("Label_NouvelleCommande_prixTotal");
                }
            }
        }
        public ObservableCollection<ArticlesVM> ContentDetailCommande
        {
            get
            {
                if (ClasseGlobale._contentDetailCommande == null)
                {
                    ClasseGlobale.initializeContentDetailCommande();
                }
                return ClasseGlobale._contentDetailCommande;
               
            }

            set
            {
                if (value != null)
                {
                    ClasseGlobale._contentDetailCommande = value;
                    OnPropertyChanged("ContentDetailCommande");
                }
            }
        }

        public DelegateCommand<ArticlesVM> DeleteArticles
        {
            get
            {
                return this._deleteArticles ?? (this._deleteArticles = new DelegateCommand<ArticlesVM>(
                                                                       this.ExecuteDeleteArticles,
                                                                       (arg) => true));
            }
        }

      
        #endregion


        #endregion


        #region Méthodes

        /**
         * Permet le défilement des départements 
         **/

        public void defileDepartementPrecedente()
        {
            ListeDepartements = new List<CategoryItem>();
            listeDepartementDTO = (List<Departement>)DepartementDAO.selectDepartements();


            if (listeDepartementDTO != null)
            {
                for (int i = 0; i < 5; i++)
                    ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
            }

        }
       
        public void defileDepartementSuivante()
        {
            ListeDepartements = new List<CategoryItem>();

            if (listeDepartementDTO != null)
            {

                for (int i = 5; i < listeDepartementDTO.Count; i++)
                    ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
            }

        }

        /**
         * Permet d'afficher  les articles correspondants à un département
         * */
        private void Contenudepartement(object button)
        {
            Button clickedbutton = button as Button;
            List<CategoryItem> listedesArticles = new List<CategoryItem>();
            articlesByDep = new List<TypeArticle>();
            articlesByDep = (List<TypeArticle>)TypeArticleDAO.selectTypeByDepId(Int32.Parse(clickedbutton.Tag.ToString()));
            
            if (articlesByDep.Count > 0)
            {
                if (clickedbutton != null)
                {
                    int x = 15, y = 5;
                   //deselectButtons();
                    //clickedbutton.Background = Brushes.Blue;

                    foreach (TypeArticle type in articlesByDep)
                    {
                        listedesArticles.Add(new CategoryItem() { ButtonArticlesContent = type.nom, ButtonArticlesTag = type.id, X = x, Y = y });
                        if (x < 16) { x = 220; }
                        else if (x < 221) { x = 425; }
                        else if (x > 424)
                        {
                            x = 15;
                            y += 70;
                        }
                    }
                    ListeArticles = listedesArticles;
                }
            }
            else
            {
                clickedbutton.Background = Brushes.Blue;
                string msg = string.Format("You Pressed : {0} button", clickedbutton.Tag);
                MessageBox.Show(msg);
            }

        }

       

        public void AjouterArticles(object button)
        {
            Button clickedbutton = button as Button;
            typeArticleDTO = (TypeArticle)TypeArticleDAO.selectTypesById(Int32.Parse(clickedbutton.Tag.ToString()));

            if (clickedbutton != null)
            {

                ClasseGlobale._contentDetailCommande.Add(new ArticlesVM()
                {
                    article = typeArticleDTO,
                    ArticlesName = typeArticleDTO.nom,

                });

                Label_NouvelleCommande_prixTotal = 0;
                for (int i = 0; i < ContentDetailCommande.Count; i++)
                {
                    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.TTC);
                }


            }


        }


        private void ExecuteDeleteArticles(ArticlesVM obj)
        {
            if (ClasseGlobale._contentDetailCommande.Contains(obj))
            {
                ClasseGlobale._contentDetailCommande.Remove(obj);
                Label_NouvelleCommande_prixTotal -= obj.article.TTC;
            }
        }

       
        #endregion


        #region Class
        public class CategoryItem
        {
            public string ButtonContent { get; set; }
            public string ButtonArticlesContent { get; set; }

            public Brush ButtonDepBackground { get; set; }
            public int ButtonTag { get; set; }
            public int ButtonArticlesTag { get; set; }

            public int X { get; set; }
            public int Y { get; set; }


        }

        #endregion
    }
}
