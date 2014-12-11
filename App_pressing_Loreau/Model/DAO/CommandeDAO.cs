using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;

namespace App_pressing_Loreau.Model.DAO
{
    class CommandeDAO
    {
        public static void insertCommande(Commande commande)
        {
            try
            {
                String sql = "INSERT INTO commande(cmd_date, cmd_payee, cmd_clt_id, cmd_remise) VALUES (?,?,?,?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", commande.date);
                int payee = (commande.payee) ? 1 : 0;
                cmd.Parameters.AddWithValue("payee", payee);
                cmd.Parameters.AddWithValue("clt_id", commande.client.id);
                cmd.Parameters.AddWithValue("remise", commande.remise);

                int retour = cmd.ExecuteNonQuery();
                retour = (retour = 0) ? 0 : lastId();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'une commande dans la base de données."));
            }
        }

        //Methode permettant de renvoyer l'id de la dernière commande
        private static int lastId()
        {
            String sql = "SELECT cmd_id FROM commande ORDER BY cmd_id DESC LIMIT 1;";
            int retour;

            //connection à la base de données
            MySqlConnection connection = Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            try
            {
                retour = cmd.ExecuteNonQuery();
                return retour;
            }
            catch
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la récupération de l'id de la dernière commande dans la base de données."));
            }
        }

    }
}
