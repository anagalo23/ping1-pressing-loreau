using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class TypePayementPatternDAO
    {
        public static void open()
        {
            MySqlConnection connection = Bdd.connexion();
        }

        public static TypePayementPattern selectPayementPatternByName(String patternName)
        {
            
            try
            {
                TypePayementPattern retour = new TypePayementPattern();

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.selectPayementPatternByName, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("patternName", patternName);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new TypePayementPattern(
                        Int32.Parse(msdr["tppp_id"].ToString()),
                        msdr["tppp_nom"].ToString());
                        
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }
        }

        public static TypePayementPattern selectPayementPatternById(int pattern_id)
        {
            try
            {
                TypePayementPattern retour = new TypePayementPattern();

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.selectPayementPatternById, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("pattern_id", pattern_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new TypePayementPattern(
                        Int32.Parse(msdr["tppp_id"].ToString()),
                        msdr["tppp_nom"].ToString());

                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un article dans la base de données."));
                return null;
            }
        }

        public static List<TypePayementPattern> selectPayementsPatterns()
        {
            try
            {
                List<TypePayementPattern> retour = new List<TypePayementPattern>();

                //connection à la base de données  
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.selectPayementsPatterns, connection);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                TypePayementPattern typePayementPattern;
                while (msdr.Read())
                {
                    typePayementPattern = new TypePayementPattern(
                        Int32.Parse(msdr["tppp_id"].ToString()),
                        msdr["tppp_nom"].ToString());
                    retour.Add(typePayementPattern);
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
    }
}
