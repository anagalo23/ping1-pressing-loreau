﻿using App_pressing_Loreau.Model.DTO;
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
        public static int insertClient(Client client)
        {
            MySqlConnection connection = Bdd.connexion();

            String sql = "INSERT INTO client(clt_nom, clt_prenom, clt_num_fix, clt_num_portable, clt_adresse, clt_date_naissance, clt_email, clt_date_inscription, clt_idCleanway, clt_contactmail, clt_contactsms) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            
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
            int retour = cmd.ExecuteNonQuery();
            connection.Close();

            //Execute la commande
            //try
            //{
                return retour;
            //}
            //catch (Exception Ex)
            //{
            //    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //    return 0;
            //}
        }


        public static List<Client> seekClients(String nom, String prenom, String tel)
        {


            List<Client> retour = new List<Client>();

            bool ifPreviousElementSearch = false; //variable servant a verifier si un élément de recheche a déja été inséré. Sert notament au AND de la requete.

            String sql = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms FROM client WHERE ";

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
            MySqlConnection connection = Bdd.connexion();
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