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


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// ViewModel pour la vue NouvelleCommande.xaml
    /// </summary>
    class NouvelleCommandeVM : ObservableObject, IPageViewModel
    {


        #region Attributs
        Commande commande;

        private List<CategoryItem> _listeDepartement;
        private List<CategoryItem> _listeArticles;

        private ICommand onButtonClickCommand;

        private ObservableCollection<ArticlesVM> _contentDetailCommande;
        private DelegateCommand<ArticlesVM> _deleteArticles;

        #endregion


        public String Name
        {
            get { return ""; }
        }

        #region Constructeur
        public NouvelleCommandeVM()
        {
            DefileDepartement("Commande_suivante");
        }

        #endregion


        #region Proprietés et Commandes



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

       
        #region Méthodes

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
            _listeDepartement = new List<CategoryItem>();


            if (lang.ToString().Equals("Commande_suivante"))
            {

                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category1.Dep1, ButtonTag = ButtonNames.NameC1D1 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category1.Dep2, ButtonTag = ButtonNames.NameC1D2 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category1.Dep3, ButtonTag = ButtonNames.NameC1D3 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category1.Dep4, ButtonTag = ButtonNames.NameC1D4 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category1.Dep5, ButtonTag = ButtonNames.NameC1D5 });
            }
            else
            {

                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category2.Dep1, ButtonTag = ButtonNames.NameC2D1 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category2.Dep2, ButtonTag = ButtonNames.NameC2D2 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category2.Dep3, ButtonTag = ButtonNames.NameC2D3 });
                _listeDepartement.Add(new CategoryItem() { ButtonContent = Category2.Dep4, ButtonTag = ButtonNames.NameC2D4 });

            }
        }


        private void Contenudepartement(object button)
        {
            Button clickedbutton = button as Button;
            ListeArticles = new List<CategoryItem>();
            if (clickedbutton != null & clickedbutton.Tag.ToString().Equals("Accessoire"))
            {

                clickedbutton.Background = Brushes.Blue;

                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Pantalon", ButtonArticlesTag = "Pantalon" });
                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Veste", ButtonArticlesTag = "Veste" });
                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Chemise", ButtonArticlesTag = "Chemise" });

            }
            else if (clickedbutton != null & clickedbutton.Tag.ToString().Equals("Ameublement"))
            {

                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Robe", ButtonArticlesTag = "Robe" });
                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Jupe", ButtonArticlesTag = "Jupe" });
                ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "écharpe", ButtonArticlesTag = "écharpe" });

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
