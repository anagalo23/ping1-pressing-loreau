﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DAO
{
    class Commande
    {
        #region attributs
        //public int cltP_id;
        public DateTime date { get; set; }
        public bool payee { get; set; }
        public Client client { get; set; }
        public ClientPro clientpro { get; set; }
        public List<Tuple<Prix, Article>> listArticles { get; set; }
        #endregion

        #region classes
        public Commande(DateTime date, bool payee, Client client, ClientPro clientPro)
        {
            this.date = date;
            this.payee = payee;
            this.client = client;
            this.clientpro = clientPro;
        }
        #endregion
    }
}