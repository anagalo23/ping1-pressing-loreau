using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using App_pressing_Loreau.Model;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;


///S
/// 
/// 
/// 
///

namespace App_pressing_Loreau.View
{
    class NouvelleCommandeVM : ObservableObject, IPageViewModel
    {


        #region Attributs
        Commande commande;

        private List<CategoryItem> _listeDepartement;
        private ICommand onButtonClickCommand;

        private ObservableCollection<ArticlesVM> _contentDetailCommande;
        private DelegateCommand<ArticlesVM> _deleteArticles;

        private List<CategoryItem> _listeArticles;

        private string _defaultPath;

        #endregion
        public String Name
        {
            get { return ""; }
        }

        public NouvelleCommandeVM()
        {
            DefileDepartement("Commande_suivante");
        }


        #region Properties/Commande



        //gestion du choix des articles

        #region Bouton departement
        public ICommand OnButtonClickCommand
        {
            get { return onButtonClickCommand ?? (onButtonClickCommand = new RelayCommand(Contenudepartement)); }
        }

        ICommand onCollectionChangeCommand;
        public ICommand OnCollectionChangeCommand
        {
            get { return onCollectionChangeCommand ?? (onCollectionChangeCommand = new RelayCommand(DefileDepartement)); }
        }

        ICommand listesArtclesCommandes;
        public ICommand ListesArtclesCommandes
        {
            get { return listesArtclesCommandes ?? (listesArtclesCommandes = new RelayCommand(AjouterArticles)); }

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
        
       
        public ObservableCollection<ArticlesVM> ContentDetailCommande
        {
            get
            {
                return this._contentDetailCommande ??
                    (this._contentDetailCommande = new ObservableCollection<ArticlesVM>());
            }

            set
            {
                if (value != null)
                {
                    this._contentDetailCommande = value;
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

       
        #region Methods

        private void ajoutCommande()
        {
            //  -   each articles (List of articles)
            //  -   the client

            // Steps for add the command in the Databases
            //  -   Assert each field of the object command is fill in, except client ids
            //  -   Initialize the field 'payee'
            //  -   Save the command in Database
            //  -   Get his id
            //  -   Complete 'articles' objects
            //  -   We don't care the 'paiement', by default it's not paid

            //Initialize the command field 'payee' to false
            commande.payee = false;

            //Get the client id
            //int idClt;
            //idClt = commande.client.id;



            CommandeDAO.open();

            //Assert the integrity ------------------> TODO

            //Add the command in the dataBase
            CommandeDAO.insertCommande(commande);

            //Get the command id ------------------> TODO
            // Good question
            // Check commande.id = ?
            int fk_cmd = CommandeDAO.lastId();
            //Set paiement  ------------------------> TODO


            //Set articles --> Review architecture, theses lines have not to be here
            // perhaps : public static int insertArticles(List<Article> articles);
            //ArticleDAO.insertArticles(listArticles);
            List<Article> listArticles = commande.listArticles;
            //for (int i = 0; i < listArticles.Count; i++)
            //{
            //    ArticleDAO.insertArticle(listArticles[i]);
            //}
            // Better ;-)
            foreach (Article article in listArticles)
            {
                article.fk_commande = fk_cmd;
                ArticleDAO.insertArticle(article);
            }

            Bdd.deconnexion();
        }

      

        public void DefileDepartement(Object lang)
        {
            CategoryItem item1 = new CategoryItem();
            CategoryItem item2 = new CategoryItem();
            CategoryItem item3 = new CategoryItem();
            CategoryItem item4 = new CategoryItem();
            CategoryItem item5 = new CategoryItem();

            ListeDepartements = new List<CategoryItem>();           // Intialize the button list

            if (lang.ToString().Equals("Commande_suivante"))
            {
                item1.ButtonContent = Category1.Dep1;
                item1.ButtonTag = ButtonNames.NameC1D1;
                

                item2.ButtonContent = Category1.Dep2;
                item2.ButtonTag = ButtonNames.NameC1D2;

                item3.ButtonContent = Category1.Dep3;
                item3.ButtonTag = ButtonNames.NameC1D3;

                item4.ButtonContent = Category1.Dep4;
                item4.ButtonTag = ButtonNames.NameC1D4;

                item5.ButtonContent = Category1.Dep5;
                item5.ButtonTag = ButtonNames.NameC1D5;

                ListeDepartements.Add(item1);
                ListeDepartements.Add(item2);
                ListeDepartements.Add(item3);
                ListeDepartements.Add(item4);
                ListeDepartements.Add(item5);
            }
            else
            {
                item1.ButtonContent = Category2.Dep1;
                item1.ButtonTag = ButtonNames.NameC2D1;

                item2.ButtonContent = Category2.Dep2;
                item2.ButtonTag = ButtonNames.NameC2D2;

                item3.ButtonContent = Category2.Dep3;
                item3.ButtonTag = ButtonNames.NameC2D3;

                item4.ButtonContent = Category2.Dep4;
                item4.ButtonTag = ButtonNames.NameC2D4;

                ListeDepartements.Add(item1);
                ListeDepartements.Add(item2);
                ListeDepartements.Add(item3);
                ListeDepartements.Add(item4);

            }
        }


        private void Contenudepartement(object button)
        {
            Button clickedbutton = button as Button;
            
            if (clickedbutton != null & clickedbutton.Tag.ToString().Equals("Accessoire"))
            {

                CategoryItem item1 = new CategoryItem();
                CategoryItem item2 = new CategoryItem();
                CategoryItem item3 = new CategoryItem();

                item1.ButtonArticlesContent = "Pantalon";
                item1.ButtonArticlesTag = "Pantalon";

                item2.ButtonArticlesContent = "Veste";
                item2.ButtonArticlesTag = "Veste";

                item3.ButtonArticlesContent = "Chemise";
                item3.ButtonArticlesTag = "Chemise";

                clickedbutton.Background = Brushes.Blue;
                ListeArticles = new List<CategoryItem>();

                ListeArticles.Add(item1);
                ListeArticles.Add(item2);
                ListeArticles.Add(item3);
                
            }
            else if (clickedbutton != null & clickedbutton.Tag.ToString().Equals("Ameublement"))
            {
                clickedbutton.Background = Brushes.Blue;
                CategoryItem item1 = new CategoryItem();
                CategoryItem item2 = new CategoryItem();
                CategoryItem item3 = new CategoryItem();

                item1.ButtonArticlesContent = "Robe";
                item1.ButtonArticlesTag = "Robe";

                item2.ButtonArticlesContent = "Jupe";
                item2.ButtonArticlesTag = "Jupe";

                item3.ButtonArticlesContent = "écharpe";
                item3.ButtonArticlesTag = "echarpe";

                ListeArticles = new List<CategoryItem>();

                ListeArticles.Add(item1);
                ListeArticles.Add(item2);
                ListeArticles.Add(item3);

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
            if (clickedbutton != null )
            {
                this._contentDetailCommande.Add(new ArticlesVM()
                {
                    ArticlesName = clickedbutton.Tag.ToString()

                });
            }
        }

       
        private void ExecuteDeleteArticles(ArticlesVM obj)
        {
            if (this._contentDetailCommande.Contains(obj))
            {
                this._contentDetailCommande.Remove(obj);
            }
        }


        #endregion


        #region Class
        public class CategoryItem
        {
            public string ButtonContent { get; set; }
            public string ButtonArticlesContent { get; set; }

            public string ButtonTag { get; set; }
            public string ButtonArticlesTag { get; set; }

        }
        #endregion
    }
}
