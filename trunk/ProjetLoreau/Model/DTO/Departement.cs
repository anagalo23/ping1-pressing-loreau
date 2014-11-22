using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetLoreau.Model.DTO
{
    class Departement
    {
        #region attributs
        public int id {get; set;}
        public string nom { get; set; }
        #endregion

        #region classes
        public Departement(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }
        #endregion
    }
}
