using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.Model.DAO
{
    class DepartementDAO
    {
        public static void open()
        {
            MySqlConnection connection = Bdd.connexion();
        }

        public static void insertDepartement(Departement departement)
        {
            try
            {
                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.insertDepartement, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", departement.nom);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
            }
        }

        public static List<Departement> selectDepartements()
        {
            try
            {
                List<Departement> retour = new List<Departement>();
                
                //connection à la base de données  
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.selectDepartements, connection);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Departement departement;
                while (msdr.Read())
                {
                    departement = new Departement(
                        Int32.Parse(msdr["dep_id"].ToString()),
                        msdr["dep_nom"].ToString());
                    retour.Add(departement);
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

        public static Departement selectDepartementById(int id)
        {
            try
            {
                Departement retour = new Departement();
                
                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(Bdd.selectDepartementById, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Departement departement;
                while (msdr.Read())
                {
                    departement = new Departement(
                        Int32.Parse(msdr["dep_id"].ToString()),
                        msdr["dep_nom"].ToString());
                    retour = departement;
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
