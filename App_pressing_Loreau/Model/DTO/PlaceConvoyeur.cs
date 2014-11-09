using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class PlaceConvoyeur
    {
        #region attributs
        public int id {get; set;}
        public int emplacement { get; set; }
        #endregion

        #region classes
        public PlaceConvoyeur(int emplacement)
        {
            this.emplacement = emplacement;
        }
        #endregion
    }
}
