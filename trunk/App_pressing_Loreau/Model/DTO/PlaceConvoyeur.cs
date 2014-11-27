using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class PlaceConvoyeur
    {
        #region attributs
        public int id {get; set;}
        public int emplacement { get; set; }
        #endregion

        #region classes

        public PlaceConvoyeur() { }
        public PlaceConvoyeur(int emplacement)
        {
            id = 0;
            this.emplacement = emplacement;
        }
        public PlaceConvoyeur(int id, int emplacement)
        {
            this.id = id;
            this.emplacement = emplacement;
        }
        #endregion
    }
}
