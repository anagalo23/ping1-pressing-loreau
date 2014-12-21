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
        public List<TypePayement> typesPayements { get; set; }
        #endregion

        #region classes
        public Payement()
        {
        }

        public Payement(DateTime date, float montant, List<TypePayement> typesPayements)
        {
            id = 0;
            this.date = date;
            this.montant = montant;
            this.typesPayements = typesPayements;
        }
        public Payement(int id, DateTime date, float montant, List<TypePayement> typesPayements)
        {
            this.id = id;
            this.date = date;
            this.montant = montant;
            this.typesPayements = typesPayements;
        }
        #endregion
    }
}
