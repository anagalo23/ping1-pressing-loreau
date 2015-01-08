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
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertCommande, Bdd.MSConnexion);

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", commande.date);
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee); 
                cmd.Parameters.AddWithValue("remise", commande.remise);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);

                int retour = cmd.ExecuteNonQuery();

                #region Insert Articles
                if (commande.listArticles.Count != 0 && commande.listArticles != null)
                {
                    foreach (Article article in commande.listArticles)
                        ArticleDAO.insertArticle(article);
                }
                #endregion

                #region Insert payement
                if (commande.listPayements.Count != 0 && commande.listPayements != null)
                {
                    foreach (Payement payement in commande.listPayements)
                        PayementDAO.insertPaiement(payement, commande.id);
                }
                #endregion
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
            }
        }

        
        public static void selectCommandes(Boolean addPaiement, Boolean addArticles)
        {
            try
            {
                List<Commande> retour = new List<Commande>();
                List<int> id_clients = new List<int>();
                
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommandes, Bdd.MSConnexion);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Commande commande;
                while (msdr.Read())
                {
                    commande = new Commande(
                        Int32.Parse(msdr["cmd_id"].ToString()),
                        DateTime.Parse(msdr["cmd_date"].ToString()),
                        Boolean.Parse(msdr["cmd_payee"].ToString()),
                        float.Parse(msdr["cmd_remise"].ToString()));
                    retour.Add(commande);
                }
                msdr.Dispose();

                #region ajout paiement
                if(addPaiement)
                {
                    foreach (Commande c1 in retour)
                        c1.listPayements = PayementDAO.selectPayementByCommande(c1.id);
                }
                #endregion

                #region ajout article
                if(addArticles)
                {
                    foreach (Commande c1 in retour)
                        c1.listArticles = ArticleDAO.
                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
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
