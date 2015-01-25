using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data.DAO
{
    class CommentaireDAO
    {
        //Inserer un commentaire dans la base de données
        public static int insertCommentaire(Commentaire commentaire)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("photo", commentaire.com);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un article dans la base de données."));
                return 0;
            }
        }

        //Selectionner l'ensemble des commentaires de la base de données
        public static List<Commentaire> selectCommentaire()
        {
            try
            {
                List<Commentaire> retour = new List<Commentaire>();

                //connection à la base de données  
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommentaire, Bdd.connexion());

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                Commentaire commentaire;
                while (msdr.Read())
                {
                    commentaire = new Commentaire(
                        Int32.Parse(msdr["com_id"].ToString()),
                        msdr["com_com"].ToString());
                    retour.Add(commentaire);
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

        //Selectionner un commentaire de la base de données à partir de son id
        public static Commentaire selectCommentaireById(int id)
        {
            try
            {
                Commentaire retour = new Commentaire();

                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.selectCommentaireById, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", id);

                //Execute la commande
                MySqlDataReader msdr = cmd.ExecuteReader();
                while (msdr.Read())
                {
                    retour = new Commentaire(
                        Int32.Parse(msdr["com_id"].ToString()),
                        msdr["com_com"].ToString());
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

        //Update un commentaire
        public static int updateCommentaire(Commentaire commentaire)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commentaire.id);
                cmd.Parameters.AddWithValue("nom", commentaire.com);

                //Execute la commande
                return cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //LogDAO.insertLog(new Log(DateTime.Now, "ERREUR BDD : Erreur dans l'insertion d'un type dans la base de données."));
                return 0;
            }
        }

        //Delete un commentaire
        public static int deleteCommentaire(Commentaire commentaire)
        {
            try
            {
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commentaire.id);

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
