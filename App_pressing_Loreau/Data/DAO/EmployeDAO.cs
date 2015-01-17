using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.Data.DAO
{
    class EmployeDAO
    {
        //Inserer un département dans la base de données
        public static void insertEmploye(Employe employee)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertEmploye, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", employee.nom);
                cmd.Parameters.AddWithValue("prenom", employee.prenom);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
            }
        }

        //Selectionner l'ensemble des départements de la base de données
        public static List<Employe> selectEmployes()
        {
            try
            {
                List<Employe> retour = new List<Employe>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectEmployes, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Employe employee;
                while (msdr.Read())
                {
                    employee = new Employe(
                        Int32.Parse(msdr["emp_id"].ToString()),
                        msdr["emp_nom"].ToString(),
                        msdr["emp_prenom"].ToString());
                    retour.Add(employee);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                return null;
            }
        }

        //Selectionner un département à partir d'un ID
        public static Employe selectEmployeById(int id)
        {
            try
            {
                Employe retour = new Employe();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectEmployeById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new Employe(
                        Int32.Parse(msdr["emp_id"].ToString()),
                        msdr["emp_nom"].ToString(),
                        msdr["emp_prenom"].ToString());
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }


        }

        //Update d'un département
        public static int updateEmploye(Employe employee)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateEmploye, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", employee.id);
                cmd.Parameters.AddWithValue("nom", employee.nom);
                cmd.Parameters.AddWithValue("prenom", employee.prenom);
                cmd.Parameters.AddWithValue("id", employee.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
                return 0;
            }
        }

        //Delete d'un département
        public static int deleteEmploye(Employe employee)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteEmploye, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", employee.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
                return 0;
            }
        }

    }
}
