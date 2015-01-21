using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missing = System.Reflection.Missing;

namespace App_pressing_Loreau.Model
{
    class LogExcel
    {
        #region parametres LogExcel
        public DateTime date { get; set; }
        public String type { get; set; }
        public String message { get; set; }
        public String complement { get; set; }
        public static String pattern_path = "C:\\Users\\syncrase\\Documents\\Visual Studio 2013\\Projects\\loreau_project\\App_pressing_Loreau\\Resources\\PatternExcel\\LogPattern";
        private static Microsoft.Office.Interop.Excel.Application oXL;
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet;

        #endregion

        #region methodes
        public LogExcel(String type, String message, String complement)
        {
            date = DateTime.Now;
            this.type = type;
            this.message = message;
            this.complement = complement;
        }

        public void ajouterLog()
        {
            int index = 3;
            int logCount;

            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(pattern_path + ".xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            logCount = (int) xlWorkSheet.Cells[1, 2].Value;
            //mWSheet.Cells[1, index + logCount].value = String.Format("{0:d/M/yyyy HH:mm:ss}", date);
            mWSheet.Cells[2, index + logCount] = type+"";
            mWSheet.Cells[3, index + logCount] = message;
            mWSheet.Cells[4, index + logCount] = complement;
            mWSheet.Cells[1, index + logCount].value = logCount + 1;

            xlWorkBook.Close(true, misValue, misValue);

            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
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
