using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class Article
    {
        #region attributs
        //public int id {get; set;}
        public string photo { get; set; }
        public string commentaire { get; set; }
        public bool ifRendu { get; set; }
        public float tva { get; set; }
        public float ht { get; set; }
        public Type type { get; set; }
        public Departement departement { get; set; }
        public PlaceConvoyeur placeConvoyeur { get; set; }
        #endregion

        #region classes
        public Article(string photo, string commentaire, bool ifRendu, float tva, float ht, Type type, Departement departement, PlaceConvoyeur placeConvoyeur)
        {
            this.photo = photo;
            this.commentaire = commentaire;
            this.ifRendu = ifRendu;
            this.tva = tva;
            this.ht = ht;
            this.type = type;
            this.departement = departement;
            this.placeConvoyeur = placeConvoyeur;
        }
        #endregion
    }
}