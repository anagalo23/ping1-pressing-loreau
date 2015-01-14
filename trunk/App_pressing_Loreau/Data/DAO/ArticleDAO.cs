using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;

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
                cmd.Parameters.AddWithValue("photo", article.photo);
                cmd.Parameters.AddWithValue("commentaire", article.commentaire);
                cmd.Parameters.AddWithValue("rendu", article.ifRendu);
                cmd.Parameters.AddWithValue("TVA", article.TVA);
                cmd.Parameters.AddWithValue("HT", article.HT);
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
                        float.Parse(msdr["art_HT"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr[" art_conv_id"].ToString()), 0, 0));
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
                        float.Parse(msdr["art_HT"].ToString()),
                        new TypeArticle(Int32.Parse(msdr["art_typ_id"].ToString()), null, 0, 0, 0, null),
                        new PlaceConvoyeur(Int32.Parse(msdr[" art_conv_id"].ToString()), 0, 0));

                    retour.Add(article);
                }
                msdr.Dispose();

                #region ajout des types, des departements et des places convoyeurs
                foreach(Article art in retour)
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
    }
}
