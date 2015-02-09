using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using MySql.Data.MySqlClient;
using System.Windows;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.Data.DAO
{
    class CommandeDAO
    {
        /* Inserer une commande dans la base de données
         * @param commande : commande à inserer
         */
        public static int insertCommande(Commande commande)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertCommande, Bdd.connexion());

                //ajout des parametres
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee);
                cmd.Parameters.AddWithValue("remise", commande.remise);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);

                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : insertCommande");
                Bdd.deconnexion();
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
                Bdd.deconnexion();

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
                        //parametres en false afin de ne pas boucler
                        comm.client = ClientDAO.selectClientById(comm.client.id, false, false, false);

                }
                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectCommandes");
                Bdd.deconnexion();
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
                Bdd.deconnexion();

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
                MessageBox.Show("ERREUR BDD : selectCommandesByClient");
                Bdd.deconnexion();
                return null;
            }
        }


        /*Selectionner une commande à partir de son id
         * @param addPaiement : True pour insérer les paiements dans l'object commande
         * @param addArticles : True pour insérer les Articles dans l'objet Commande
         */
        public static Commande selectCommandeById(int id_cmd, Boolean addPaiement, Boolean addArticles, Boolean addClient)
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

                int id_clt = -1;

                while (msdr.Read())
                {
                    retour = new Commande(
                        Int32.Parse(msdr["cmd_id"].ToString()),
                        DateTime.Parse(msdr["cmd_date"].ToString()),
                        Boolean.Parse(msdr["cmd_payee"].ToString()),
                        float.Parse(msdr["cmd_remise"].ToString())
                        );

                    id_clt = Int32.Parse(msdr["cmd_clt_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();

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

                #region ajout du client
                if (addClient)
                {
                    if (id_clt > 0)
                    {
                        retour.client = ClientDAO.selectClientById(id_clt, false, false, false);
                    }
                    else if (id_clt == 0)
                    {
                        MessageBox.Show("Erreur (ComomandeDAO.cs:232) : impossible de rechercher un client dont l'id vaut 0");
                    }

                }
                #endregion


                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectCommandeById");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Give the total price of the command
         * @param id_cmd : id de la commande à calculer
         */
        public static float totalTTCCommandeById(int id_cmd)
        {
            try
            {
                float retour = -1;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.totalTTCCommandeById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("cmd_id", id_cmd);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = float.Parse(msdr["total"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : totalTTCCommandeById");
                Bdd.deconnexion();
                return -2;
            }
        }


        /* Give the total payed of the command
         * @param id_cmd : id de la commande à calculer
         */
        public static float totalPayedCommandeById(int id_cmd)
        {
            try
            {
                float retour = -1;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.totalPayedCommandeById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("cmd_id", id_cmd);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = float.Parse(msdr["total"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : totalPayedCommandeById");
                Bdd.deconnexion();
                return -2;
            }
        }


        /*Give the amount command open today
         * @Param plage date :
         *      1 : par jour
         *      2 : par semaine
         *      3 : par mois
         *      4 : par année
         */
        public static List<Commande> listCommandeRecuToday(int plageDate)
        {
            try
            {
                List<Commande> retour = new List<Commande>();
                List<int> cltList = new List<int>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.listCommandeRecuToday, Bdd.connexion());

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
                Commande commande;
                int id_clt;
                while (msdr.Read())
                {
                    commande = new Commande(
                        Int32.Parse(msdr["cmd_id"].ToString()),
                        DateTime.Parse(msdr["cmd_date"].ToString()),
                        Boolean.Parse(msdr["cmd_payee"].ToString()),
                        float.Parse(msdr["cmd_remise"].ToString()));
                    if (!msdr["cmd_date_rendu"].ToString().Equals(""))
                        commande.date_rendu = DateTime.Parse(msdr["cmd_date_rendu"].ToString());
                    id_clt = Int32.Parse(msdr["cmd_clt_id"].ToString());
                    retour.Add(commande);
                    cltList.Add(id_clt);
                }
                msdr.Dispose();
                Bdd.deconnexion();
                #region ajout article

                foreach (Commande comm in retour)
                    comm.listArticles = ArticleDAO.selectArticleByIdCmd(comm.id);

                #endregion

                #region ajout client
                for (int i = 0; i < retour.Count; i++)
                {
                    //parametres en false afin de ne pas boucler
                    retour[i].client = ClientDAO.selectClientById(cltList[i], false, false, false);
                }
                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : listCommandeRecuToday");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Inserer une commande dans la base de données
         * @pram commande : commande à update
         */
        public static int updateCommande(Commande commande)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commande.id);
                cmd.Parameters.AddWithValue("date", commande.date);
                cmd.Parameters.AddWithValue("payee", (commande.payee) ? 1 : 0);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);
                cmd.Parameters.AddWithValue("remise", commande.remise);
                cmd.Parameters.AddWithValue("date_rendu", commande.date_rendu);
                cmd.Parameters.AddWithValue("id2", commande.id);
                


                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : updateCommande");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Delete une commande dans la base de données
         * @pram commande : commande à delete
         */
        public static int deleteCommande(Commande commande)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commande.id);

                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : deleteCommande");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Last Commande Inserted
         */
        public static Commande lastCommande()
        {
            int cmd_id = 0;
            try
            {
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.lastCommande, Bdd.connexion());

                //Execute la commandekkke
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    cmd_id = Int32.Parse(msdr["cmd_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return CommandeDAO.selectCommandeById(cmd_id, false, false, false);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : lastCommande");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Vérifie qu'un commande à été payé par cleanway
         * @param id_cmd : id de la commande à étudier
         * @return bool
         *      false : la commande n'a pas été payée par cleanway
         *      true : la commande à été payée par cleanway
         */
        public static bool isPayedByCleanWay(int id_cmd)
        {
            try
            {

                int result = -1;
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.isPayedByCleanWay, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id_cmd);

                //Execute la commandekkke
                MySqlDataReader msdr = cmd.ExecuteReader();

                while (msdr.Read())
                {
                    result = Int32.Parse(msdr["cmd_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return result != -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : isPayedByCleanWay");
                Bdd.deconnexion();
                return false;
            }
        }

    }
}
