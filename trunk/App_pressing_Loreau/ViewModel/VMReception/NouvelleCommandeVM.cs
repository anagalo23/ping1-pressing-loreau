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
        public float prixTotal { get; private set;}


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

            defileDepartementSuivante();
            prixTotal = 0;
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


        public ICommand CommandeSuivante
        { get { return new RelayCommand(p => defileDepartementSuivante(),
            p => ListeDepartements.Count!=5);
        }
        }

        public ICommand CommandePrecedente
        { get { return new RelayCommand(p => defileDepartementPrecedente(),
            p => ListeDepartements.Count!=4);
        }
        }

        //Ajouter un article a la commande 
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

        public float Label_NouvelleCommande_prixTotal
        {
            get { return _label_NouvelleCommande_prixTotal; }
            set
            {
                if (value != _label_NouvelleCommande_prixTotal)
                {
                     _label_NouvelleCommande_prixTotal=value;
                    OnPropertyChanged("Label_NouvelleCommande_prixTotal");
                }
            }
        }
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


        public void defileDepartementSuivante()
        {
            ListeDepartements = new List<CategoryItem>();
            listeDepartementDTO = (List<Departement>)DepartementDAO.selectDepartements();

           
                if (listeDepartementDTO != null)
                {
                    for (int i = 0; i < 5; i++)
                        ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });   
                }
            
        }
        public void defileDepartementPrecedente()
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
                    int x = 5, y = 5;
                    clickedbutton.Background = Brushes.Blue;

                    foreach (TypeArticle type in articlesByDep)
                    {
                        if (x < 100) x = 190;
                        else if (x < 200) { x = 400; }
                        else x = 5;

                        listedesArticles.Add(new CategoryItem() { ButtonArticlesContent = type.nom, ButtonArticlesTag = type.id, X = x, Y = y });

                        y += 23;
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
            //prixTotal = 0;
            typeArticleDTO = (TypeArticle)TypeArticleDAO.selectTypesById(Int32.Parse(clickedbutton.Tag.ToString()));

            if (clickedbutton != null)
            {
                //String typeDelArticle = clickedbutton.Tag.ToString();
                //Ajout de l'article à l'interface graphique
                //article=typeArticleDTO;
                this._contentDetailCommande.Add(new ArticlesVM()
                {
                    article=typeArticleDTO,ArticlesName=typeArticleDTO.nom
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

                
                for (int i = 0; i < ContentDetailCommande.Count; i++)
                {
                    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.HT);
                }
               

            }
          
            
        }


        private void ExecuteDeleteArticles(ArticlesVM obj)
        {
            if (this._contentDetailCommande.Contains(obj))
            {
                this._contentDetailCommande.Remove(obj);
                Label_NouvelleCommande_prixTotal -= obj.article.HT;
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

            public int X { get; set; }
            public int Y { get; set; }
         

        }
        #endregion
    }
}
