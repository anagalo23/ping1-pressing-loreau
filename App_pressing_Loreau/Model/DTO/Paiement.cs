using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Paiement
    {
        #region attributs
        public int id {get; set;}
        public DateTime date { get; set; }
        public float montant {get; set;}
        public TypePaiement typePayement { get; set; }
        #endregion

        #region classes
        public Paiement()
        {
        }

        public Paiement(DateTime date, float montant, TypePaiement typePayement)
        {
            id = 0;
            this.date = date;
            this.montant = montant;
            this.typePayement = typePayement;
        }
        public Paiement(int id, DateTime date, float montant, TypePaiement typePayement)
        {
            this.id = id;
            this.date = date;
            this.montant = montant;
            this.typePayement = typePayement;
        }
        #endregion
    }
}
