using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class TypePaiement
    {
        #region attributs
        public int id {get; set;}
        public string nom {get; set;}
        #endregion

        #region classes
        public TypePaiement()
        {
        }
        public TypePaiement(string nom)
        {
            id = 0;
            this.nom = nom;
        }
        public TypePaiement(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }
        #endregion
    }
}
