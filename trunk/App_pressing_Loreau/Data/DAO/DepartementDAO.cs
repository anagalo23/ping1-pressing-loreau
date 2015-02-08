using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.Data.DAO
{
    class DepartementDAO
    {
        //Inserer un département dans la base de données
        public static int insertDepartement(Departement departement)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertDepartement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", departement.nom);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
                Bdd.deconnexion();
                return 0;
            }
        }

        //Selectionner l'ensemble des départements de la base de données
        public static List<Departement> selectDepartements()
        {
            try
            {
                List<Departement> retour = new List<Departement>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectDepartements, Bdd.connexion());

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
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de département dans la base de données."));
                Bdd.deconnexion();
                return null;
            }
        }

        //Selectionner un département à partir d'un ID
        public static Departement selectDepartementById(int id)
        {
            try
            {
                Departement retour = new Departement();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectDepartementById, Bdd.connexion());

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
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                Bdd.deconnexion();
                return null;
            }


        }

        //Update d'un département
        public static int updateDepartement(Departement dep)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateDepartement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", dep.id);
                cmd.Parameters.AddWithValue("nom", dep.nom);
                cmd.Parameters.AddWithValue("id", dep.id);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
                Bdd.deconnexion();
                return 0;
            }
        }

        //Delete d'un département
        public static int deleteDepartement(Departement dep)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteDepartement, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", dep.id);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un departement dans la base de données."));
                Bdd.deconnexion();
                return 0;
            }
        }



    }
}
