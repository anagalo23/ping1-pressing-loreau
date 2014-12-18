﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class TypeArticle
    {
        #region attributs
        public int id {get; set;}
        public string nom { get; set; }
        public float encombrement { get; set; }
        public float TVA { get; set; }
        public float HT { get; set; }
        public int typ_id_dep { get; set; }
        public Departement departement { get; set; }
        #endregion

        #region classes
        public TypeArticle() { }
        public TypeArticle(string nom, float encombrement, float TVA, float HT, Departement departement)
        {
            id = 0;
            this.nom = nom;
            this.encombrement = encombrement;
            this.TVA = TVA;
            this.HT = HT;
            this.departement = departement;
        }
    
        public TypeArticle(int id, string nom, float encombrement, float TVA, float HT, int dep_id)
        {
            this.id = id;
            this.nom = nom;
            this.encombrement = encombrement;
            this.TVA = TVA;
            this.HT = HT;
            this.typ_id_dep = dep_id;
        }
        #endregion
    }
}
