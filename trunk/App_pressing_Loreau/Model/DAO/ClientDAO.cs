using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class ClientDAO
    {

        public static void open()
        {
            MySqlConnection connection = Bdd.connexion();
        }

        public static void insertClient(MySqlConnection connection, Client client)
        {
            try
            {
                String sql = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", client.nom);
                cmd.Parameters.AddWithValue("prenom", client.prenom);
                cmd.Parameters.AddWithValue("telfixe", client.telfix);
                cmd.Parameters.AddWithValue("telport", client.telmob);
                cmd.Parameters.AddWithValue("adresse", client.adresse);
                cmd.Parameters.AddWithValue("dateNaissance", client.dateNaissance);
                cmd.Parameters.AddWithValue("email", client.email);
                cmd.Parameters.AddWithValue("dateInsc", client.dateInscription);
                cmd.Parameters.AddWithValue("idCleanWay", client.idCleanWay);
                cmd.Parameters.AddWithValue("contactMail", client.contactMail);
                cmd.Parameters.AddWithValue("contactSms", client.contactSms);
                cmd.Parameters.AddWithValue("type", client.type);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
            }
        }


        //Recherche de client classique a partir du nom, du prenom et/ou du numéro de telephone
        public static List<Client> seekClients(MySqlConnection connection, String nom, String prenom, String tel)
        {
            try
            {

                List<Client> retour = new List<Client>();
                bool ifPreviousElementSearch = false; //variable servant a verifier si un élément de recheche a déja été inséré. Sert notament au AND de la requete.
                String sql = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_type=0 ";

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
                MySqlCommand cmd = new MySqlCommand(sql, connection);

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
                        new Adresse(),
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
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                return null;
            }


        }
    }
}

