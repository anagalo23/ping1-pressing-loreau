﻿using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class LogDAO
    {
        public static int insertLog(Log log)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertLog, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("date", log.date);
                cmd.Parameters.AddWithValue("message", log.message);
                cmd.Parameters.AddWithValue("emp_id", log.employe.id);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
                return retour;
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
    }
}