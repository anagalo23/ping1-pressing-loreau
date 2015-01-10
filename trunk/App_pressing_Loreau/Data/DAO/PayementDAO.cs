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
        public static void insertPaiement(Payement paiement, int id_commande)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertPaiement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("datePaiement", paiement.date);
                cmd.Parameters.AddWithValue("montant", paiement.montant);
                cmd.Parameters.AddWithValue("name", paiement.typePaiement);
                cmd.Parameters.AddWithValue("commande_id", id_commande);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
            }
        }
        
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
                        msdr["pai_name"].ToString());
                        retour.Add(payement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }

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
                        msdr["pai_name"].ToString());
                        retour.Add(payement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }
    }
}
