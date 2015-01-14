using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class PlaceConvoyeurDAO
    {
        //Inserer une place convoyeur dans la base de données
        public static int insertConvoyeur(PlaceConvoyeur convoyeur)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertConvoyeur, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("emplacement", convoyeur.emplacement);
                cmd.Parameters.AddWithValue("encombrement", convoyeur.encombrement);

                //Execute la commande
               return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un convoyeur dans la base de données."));
                return 0;
            }
        }

        //Selectionner l'ensemble des places convoyeurs de la base de données
        public static List<PlaceConvoyeur> selectConvoyeurs()
        {
            try
            {
                List<PlaceConvoyeur> retour = new List<PlaceConvoyeur>();
                
                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectConvoyeurs, Bdd.connexion());
                
                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                PlaceConvoyeur convoyeur;
                while (msdr.Read())
                {
                    convoyeur = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()),
                        float.Parse(msdr["conv_encombrement"].ToString()));
                    retour.Add(convoyeur);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de convoyeurs dans la base de données."));
                return null;
            }
        }

        //Selectionner une place convoyeur de la base de données à partir de son id
        public static PlaceConvoyeur selectConvoyeurById(int id)
        {
            try
            {
                PlaceConvoyeur retour = new PlaceConvoyeur();
                
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectConvoyeurById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("conv_id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()),
                        float.Parse(msdr["conv_encombrement"].ToString()));
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'un convoyeur dans la base de données."));
                return null;
            }
        }

        //Selectionner l'ensemble des places convoyeurs de la base de données ou encombrement=0
        public static List<PlaceConvoyeur> selectConvoyeursEmpty()
        {
            try
            {
                List<PlaceConvoyeur> retour = new List<PlaceConvoyeur>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectConvoyeursEmpty, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                PlaceConvoyeur convoyeur;
                while (msdr.Read())
                {
                    convoyeur = new PlaceConvoyeur(
                        Int32.Parse(msdr["conv_id"].ToString()),
                        Int32.Parse(msdr["conv_emplacement"].ToString()),
                        float.Parse(msdr["conv_encombrement"].ToString()));
                    retour.Add(convoyeur);
                }
                msdr.Dispose();
                return retour;
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans la selection d'une liste de convoyeurs dans la base de données."));
                return null;
            }
        }

        //Update une place convoyeur
        public static int updatePlaceConvoyeur(PlaceConvoyeur conv)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateType, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", conv.id);
                cmd.Parameters.AddWithValue("emplacement", conv.emplacement);
                cmd.Parameters.AddWithValue("encombrement", conv.encombrement);
                cmd.Parameters.AddWithValue("id", conv.id);

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
