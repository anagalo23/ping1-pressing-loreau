using App_pressing_Loreau.Data.DAO;
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
        private static String printerName = "EPSON TM-T20II";

         //"EPSON TM-T20II Receipt5";
        public Commande commande { get; set; }
        public static String pattern_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Resources\\PatternFile\\RecuPaiement";
        public static String copy_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10)+"Resources\\Temp\\RecuPaiement";

        
        

        Font verdana10Font;
        StreamReader reader;

        public RecuPaiement(Commande commande)
        {
            this.commande = commande;
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
                File.AppendAllText(copy_path + ".txt", "Déposée le : " + commande.date.ToString("dd/MM/yyyy") + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Total TTC : " + total_TTC + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Total payé : " + total_payee + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Commande : " + commande.listArticles.Count + " articles" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);

                //ajout des articles
                foreach (Article art in commande.listArticles)
                {
                    File.AppendAllText(copy_path + ".txt", "~ " + art.type.nom + Environment.NewLine);
                }

                //fin du ticket
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);

                PrintOff();
                System.IO.File.Delete(copy_path + ".txt");
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur imprimante");
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
