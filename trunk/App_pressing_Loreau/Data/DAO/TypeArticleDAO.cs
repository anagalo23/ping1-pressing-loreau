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
        //Inserer un type dans la base de données
        public static int insertType(TypeArticle type)
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
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }

        //Selectionner l'ensemble des types de la base de données
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
                while (msdr.Read())
                {
                    type = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["typ_nom"].ToString(),
                        float.Parse(msdr["typ_encombrement"].ToString()),
                        Int32.Parse(msdr["typ_TVA"].ToString()),
                        Int32.Parse(msdr["typ_HT"].ToString()),
                        new Departement(Int32.Parse(msdr["typ_dep_id"].ToString()), null));
                    retour.Add(type);
                }
                msdr.Dispose();

                #region ajout des départements

                foreach (TypeArticle typ in retour)
                    typ.departement = DepartementDAO.selectDepartementById(typ.departement.id);

                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }

        //Selectionner un types de la base de données à partir de son id
        public static TypeArticle selectTypesById(int type_id)
        {
            try
            {
                TypeArticle retour = new TypeArticle();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectTypesById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("typ_id", type_id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["typ_nom"].ToString(),
                        float.Parse(msdr["typ_encombrement"].ToString()),
                        Int32.Parse(msdr["typ_TVA"].ToString()),
                        Int32.Parse(msdr["typ_HT"].ToString()),
                        new Departement(Int32.Parse(msdr["typ_dep_id"].ToString()), null));
                }
                msdr.Dispose();

                #region ajout des départements
                    retour.departement = DepartementDAO.selectDepartementById(retour.departement.id);
                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de types dans la base de données."));
                return null;
            }
        }

        //Selectionner les types d'articles pour un département
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
                while (msdr.Read())
                {
                    type = new TypeArticle(
                        Int32.Parse(msdr["typ_id"].ToString()),
                        msdr["typ_nom"].ToString(),
                        float.Parse(msdr["typ_encombrement"].ToString()),
                        float.Parse(msdr["typ_TVA"].ToString()),
                        float.Parse(msdr["typ_HT"].ToString()),
                        new Departement(Int32.Parse(msdr["typ_dep_id"].ToString()), null));
                    retour.Add(type);
                }
                msdr.Dispose();

                #region ajout des départements

                foreach (TypeArticle typ in retour)
                    typ.departement = DepartementDAO.selectDepartementById(typ.departement.id);

                #endregion

                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un département dans la base de données."));
                return null;
            }
        }

        //Update un type d'article
        public static int updateType(TypeArticle type)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateType, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", type.id);
                cmd.Parameters.AddWithValue("nom", type.nom);
                cmd.Parameters.AddWithValue("encombrement", type.encombrement);
                cmd.Parameters.AddWithValue("tva", type.TVA);
                cmd.Parameters.AddWithValue("ht", type.HT);
                cmd.Parameters.AddWithValue("dep_id", type.departement.id);
                cmd.Parameters.AddWithValue("id", type.id);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }
    }
}
