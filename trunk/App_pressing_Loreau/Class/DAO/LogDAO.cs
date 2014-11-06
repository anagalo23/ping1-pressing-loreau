using App_pressing_Loreau.Class.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DAO
{
    class LogDAO
    {
        public static void insertLog(Log log)
        {
            Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand("SELECT BDD_gold, BDD_turrets, BDD_Life, BDD_NbWave, BDD_Score FROM game ORDER BY BDD_Score");
            cmd.Connection = Bdd.connexion();
            MySqlDataReader msdr;
            msdr = cmd.ExecuteReader();
            while (msdr.Read())
            {
                //LC.Add(new Game(MSDR.GetInt32(0), MSDR.GetInt32(1), MSDR.GetInt32(2), MSDR.GetInt32(3), MSDR.GetInt32(4)));
            }
            msdr.Dispose();
            //return LC;
            Bdd.deconnexion();
        }
    }
}
