using LoreauApplication.Class.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DAO
{
    class ClientDAO
    {
        public static int insertClient(Client client)
        {
            String sql = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway) VALUES (\"@nom\",\"@prenom\",\"@telfix\",\"@telport\",\"@adresse\",\"@dateNaissance\",\"@email\",\"@dateInsc\",\"@idCleanWay\");";

            //connection à la base de données   
            MySqlConnection connection = Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            //ajout des parametres
            cmd.Parameters.AddWithValue("nom", client.nom);
            cmd.Parameters.AddWithValue("prenom", client.prenom);
            cmd.Parameters.AddWithValue("telfixe", client.telfix);
            cmd.Parameters.AddWithValue("telport", client.telmob);
            cmd.Parameters.AddWithValue("adresse", client.adresse);
            cmd.Parameters.AddWithValue("dateNaissance", String.Format("{0:YYYYMMddHHmmss}", client.dateNaissance)); //parametre date sous format annee + mois + jour + heure + minute + seconde
            cmd.Parameters.AddWithValue("email", client.email);
            cmd.Parameters.AddWithValue("dateInsc", String.Format("{0:YYYYMMddHHmmss}", client.dateInscription)); //parametre date sous format annee + mois + jour + heure + minute + seconde
            cmd.Parameters.AddWithValue("idCleanWay", client.idCleanWay);

            //Execute la commande
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return 0;
            }
        }

        public static List<Client> seekClient(String nom, String prenom, String tel)
        {

            List<Client> retour = new List<Client>();

            bool ifPreviousElementSearch = false; //variable servant a verifier si un élément de recheche a déja été inséré. Sert notament au AND de la requete.

            String sql = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway FROM client WHERE ";

            #region complete la requete en fonction de la recherche voulu
            if (nom != null)
            {
                String.Format("{0}{1}", sql, "clt_nom=\"@nom\"");
                ifPreviousElementSearch = true;
            }
            if (prenom != null)
            {
                if (!ifPreviousElementSearch) String.Format("{0}{1}", sql, " AND ");
                String.Format("{0}{1}", sql, "clt_nom=\"@prenom\"");
                ifPreviousElementSearch = true;
            }
            if (tel != null)
            {
                if (!ifPreviousElementSearch) String.Format("{0}{1}", sql, " AND ");
                String.Format("{0}{1}", sql, "(clt_fix=\"@tel\" OR clt_mob = \"@tel\")");
                ifPreviousElementSearch = true;
            }
            String.Format("{0}{1}", sql, " ORDER BY clt_nom ASC;");
            #endregion

            //connection à la base de données   
            MySqlConnection connection = Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            //ajout des parametres
            cmd.Parameters.AddWithValue("nom", nom);
            cmd.Parameters.AddWithValue("prenom", prenom);
            cmd.Parameters.AddWithValue("tel", tel);

            //Execute la commande
            try
            {
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour.Add(new Client(msdr["clt_nom"].ToString(),
                        msdr["clt_prenom"].ToString(),
                        msdr["clt_fix"].ToString(),
                        msdr["clt_mob"].ToString(),
                        msdr["clt_adresse"].ToString(),
                        DateTime.Parse(msdr["clt_dateNaissance"].ToString()),
                        msdr["clt_email"].ToString(),
                        DateTime.Parse(msdr["clt_dateInscription"].ToString()),
                        Int32.Parse(msdr["clt_idCleanway"].ToString())));
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }


        }
    }
}
