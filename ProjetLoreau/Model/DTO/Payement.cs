﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetLoreau.Model.DTO
{
    class Payement
    {
        #region attributs
        public int id {get; set;}
        public DateTime date { get; set; }
        public float montant {get; set;}
        #endregion

        #region classes
        public Payement(DateTime date, float montant)
        {
            this.date = date;
            this.montant = montant;
        }
        #endregion
    }
}
