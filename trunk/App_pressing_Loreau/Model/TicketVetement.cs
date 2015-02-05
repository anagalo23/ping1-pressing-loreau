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
    class TicketVetement
    {

        private static String printerName = "";
        public Commande cmd { get; set; }
        public static String pattern_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Resources\\PatternFile\\TicketVetement";
        public static String copy_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10)+"Resources\\Temp\\TicketVetement";

        
        

        private static Font verdana10Font;
        private static StreamReader reader;

        public TicketVetement(Commande cmdWithClientArticles)
        {
            cmd = cmdWithClientArticles;
        }

        public void printAllArticleCmd()
        {
            foreach(Article art in cmd.listArticles)
            {
                printRecu(art, cmd.id, cmd.client);
            }
        }
        public void printRecu(Article art, int cmd_id, Client clt)
        {
            try
            {
                //creation du fichier temporaire dans resource/temp
                if (System.IO.File.Exists(copy_path + ".txt"))
                    System.IO.File.Delete(copy_path + ".txt");
                System.IO.File.Copy(pattern_path + ".txt", copy_path + ".txt");

                //Ajout du contenue du ticket
                File.AppendAllText(copy_path + ".txt", clt.nom + " " + clt.prenom + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", DateTime.Now.ToString() + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Commande " + cmd_id + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "N° article : " + art.id + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "_________________________" + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", "Département " + art.type.departement.nom + Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", Environment.NewLine);
                File.AppendAllText(copy_path + ".txt", " ~" + art.type.nom +Environment.NewLine);

                PrintOff();
                System.IO.File.Delete(copy_path + ".txt");
            }
            catch (Exception e)
            {

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
