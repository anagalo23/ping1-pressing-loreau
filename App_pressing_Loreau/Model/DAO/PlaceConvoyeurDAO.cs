using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class PlaceConvoyeurDAO
    {
        public static void insertConvoyeur(PlaceConvoyeur convoyeur)
        {
            try
            {
                String sql = "INSERT INTO convoyeur(conv_emplacement) VALUES (?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("emplacement", convoyeur.emplacement);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un convoyeur dans la base de données."));
            }
        }

        public static List<PlaceConvoyeur> selectConvoyeurs()
        {
            try
            {
                List<PlaceConvoyeur> retour = new List<PlaceConvoyeur>();
                String sql = "SELECT conv_id, conv_emplacement FROM convoyeur";

                //connection à la base de données  
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                PlaceConvoyeur convoyeur;
                while (msdr.Read())
                {
                    convoyeur = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()));
                    retour.Add(convoyeur);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de convoyeurs dans la base de données."));
                return null;
            }
        }

        public static PlaceConvoyeur selectTypeById(int id)
        {
            try
            {
                PlaceConvoyeur retour = new PlaceConvoyeur();
                String sql = "SELECT conv_id, conv_emplacement FROM convoyeur WHERE T.typ_id=?";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()));
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un convoyeur dans la base de données."));
                return null;
            }
        }
    }
}
