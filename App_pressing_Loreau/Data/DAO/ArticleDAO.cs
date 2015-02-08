using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;
using App_pressing_Loreau.Model;
using System.Windows;

namespace App_pressing_Loreau.Data.DAO
{
    class ArticleDAO
    {
        /* Inserer un article dans la base de données
         * @param article : article à insérer
         */
        public static int insertArticle(Article article)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertArticle, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("art_photo", article.photo);
                cmd.Parameters.AddWithValue("art_commentaire", article.commentaire);
                cmd.Parameters.AddWithValue("art_rendu", article.ifRendu);
                cmd.Parameters.AddWithValue("art_TVA", article.TVA);
                cmd.Parameters.AddWithValue("art_TTC", article.TTC);
                if (article.convoyeur != null)
                {
                    cmd.Parameters.AddWithValue("art_conv_id", article.convoyeur.id);
                }
                else
                {
                    cmd.Parameters.AddWithValue("art_conv_id", null);
                }
                cmd.Parameters.AddWithValue("art_typ_id", article.type.id);
                cmd.Parameters.AddWithValue("art_cmd_id", article.fk_commande);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : InsertArticle");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Selectionner un article à partir de son id
         * @param art_id : id de l'article à selectionner
         */
        public static Article selectArticleById(int art_id)
        {
            try
            {
                Article retour = new Article();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectArticleById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", art_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    new PlaceConvoyeur(Int32.Parse(msdr["art_conv_id"].ToString()), 0, 0);
                    Int32.Parse(msdr["art_cmd_id"].ToString());

                    retour = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_TTC"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr["art_conv_id"].ToString()), 0, 0),
                        Int32.Parse(msdr["art_cmd_id"].ToString()));
                    if (!msdr["art_date_rendu"].ToString().Equals(""))
                        retour.date_rendu = DateTime.Parse(msdr["art_date_rendu"].ToString());
                }
                msdr.Dispose();

                Bdd.deconnexion();

                #region ajout du type et du departement
                retour.type = TypeArticleDAO.selectTypesById(retour.type.id);
                #endregion

                #region ajout de la place convoyeur
                retour.convoyeur = PlaceConvoyeurDAO.selectConvoyeurById(retour.convoyeur.id);
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : SelectArticleById");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Selectionner l'ensemble des articles d'une commande dans la base de données
         * @param cmd_id : id d'une commande
         */
        public static List<Article> selectArticleByIdCmd(int cmd_id)
        {
            try
            {
                List<Article> retour = new List<Article>();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectArticleByIdCmd, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", cmd_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();

                while (msdr.Read())
                {
                    Article article = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_TTC"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr["art_conv_id"].ToString()), 0, 0),
                        Int32.Parse(msdr["art_cmd_id"].ToString()));
                    if (msdr["art_date_rendu"].ToString().Equals(null))
                        article.date_rendu = DateTime.Parse(msdr["art_date_rendu"].ToString());

                    retour.Add(article);
                }
                msdr.Dispose();
                Bdd.deconnexion();

                #region ajout des types, des departements et des places convoyeurs
                foreach (Article art in retour)
                {
                    art.type = TypeArticleDAO.selectTypesById(art.type.id);
                    art.convoyeur = PlaceConvoyeurDAO.selectConvoyeurById(art.convoyeur.id);
                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : SelectArticleByIdCmd");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Selectionner les articles rendu en fonction de la date
         * @Param plage date :
         * 1 : par jour
         * 2 : par semaine
         * 3 : par mois
         * 4 : par année
         */
        public static List<Article> selectArticleRenduByDate(int plageDate)
        {
            try
            {
                List<Article> retour = new List<Article>();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectArticleRenduByDate, Bdd.connexion());

                #region ajout des parametres
                switch (plageDate)
                {
                    //par jour
                    case 1:
                        cmd.Parameters.AddWithValue("startTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
                        cmd.Parameters.AddWithValue("endTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
                        break;
                    //par semaine
                    case 2:
                        cmd.Parameters.AddWithValue("startTime", new DateTime(SecondaryDateTime.GetMonday(DateTime.Now).Year, SecondaryDateTime.GetMonday(DateTime.Now).Month, SecondaryDateTime.GetMonday(DateTime.Now).Day, 0, 0, 0));
                        cmd.Parameters.AddWithValue("endTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
                        break;
                    //par mois
                    case 3:
                        cmd.Parameters.AddWithValue("startTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0));
                        cmd.Parameters.AddWithValue("endTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
                        break;
                    //par année
                    case 4:
                        cmd.Parameters.AddWithValue("startTime", new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0));
                        cmd.Parameters.AddWithValue("endTime", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
                        break;
                }
                #endregion

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Article article;
                while (msdr.Read())
                {
                    article = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_TTC"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr["art_conv_id"].ToString()), 0, 0),
                        Int32.Parse(msdr["art_cmd_id"].ToString()));

                    retour.Add(article);
                }

                msdr.Dispose();
                Bdd.deconnexion();

                #region ajout des types, des departements et des places convoyeurs
                foreach (Article art in retour)
                {
                    art.type = TypeArticleDAO.selectTypesById(art.type.id);
                    art.convoyeur = PlaceConvoyeurDAO.selectConvoyeurById(art.convoyeur.id);
                }
                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : SelectArticleRenduByDate");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Update un article dans la base de données
         * @param article : article à update
         */
        public static int updateArticle(Article article)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateArticle, Bdd.connexion());

                #region ajout des parametres
                if (article.photo.Equals(""))
                    cmd.Parameters.AddWithValue("photo", null);
                else
                    cmd.Parameters.AddWithValue("photo", article.photo);

                if (article.commentaire.Equals(""))
                    cmd.Parameters.AddWithValue("commentaire", null);
                else
                    cmd.Parameters.AddWithValue("commentaire", article.commentaire);

                cmd.Parameters.AddWithValue("rendu", article.ifRendu);
                cmd.Parameters.AddWithValue("TVA", article.TVA);
                cmd.Parameters.AddWithValue("TTC", article.TTC);
                cmd.Parameters.AddWithValue("conv_id", article.convoyeur.id);
                cmd.Parameters.AddWithValue("cmd_id", article.fk_commande);
                cmd.Parameters.AddWithValue("typ_id", article.type.id);

                if (article.date_rendu.Equals(DateTime.MinValue))
                    cmd.Parameters.AddWithValue("date_rendu", null);
                else
                    cmd.Parameters.AddWithValue("date_rendu", article.date_rendu);

                cmd.Parameters.AddWithValue("id", article.id);
                #endregion

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();

                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : UpdateArticle");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Delete un article dans la base de données
         * @param article : article à supprimer
         */
        public static int deleteArticle(Article article)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteArticle, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", article.id);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : DeleteArticle");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Last Article Inserted
         * 
         */
        public static Article lastArticle()
        {
            int art_id = 0;
            try
            {
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.lastArticle, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    art_id = Int32.Parse(msdr["art_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return ArticleDAO.selectArticleById(art_id);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : LastArticle");
                Bdd.deconnexion();
                return null;
            }
        }
    }
}
