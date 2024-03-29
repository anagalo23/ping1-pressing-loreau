﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using System.Windows;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.Data.DAO
{
    class ClientDAO
    {
        /*Inserer un client dans la base de données
         * @param client : client à insérer
         */
        public static int insertClient(Client client)
        {
            try
            {
                int retour = 0;

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertClient, Bdd.connexion());

                cmd.Parameters.AddWithValue("nom", client.nom);
                cmd.Parameters.AddWithValue("prenom", client.prenom);
                cmd.Parameters.AddWithValue("telfixe", client.telfix);
                cmd.Parameters.AddWithValue("telport", client.telmob);
                cmd.Parameters.AddWithValue("adresse", client.adresse.giveAdresse().Replace("//", "///"));
                cmd.Parameters.AddWithValue("dateNaissance", client.dateNaissance);
                cmd.Parameters.AddWithValue("email", client.email);
                cmd.Parameters.AddWithValue("idCleanWay", client.idCleanWay);
                cmd.Parameters.AddWithValue("contactMail", client.contactMail);
                cmd.Parameters.AddWithValue("contactSms", client.contactSms);
                cmd.Parameters.AddWithValue("type", client.type);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();

                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : insertClient");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Selectionner l'ensemble des clients de la base de données a partir du nom, du prenom et/ou du numéro de telephone
         * @param nom : partie du nom du client
         * @param prenom : partie du prenom du client
         * @param tel : telephone
         * @param idcleanway : idcleanway du client
         */
        public static List<Client> seekClients(String nom, String prenom, String tel, int idcleanway)
        {
            List<Client> retour = new List<Client>();
            try
            {
                String sql = Bdd.seekClients;

                #region complete la requete en fonction de la recherche voulue
                if (nom != null)
                {
                    sql += " AND clt_nom LIKE '%" + nom + "%'";
                }
                if (prenom != null)
                {
                    sql += " AND clt_prenom LIKE '%" + prenom + "%'";
                }
                if (tel != null)
                {
                    sql += " AND (clt_fix LIKE '%" + tel + "%' OR clt_mob LIKE '%" + tel + "%')";
                }
                if (idcleanway != 0)
                {
                    sql += " AND clt_idCleanway=" + idcleanway;
                }
                #endregion

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(sql, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();

                while (msdr.Read())
                {
                    Client client;
                    int clt_id = Int32.Parse(msdr["clt_id"].ToString());
                    string clt_nom = msdr["clt_nom"].ToString();
                    string clt_prenom = msdr["clt_prenom"].ToString();
                    string clt_fix = msdr["clt_fix"].ToString();
                    string clt_mob = msdr["clt_mob"].ToString();
                    Adresse clt_adresse = Adresse.Parse(msdr["clt_adresse"].ToString());
                    DateTime clt_dateNaissance = DateTime.Parse(msdr["clt_dateNaissance"].ToString());
                    string clt_email = msdr["clt_email"].ToString();
                    DateTime clt_dateInscription = DateTime.Parse(msdr["clt_dateInscription"].ToString());
                    int clt_idCleanway = Int32.Parse(msdr["clt_idCleanway"].ToString());
                    bool clt_contactmail = ((msdr["clt_contactmail"].ToString()).Equals("False")) ? false : true;
                    bool clt_contactsms = ((msdr["clt_contactsms"].ToString()).Equals("False")) ? false : true;
                    int clt_type = ((msdr["clt_type"].ToString()).Equals("False")) ? 0 : 1;

                    client = new Client(
                        clt_id,
                        clt_nom,
                        clt_prenom,
                        clt_fix,
                        clt_mob,
                        clt_adresse,
                        clt_dateNaissance,
                        clt_email,
                        clt_dateInscription,
                        clt_idCleanway,
                        clt_contactmail,
                        clt_contactsms,
                        clt_type
                        );
                    retour.Add(client);
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : seekClients");
                Bdd.deconnexion();
                return null;
            }

        }


        /* Selectionner l'ensemble des clients pro
         */
        public static List<Client> selectProClient()
        {
            try
            {
                List<Client> retour = new List<Client>();

                //paramètres utilisés
                Client client;
                bool contactmail = false;
                bool clt_contactsms;
                int clt_type;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectProClient, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    contactmail = ((msdr["clt_contactmail"].ToString()).Equals("False")) ? false : true;
                    clt_contactsms = ((msdr["clt_contactsms"].ToString()).Equals("False")) ? false : true;
                    clt_type = ((msdr["clt_type"].ToString()).Equals("False")) ? 0 : 1;

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
                        contactmail,
                        clt_contactsms,
                        clt_type);
                    retour.Add(client);
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectProClient");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Selectionner un client à l'aide de l'id
         * @param addCommande : true pour ajouter les commandes dans l'objet client
         * @param cmd_addPaiement : Completer les commandes avec les paiements
         * @param cmd_addArticles : Completer les commandes avec les articles
         */
        public static Client selectClientById(int client_id, Boolean addCommandes, Boolean cmd_addPaiement, Boolean cmd_addArticles)
        {
            Client retour = new Client();
            Client client = new Client();
            try
            {
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectClientById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("clt_id", client_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();

                msdr.Read();
                client.id = Int32.Parse(msdr["clt_id"].ToString());
                client.nom = msdr["clt_nom"].ToString();
                client.prenom = msdr["clt_prenom"].ToString();
                client.telfix = msdr["clt_fix"].ToString();
                client.telmob = msdr["clt_mob"].ToString();
                client.adresse = Adresse.Parse(msdr["clt_adresse"].ToString());
                client.dateNaissance = DateTime.Parse(msdr["clt_dateNaissance"].ToString());
                client.email = msdr["clt_email"].ToString();
                client.dateInscription = DateTime.Parse(msdr["clt_dateInscription"].ToString());
                client.idCleanWay = Int32.Parse(msdr["clt_idCleanway"].ToString());

                //client.contactMail = false;// bool.Parse(msdr["clt_contactmail"].ToString());
                //client.contactSms = false;//bool.Parse(msdr["clt_contactsms"].ToString());

                if (msdr["clt_contactmail"].ToString() == "False")
                {
                    client.contactMail = false;
                }
                else
                {
                    client.contactMail = true;
                }

                if (msdr["clt_contactsms"].ToString() == "False")
                {
                    client.contactSms = false;
                }
                else
                {
                    client.contactSms = true;
                }

                if (msdr["clt_type"].ToString() == "False")
                {
                    client.type = 0;
                }
                else
                {
                    client.type = 1;
                }

                msdr.Dispose();
                Bdd.deconnexion();

                #region ajout des commandes
                if (addCommandes)
                {
                    // Attention ! dernier parametre obligatoirement en false afin de ne pas boucler.
                    retour.listCommandes = CommandeDAO.selectCommandesByClient(retour.id, cmd_addPaiement, cmd_addArticles, false);
                }
                #endregion

                return client;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectClientById");
                Bdd.deconnexion();
                return null;
            }

        }

        /* Give the amount of today opened commands 
         * @Param plage date :
         * 1 : par jour
         * 2 : par semaine
         * 3 : par mois
         * 4 : par année
         */
        public static List<Client> listClientAddToday(int plageDate)
        {
            try
            {
                //paramètres
                List<Client> retour = new List<Client>();
                List<int> cltList = new List<int>();
                Client client;
                bool contactmail = false;
                bool clt_contactsms;
                int clt_type;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.listClientAddToday, Bdd.connexion());

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
                while (msdr.Read())
                {
                    contactmail = ((msdr["clt_contactmail"].ToString()).Equals("False")) ? false : true;
                    clt_contactsms = ((msdr["clt_contactsms"].ToString()).Equals("False")) ? false : true;
                    clt_type = ((msdr["clt_type"].ToString()).Equals("False")) ? 0 : 1;


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
                        contactmail,
                        clt_contactsms,
                        clt_type);
                    retour.Add(client);
                }
                msdr.Dispose();

                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : listClientAddToday");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Update un client
         * @param client : client à update
         */
        public static int updateClient(Client client)
        {
            try
            {
                int retour = 0;

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateClient, Bdd.connexion());

                //ajout des parametres
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
                cmd.Parameters.AddWithValue("idC", client.id);



                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : updateClient");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Delete un client
         * @param client : client à delete
         */
        public static int deleteClient(Client client)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteClient, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", client.id);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : deleteClient");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Last Client Inserted
         */
        public static Client lastClient()
        {
            try
            {
                int clt_id = 0;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.lastClient, Bdd.connexion());

                //Execute la commandekkke
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    clt_id = Int32.Parse(msdr["clt_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();

                //retourne un client sans commandes
                return ClientDAO.selectClientById(clt_id, false, false, false); ;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : lastClient");
                Bdd.deconnexion();
                return null;
            }
        }


        /* Vérifie si le client existe déja en BDD
         * @param nom : nom du client
         * @param prenom : prenom du client
         * @return bool
         *      false : le client n'existe pas en Bdd
         *      true : le client existe en Bdd
         */
        public static bool verificationNomEtPrenom(String nom, String prenom)
        {
            try
            {
                Client client = null;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.verificationNomEtPrenom, Bdd.connexion());

                //ajout des parametres

                cmd.Parameters.AddWithValue("nom", nom);
                cmd.Parameters.AddWithValue("prenom", prenom);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();

                while (msdr.Read())
                {
                    client = new Client();
                    client.id = Int32.Parse(msdr["clt_id"].ToString());
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return client != null;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : verificationNomEtPrenom");
                Bdd.deconnexion();
                return true;
            }
        }


        /* Nombre de clients ayants déposés un ou plusieurs articles selon la plage de dates
         * @Param plageDate :
         * 1 : par jour
         * 2 : par semaine
         * 3 : par mois
         * 4 : par année
         */
        public static int nbClientDepot(int plageDate)
        {
            try
            {
                Client client = null;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.nbClientDepot, Bdd.connexion());

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
                int totalClients=0;
                int nBCommandes;
                while (msdr.Read())
                {
                    client = new Client();
                    client.id = Int32.Parse(msdr["cmd_clt_id"].ToString());
                    nBCommandes = Int32.Parse(msdr["nbCommandes"].ToString());
                    if (client.id != 0 && nBCommandes != 0)
                    {
                        totalClients++;
                    }
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return totalClients;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : Impossible de déterminer le nombre de clients ayant déposé des articles");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Nombre de clients ayants repris un ou plusieurs articles selon la plage de dates
         * @Param plageDate :
         * 1 : par jour
         * 2 : par semaine
         * 3 : par mois
         * 4 : par année
         */
        public static int nbClientRecup(int plageDate)
        {
            try
            {
                Client client = null;

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.nbClientRecup, Bdd.connexion());

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
                int totalClients = 0;
                int nBCommandes;
                while (msdr.Read())
                {
                    client = new Client();
                    client.id = Int32.Parse(msdr["cmd_clt_id"].ToString());
                    nBCommandes = Int32.Parse(msdr["nbCommandes"].ToString());
                    if (client.id != 0 && nBCommandes != 0)
                    {
                        totalClients++;
                    }
                }
                msdr.Dispose();
                Bdd.deconnexion();
                return totalClients;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : Impossible de déterminer le nombre de clients ayant récupéré des articles");
                Bdd.deconnexion();
                return 0;
            }
        }
    }
}

