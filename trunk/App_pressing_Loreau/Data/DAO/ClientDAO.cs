using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using System.Windows;

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
                string dateNaissance=null;
                //ajout des parametres
                if (client.dateNaissance != null)
                {
                    string[] tab = (client.dateNaissance).Split('/');
                    dateNaissance = "";
                    for (int i = 2; i >= 0; i--)
                    {
                        dateNaissance += tab[i];
                        if (i != 0)
                        {
                            dateNaissance += "-";
                        }
                    }
                }
                
                
                //dateNaissance += " 00:00:00";
                //MessageBox.Show(dateNaissance);
                //try

                
                //DateTime.Parse(dateNaissance);
                //MessageBox.Show(DateTime.Parse(dateNaissance).ToString());
                //DateTime date = new DateTime();
               // DateTime myDate = DateTime.ParseExact(dateNaissance, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);

                //MessageBox.Show("TODO");

                //date.
                //dateNaissance = (client.dateNaissance != null) ? (new DateTime(0001, 01, 01)).ToString() : dateNaissance;//client.dateNaissance+" 00:00:00"
                
                DateTime myDate = Convert.ToDateTime(dateNaissance);

                cmd.Parameters.AddWithValue("nom", client.nom);
                cmd.Parameters.AddWithValue("prenom", client.prenom);
                cmd.Parameters.AddWithValue("telfixe", client.telfix);
                cmd.Parameters.AddWithValue("telport", client.telmob);
                cmd.Parameters.AddWithValue("adresse", client.adresse.giveAdresse().Replace("//", "///"));
                cmd.Parameters.AddWithValue("dateNaissance", myDate);
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
        public static List<Client> seekClients(String nom, String prenom, String tel, int idcleanway)
        {
            List<Client> retour = new List<Client>();
            try
            {
                //nom.ToLower();
                //List<Client> retour = new List<Client>();

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
                    sql = String.Format("{0}{1}{2}{3}", sql, " AND (clt_fix LIKE '%", tel, "%' OR clt_mob LIKE '%", tel, "%')");
                }
                if (idcleanway != 0)
                {
                    sql = String.Format("{0}{1}{2}", sql, " AND clt_idCleanway=", idcleanway);
                }
                #endregion

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(sql, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Client client;
                while (msdr.Read())
                {
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
                //return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                return null;
            }
            return retour;
        }

        //Selectionner l'ensemble des clients pro
        public static List<Client> selectProClient()
        {
            try
            {

                List<Client> retour = new List<Client>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(/*Bdd.selectProClient*/Bdd.insertArticle, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Client client;
                bool contactmail = false;
                bool clt_contactsms;
                int clt_type;

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
                return retour;
            }
            catch (Exception Ex)
            {
                //Commande cmd = new Commande(DateTime.Now, false, 3 / 2F, ClientDAO.lastClient());
                //CommandeDAO.insertCommande(cmd);
                //cmd = CommandeDAO.lastCommande();
                //Article article = new Article(null, null, false, 20, 6, TypeArticleDAO.selectTypesById(1), PlaceConvoyeurDAO.selectConvoyeurById(4), cmd.id);
                //ArticleDAO.insertArticle(article);
                //ArticleDAO.insertArticle(article);
                //ArticleDAO.insertArticle(article);
                //ArticleDAO.insertArticle(article);
                //Payement paiement = new Payement(DateTime.Now, 4 / 3F, TypePayementDAO.selectTypePayementById(1).nom, cmd.id);
                //PayementDAO.insertPaiement(paiement);

                //LectureExcel le = new LectureExcel(0);
                //le.printLecture();





                //LogExcel log = new LogExcel("bdfvhk", "djsfbhh", "jhfsd");
                //log.ajouterLog();
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
                cmd.Parameters.AddWithValue("clt_id", client_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();//Le msdr contient bien toutes les infos du client

                Client client = new Client();

                //client.id = Int32.Parse(msdr["id_client"].ToString());
                //int test = (int)msdr["id_client"];
                msdr.Read();
                client.id = Int32.Parse(msdr["clt_id"].ToString());
                client.nom = msdr["clt_nom"].ToString();
                client.prenom = msdr["clt_prenom"].ToString();
                client.telfix = msdr["clt_fix"].ToString();
                client.telmob = msdr["clt_mob"].ToString();
                client.adresse = Adresse.Parse(msdr["clt_adresse"].ToString());
                client.dateNaissance = msdr["clt_dateNaissance"].ToString();//DateTime.Parse(
                client.email = msdr["clt_email"].ToString();
                client.dateInscription = DateTime.Parse(msdr["clt_dateInscription"].ToString());
                client.idCleanWay = Int32.Parse(msdr["clt_idCleanway"].ToString());

                client.contactMail = false;// bool.Parse(msdr["clt_contactmail"].ToString());
                client.contactSms = false;//bool.Parse(msdr["clt_contactsms"].ToString());


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


                //client.type = Int32.Parse(msdr["clt_type"].ToString());
                if (msdr["clt_type"].ToString() == "False")
                {
                    client.type = 0;
                }
                else
                {
                    client.type = 1;
                }
                //MessageBox.Show();
                //while (msdr.Read())
                //{
                //    retour = new Client(
                //        clt_id,
                //        nom,
                //        prenom,
                //        fix,
                //        mob,
                //        adresse,
                //        dateNaissance,
                //        email,
                //        dateInscription,
                //        idCleanway,
                //        contactMail,
                //        contactSms,
                //        typeClient
                //        );
                //}
                msdr.Dispose();

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
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Impossible de selectionner une liste de clients dans la base de données."));
                MessageBox.Show(Ex.InnerException.ToString());
                return null;
            }
        }

        //Give the amount command open today
        /* @Param plage date :
            * 1 : par jour
            * 2 : par semaine
            * 3 : par mois
            * 4 : par année
         */
        public static List<Client> listClientAddToday(int plageDate)
        {
            try
            {
                List<Client> retour = new List<Client>();
                List<int> cltList = new List<int>();

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
                Client client;
                bool contactmail = false;
                bool clt_contactsms;
                int clt_type;

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
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
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

        //Last Client Inserted
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
                Client client = new Client();
                client = ClientDAO.selectClientById(clt_id, false, false, false);
                return client;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un client dans la base de données."));
                return null;
            }
        }
    }
}

