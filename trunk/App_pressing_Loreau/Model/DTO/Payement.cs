using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Payement
    {
        #region attributs
        public int id {get; set;}
        public DateTime date { get; set; }
        public float montant {get; set;}
        public String typePaiement { get; set; }
        #endregion

        #region classes
        public Payement()
        {
        }

        public Payement(DateTime date, float montant, String typePaiement)
        {
            id = 0;
            this.date = date;
            this.montant = montant;
            this.typePaiement = typePaiement;
        }
        public Payement(int id, DateTime date, float montant, String typePaiement)
        {
            this.id = id;
            this.date = date;
            this.montant = montant;
            this.typePaiement = typePaiement;
        }
        #endregion
    }
}
