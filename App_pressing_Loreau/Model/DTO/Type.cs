using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class Type
    {
        #region attributs
        public int id {get; set;}
        public string nom { get; set; }
        #endregion

        #region classes
        public Type(string nom)
        {
            this.nom = nom;
        }
        #endregion

        public void test()
        {
            Bdd.connexion();

        }
    }
}
