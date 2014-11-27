using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Convoyeur
    {
        #region attributs
        public int id {get; set;}
        public int emplacement { get; set; }
        #endregion

        #region classes

        public Convoyeur() { }
        public Convoyeur(int emplacement)
        {
            id = 0;
            this.emplacement = emplacement;
        }
        public Convoyeur(int id, int emplacement)
        {
            this.id = id;
            this.emplacement = emplacement;
        }
        #endregion
    }
}
