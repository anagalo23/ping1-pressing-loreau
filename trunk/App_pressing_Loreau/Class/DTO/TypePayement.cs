using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DTO
{
    class TypePayement
    {
                #region attributs
        //public int id {get; set;}
        public string nom {get; set;}
        #endregion

        #region classes
        public TypePayement(string nom)
        {
            this.nom = nom;
        }
        #endregion
    }
}
