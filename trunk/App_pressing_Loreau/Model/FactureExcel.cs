using App_pressing_Loreau.Model.DTO;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App_pressing_Loreau.Model
{
    class FactureExcel
    {
        #region parametres LogExcel

        public Commande commande;

        public static String printName = "Canon MG2400 series";

        //public static String pattern_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Resources\\PatternFile\\FacturePattern.xlsx";
        public static String pattern_path = "D:\\Application_Pressing\\Resources\\PatternFile\\FacturePattern.xlsx";
        private Microsoft.Office.Interop.Excel.Application oXL;
        private Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet mWorkSheets;
        private static object misValue = System.Reflection.Missing.Value;

        #endregion

        #region methodes

        public FactureExcel(Commande commandeWithClient)
        {
            commande = commandeWithClient;
        }

        public void createFacture()
        {
            try
            {
                //Ouvre le fichier excel
                oXL = new Microsoft.Office.Interop.Excel.Application();
                mWorkBook = oXL.Workbooks.Open(pattern_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                mWorkSheets = (Worksheet)mWorkBook.Worksheets.get_Item(1);

                //Ajout de la référence
                mWorkSheets.Cells[8, 1] = DateTime.Now.ToString("dd/MM/yyyy");
                mWorkSheets.Cells[9, 2] = commande.id;

                //Ajout des informations client
                mWorkSheets.Cells[12, 1] = String.Format("{0} {1}", commande.client.nom.ToUpper(), commande.client.prenom);
                mWorkSheets.Cells[13, 1] = String.Format("{0} {1}", commande.client.adresse.numero, commande.client.adresse.rue);
                mWorkSheets.Cells[14, 1] = String.Format("{0}", commande.client.adresse.complement);
                mWorkSheets.Cells[15, 1] = String.Format("{0} {1}", commande.client.adresse.codePostal, commande.client.adresse.ville.ToUpper());

                //ajout des articles
                int index = 19;
                decimal total = 0;
                decimal tva = 0;
                foreach (Article art in commande.listArticles)
                {
                    mWorkSheets.Cells[index, 2] = art.type.nom;
                    mWorkSheets.Cells[index, 9] = (decimal)art.TTC * (1 - (decimal)art.TVA / 100);
                    total = (decimal)total + (decimal)art.TTC * (1 - (decimal)art.TVA / 100);
                    tva = tva + (decimal)art.TTC * ((decimal)art.TVA / 100);

                    index++;
                }

                //ajout du total
                index += 2;
                mWorkSheets.Cells[index, 7] = "TVA :";
                mWorkSheets.Cells[index, 9] = tva;
                mWorkSheets.Cells[index + 1, 7] = "Remise :";
                mWorkSheets.Cells[index + 1, 9] = commande.remise;
                mWorkSheets.Cells[index + 2, 7] = "Total :";
                mWorkSheets.Cells[index + 2, 9] = total + tva - (decimal)commande.remise;
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible d'ouvrir le fichier FacturePattern.xlsx");
                try
                {
                    //close files
                    mWorkBook.Close(false, misValue, misValue);
                    oXL.Quit();

                    //release file
                    releaseObject(mWorkSheets);
                    releaseObject(mWorkBook);
                    releaseObject(oXL);
                }
                catch
                {

                }
            }


        }

        public void printFacture()
        {
            try
            {
                createFacture();
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
                mWorkSheets.PrintOut(1, 1, 1, false, printName, false, false, misValue);

                //close files
                mWorkBook.Close(false, misValue, misValue);
                oXL.Quit();

                //release file
                releaseObject(mWorkSheets);
                releaseObject(mWorkBook);
                releaseObject(oXL);
            }
            catch (Exception e)
            {
                try
                {
                    //close files
                    mWorkBook.Close(false, misValue, misValue);
                    oXL.Quit();

                    //release file
                    releaseObject(mWorkSheets);
                    releaseObject(mWorkBook);
                    releaseObject(oXL);
                }
                catch
                {

                }
            }

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

        #endregion
    }
}
