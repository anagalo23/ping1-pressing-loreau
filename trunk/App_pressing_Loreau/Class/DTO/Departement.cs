using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class Departement
    {
        #region attributs
        //public int id {get; set;}
        public string nom { get; set; }
        #endregion

        #region classes
        public Departement(string nom)
        {
            this.nom = nom;
        }
        #endregion
    }
}
