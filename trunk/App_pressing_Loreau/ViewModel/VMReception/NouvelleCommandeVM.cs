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
using System.Drawing;

namespace App_pressing_Loreau.View
{
    class NouvelleCommandeVM : ObservableObject, IPageViewModel
    {
        Commande commande;
      
        private List<CategoryItem> categorybuttonList;
        private ICommand onButtonClickCommand;

        private DelegateCommand<string> _addArticles;
        private ObservableCollection<ContenuCommande> _contentDetailCommande;
        private DelegateCommand<ContenuCommande> _deleteProduct;
        private List<CategoryItem> _listeArticles;


        public String Name
        {
            get { return ""; }
        }

        public NouvelleCommandeVM()
        {
            DefileDepartement("Commande_suivante");
        }


        #region Properties/Commands

        /*public ICommand GetProductCommand
        {
            get
            {
                if (_ajoutCommande == null)
                {
                    _ajoutCommande = new RelayCommand(
                        param => ajoutCommande()
                    );
                }
                return _ajoutCommande;
            }
        }
        */




        #region Commande
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
        public List<CategoryItem> CategoryButtonList
        {
            get
            {
                return categorybuttonList;
            }
            set
            {
                categorybuttonList = value;
                RaisePropertyChanged("CategoryButtonList");
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
        #endregion
        #region contenu commande
        

        public DelegateCommand<string> AddArticles
        {
            get
            {
                return this._addArticles ?? (this._addArticles = new DelegateCommand<string>(
                                                                  this.ExecuteAddProduct,
                                                                  (arg) => true));
            }
        }

        public ObservableCollection<ContenuCommande> ContentDetailCommande
        {
            get
            {
                return this._contentDetailCommande ??
                    (this._contentDetailCommande = new ObservableCollection<ContenuCommande>());
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
        public DelegateCommand<ContenuCommande> DeleteProduct
        {
            get
            {
                return this._deleteProduct ?? (this._deleteProduct = new DelegateCommand<ContenuCommande>(
                                                                       this.ExecuteDeleteProduct,
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

        public void menu1()
        {
         
        }

        public void DefileDepartement(Object lang)
        {
            CategoryItem item1 = new CategoryItem();
            CategoryItem item2 = new CategoryItem();
            CategoryItem item3 = new CategoryItem();
            CategoryItem item4 = new CategoryItem();
            CategoryItem item5 = new CategoryItem();

            CategoryButtonList = new List<CategoryItem>();           // Intialize the button list

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

                CategoryButtonList.Add(item1);
                CategoryButtonList.Add(item2);
                CategoryButtonList.Add(item3);
                CategoryButtonList.Add(item4);
                CategoryButtonList.Add(item5);
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

                CategoryButtonList.Add(item1);
                CategoryButtonList.Add(item2);
                CategoryButtonList.Add(item3);
                CategoryButtonList.Add(item4);

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

                item1.ButtonContent = "Pantalon";
                item2.ButtonContent = "Veste";
                item3.ButtonContent = "Chemise";

                ListeArticles = new List<CategoryItem>();

                ListeArticles.Add(item1);
                ListeArticles.Add(item2);
                ListeArticles.Add(item3);
                
            }
            if (clickedbutton != null & clickedbutton.Tag.ToString().Equals("Ameublement"))
            {

                CategoryItem item1 = new CategoryItem();
                CategoryItem item2 = new CategoryItem();
                CategoryItem item3 = new CategoryItem();

                item1.ButtonContent = "Robe";
                item2.ButtonContent = "Jupe";
                item3.ButtonContent = "écharpe";

                ListeArticles = new List<CategoryItem>();

                ListeArticles.Add(item1);
                ListeArticles.Add(item2);
                ListeArticles.Add(item3);

            }
        }

        private void ExecuteAddProduct(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                this._contentDetailCommande.Add(new ContenuCommande()
                {
                    ProductName = "Bonjour"
                });
            }
        }

        private void ExecuteDeleteProduct(ContenuCommande obj)
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
            public string ButtonTag { get; set; }
        }
        #endregion
    }
}
