using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class PayementDAO
    {
        public static void insertPaiement(Payement paiement, int id_commande)
        {
            try
            {
                String sql = "INSERT INTO paiement (pai_date, pai_montant, pai_cmd_id) VALUES (?,?,?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("datePaiement", paiement.date);
                cmd.Parameters.AddWithValue("montant", paiement.montant);
                cmd.Parameters.AddWithValue("commande_id", id_commande);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
            }
        }
        /*
        public static List<Payement> selectPayement(Commande commande)
        {
            try
            {
                List<Payement> retour = new List<Payement>();
                String sql = "SELECT T.typ_id, T.type_nom, T.type_encombrement, T.type_TVA, T.type_HT, T.type_dep_id, D.dep_nom FROM type T, departement D WHERE T.type_dep_id=D.dep_id";

                //connection à la base de données  
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                TypeArticle type;
                Departement departement;
                while (msdr.Read())
                {
                    departement = new Departement(
                        Int32.Parse(msdr["type_dep_id"].ToString()),
                        msdr["dep_nom"].ToString());
                    type = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["type_nom"].ToString(),
                        float.Parse(msdr["type_encombrement"].ToString()),
                        Int32.Parse(msdr["type_TVA"].ToString()),
                        Int32.Parse(msdr["type_HT"].ToString()),
                        departement);
                    retour.Add(type);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }
       
        public static TypeArticle selectTypeById(int id)
        {
            try
            {
                TypeArticle retour = new TypeArticle();
                String sql = "SELECT T.typ_id, T.type_nom, T.type_encombrement, T.type_TVA, T.type_HT, T.type_dep_id, D.dep_nom FROM type T, departement D WHERE T.type_dep_id=D.dep_id AND T.typ_id=?";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Departement departement;
                while (msdr.Read())
                {
                    departement = new Departement(
                        Int32.Parse(msdr["type_dep_id"].ToString()),
                        msdr["dep_nom"].ToString());
                    retour = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["type_nom"].ToString(),
                        float.Parse(msdr["type_encombrement"].ToString()),
                        Int32.Parse(msdr["type_TVA"].ToString()),
                        Int32.Parse(msdr["type_HT"].ToString()),
                        departement);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }
        } */
    }
}
