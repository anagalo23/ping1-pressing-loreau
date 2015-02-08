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

        #endregion

        #region methodes
        public LogExcel(String type, String message, String complement)
        {
            date = DateTime.Now;
            this.type = type;
            this.message = message;
            this.complement = complement;
        }
        #endregion
    }
}
