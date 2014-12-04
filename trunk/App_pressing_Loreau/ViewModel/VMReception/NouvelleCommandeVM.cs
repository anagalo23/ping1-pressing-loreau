using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.View
{
    class NouvelleCommandeVM : ObservableObject, IPageViewModel
    {
        public String Name
        {
            get { return ""; }
        }
        private ICommand _ajoutCommande;

        #region Properties/Commands

        public ICommand GetProductCommand
        {
            get
            {
                if (_ajoutCommande == null)
                {
                    _ajoutCommande = new RelayCommand(
                        param => ajoutCommande(commande)
                    );
                }
                return _ajoutCommande;
            }
        }

        #endregion


        #region Methods

        private void ajoutCommande(Commande commande)
        {
            //Command contain : 
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
            int idClt;
            idClt = commande.client.id;
            /*if ((commande.client.id == 0) && (commande.clientpro.id != 0))
            {
                idClt = commande.clientpro.id;
            }
            else if ((commande.client.id != 0) && (commande.clientpro.id == 0))
            {
                idClt = commande.client.id;
            }
            else
            {
                //Both Ids are null or both are initialized. Theses impossibles cases are to be avoided
                throw new Exception("Problème dans la construction de la commande : soit pas de client, soit deux. Faudrait savoir...");
            }*/

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
        }


        #endregion

    }
}
