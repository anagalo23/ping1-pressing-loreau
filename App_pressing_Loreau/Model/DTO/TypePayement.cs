﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class TypePayement
    {
        #region attributs
        public int id {get; set;}
        public string nom {get; set;}
        public TypePayementPattern typesPayementPattern {get; set;}

        #endregion

        #region classes
        public TypePayement()
        {
        }
        public TypePayement(string nom, TypePayementPattern typesPayementPattern)
        {
            id = 0;
            this.nom = nom;
            this.typesPayementPattern = typesPayementPattern;
        }
        public TypePayement(int id, string nom, TypePayementPattern typesPayementPattern)
        {
            this.id = id;
            this.nom = nom;
            this.typesPayementPattern = typesPayementPattern;
        }
        #endregion
    }
}
