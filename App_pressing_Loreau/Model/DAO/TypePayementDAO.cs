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
        public static List<TypePayement> selectTypePayement()
        {
            try
            {
                List<TypePayement> retour = new List<TypePayement>();
                String sql = "SELECT tpp_id, tpp_nom FROM typepaiement";

                //connection à la base de données  
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

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
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }


        }

        public static TypePayement selectTypePayementById(int id)
        {
            try
            {
                TypePayement retour = new TypePayement();
                String sql = "SELECT tpp_id, tpp_nom FROM typepaiement WHERE tpp_id=?";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                TypePayement typePayement;
                while (msdr.Read())
                {
                    typePayement = new TypePayement(
                        Int32.Parse(msdr["tpp_id"].ToString()),
                        msdr["tpp_nom"].ToString());
                    retour = typePayement;
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }
    }
}
