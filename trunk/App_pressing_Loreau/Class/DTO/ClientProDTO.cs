using LoreauApplication.Class.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DTO
{
    class ClientProDTO
    {
        public static List<ClientPro> allClientPro(String nom, String prenom, String tel)
        {

            List<ClientPro> retour = new List<ClientPro>();

            String sql = "SELECT cltp_id, cltp_noment, cltp_fix, cltp_mob, cltp_adresse, cltp_email, cltp_dateinscription FROM clientpro ORDER BY cltp_noment ASC";
            
            //connection à la base de données   
            MySqlConnection connection = Bdd.connexion();
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            //Execute la commande
            try
            {
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour.Add(new ClientPro(msdr["clt_noment"].ToString(),
                        msdr["cltp_fix"].ToString(),
                        msdr["cltp_mob"].ToString(),
                        msdr["cltp_adresse"].ToString(),
                        msdr["cltp_email"].ToString(),
                        DateTime.Parse(msdr["cltp_dateinscription"].ToString())));
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }


        }
    }
}
