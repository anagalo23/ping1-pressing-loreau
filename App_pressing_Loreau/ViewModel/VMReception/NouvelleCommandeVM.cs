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


        private ObservableCollection<ArticlesVM> _contentDetailCommande;
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

            DefileDepartement("CommandeSuivante");
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

        //Permet de changer switcher d'écran de sélection de département
        ICommand onCollectionChangeCommand;
        public ICommand OnCollectionChangeCommand
        {
            get { return onCollectionChangeCommand ?? (onCollectionChangeCommand = new RelayCommand(DefileDepartement)); }
        }

        ICommand listesArticlesCommandes;
        public ICommand ListesArticlesCommandes
        {
            get { return listesArticlesCommandes ?? (listesArticlesCommandes = new RelayCommand(AjouterArticles)); }

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

        public ICommand Btn_nouvellecommande_valider
        {
            get
            {
                return new RelayCommand(
                    p => EnregistrerCommande(),
                    p => ContentDetailCommande.Count() > 0);
            }
        }
        #endregion


        #endregion


        #region Méthodes


        /**
         * Permet le défilement des départements 
         **/


        public void DefileDepartement(Object lang)
        {
            ListeDepartements = new List<CategoryItem>();
            listeDepartementDTO = (List<Departement>)DepartementDAO.selectDepartements();
            if (listeDepartementDTO != null)
            {
                if (lang.ToString().Equals("CommandeSuivante"))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
                    }
                }

                else
                {

                    for (int i = 5; i < listeDepartementDTO.Count; i++)
                    {
                        ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
                    }


                }
            }
            else
            {
                //Inscription dans le log -> liste departement dto n'existe pas
            }
        }

        /**
         * Permet d'afficher  les articles correspondants à un département
         * */
        private void Contenudepartement(object button)
        {
            Button clickedbutton = button as Button;
            ListeArticles = new List<CategoryItem>();
            articlesByDep = (List<TypeArticle>)TypeArticleDAO.selectTypeByDepId(Int32.Parse(clickedbutton.Tag.ToString()));

            if (articlesByDep != null)
            {
                if (articlesByDep.Count > 0)
                {
                    if (clickedbutton != null)
                    {

                        clickedbutton.Background = Brushes.Blue;
                        for (int i = 0; i < articlesByDep.Count; i++)
                        {
                            ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = articlesByDep[i].nom, ButtonArticlesTag = articlesByDep[i].id });
                        }
                        //ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Pantalon", ButtonArticlesTag = "Pantalon" });
                        //ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Veste", ButtonArticlesTag = "Veste" });
                        //ListeArticles.Add(new CategoryItem() { ButtonArticlesContent = "Chemise", ButtonArticlesTag = "Chemise" });

                    }
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
            if (clickedbutton != null)
            {
                //String typeDelArticle = clickedbutton.Tag.ToString();
                //Ajout de l'article à l'interface graphique
                this._contentDetailCommande.Add(new ArticlesVM()
                {
                    ArticlesName = clickedbutton.Tag.ToString()
                });

                //Ajout du même article à la liste d'article locale
                //TypeArticle type = TypeArticleDAO.getTypeObjectByName(typeDelArticle);
                //if (type != null)
                //{
                //    float TVA = type.TVA;
                //    float HT = type.HT;

                //    //Récupération du lien de la photo et du commentaire
                //    string photo = null;
                //    string commentaire = null;
                //    bool ifRendu = false;


                //    PlaceConvoyeur convoyeur = PlaceConvoyeurDAO.getFirstPlace(type.encombrement);

                //    lArticles.Add(new Article(photo, commentaire, ifRendu, TVA, HT, type, convoyeur));
                //}
                //else
                //{
                //    // Problème de récupération du type
                //    // Cette erreur ne devrait jamais arriver puisque les types d'articles sont constants et inscrits en BDD
                //}



            }
        }


        private void ExecuteDeleteArticles(ArticlesVM obj)
        {
            if (this._contentDetailCommande.Contains(obj))
            {
                this._contentDetailCommande.Remove(obj);
            }
        }

        public void EnregistrerCommande()
        {

            Commande commande = new Commande();
            //Parcours des articles de la commande et stockage de ceux-ci dans l'objet commande
            foreach (Article art in lArticles)
            {
                commande.addArticle(art);
            }
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



            //CommandeDAO.open();

            //Assert the integrity ------------------> TODO

            //Add the command in the dataBase
            CommandeDAO.insertCommande(commande);

            //Get the command id ------------------> TODO
            // Good question
            // Check commande.id = ?
            //int fk_cmd = CommandeDAO.lastId();
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
                //article.fk_commande = fk_cmd;
                ArticleDAO.insertArticle(article);
            }

            Bdd.deconnexion();
        }
        #endregion


        #region Class
        public class CategoryItem
        {
            public string ButtonContent { get; set; }
            public string ButtonArticlesContent { get; set; }

            public int ButtonTag { get; set; }
            public int ButtonArticlesTag { get; set; }

        }
        #endregion
    }
}
