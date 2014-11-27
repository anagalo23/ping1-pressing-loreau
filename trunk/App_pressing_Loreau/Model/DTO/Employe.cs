using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Employe
    {
        #region attributs
        public int id { get; set; }
        public string nom { get; set; }
        #endregion

        #region classes
        public Employe() { }
        public Employe(string nom)
        {
            this.nom = nom;
        }
        public Employe(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }
        #endregion
    }
}
