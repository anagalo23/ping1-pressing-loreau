using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;

namespace App_pressing_Loreau.Data.DAO
{
    class ClientDAO
    {
        //Inserer un client dans la base de données
        public static int insertClient(Client client)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertClient, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", client.nom);
                cmd.Parameters.AddWithValue("prenom", client.prenom);
                cmd.Parameters.AddWithValue("telfixe", client.telfix);
                cmd.Parameters.AddWithValue("telport", client.telmob);
                cmd.Parameters.AddWithValue("adresse", client.adresse.giveAdresse());
                cmd.Parameters.AddWithValue("dateNaissance", client.dateNaissance);
                cmd.Parameters.AddWithValue("email", client.email);
                cmd.Parameters.AddWithValue("idCleanWay", client.idCleanWay);
                cmd.Parameters.AddWithValue("contactMail", client.contactMail);
                cmd.Parameters.AddWithValue("contactSms", client.contactSms);
                cmd.Parameters.AddWithValue("type", client.type);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return 0;
            }
        }


        //Selectionner l'ensemble des clients de la base de données a partir du nom, du prenom et/ou du numéro de telephone
        public static List<Client> seekClients(String nom, String prenom, String tel)
        {
            try
            {

                List<Client> retour = new List<Client>();
                
                String sql = Bdd.seekClients;

                #region complete la requete en fonction de la recherche voulue
                if (nom != null)
                {
                    sql = String.Format("{0}{1}{2}{3}", sql, " AND clt_nom LIKE '%", nom, "%'");
                }
                if (prenom != null)
                {
                    sql = String.Format("{0}{1}{2}{3}", sql, " AND clt_prenom LIKE '%", prenom, "%'");
                }
                if (tel != null)
                {
                    sql = String.Format("{0}{1}{2}{3}", sql, " AND (clt_fix LIKE '%", tel , "%' OR clt_mob LIKE '%" , tel , "%')");
                    
                }
                #endregion

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(sql, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Client client;
                while (msdr.Read())
                {
                    client = new Client(
                        Int32.Parse(msdr["clt_id"].ToString()),
                        msdr["clt_nom"].ToString(),
                        msdr["clt_prenom"].ToString(),
                        msdr["clt_fix"].ToString(),
                        msdr["clt_mob"].ToString(),
                        Adresse.Parse(msdr["clt_adresse"].ToString()),
                        DateTime.Parse(msdr["clt_dateNaissance"].ToString()),
                        msdr["clt_email"].ToString(),
                        DateTime.Parse(msdr["clt_dateInscription"].ToString()),
                        Int32.Parse(msdr["clt_idCleanway"].ToString()),
                        Int32.Parse(msdr["clt_contactmail"].ToString()),
                        Int32.Parse(msdr["clt_contactsms"].ToString()),
                        Int32.Parse(msdr["clt_type"].ToString())
                        );
                    retour.Add(client);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                return null;
            }
        }

        //Selectionner l'ensemble des clients pro
        public static List<Client> selectProClient()
        {
            try
            {

                List<Client> retour = new List<Client>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectProClient, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Client client;
                while (msdr.Read())
                {
                    client = new Client(
                        Int32.Parse(msdr["clt_id"].ToString()),
                        msdr["clt_nom"].ToString(),
                        msdr["clt_prenom"].ToString(),
                        msdr["clt_fix"].ToString(),
                        msdr["clt_mob"].ToString(),
                        Adresse.Parse(msdr["clt_adresse"].ToString()),
                        DateTime.Parse(msdr["clt_dateNaissance"].ToString()),
                        msdr["clt_email"].ToString(),
                        DateTime.Parse(msdr["clt_dateInscription"].ToString()),
                        Int32.Parse(msdr["clt_idCleanway"].ToString()),
                        Int32.Parse(msdr["clt_contactmail"].ToString()),
                        Int32.Parse(msdr["clt_contactsms"].ToString()),
                        Int32.Parse(msdr["clt_type"].ToString())
                        );
                    retour.Add(client);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                return null;
            }
        }

        /* Selectionner un client à l'aide de l'id
         * @param addCommande : truepour ajouter les commandes dans l'objet client
         * @param cmd_addPaiement : Completer les commandes avec les paiements
         * @param cmd_addArticles : Completer les commandes avec les articles
         */
        public static Client selectClientById(int client_id, Boolean addCommandes, Boolean cmd_addPaiement, Boolean cmd_addArticles)
        {
            try
            {
                Client retour = new Client();
                
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectClientById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id_client", client_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Client client;
                while (msdr.Read())
                {
                    client = new Client(
                        Int32.Parse(msdr["clt_id"].ToString()),
                        msdr["clt_nom"].ToString(),
                        msdr["clt_prenom"].ToString(),
                        msdr["clt_fix"].ToString(),
                        msdr["clt_mob"].ToString(),
                        Adresse.Parse(msdr["clt_adresse"].ToString()),
                        DateTime.Parse(msdr["clt_dateNaissance"].ToString()),
                        msdr["clt_email"].ToString(),
                        DateTime.Parse(msdr["clt_dateInscription"].ToString()),
                        Int32.Parse(msdr["clt_idCleanway"].ToString()),
                        Int32.Parse(msdr["clt_contactmail"].ToString()),
                        Int32.Parse(msdr["clt_contactsms"].ToString()),
                        Int32.Parse(msdr["clt_type"].ToString())
                        );
                }
                msdr.Dispose();

                #region ajout des commandes
                if(addCommandes)
                {
                    // Attention ! dernier parametre obligatoirement en false afin de ne pas boucler.
                    retour.listCommandes = CommandeDAO.selectCommandesByClient(retour.id, cmd_addPaiement, cmd_addArticles, false);
                }
                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                return null;
            }
        }

        //Update un client
        public static int updateClient(Client client)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateClient, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", client.id);
                cmd.Parameters.AddWithValue("type", client.type);
                cmd.Parameters.AddWithValue("nom", client.nom);
                cmd.Parameters.AddWithValue("prenom", client.prenom);
                cmd.Parameters.AddWithValue("contactMail", client.contactMail);
                cmd.Parameters.AddWithValue("contactSms", client.contactSms);
                cmd.Parameters.AddWithValue("telfixe", client.telfix);
                cmd.Parameters.AddWithValue("telport", client.telmob);
                cmd.Parameters.AddWithValue("adresse", client.adresse.giveAdresse());
                cmd.Parameters.AddWithValue("dateNaissance", client.dateNaissance);
                cmd.Parameters.AddWithValue("email", client.email);
                cmd.Parameters.AddWithValue("idCleanWay", client.idCleanWay);
                cmd.Parameters.AddWithValue("id", client.id);

                

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return 0;
            }
        }

        //Delete un client
        public static int deleteClient(Client client)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteClient, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", client.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return 0;
            }
        }
    }
}

