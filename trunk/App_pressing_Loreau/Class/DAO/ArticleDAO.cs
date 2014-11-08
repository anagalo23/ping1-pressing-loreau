using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using App_pressing_Loreau.Class.DTO;

namespace App_pressing_Loreau.Class.DAO
{
    class ArticleDAO
    {
        public static int insertArticle(Article article)
        {
            MySqlConnection connection = Bdd.connexion();


            String sql = "INSERT INTO article (art_photo, art_commentaire, art_rendu, convoyeur_conv_id,"
                           + "departement_dep_id, type_typ_id) VALUES (@photo,@comm,@rendu,@conv_id,@dep_id,@typ_id)";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.CommandText = sql;
            //ajout des parametres
            cmd.Parameters.Add("@photo", article.photo);
            cmd.Parameters.Add("@comm", article.commentaire);
            cmd.Parameters.Add("@rendu", article.ifRendu);
            cmd.Parameters.Add("@conv_id", article.conv_id);
            cmd.Parameters.Add("@dep_id", article.dep_id);
            cmd.Parameters.Add("@typ_id", article.typ_id);

            int retour = cmd.ExecuteNonQuery();
            connection.Close();
            try
            {
                return retour;

            }
            catch
            {
                return 0;
            }



        }

        public static List<Article> getArticlesById(int art_id)
        {
            MySqlConnection connection = Bdd.connexion();

            List<Article> listArticle = new List<Article>();

            String sql = "SELECT art_id, art_photo, art_commentaire, art_rendu, convoyeur_conv_id,"
                           + "departement_dep_id, type_typ_id FROM article WHERE =" + art_id;

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.CommandText = sql;

            //Execute la commande
            try
            {
                MySqlDataReader msdr = cmd.ExecuteReader();
                Article article;
                while (msdr.Read())
                {
                    article = new Article(Int32.Parse(msdr["art_id"].ToString()),
                        msdr["art_photo"].ToString(), msdr["art_commentaire"].ToString(),
                         bool.Parse(msdr["art_rendu"].ToString()), Int32.Parse(msdr["type_typ_id"].ToString()),
                         Int32.Parse(msdr["departement_dep_id"].ToString()), Int32.Parse(msdr["convoyeur_conv_id"].ToString()));
                    listArticle.Add(article);
                }
                msdr.Dispose();

            }
            catch
            {
               
            }
            return listArticle;

        }
    }
}
