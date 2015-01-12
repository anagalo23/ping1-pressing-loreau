using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class TypeArticleDAO
    {
        public static void insertType(TypeArticle type)
        {
            try
            {                
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertType, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("nom", type.nom);
                cmd.Parameters.AddWithValue("encombrement", type.encombrement);
                cmd.Parameters.AddWithValue("TVA", type.TVA);
                cmd.Parameters.AddWithValue("HT", type.HT);
                cmd.Parameters.AddWithValue("id_dep", type.departement.id);

                //Execute la commande
                int retour = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
            }
        }

        public static List<TypeArticle> selectTypes()
        {
            try
            {
                List<TypeArticle> retour = new List<TypeArticle>();
                
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypes, Bdd.connexion());

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

        public static List<TypeArticle> selectTypeByDepId(int dep_id)
        {
            try
            {
                List<TypeArticle> retour = new List<TypeArticle>();
                
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypeByDepId, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("typ_dep_id", dep_id);

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
                LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }
        }

    }
}
