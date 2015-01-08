using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class TypePayementDAO
    {
        public static void open()
        {
            MySqlConnection connection = Bdd.connexion();
        }

        public static List<TypePayement> selectTypesPayement(MySqlConnection connection)
        {
            try
            {
                List<TypePayement> retour = new List<TypePayement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypesPayement, connection);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                TypePayement typePayement;
                while (msdr.Read())
                {
                    typePayement = new TypePayement(
                        Int32.Parse(msdr["tpp_id"].ToString()),
                        msdr["tpp_nom"].ToString());
                    retour.Add(typePayement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }


        }

        public static TypePayement selectTypePayementById(MySqlConnection connection, int id)
        {
            try
            {
                TypePayement retour = new TypePayement();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypePayementById, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new TypePayement(
                        Int32.Parse(msdr["tpp_id"].ToString()),
                        msdr["tpp_nom"].ToString());
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }

        public static List<TypePayement> selectTypePayementByName(MySqlConnection connection, String name)
        {
            try
            {
                List<TypePayement> retour = new List<TypePayement>();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypePayementByName, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("name", name);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                TypePayement typePayement;
                while (msdr.Read())
                {
                    typePayement = new TypePayement(
                        Int32.Parse(msdr["tpp_id"].ToString()),
                        msdr["tpp_nom"].ToString());

                    retour.Add(typePayement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(connection, new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }
    }
}
