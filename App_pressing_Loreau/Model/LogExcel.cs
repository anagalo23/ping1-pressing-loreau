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
        public static String pattern_path = "I:\\PING\\App_pressing_loreau\\App_pressing_Loreau\\Resources\\PatternExcel\\LogPattern.xlsx";
        private static Microsoft.Office.Interop.Excel.Application oXL;
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;

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
            int index = 2;
            int logCount;

            //open the file
            oXL = new Microsoft.Office.Interop.Excel.Application();
            mWorkBook = oXL.Workbooks.Open(pattern_path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            mWorkSheets = mWorkBook.Worksheets;
            mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Sheet1");
            logCount = mWSheet1.Cells[2, 1].Value;

            //add values
            mWSheet1.Cells[logCount + index, 1] = String.Format("{0:d/M/yyyy HH:mm:ss}", date);
            mWSheet1.Cells[logCount + index, 2] = type;
            mWSheet1.Cells[logCount + index, 3] = message;
            mWSheet1.Cells[logCount + index, 4] = complement;
            mWSheet1.Cells[2, 1] = logCount + 1;


            //Save and close the file
            mWorkBook.SaveAs(pattern_path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
        }

        #endregion
    }
}
