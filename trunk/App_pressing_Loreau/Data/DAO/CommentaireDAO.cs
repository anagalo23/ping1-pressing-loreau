using App_pressing_Loreau.Model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App_pressing_Loreau.Data.DAO
{
    class CommentaireDAO
    {
        /* Inserer un commentaire dans la base de données
         * @param commentaire : commentaire à insérer
         */
        public static int insertCommentaire(Commentaire commentaire)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.insertCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("photo", commentaire.com);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : insertCommentaire");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Selectionner l'ensemble des commentaires de la base de données
         */
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
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectCommentaire");
                Bdd.deconnexion();
                return null;
            }


        }


        /* Selectionner un commentaire de la base de données à partir de son id
         * @param id : id du commentaire à selectionner
         */
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
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : selectCommentaireById");
                Bdd.deconnexion();
                return null;
            }


        }


        /* Update un commentaire
         * @param commentaire : commentaire à update
         */
        public static int updateCommentaire(Commentaire commentaire)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.updateCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commentaire.id);
                cmd.Parameters.AddWithValue("nom", commentaire.com);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : updateCommentaire");
                Bdd.deconnexion();
                return 0;
            }
        }


        /* Delete un commentaire
         * @param commentaire : commentaire à delete
         */
        public static int deleteCommentaire(Commentaire commentaire)
        {
            try
            {
                int retour = 0;
                //connection à la base de données
                MySqlCommand cmd = new MySqlCommand(Bdd.deleteCommentaire, Bdd.connexion());

                //ajout des parametres
                cmd.Parameters.AddWithValue("id", commentaire.id);

                //Execute la commande
                retour = cmd.ExecuteNonQuery();
                Bdd.deconnexion();
                return retour;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERREUR BDD : deleteCommentaire");
                return 0;
            }
        }
    }
}
