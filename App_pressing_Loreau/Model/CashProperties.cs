using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class CashProperties
    {
        #region parametres Excel

        ////public static String pattern_path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Resources\\properties.xlsx";
        //public static String pattern_path ="D:\\matlab\\3ème année\\Ping1_2\\ProjetPing\\ProjetIngenieur\\App_pressing_Loreau\\Resources\\properties.xlsx";
        public static String pattern_path = "J:\\Resources\\properties.xlsx";
        private static Microsoft.Office.Interop.Excel.Application oXL;
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Worksheet mWorkSheets;
        private static object misValue = System.Reflection.Missing.Value;

        #endregion

        #region parametres preperties
        public static decimal fondCaisse { get; set; }
        
        #endregion

        #region methodes

        public static void openProperties()
        {
            //Ouvre le fichier excel
            oXL = new Microsoft.Office.Interop.Excel.Application();
            mWorkBook = oXL.Workbooks.Open(pattern_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            mWorkSheets = (Worksheet)mWorkBook.Worksheets.get_Item(1);
        }

        public static void selectProperties()
        {
            openProperties();

            //récupération du fond de caisse
            if (mWorkSheets.Cells[2, 3].Value != null)
                fondCaisse = (decimal)mWorkSheets.Cells[2, 3].Value;

            closeProperties();
        }
        
        public static void insertProperties(float fdc)
        {
            openProperties();
            //insertion du fond de caisse
            mWorkSheets.Cells[2, 3] = fdc;

            //save
            mWorkBook.SaveAs(pattern_path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //close
            closeProperties();
            selectProperties();
        }

        public static void closeProperties()
        {
                //close files
                mWorkBook.Close(false, misValue, misValue);
                oXL.Quit();

                //release file
                releaseObject(mWorkSheets);
                releaseObject(mWorkBook);
                releaseObject(oXL);

        }

        private static void releaseObject(object obj)
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
