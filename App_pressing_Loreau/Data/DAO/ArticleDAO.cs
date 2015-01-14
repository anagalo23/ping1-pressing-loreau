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
        public static void insertArticle(Article article)
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
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un article dans la base de données."));
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
                    Departement dep = new Departement(
                        Int32.Parse(msdr["type_dep_id"].ToString()),
                        msdr["dep_nom"].ToString());

                    TypeArticle type = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["type_nom"].ToString(),
                        float.Parse(msdr["type_encombrement"].ToString()),
                        float.Parse(msdr["type_TVA"].ToString()),
                        float.Parse(msdr["type_HT"].ToString()),
                        dep);
                    PlaceConvoyeur conv = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()));
                    retour = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_HT"].ToString()),
                        type,
                        conv);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
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
                Departement dep;
                TypeArticle type;
                PlaceConvoyeur conv;
                Article article;
                while (msdr.Read())
                {
                    dep = new Departement(
                        Int32.Parse(msdr["type_dep_id"].ToString()),
                        msdr["dep_nom"].ToString());

                    type = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["type_nom"].ToString(),
                        float.Parse(msdr["type_encombrement"].ToString()),
                        float.Parse(msdr["type_TVA"].ToString()),
                        float.Parse(msdr["type_HT"].ToString()),
                        dep);
                    conv = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()));
                    article = new Article(
                        Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(),
                        msdr["art_commentaire"].ToString(),
                        bool.Parse(msdr["art_rendu"].ToString()),
                        float.Parse(msdr["art_TVA"].ToString()),
                        float.Parse(msdr["art_HT"].ToString()),
                        type,
                        conv);

                    retour.Add(article);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }
        }
    }
}
