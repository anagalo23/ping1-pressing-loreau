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
        public static String pattern_path = "I:\\PING\\App_pressing_loreau\\App_pressing_Loreau\\Resources\\PatternFile\\LecturePattern";
        private Microsoft.Office.Interop.Excel.Application oXL;
        private Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet mWorkSheets;
        private static object misValue = System.Reflection.Missing.Value;

        #endregion

        #region methodes
        //type = 0 : Lescture X
        //type = 1 : lecture Z
        public LectureExcel(int type)
        {
            this.type = type;
            listUsedTypePaiement = PayementDAO.listSommePaiementToday();
            List<Commande> listCommandeRecuToday = CommandeDAO.listCommandeRecuToday(1);
            listUsedTypeArticle = new List<string>();
            reçutArticle = new List<int>();
            renduArticle = new List<int>();
            TypeArticlesRendu(ArticleDAO.selectArticleRenduByDate(1), listCommandeRecuToday);
            nbNewCommande = listCommandeRecuToday.Count;
            nbNewClient = ClientDAO.listClientAddToday(1).Count;
        }

        public void createLecture()
        {
            oXL = new Application();
            mWorkBook = oXL.Workbooks.Open(pattern_path + ".xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            mWorkSheets = (Worksheet)mWorkBook.Worksheets.get_Item(1);

            #region Inscription sur le fichier Excel
            int index = 7;

            //Titre
            if (type == 0)
                mWorkSheets.Cells[index, 1] = "Lecture X - " + DateTime.Now.ToString("dd/MM/yyyy");
            if (type == 1)
                mWorkSheets.Cells[index, 1] = "Lecture Z - " + DateTime.Now.ToString("dd/MM/yyyy");

            //Inscription des Types de payements
            index = index + 3;
            float total_payements = 0;
            foreach (Payement paie in listUsedTypePaiement)
            {
                mWorkSheets.Cells[index, 8] = paie.typePaiement;
                mWorkSheets.Cells[index, 12] = paie.montant;
                total_payements = total_payements + paie.montant;
                index++;
            }
            index = 22;
            mWorkSheets.Cells[index, 12] = total_payements;

            //Ajout des données des articles
            index = 26;
            int total_recu = 0;
            int total_rendu = 0;
            for (int i = 0; i < listUsedTypeArticle.Count; i++)
            {
                mWorkSheets.Cells[index, 1] = listUsedTypeArticle[i];
                mWorkSheets.Cells[index, 4] = reçutArticle[i];
                total_recu = total_recu + reçutArticle[i];
                mWorkSheets.Cells[index, 5] = renduArticle[i];
                total_rendu = total_rendu + renduArticle[i];
                index++;
            }

            index = index + 2;
            mWorkSheets.Cells[index, 1] = "total";
            mWorkSheets.Cells[index, 4] = total_recu;
            mWorkSheets.Cells[index, 5] = total_rendu;

            //nouvelle commande
            index = 26;
            mWorkSheets.Cells[index, 12] = nbNewCommande;
            mWorkSheets.Cells[index + 1, 12] = nbNewClient;
            #endregion
        }

        public void printLecture()
        {
            createLecture();
            /*
             *@param From : The number of the page at which to start printing. If this argument is omitted, printing starts at the beginning.
             *@param To : The number of the last page to print. If this argument is omitted, printing ends with the last page.
             *@param Copies : The number of copies to print. If this argument is omitted, one copy is printed.
             *@param Preview : True to have Microsoft Excel invoke print preview before printing the object. False (or omitted) to print the object immediately
             *@param ActivePrinter : Sets the name of the active printer.
             *@param PrintToFile : True to print to a file. If PrToFileName is not specified, Microsoft Excel prompts the user to enter the name of the output file.
             *@param Collate : True to collate multiple copies.
             *@param PrToFileName : If PrintToFile is set to True, this argument specifies the name of the file you want to print to.
             */
            mWorkSheets.PrintOut(1, 1, 1, false, misValue, false, false, misValue);

            //close files
            mWorkBook.Close(false, misValue, misValue);
            oXL.Quit();

            //release file
            releaseObject(mWorkSheets);
            releaseObject(mWorkBook);
            releaseObject(oXL);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public void TypeArticlesRendu(List<Article> articlesrendu, List<Commande> articlesrendurecu)
        {
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
