using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class LectureExcel
    {

        #region parametres
        //autres paramètres
        int type; //0 : LectureX // 1 : LectureZ

        //informations départements
        List<Departement> listUsedDepartements;
        List<float> caTTCDep;

        //informations moyen de paiement
        List<Payement> listUsedTypePaiement;

        //informations type article
        List<String> listUsedTypeArticle;
        List<int> reçutArticle;
        List<int> renduArticle;

        //autres paramètres statistiques
        int nbNewCommande;
        int nbNewClient;

        //Excel Interop
        public static String pattern_path = "I:\\PING\\App_pressing_loreau\\App_pressing_Loreau\\Resources\\PatternExcel\\LecturePattern";
        private Microsoft.Office.Interop.Excel.Application oXL;
        private Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet mWorkSheets;
        private static object misValue = System.Reflection.Missing.Value;

        #endregion

        #region methodes

        public LectureExcel(int type)
        {
            this.type = type;
            
            listUsedTypePaiement = PayementDAO.listSommePaiementToday();
            List<Commande> listCommandeRecuToday = CommandeDAO.listCommandeRecuToday(1);
            TypeArticlesRendu(ArticleDAO.selectArticleRenduByDate(1),listCommandeRecuToday);
            nbNewCommande = listCommandeRecuToday.Count;
            nbNewClient = ClientDAO.listClientAddToday(1).Count;
        }

        public void TypeArticlesRendu(List<Article> articlesrendu, List<Commande> articlesrendurecu)
        {
            IDictionary<string, float> retour;
            Boolean ifExist;

            foreach (Article art in articlesrendu)
            {
                ifExist = false;
                //Jusque là on a déroulé tout les articles de toute les commandes
                //recherche d'articles déja entrés
                for (int i = 0; i < listUsedTypeArticle.Count; i++)
                {
                    if (listUsedTypeArticle[i].Contains(art.type.nom))
                    {
                        renduArticle[i]++;
                        ifExist = true;
                        break;
                    }
                }

                if (!ifExist)
                {
                    listUsedTypeArticle.Add(art.type.nom);
                    renduArticle.Add(1);
                    reçutArticle.Add(0);
                }
            }
            foreach (Commande cmd in articlesrendurecu)
                foreach (Article art in cmd.listArticles)
                {
                    ifExist = false;
                    //Jusque là on a déroulé tout les articles de toute les commandes
                    //recherche d'articles déja entrés
                    for (int i = 0; i < listUsedTypeArticle.Count; i++)
                    {
                        if (listUsedTypeArticle[i].Contains(art.type.nom))
                        {
                            reçutArticle[i]++;
                            ifExist = true;
                            break;
                        }
                    }

                    if (!ifExist)
                    {
                        listUsedTypeArticle.Add(art.type.nom);
                        renduArticle.Add(0);
                        reçutArticle.Add(1);
                    }
                }
        }
        #endregion
    }
}
