using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App_pressing_Loreau.Model
{
    class RecuPaiement
    {

        private static String printerName;
        //= "EPSON TM-U220 Receipt";

        //"EPSON TM-T20II Receipt5";
        public Commande commande { get; set; }
        public static String printName = "TM-T20";
        //public static String pattern_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Resources\\PatternFile\\RecuPaiement";
        //public static String copy_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10)+"Resources\\Temp\\RecuPaiement";

        public static String pattern_path = "D:\\Application_Pressing\\Resources\\PatternFile\\RecuPaiement";
        public static String copy_path = "D:\\Application_Pressing\\Resources\\Temp\\RecuPaiement";




        Font verdana10Font;
        StreamReader reader;

        public RecuPaiement(Commande commande)
        {
            this.commande = commande;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                if (PrinterSettings.InstalledPrinters[i].Contains(printName))
                {
                    printerName = PrinterSettings.InstalledPrinters[i];
                }

            }

            if (printerName == null)
            {
                printerName = "";
                MessageBox.Show("Imprimante non trouvée");
            }
        }

        public void printRecu()
        {
            try
            {
                //creation du fichier temporaire dans resource/temp
                if (System.IO.File.Exists(copy_path + ".txt"))
                    System.IO.File.Delete(copy_path + ".txt");
                System.IO.File.Copy(pattern_path + ".txt", copy_path + ".txt");

                //calcul des totals
                float total_TTC = 0;
                float total_payee = 0;
                foreach (Article art in commande.listArticles)
                {
                    total_TTC = total_TTC + art.TTC;
                }
                foreach (Payement paie in commande.listPayements)
                {
                    total_payee = total_payee + paie.montant;
                }

                //Ajout du contenue du ticket
                File.AppendAllText(copy_path + ".txt", commande.client.nom + " " + commande.client.prenom + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);

                File.AppendAllText(copy_path + ".txt", DateTime.Now.ToString() + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "N° de commande : " + commande.id + Environment.NewLine);

                DateTime dateRendu;
                double tempsTraitement = 3;
                foreach (Article art in commande.listArticles)
                {
                    if (art.type.departement.id == 14)
                        tempsTraitement = 7;
                }

                File.AppendAllText(copy_path + ".txt", "Remise prévue le " + commande.date.AddDays(tempsTraitement).ToString("dd/MM/yyyy") + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Employé : " + ClasseGlobale.employeeEnCours.nom + " " + ClasseGlobale.employeeEnCours.prenom + Environment.NewLine);
                if (commande.payee)
                    File.AppendAllText(copy_path + ".txt", "Commande payée" + Environment.NewLine);
                else
                    File.AppendAllText(copy_path + ".txt", "NON PAYEE" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Commande : " + commande.listArticles.Count + " articles" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);

                //ajout des articles
                decimal totalTVA = 0;
                decimal totalTTC = 0;
                decimal totalHT = 0;

                foreach (Article arti in commande.listArticles)
                {
                    //ajout du nom de l'article avec son nombre d'espaces
                    int nbespace = 22;

                    if (arti.ifRendu)
                        File.AppendAllText(copy_path + ".txt", "r " + arti.type.nom);
                    else
                        File.AppendAllText(copy_path + ".txt", "~ " + arti.type.nom);


                    for (int i = 0; i < (nbespace - arti.type.nom.Length); i++)
                        File.AppendAllText(copy_path + ".txt", "-");
                    File.AppendAllText(copy_path + ".txt", (decimal)arti.TTC + "€ ---- ");
                    //dans ou hors convoyeur
                    if (arti.convoyeur != null && arti.convoyeur.emplacement != 0)
                        File.AppendAllText(copy_path + ".txt", arti.convoyeur.emplacement + Environment.NewLine);
                    else
                        File.AppendAllText(copy_path + ".txt", "HC" + Environment.NewLine);

                    //si commentaire
                    if (arti.commentaire != null)
                        if (!arti.commentaire.Equals(""))
                            File.AppendAllText(copy_path + ".txt", "     " + arti.commentaire + Environment.NewLine);

                    totalTTC = totalTTC + (decimal)arti.TTC;
                    totalHT = totalHT + (decimal)(arti.TTC / (1 + arti.TVA / 100));
                }
                //ajout des totals
                totalTVA = totalTTC - totalHT;
                File.AppendAllText(copy_path + ".txt", "           ______________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "TTC                    " + (decimal)totalTTC + "€" + Environment.NewLine);
                if (commande.remise != 0)
                    File.AppendAllText(copy_path + ".txt", "Remise                 " + (decimal)commande.remise + "€" + Environment.NewLine);

                decimal TVARemise = (decimal)commande.remise * (decimal)(TypeArticleDAO.selectTypesById(2).TVA / 100);

                File.AppendAllText(copy_path + ".txt", "Total TTC              " + ((decimal)totalTTC - (decimal)commande.remise) + "€" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Dont TVA               " + (decimal)((decimal)totalTVA - TVARemise) + "€" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Paiements par :          " + Environment.NewLine);

                //ajout des paiements
                foreach (Payement paie in commande.listPayements)
                {
                    int nbespace = 18;
                    File.AppendAllText(copy_path + ".txt", "    " + paie.typePaiement);
                    for (int i = 0; i < (nbespace - paie.typePaiement.Length); i++)
                        File.AppendAllText(copy_path + ".txt", "-");
                    File.AppendAllText(copy_path + ".txt", (decimal)paie.montant + "€" + Environment.NewLine);
                }
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);

                PrintOff();
                System.IO.File.Delete(copy_path + ".txt");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Impr : " + e);
            }

        }

        //Print the document
        //source : http://www.c-sharpcorner.com/UploadFile/mahesh/printfile06062007133250PM/printfile.aspx
        public void PrintOff()
        {
            string filename = copy_path + ".txt";
            //Create a StreamReader object
            reader = new StreamReader(filename);
            //Create a Verdana font with size 10
            verdana10Font = new Font("Verdana", 10);
            //Create a PrintDocument object
            PrintDocument pd = new PrintDocument();
            //gestion des marges
            pd.OriginAtMargins = true;
            pd.DefaultPageSettings.Margins.Top = 0;
            pd.DefaultPageSettings.Margins.Left = 5;
            pd.DefaultPageSettings.Margins.Right = 0;
            pd.DefaultPageSettings.Margins.Bottom = 0;
            //Add PrintPage event handler
            pd.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
            //Call Print Method
            pd.PrinterSettings.PrinterName = printerName;
            pd.Print();
            //Close the reader
            if (reader != null)
                reader.Close();
        }

        private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
        {

            //Get the Graphics object
            Graphics g = ppeArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs
            float leftMargin = ppeArgs.MarginBounds.Left;
            float topMargin = ppeArgs.MarginBounds.Top;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            linesPerPage = ppeArgs.MarginBounds.Height /
            verdana10Font.GetHeight(g);
            //Now read lines one by one, using StreamReader
            while (count < linesPerPage &&
            ((line = reader.ReadLine()) != null))
            {
                //Calculate the starting position
                yPos = topMargin + (count *
                verdana10Font.GetHeight(g));
                //Draw text
                g.DrawString(line, verdana10Font, Brushes.Black,
                leftMargin, yPos, new StringFormat());
                //Move to next line
                count++;
            }

            //If PrintPageEventArgs has more pages to print
            if (line != null)
            {
                ppeArgs.HasMorePages = true;
            }
            else
            {
                ppeArgs.HasMorePages = false;
            }
        }
    }
}
