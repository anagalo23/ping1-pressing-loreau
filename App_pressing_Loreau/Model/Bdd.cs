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
        private static MySqlConnection MSConnexion;
        private static int ReturnCode = 0;

        public static MySqlConnection connexion()
        {
            try
            {

                if (MSConnexion == null)
                {
                    MSConnexion = new MySqlConnection("Server=localhost;Database=bddping1;Uid=root;Pwd=;");
                    MSConnexion.Open();
                }
                return MSConnexion;
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }

        }

        public static void deconnexion()
        {

            MSConnexion.Close();
            MSConnexion = null;
        }
    }
}
