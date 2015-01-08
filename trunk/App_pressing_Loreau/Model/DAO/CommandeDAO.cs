using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using MySql.Data.MySqlClient;

namespace App_pressing_Loreau.Model.DAO
{
    class CommandeDAO
    {
        public static void insertCommande(Commande commande)
        {
            try
            {
                String sql = "INSERT INTO commande(cmd_date, cmd_payee, cmd_clt_id, cmd_remise) VALUES (?,?,?,?)";

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", commande.date);
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);
                cmd.Parameters.AddWithValue("remise", commande.remise);

                int retour = cmd.ExecuteNonQuery();

                #region Insert Articles
                if (commande.listArticles.Count != 0 && commande.listArticles.Count != null)
                {
                    String sqlarticle = "INSERT INTO article(art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id) VALUES (?,?,?,?,?,?,?)";
                    cmd.CommandText = sqlarticle;

                    foreach (Article article in commande.listArticles)
                    {
                        //ajout des parametres
                        cmd.Parameters.AddWithValue("photo", article.photo);
                        cmd.Parameters.AddWithValue("commentaire", article.commentaire);
                        cmd.Parameters.AddWithValue("rendu", article.ifRendu);
                        cmd.Parameters.AddWithValue("TVA", article.TVA);
                        cmd.Parameters.AddWithValue("HT", article.HT);
                        cmd.Parameters.AddWithValue("conv_id", article.convoyeur.id);
                        cmd.Parameters.AddWithValue("typ_id", article.type.id);

                        //Execute la commande
                        cmd.ExecuteNonQuery();
                    }
                }
                #endregion

                #region Insert payement
                /*if (commande.listArticles.Count != 0 && commande.listArticles.Count != null)
                {
                    String sqlarticle = "INSERT INTO article(art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id) VALUES (?,?,?,?,?,?,?)";
                    cmd.CommandText = sqlarticle;

                    foreach (Article article in commande.listArticles)
                    {
                        //ajout des parametres
                        cmd.Parameters.AddWithValue("photo", article.photo);
                        cmd.Parameters.AddWithValue("commentaire", article.commentaire);
                        cmd.Parameters.AddWithValue("rendu", article.ifRendu);
                        cmd.Parameters.AddWithValue("TVA", article.TVA);
                        cmd.Parameters.AddWithValue("HT", article.HT);
                        cmd.Parameters.AddWithValue("conv_id", article.convoyeur.id);
                        cmd.Parameters.AddWithValue("typ_id", article.type.id);

                        //Execute la commande
                        cmd.ExecuteNonQuery();
                    }
                }*/

                #endregion

                //Ferme la connexion
                connection.Close();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
            }
        }

        public static int lastId(MySqlConnection connection)
        {
            String sql = "";

            //connection à la base de données  

            MySqlCommand cmd = new MySqlCommand(sql, Bdd.MSConnexion);
            // Le langage d'insertion en bdd est le sql
            cmd.CommandText = sql;
            //ajout des parametres


            int id_de_la_derniere_commande_enregistree;


            try
            {
                id_de_la_derniere_commande_enregistree = cmd.ExecuteNonQuery();
                connection.Close();
                return id_de_la_derniere_commande_enregistree;
            }
            catch
            {
                return 0;
            }
        }

    }
}
