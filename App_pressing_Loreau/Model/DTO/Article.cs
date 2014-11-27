using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Article
    {
        #region attributs
        public int id {get; set;}
        public string photo { get; set; }
        public string commentaire { get; set; }
        public bool ifRendu { get; set; }
        public float TVA { get; set; }
        public float HT { get; set; }
        public TypeArticle type { get; set; }
        public PlaceConvoyeur convoyeur { get; set; }
        #endregion

        #region classes

        public Article() { }
        public Article(string photo, string commentaire, bool ifRendu, float TVA, float HT, TypeArticle type, PlaceConvoyeur convoyeur)
        {
            id = 0;
            this.photo = photo;
            this.commentaire = commentaire;
            this.ifRendu = ifRendu;
            this.TVA = TVA;
            this.HT = HT;
            this.type = type;
            this.convoyeur = convoyeur;

        }
        public Article(int id, string photo, string commentaire, bool ifRendu, float TVA, float HT, TypeArticle type, PlaceConvoyeur convoyeur)
        {
            this.id = id;
            this.photo = photo;
            this.commentaire = commentaire;
            this.ifRendu = ifRendu;
            this.TVA = TVA;
            this.HT = HT;
            this.type = type;
            this.convoyeur = convoyeur;
         
        }
        #endregion
    }
}