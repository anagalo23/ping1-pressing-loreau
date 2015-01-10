using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class TypePayementDAO
    {
        public static List<TypePayement> selectTypesPayement()
        {
            try
            {
                List<TypePayement> retour = new List<TypePayement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypesPayement, Bdd.MSConnexion);

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

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypePayementById, Bdd.MSConnexion);

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
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }

        public static List<TypePayement> selectTypePayementByName(String name)
        {
            try
            {
                List<TypePayement> retour = new List<TypePayement>();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypePayementByName, Bdd.MSConnexion);

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
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }
    }
}
