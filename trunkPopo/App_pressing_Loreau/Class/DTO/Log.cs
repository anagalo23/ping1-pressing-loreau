using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class Log
    {
        #region attributs
        public int id {get; set;}
        public DateTime date { get; set; }
        public String message { get; set; }
        public Employe employe { get; set; }
        #endregion

        #region classes
        public Log(DateTime date, String message)
        {
            this.date = date;
            this.message = message;
        }
        #endregion
    }
}
