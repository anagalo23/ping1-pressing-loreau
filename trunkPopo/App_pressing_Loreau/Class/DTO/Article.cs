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
        public int id {get; set;}
        public string photo { get; set; }
        public string commentaire { get; set; }
        public Boolean ifRendu { get; set; }
       
        public int typ_id { get; set; }
        public int dep_id { get; set; }
        public int conv_id { get; set; }
        #endregion

        #region classes
        public Article(int idArticle, string photo, string commentaire, Boolean ifRendu, int typeid, int departementid, int placeConvoyeurid)
        {
            this.id = idArticle;
            this.photo = photo;
            this.commentaire = commentaire;
            this.ifRendu = ifRendu;
            this.typ_id = typ_id;
            this.dep_id = departementid;
            this.conv_id = placeConvoyeurid;
         
        }
        #endregion
    }
}