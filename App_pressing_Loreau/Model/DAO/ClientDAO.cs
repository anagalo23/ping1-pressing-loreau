using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class ClientDAO
    {
        public static int insertClient(Client client)
        {
            MySqlConnection connection = Bdd.connexion();

            String sql = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms) VALUES (\"@nom\",\"@prenom\",\"@telfix\",\"@telport\",\"@adresse\",\"@dateNaissance\",\"@email\",\"@dateInsc\",\"@idCleanWay\",\"@contactMail\",\"@contactSms\")";
            //String sql = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms) VALUES (\"" + client.nom + "\",\"" + client.prenom + "\",\"" + client.telfix + "\",\"" + client.telmob + "\",\"" + client.adresse + "\",\"" + String.Format("{0:YYYYMMddHHmmss}", client.dateNaissance) + "\",\"" + client.email + "\",\"" + String.Format("{0:YYYYMMddHHmmss}", client.dateInscription) + "\",\"" + client.idCleanWay + "\",\"" + client.contactMail + "\",\"" + client.contactSms + "\")";
            
            //connection à la base de données   
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            //ajout des parametres
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = client.nom;
            cmd.Parameters.Add("@prenom", SqlDbType.VarChar).Value = client.prenom;
            cmd.Parameters.Add("@telfixe", SqlDbType.VarChar).Value = client.telfix;
            cmd.Parameters.Add("@telport", SqlDbType.VarChar).Value = client.telmob;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = client.adresse;
            cmd.Parameters.Add("@dateNaissance", SqlDbType.DateTime).Value = client.dateNaissance; //parametre date sous format annee + mois + jour + heure + minute + seconde
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = client.email;
            cmd.Parameters.Add("@dateInsc", SqlDbType.DateTime).Value = client.dateInscription; //parametre date sous format annee + mois + jour + heure + minute + seconde
            cmd.Parameters.Add("@idCleanWay", SqlDbType.Int).Value = client.idCleanWay;

            if (client.contactMail)     cmd.Parameters.Add("@contactMail", SqlDbType.Int).Value = 1;
            else                        cmd.Parameters.Add("@contactMail", SqlDbType.Int).Value = 0;

            if (client.contactMail)     cmd.Parameters.Add("@contactSms", SqlDbType.Int).Value = 1;
            else                        cmd.Parameters.Add("@contactSms", SqlDbType.Int).Value = 0;

            
            int retour = cmd.ExecuteNonQuery();
            connection.Close();

            //Execute la commande
            try
            {
                return retour;
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return 0;
            }
        }


        public static List<Client> seekClients(String nom, String prenom, String tel)
        {


            List<Client> retour = new List<Client>();

            bool ifPreviousElementSearch = false; //variable servant a verifier si un élément de recheche a déja été inséré. Sert notament au AND de la requete.

            String sql = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms FROM client WHERE ";

            #region complete la requete en fonction de la recherche voulue
            if (nom != null)
            {
                //sql = String.Format("{0}{1}", sql, "clt_nom=\"@nom\"");
                sql = String.Format("{0}{1}", sql, "clt_nom=\"" + nom + "\"");
                ifPreviousElementSearch = true;
            }
            if (prenom != null)
            {
                if (ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, " AND ");
                sql = String.Format("{0}{1}", sql, "clt_nom=\"" + prenom + "\"");
                ifPreviousElementSearch = true;
            }
            if (tel != null)
            {
                if (ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, " AND ");
                sql = String.Format("{0}{1}", sql, "(clt_fix=\"" + tel + "\" OR clt_mob = \"" + tel + "\")");
                ifPreviousElementSearch = true;
            }
            if (!ifPreviousElementSearch) sql = String.Format("{0}{1}", sql, "1=1");
            //sql = String.Format("{0}{1}", sql, " ORDER BY clt_nom ASC;");
            #endregion

            //connection à la base de données   
            MySqlConnection connection = Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            //ajout des parametres
            //cmd.Parameters.AddWithValue("nom", nom);
            //cmd.Parameters.AddWithValue("prenom", prenom);
            //cmd.Parameters.AddWithValue("tel", tel);

            //Execute la commande
            //try
            //{
            MySqlDataReader msdr = cmd.ExecuteReader();
            Client client;
            while (msdr.Read())
            {
                client = new Client(msdr["clt_nom"].ToString(),
                    msdr["clt_prenom"].ToString(),
                    msdr["clt_fix"].ToString(),
                    msdr["clt_mob"].ToString(),
                    msdr["clt_adresse"].ToString(),
                    DateTime.Parse(msdr["clt_dateNaissance"].ToString()),
                    msdr["clt_email"].ToString(),
                    DateTime.Parse(msdr["clt_dateInscription"].ToString()),
                    Int32.Parse(msdr["clt_idCleanway"].ToString()));
                client.setContactMail(Int32.Parse(msdr["clt_contactmail"].ToString()));
                client.setContactSms(Int32.Parse(msdr["clt_contactsms"].ToString()));
                retour.Add(client);
            }
            msdr.Dispose();
            return retour;
            //}
            //catch (Exception Ex)
            //{
            //    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //    return null;
            //}


        }
    }
}
