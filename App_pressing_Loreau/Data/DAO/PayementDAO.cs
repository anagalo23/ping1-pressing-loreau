using App_pressing_Loreau.Model;
using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class PayementDAO
    {
        //Inserer un payement dans la base de données
        public static int insertPaiement(Payement paiement)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertPaiement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("montant", paiement.montant);
                cmd.Parameters.AddWithValue("name", paiement.typePaiement);
                cmd.Parameters.AddWithValue("commande_id", paiement.fk_cmd_id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }

        //Selectionner l'ensemble des payements par commande de la base de données
        public static List<Payement> selectPayementByCommande(Commande commande)
        {
            try
            {
                List<Payement> retour = new List<Payement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectPayementByCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("idCommande", commande.id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Payement payement;
                while (msdr.Read())
                {
                    payement = new Payement(
                        Int32.Parse(msdr["pai_id"].ToString()),
                        DateTime.Parse(msdr["pai_date"].ToString()),
                        float.Parse(msdr["pai_montant"].ToString()),
                        msdr["pai_name"].ToString(),
                        Int32.Parse(msdr["pai_cmd_id"].ToString()));
                        retour.Add(payement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }

        //Selectionner l'ensemble des payements par commande de la base de données
        public static List<Payement> selectPayementByCommande(int id_commande)
        {
            try
            {
                List<Payement> retour = new List<Payement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectPayementByCommande, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("idCommande", id_commande);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Payement payement;
                while (msdr.Read())
                {
                    payement = new Payement(
                        Int32.Parse(msdr["pai_id"].ToString()),
                        DateTime.Parse(msdr["pai_date"].ToString()),
                        float.Parse(msdr["pai_montant"].ToString()),
                        msdr["pai_type"].ToString(),
                        Int32.Parse(msdr["pai_cmd_id"].ToString()));
                        retour.Add(payement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }

        //Update un paiement
        public static int updatePaiement(Payement paiement)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertPaiement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", paiement.id);
                cmd.Parameters.AddWithValue("date", paiement.date);
                cmd.Parameters.AddWithValue("montant", paiement.montant);
                cmd.Parameters.AddWithValue("type", paiement.typePaiement);
                cmd.Parameters.AddWithValue("pai_cmd_id", paiement.fk_cmd_id);
                cmd.Parameters.AddWithValue("id", paiement.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }

        //Delete un paiement
        public static int deletePaiement(Payement paiement, int commandeId)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deletePaiement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", paiement.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }

        //Liste des paiement effectués dans la journée, rassemblé par type
        public static List<Payement> listSommePaiementToday(int plageDate)
        {
            try
            {
                List<Payement> retour = new List<Payement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.listSommePaiementToday, Bdd.connexion());

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
                Payement payement;
                while (msdr.Read())
                {
                    payement = new Payement(DateTime.Now, float.Parse(msdr["pai_montant"].ToString()), msdr["pai_type"].ToString(), -1);
                    retour.Add(payement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }
    }
}
