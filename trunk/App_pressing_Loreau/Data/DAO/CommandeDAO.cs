﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using MySql.Data.MySqlClient;

namespace App_pressing_Loreau.Data.DAO
{
    class CommandeDAO
    {
        //Inserer une commande dans la base de données
        public static int insertCommande(Commande commande)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", commande.date);
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee); 
                cmd.Parameters.AddWithValue("remise", commande.remise);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);

                return cmd.ExecuteNonQuery();

                /*#region Insert Articles
                if (commande.listArticles.Count != 0 && commande.listArticles != null)
                {
                    foreach (Article article in commande.listArticles)
                        ArticleDAO.insertArticle(article);
                }
                #endregion*/

                /*#region Insert payement
                if (commande.listPayements.Count != 0 && commande.listPayements != null)
                {
                    foreach (Payement payement in commande.listPayements)
                        PayementDAO.insertPaiement(payement, commande.id);
                }
                #endregion*/
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
                return 0;
            }
        }

        /*Selectionner l'ensemble des commandes de la base de données
         * @param addPaiement : True pour insérer les paiements dans l'object commande
         * @param addArticles : True pour insérer les Articles dans l'objet Commande
         * @param addClient : True pour insérer les Clients dans l'objet Commande
         */
        public static List<Commande> selectCommandes(Boolean addPaiement, Boolean addArticles, Boolean addClient)
        {
            try
            {
                List<Commande> retour = new List<Commande>();
                
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommandes, Bdd.connexion());

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
                    commande.client = new Client();
                    commande.client.id = Int32.Parse(msdr["cmd_clt_id"].ToString());

                    retour.Add(commande);
                }
                msdr.Dispose();

                #region ajout paiement
                if(addPaiement)
                {
                    foreach (Commande comm in retour)
                        comm.listPayements = PayementDAO.selectPayementByCommande(comm.id);
                }
                #endregion

                #region ajout article
                if(addArticles)
                {
                    foreach (Commande comm in retour)
                        comm.listArticles = ArticleDAO.selectArticleByIdCmd(comm.id);
                }
                #endregion

                #region ajout client
                if (addClient)
                {
                    foreach (Commande comm in retour)
                        //parametres en false afin de ne pas boucler
                        comm.client = ClientDAO.selectClientById(comm.client.id, false, false, false);
                    
                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }
        }

        /*Selectionner l'ensemble des commandes de la base de données à partir de son id
         * @param addPaiement : True pour insérer les paiements dans l'object commande
         * @param addArticles : True pour insérer les Articles dans l'objet Commande
         * @param addClient : True pour insérer les Clients dans l'objet Commande
         */
        public static List<Commande> selectCommandesByClient(int id_client, Boolean addPaiement, Boolean addArticles, Boolean addClient)
        {
            try
            {
                List<Commande> retour = new List<Commande>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommandesByClient, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("clt_id", id_client);

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
                    commande.client = new Client();
                    commande.client.id = Int32.Parse(msdr["cmd_clt_id"].ToString());

                    retour.Add(commande);
                }
                msdr.Dispose();

                #region ajout paiement
                if (addPaiement)
                {
                    foreach (Commande comm in retour)
                        comm.listPayements = PayementDAO.selectPayementByCommande(comm.id);
                }
                #endregion

                #region ajout article
                if (addArticles)
                {
                    foreach (Commande comm in retour)
                        comm.listArticles = ArticleDAO.selectArticleByIdCmd(comm.id);
                }
                #endregion

                #region ajout client
                if (addClient)
                {
                    foreach (Commande comm in retour)
                        // Attention ! Parametres en false afin de ne pas boucler!
                        comm.client = ClientDAO.selectClientById(comm.client.id, false, false, false);

                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }
        }

        /*Selectionner une commande à partir de son id
         * @param addPaiement : True pour insérer les paiements dans l'object commande
         * @param addArticles : True pour insérer les Articles dans l'objet Commande
         */
        public static Commande selectCommandeById(int id_cmd, Boolean addPaiement, Boolean addArticles)
        {
            try
            {
                Commande retour = new Commande();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommandeById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("cmd_id", id_cmd);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new Commande(
                        Int32.Parse(msdr["cmd_id"].ToString()),
                        DateTime.Parse(msdr["cmd_date"].ToString()),
                        Boolean.Parse(msdr["cmd_payee"].ToString()),
                        float.Parse(msdr["cmd_remise"].ToString()));
                }
                msdr.Dispose();

                #region ajout paiement
                if (addPaiement)
                {
                        retour.listPayements = PayementDAO.selectPayementByCommande(retour.id);
                }
                #endregion

                #region ajout article
                if (addArticles)
                {
                        retour.listArticles = ArticleDAO.selectArticleByIdCmd(retour.id);
                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }
        }

        //Inserer une commande dans la base de données
        public static int updateCommande(Commande commande)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commande.id);
                cmd.Parameters.AddWithValue("date", commande.date);
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);
                cmd.Parameters.AddWithValue("remise", commande.remise);
                cmd.Parameters.AddWithValue("id", commande.id);
                

                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
                return 0;
            }
        }

        //Delete une commande dans la base de données
        public static int deleteCommande(Commande commande)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commande.id);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
                return 0;
            }
        }

        public static int lastId(MySqlConnection connection)
        {
            int retour = 0;
            try
            {
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.commandeLastId, Bdd.connexion());

                //Execute la commandekkke
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = Int32.Parse(msdr["cmd_id"].ToString());
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return 0;
            }
        }

    }
}