using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class LogDAO
    {
        public static int insertLog(Log log)
        {
            try
            {
                String sql = "INSERT INTO Log(log_date, log_message, log_emp_id) VALUES (?,?,?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", log.date);
                cmd.Parameters.AddWithValue("message", log.message);
                cmd.Parameters.AddWithValue("emp_id", log.employe.id);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
                connection.Close();
                return retour;
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
    }
}
