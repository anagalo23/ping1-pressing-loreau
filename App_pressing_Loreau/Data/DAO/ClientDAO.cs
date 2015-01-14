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


        //Recherche de client classique a partir du nom, du prenom et/ou du numéro de telephone
        public static List<Client> seekClients(String nom, String prenom, String tel)
        {
            try
            {

                List<Client> retour = new List<Client>();
                bool ifPreviousElementSearch = false; //variable servant a verifier si un élément de recheche a déja été inséré. Sert notament au AND de la requete.

                String sql = Bdd.seekClients;

                #region complete la requete en fonction de la recherche voulue
                if (nom != null)
                {
                    sql = String.Format("{0}{1}", sql, "clt_nom=?");
                    ifPreviousElementSearch = true;
                }
                if (prenom != null)
                {
                    if (ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, " AND ");
                    sql = String.Format("{0}{1}", sql, "clt_prenom=?");
                    ifPreviousElementSearch = true;
                }
                if (tel != null)
                {
                    if (ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, " AND ");
                    sql = String.Format("{0}{1}", sql, "(clt_fix=? OR clt_mob =?)");
                    ifPreviousElementSearch = true;
                }
                if (!ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, "1=1");
                //sql = String.Format("{0}{1}", sql, " ORDER BY clt_nom ASC;");
                #endregion

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(sql, Bdd.connexion());

                //ajout des parametres
                if (nom != null)
                    cmd.Parameters.AddWithValue("nom", nom);
                if (prenom != null)
                    cmd.Parameters.AddWithValue("prenom", prenom);
                if (tel != null)
                {
                    cmd.Parameters.AddWithValue("tel1", tel);
                    cmd.Parameters.AddWithValue("tel2", tel);
                }

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
    }
}

