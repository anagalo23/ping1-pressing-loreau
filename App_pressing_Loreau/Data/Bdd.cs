using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Data
{
    class Bdd
    {
        #region attributs
        public static MySqlConnection MSConnexion
        {
            get;
            set;
        }
        private static int ReturnCode = 0;
        #endregion

        #region Methodes
        public static MySqlConnection connexion()
        {
            try
            {
                if (MSConnexion == null)
                {
                    MSConnexion = new MySqlConnection("Server=localhost;Database=bddping1;Uid=root;Pwd=;");
                    MSConnexion.Open();
                }
                return MSConnexion;

               
            }
            catch (Exception Ex)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }

        }

        public static void connecter()
        {
             if (MSConnexion == null)
                {
                    MSConnexion = new MySqlConnection("Server=localhost;Database=bddping1;Uid=root;Pwd=;");
                 try
                 {
                     MSConnexion.Open();
                 }
                 catch (MySql.Data.MySqlClient.MySqlException e)
                 {

                 }
                    
                }

        }

        public static void deconnexion()
        {

            MSConnexion.Close();
            MSConnexion = null;
        }
        #endregion

        #region requetes
        /*
         * Liste de l'ensemble des requetes sql du logiciel
         * Triés par classes
         */

        //Article
        public static String insertArticle = "INSERT INTO article(art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id) VALUES (?,?,?,?,?,?,?)";
        public static String selectArticleById = "SELECT art_id, art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id FROM article WHERE art_id=?";
        public static String selectArticleByIdCmd = "SELECT art_id, art_photo, art_commentaire, art_rendu, art_TVA, art_HT, art_conv_id, art_typ_id FROM article WHERE art_conv_id=?";
        public static String updateArticle = "UPDATE article SET art_id=?,art_photo=?,art_commentaire=?,art_rendu=?,art_TVA=?,art_HT=?,art_conv_id=?,art_cmd_id=?,art_typ_id=? WHERE art_id=?";
        public static String deleteArticle = "DELETE FROM article WHERE art_id=?";

        //Client
        public static String insertClient = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
        public static String seekClients = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_type=0";
        public static String selectClientById = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_id=?";
        public static String updateClient = "UPDATE client SET clt_id=?,clt_type=?,clt_nom=?,clt_prenom=?,clt_contactmail=?,clt_contactsms=?,clt_fix=?,clt_mob=?,clt_adresse=?,clt_dateNaissance=?,clt_email=?,clt_idCleanway=? WHERE clt_id=?";
        public static String deleteClient = "DELETE FROM client WHERE clt_id=?";

        //Commande
        public static String insertCommande = "INSERT INTO commande(cmd_date, cmd_payee, cmd_remise, cmd_clt_id) VALUES (?,?,?,?)";
        public static String selectCommandes = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande";
        public static String selectCommandeById = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande WHERE cmd_id=?";
        public static String selectCommandesByClient = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande WHERE cmd_clt_id=?";
        public static String updateCommande = "UPDATE commande SET cmd_id=?,cmd_date=?,cmd_payee=?,cmd_clt_id=?,cmd_remise=? WHERE cmd_id=?";
        public static String deleteCommande = "DELETE FROM commande WHERE cmd_id=?";

        //Département
        public static String insertDepartement = "INSERT INTO departement(dep_nom) VALUES (?)";
        public static String selectDepartements = "SELECT dep_id, dep_nom FROM departement";
        public static String selectDepartementById = "SELECT dep_id, dep_nom FROM departement WHERE dep_id=?";
        public static String updateDepartement = "UPDATE departement SET dep_id=?,dep_nom=? WHERE dep_id=?";
        public static String deleteDepartement = "DELETE FROM departement WHERE dep_id=?";

        //Employee
        public static String insertEmploye = "INSERT INTO employe(emp_nom,emp_prenom) VALUES (?,?)";
        public static String selectEmployes = "SELECT emp_id, emp_nom, emp_prenom FROM employe";
        public static String selectEmployeById = "SELECT emp_id, emp_nom, emp_prenom FROM employe WHERE emp_id=?";
        public static String updateEmploye = "UPDATE employe SET emp_id=?,emp_nom=?,emp_prenom=? WHERE emp_id=?";
        public static String deleteEmploye = "DELETE FROM employe WHERE emp_id=?";

        //log
        public static String insertLog = "INSERT INTO Log(log_date, log_message, log_emp_id) VALUES (?,?,?)";

        //Payement
        public static String insertPaiement = "INSERT INTO paiement (pai_date, pai_montant, pai_name ,pai_cmd_id) VALUES (?,?,?,?)";
        public static String selectPayementByCommande = "SELECT pai_id, pai_date, pai_name, pai_montant FROM paiement WHERE pai_cmd_id=?";
        public static String updatePaiement = "UPDATE paiement SET pai_id=?,pai_date=?,pai_montant=?,pai_cmd_id=?,pai_tpp_id=? WHERE pai_id=?";
        public static String deletePaiement = "DELETE FROM paiement WHERE pai_id=?";

        //PlaceConvoyeur
        public static String insertConvoyeur = "INSERT INTO convoyeur(conv_emplacement, conv_encombrement) VALUES (?,?)";
        public static String selectConvoyeurs = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur";
        public static String selectConvoyeurById = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur WHERE conv_id=?";
        public static String selectConvoyeursEmpty = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur WHERE conv_emplacement=0";
        public static String updatePlaceConvoyeur = "UPDATE convoyeur SET conv_id=?,conv_emplacement=?,conv_encombrement=? WHERE conv_id=?";
        public static String deletePlaceConvoyeur = "DELETE FROM convoyeur WHERE conv_id=?";

        //TypeArticle
        public static String insertType = "INSERT INTO type(typ_nom, typ_encombrement, typ_TVA, typ_HT, typ_dep_id) VALUES (?,?,?,?,?)";
        public static String selectTypes = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_HT, typ_dep_id FROM type";
        public static String selectTypesById = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_HT, typ_dep_id FROM type WHERE typ_id=?";
        public static String selectTypeByDepId = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_HT, typ_dep_id FROM type WHERE typ_dep_id=?";
        public static String updateType = "UPDATE type SET typ_id=?,typ_nom=?,typ_encombrement=?,typ_TVA=?,typ_HT=?,typ_dep_id=? WHERE typ_id=?";
        public static String deleteType = "DELETE FROM type WHERE typ_id=?";

        //TypePayement
        public static String selectTypesPayement = "SELECT tpp_id, tpp_nom FROM typepaiement";
        public static String selectTypePayementById = "SELECT tpp_id, tpp_nom FROM typepaiement WHERE tpp_id=?";
        public static String selectTypePayementByName = "SELECT tpp_id, tpp_nom FROM typepaiement WHERE tpp_nom=?";
        public static String updateTypePaiement = "UPDATE typepaiement SET tpp_id=?,tpp_nom=? WHERE tpp_id=?";
        public static String deleteTypePaiement = "DELETE FROM typepaiement WHERE tpp_id=?";
        #endregion
    }
}
