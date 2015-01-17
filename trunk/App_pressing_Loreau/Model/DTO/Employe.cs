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
        public string prenom { get; set; }
        #endregion

        #region classes
        public Employe() { }
        public Employe(string nom, string prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
        }
        public Employe(int id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
        }
        #endregion
    }
}
