using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.Model.DAO
{
    class ArticleDAO
    {
        public static void insertArticle(Article article)
        {
            try
            {
                String sql = "INSERT INTO article(art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id) VALUES (?,?,?,?,?,?,?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

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

        public static Article getArticleById(int art_id)
        {
            try
            {
                Article retour = new Article();
                String sql = "SELECT A.art_id, A.art_photo, A.art_commentaire, A.art_rendu, A.art_TVA, A.art_HT, C.conv_id, C.conv_emplacement, T.typ_id, T.type_nom, T.type_encombrement, T.type_TVA, T.type_HT, T.type_dep_id, D.dep_nom FROM article A, convoyeur C, type T, departement D WHERE A.art_conv_id=C.conv_id AND A.art_typ_id=T.typ_id AND T.type_dep_id=D.dep_id AND A.art_id=?";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

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
                        msdr["type_nom"].ToString() , 
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
    }
}