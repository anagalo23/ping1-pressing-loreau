using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DAO
{
    class TypeArticleDAO
    {
        /*
        * Table de base de donnée fixe. Ajout uniquement par l'intermédiaire de la section Administration.
        */

        public static void insertType(TypeArticle type)
        {
            try
            {
                String sql = "INSERT INTO type(typ_nom, typ_encombrement, typ_TVA, typ_HT, typ_dep_id) VALUES (?,?,?,?,?)";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

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
        /*
       public static List<TypeArticle> selectTypes()
       {
           try
           {
               List<TypeArticle> retour = new List<TypeArticle>();
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

       // Modifier par Alexis
       //Dans notre application, on recuprere l'article par departement
       
       public static TypeArticle selectTypeById(int id)
       {
           try
           {
               TypeArticle retour = new TypeArticle();
               String sql = "SELECT T.typ_id, T.typ_nom, T.typ_encombrement, T.typ_TVA, T.typ_HT, T.typ_dep_id, D.dep_nom FROM type T, departement D WHERE T.typ_dep_id=D.dep_id AND T.typ_id=?";

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
                      Int32.Parse(msdr["typ_dep_id"].ToString()),
                      msdr["dep_nom"].ToString());
                   retour = new TypeArticle(
                       Int32.Parse(msdr["typ_id"].ToString()),
 
                       msdr["typ_nom"].ToString(),
                       float.Parse(msdr["typ_encombrement"].ToString()),
                       float.Parse(msdr["typ_TVA"].ToString()),
                       float.Parse(msdr["typ_HT"].ToString()),
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
       }
        */
        public static TypeArticle selectTypeByIdDep(int idDep)
        {
            try
            {
                TypeArticle retour = new TypeArticle();
                String sql = "SELECT T.typ_id, T.typ_nom, T.typ_encombrement, T.typ_TVA, T.typ_HT, T.typ_dep_id FROM type T WHERE  T.typ_dep_id=?";

                //connection à la base de données
                MySqlConnection connection = Bdd.connexion();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", idDep);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
               // Departement departement;
                while (msdr.Read())
                {
                   
                    retour = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["type_nom"].ToString(),
                        float.Parse(msdr["typ_encombrement"].ToString()),
                        float.Parse(msdr["typ_TVA"].ToString()),
                        float.Parse(msdr["typ_HT"].ToString()),
                        Int32.Parse(msdr["typ_dep_id"].ToString()));
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
        
        
        internal static TypeArticle getTypeObjectByName(string typeDelArticle)
        {
            String requete = "SELECT * FROM type WHERE type_nom = ?";
            throw new NotImplementedException();
        }    }
}
