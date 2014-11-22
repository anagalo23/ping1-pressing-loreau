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
        //public static List<Commande> get

        public static int insertCommande(Commande commande)
        {
            MySqlConnection connection = Bdd.connexion();
            String sql = "INSERT INTO commande (cmd_date, cmd_payee) Values (\"@cmd_date\", \"@cmd_payee\")";

            //connection à la base de données   

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            // Le langage d'insertion en bdd est le sql
            cmd.CommandText = sql;
            //ajout des parametres
            cmd.Parameters.AddWithValue("@cmd_date", commande.date);
            cmd.Parameters.AddWithValue("@cmd_payee", commande.payee);

            int retour;


            try
            {
                retour = cmd.ExecuteNonQuery();
                connection.Close();
                return retour;
            }
            catch
            {
                return 0;
            }

        }

    }
}
