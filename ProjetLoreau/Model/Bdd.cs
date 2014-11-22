using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class Bdd
    {
        private static MySqlConnection MSConnexion = new MySqlConnection("Server=localhost;Database=bddping1;Uid=root;Pwd=;");
        private static int ReturnCode = 0;

        public static MySqlConnection connexion()
        {
            try
            {
                MSConnexion.Open();
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            return MSConnexion;
        }

        public static void deconnexion()
        {
            MSConnexion.Close();
        }
    }
}
