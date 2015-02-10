using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App_pressing_Loreau.Data
{
    class Bdd
    {
        #region attributs
        public static MySqlConnection MSConnexion { get; set; }
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
                MessageBox.Show("Erreur dans la connexion à la base de données");
                return null;
            }

        }

        public static void deconnexion()
        {
            try { MSConnexion.Close(); }
            finally { MSConnexion = null; }

        }
        #endregion

        #region requetes
        /*
         * Liste de l'ensemble des requetes sql du logiciel
         * Triés par classes
         */

        //Article
        public static String insertArticle = "INSERT INTO article(art_photo, art_commentaire, art_rendu, art_TVA, art_TTC, art_conv_id, art_typ_id, art_cmd_id) VALUES (?,?,?,?,?,?,?,?)";
        public static String selectArticleById = "SELECT art_id, art_photo, art_commentaire, art_rendu, art_TVA, art_TTC, art_conv_id, art_typ_id, art_cmd_id, art_date_rendu FROM article WHERE art_id=?";
        public static String selectArticleByIdCmd = "SELECT art_id, art_photo, art_commentaire, art_rendu, art_TVA, art_TTC, art_conv_id, art_typ_id, art_cmd_id, art_date_rendu FROM article WHERE art_cmd_id=?";
        public static String updateArticle = "UPDATE article SET art_id=?,art_photo=?,art_commentaire=?,art_rendu=?,art_TVA=?,art_TTC=?,art_conv_id=?,art_cmd_id=?,art_typ_id=?, art_date_rendu=? WHERE art_id=?";
        public static String deleteArticle = "DELETE FROM article WHERE art_id=?";
        public static String lastArticle = "SELECT MAX(art_id) AS art_id FROM article";
        public static String selectArticleRenduByDate = "SELECT art_id, art_photo, art_commentaire, art_rendu, art_TVA, art_TTC, art_conv_id, art_typ_id, art_cmd_id FROM article WHERE art_date_rendu BETWEEN ? AND ?";
        public static String articlesInBlanchisserie = "SELECT COUNT(A.art_typ_id) AS nbBlanchisserie, D.dep_nom FROM article A, type T, departement D WHERE A.art_typ_id=T.typ_id AND T.typ_dep_id=dep_id AND D.dep_nom='Blanchisserie' AND A.art_rendu=0 GROUP BY D.dep_nom";
        public static String articlesNonRendu = "SELECT COUNT(art_typ_id) AS nbArticles FROM article WHERE art_rendu=0";
        public static String chemisesNonRendu = "SELECT COUNT(A.art_typ_id) AS nbArticles FROM article A, type T WHERE A.art_typ_id=T.typ_id AND T.typ_nom='Chemise' AND A.art_rendu=0";
        public static String couetteNonRendu = "SELECT COUNT(A.art_typ_id) AS nbArticles FROM article A, type T WHERE A.art_typ_id=T.typ_id AND T.typ_nom LIKE '%Couette%' AND A.art_rendu=0";

        //Client
        public static String insertClient = "INSERT INTO client(clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
        public static String seekClients = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_type=0";
        public static String selectProClient = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_type=1";
        public static String selectClientById = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_id=?";
        public static String updateClient = "UPDATE client SET clt_type=?,clt_nom=?,clt_prenom=?,clt_contactmail=?,clt_contactsms=?,clt_fix=?,clt_mob=?,clt_adresse=?,clt_dateNaissance=?,clt_email=?,clt_idCleanway=? WHERE clt_id=?";
        public static String deleteClient = "DELETE FROM client WHERE clt_id=?";
        public static String lastClient = "SELECT MAX(clt_id) AS clt_id FROM client";
        public static String listClientAddToday = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client  WHERE clt_dateInscription BETWEEN ? AND ?";
        public static String verificationNomEtPrenom = "SELECT clt_id, clt_nom, clt_prenom, clt_fix, clt_mob, clt_adresse, clt_dateNaissance, clt_email, clt_dateInscription, clt_idCleanway, clt_contactmail, clt_contactsms, clt_type FROM client WHERE clt_nom=? AND clt_prenom=?";
        public static String nbClientDepot = "SELECT COUNT(cmd_id) AS nbCommandes , cmd_clt_id FROM commande WHERE cmd_date BETWEEN ? AND ? GROUP BY cmd_clt_id";
        public static String nbClientRecup = "SELECT COUNT(C.cmd_id) AS nbCommandes, C.cmd_clt_id FROM (SELECT COUNT(art_id) AS nbArticles , art_cmd_id FROM article A WHERE art_date_rendu BETWEEN ? AND ? GROUP BY art_cmd_id) R, commande C WHERE R.art_cmd_id = C.cmd_id GROUP BY cmd_clt_id";

        //Commande
        public static String insertCommande = "INSERT INTO commande(cmd_payee, cmd_remise, cmd_clt_id) VALUES (?,?,?)";
        public static String selectCommandes = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande";
        public static String selectCommandeById = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande WHERE cmd_id=?";
        public static String selectCommandesByClient = "SELECT cmd_id, cmd_date, cmd_payee, cmd_clt_id, cmd_remise FROM commande WHERE cmd_clt_id=?";
        public static String updateCommande = "UPDATE commande SET cmd_id=?,cmd_date=?,cmd_payee=?,cmd_clt_id=?,cmd_remise=?, cmd_date_rendu=? WHERE cmd_id=?";
        public static String deleteCommande = "DELETE FROM commande WHERE cmd_id=?";
        public static String lastCommande = "SELECT MAX(cmd_id) AS cmd_id FROM commande";
        public static String totalTTCCommandeById = "SELECT SUM(art_TTC) AS total FROM article WHERE art_cmd_id=?";
        public static String totalPayedCommandeById = "SELECT SUM(pai_montant) AS total FROM paiement WHERE pai_cmd_id=?";
        public static String listCommandeRecuToday = "SELECT cmd_id, cmd_clt_id, cmd_date, cmd_payee, cmd_remise, cmd_date_rendu FROM commande WHERE cmd_date BETWEEN ? AND ?";
        public static String isPayedByCleanWay = "SELECT C.cmd_id, P.pai_type FROM commande C, paiement P WHERE C.cmd_id=P.pai_cmd_id AND P.pai_type='CleanWay' AND C.cmd_id=?";

        //Commentaire
        public static String insertCommentaire = "INSERT INTO commentaire(com_com) VALUES (?)";
        public static String selectCommentaire = "SELECT com_id, com_com FROM commentaire";
        public static String selectCommentaireById = "SELECT com_id, com_com FROM commentaire WHERE com_id=?";
        public static String updateCommentaire = "UPDATE commentaire SET com_id=?,com_com=? WHERE com_id=?";
        public static String deleteCommentaire = "DELETE FROM commentaire WHERE com_id=?";

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
        public static String insertPaiement = "INSERT INTO paiement (pai_montant, pai_type ,pai_cmd_id) VALUES (?,?,?)";
        public static String selectPayementByCommande = "SELECT pai_id, pai_date, pai_type, pai_montant, pai_cmd_id FROM paiement WHERE pai_cmd_id=?";
        public static String updatePaiement = "UPDATE paiement SET pai_id=?,pai_date=?,pai_montant=?,pai_type=?, pai_cmd_id=?,pai_tpp_id=? WHERE pai_id=?";
        public static String deletePaiement = "DELETE FROM paiement WHERE pai_id=?";
        public static String listSommePaiementToday = "SELECT SUM(pai_montant) AS pai_montant, pai_type FROM paiement WHERE pai_date between ? and ? GROUP BY pai_type";

        //PlaceConvoyeur
        public static String insertConvoyeur = "INSERT INTO convoyeur(conv_emplacement, conv_encombrement) VALUES (?,?)";
        public static String selectConvoyeurs = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur";
        public static String selectConvoyeurById = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur WHERE conv_id=?";
        public static String selectConvoyeursEmpty = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur WHERE conv_encombrement!=3";
        public static String selectConvoyeursNotEmpty = "SELECT conv_id, conv_emplacement, conv_encombrement FROM convoyeur WHERE conv_encombrement!=0";
        public static String updatePlaceConvoyeur = "UPDATE convoyeur SET conv_id=?,conv_emplacement=?,conv_encombrement=? WHERE conv_id=?";
        public static String deletePlaceConvoyeur = "DELETE FROM convoyeur WHERE conv_id=?";

        //TypeArticle
        public static String insertType = "INSERT INTO type(typ_nom, typ_encombrement, typ_TVA, typ_TTC, typ_dep_id) VALUES (?,?,?,?,?)";
        public static String selectTypes = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_TTC, typ_dep_id FROM type";
        public static String selectTypesById = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_TTC, typ_dep_id FROM type WHERE typ_id=?";
        public static String selectTypeByDepId = "SELECT typ_id, typ_nom, typ_encombrement, typ_TVA, typ_TTC, typ_dep_id FROM type WHERE typ_dep_id=?";
        public static String updateType = "UPDATE type SET typ_id=?,typ_nom=?,typ_encombrement=?,typ_TVA=?,typ_TTC=?,typ_dep_id=? WHERE typ_id=?";
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
