using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DAO
{
    class Prix
    {
        #region attributs
        //public int id {get; set;}
        public decimal prix_TVA {get; set;}
        public decimal prix_HT { get; set; }
        #endregion

        #region classes
        public Prix(decimal prix_TVA, decimal prix_HT)
        {
            this.prix_TVA = prix_TVA;
            this.prix_TVA = prix_HT;
        }
        #endregion
    }
}
