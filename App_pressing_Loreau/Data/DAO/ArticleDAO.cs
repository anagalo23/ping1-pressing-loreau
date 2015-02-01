using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;
using App_pressing_Loreau.Model;

namespace App_pressing_Loreau.Data.DAO
{
    class ArticleDAO
    {
        //Inserer un article dans la base de données
        public static int insertArticle(Article article)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertArticle, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("art_photo", article.photo);
                cmd.Parameters.AddWithValue("art_commentaire", article.commentaire);
                cmd.Parameters.AddWithValue("art_rendu", article.ifRendu);
                cmd.Parameters.AddWithValue("art_TVA", article.TVA);
                cmd.Parameters.AddWithValue("art_TTC", article.TTC);
                cmd.Parameters.AddWithValue("art_conv_id", article.convoyeur.id);//*
                cmd.Parameters.AddWithValue("art_typ_id", article.type.id);
                cmd.Parameters.AddWithValue("art_cmd_id", article.fk_commande);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un article dans la base de données."));
                return 0;
            }
        }

        //Selectionner un article à partir de son id
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
                    retour = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_TTC"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr[" art_conv_id"].ToString()), 0, 0),
                        Int32.Parse(msdr["art_cmd_id"].ToString()));
                }
                msdr.Dispose();

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
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }
        }

        //Selectionner l'ensemble des articles d'une commande dans la base de données
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
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }
        }

        /*
         * Selectionner les articles rendu en fonction de la date
         * @Param plage date :
         * 1 : par jour
         * 2 : par semaine
         * 3 : par mois
         * 4 : par année
         */
        public static List<Article> selectArticleRenduByDate(int plageDate)
        {
            /*try
            {*/
                List<Article> retour = new List<Article>();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectArticleRenduByDate, Bdd.connexion());

                //ajout des parametres
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

                #region ajout des types, des departements et des places convoyeurs
                foreach (Article art in retour)
                {
                    art.type = TypeArticleDAO.selectTypesById(art.type.id);
                    art.convoyeur = PlaceConvoyeurDAO.selectConvoyeurById(art.convoyeur.id);
                }
                #endregion

                return retour;
            /*}
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }*/
        }

        //Update un article dans la base de données
        public static int updateArticle(Article article)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertArticle, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", article.id);
                cmd.Parameters.AddWithValue("photo", article.photo);
                cmd.Parameters.AddWithValue("commentaire", article.commentaire);
                cmd.Parameters.AddWithValue("rendu", article.ifRendu);
                cmd.Parameters.AddWithValue("TVA", article.TVA);
                cmd.Parameters.AddWithValue("TTC", article.TTC);
                cmd.Parameters.AddWithValue("conv_id", article.convoyeur.id);
                cmd.Parameters.AddWithValue("typ_id", article.type.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un article dans la base de données."));
                return 0;
            }
        }

        //Delete un article dans la base de données
        public static int deleteArticle(Article article)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteArticle, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", article.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un article dans la base de données."));
                return 0;
            }
        }

        //Last Article Inserted
        public static Article lastArticle()
        {
            int art_id = 0;
            try
            {
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.lastArticle, Bdd.connexion());

                //Execute la commandekkke
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    art_id = Int32.Parse(msdr["art_id"].ToString());
                }
                msdr.Dispose();
                return ArticleDAO.selectArticleById(art_id);
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return null;
            }
        }
    }
}
