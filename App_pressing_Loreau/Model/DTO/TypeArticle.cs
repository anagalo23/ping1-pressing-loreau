using System;
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
        //public float HT { get; set; }
        public float TTC { get; set; }
        public Departement departement { get; set; }
        #endregion

        #region classes
        public TypeArticle() { }
        public TypeArticle(string nom, float encombrement, float TVA, float TTC, Departement departement)
        {
            id = 0;
            this.nom = nom;
            this.encombrement = encombrement;
            //0 : ne va pas dans le convoyeur
            //1 , 1.5 et 3 vont dans le convoyeur
            //10 : hors convoyeur
            this.TVA = TVA;
            //this.HT = HT;
            this.TTC = TTC;
            this.departement = departement;
        }

        public TypeArticle(int id, string nom, float encombrement, float TVA, float TTC, Departement departement)
        {
            this.id = id;
            this.nom = nom;
            this.encombrement = encombrement;
            this.TVA = TVA;
            this.TTC = TTC;
            this.departement = departement;
        }
        #endregion
    }
}
