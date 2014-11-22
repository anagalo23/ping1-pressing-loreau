using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProjetLoreau.Model.DTO;

namespace ProjetLoreau.Model.DAO
{
    class DepartementDAO
    {
        public static int insertDepartement(Departement dep)
        {
            MySqlConnection connection = Bdd.connexion();
            String sql = "INSERT INTO departement (dep_nom) Values (\"@dep_nom\")";

            //connection à la base de données   

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.CommandText = sql;
            //ajout des parametres
            cmd.Parameters.AddWithValue("@dep_nom", dep.nom);
            int retour = cmd.ExecuteNonQuery();
            connection.Close();

            try
            {
                return retour;
            }
            catch
            {
                return 0;
            }
            
        }

        public static List<Departement> getListeDepartement()
        {
            MySqlConnection connection = Bdd.connexion();
            List<Departement> listDep= new List<Departement>();

            String sql = " SELECT dep_id, dep_nom FROM departement";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.CommandText = sql;

            //Execute la commande

            try
            {
                MySqlDataReader msdr = cmd.ExecuteReader();
                Departement dep;
                while (msdr.Read())
                {
                    dep = new Departement (Int32.Parse(msdr["dep_id"].ToString()), msdr["dep_nom"].ToString());

                    listDep.Add(dep);
                }
                msdr.Dispose();

            }
            catch
            {

            }
            return listDep;
            
        }


        public static Departement getDepartementById(int id)
        {
            MySqlConnection connection = Bdd.connexion();

            String sql = " SELECT dep_id, dep_nom FROM departement WHERE dep_id =" + id;

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.CommandText = sql;

            //Execute la commande
            try
            {
                MySqlDataReader msdr = cmd.ExecuteReader();
                Departement dep;
                msdr.Read();
                dep= new Departement(Int32.Parse(msdr["dep_id"].ToString()), msdr["dep_nom"].ToString());
                msdr.Dispose();

                return dep;
            }
            catch
            {
                return null;
            }

           
        }

    }
}
