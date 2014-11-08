using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Class.DTO
{
    class Commande
    {
        #region attributs
        public int cmd_id { get; set; }
        public DateTime date { get; set; }
        public Boolean payee { get; set; }
        public Client client { get; set; }
        public ClientPro clientpro { get; set; }
        public List<Article> listArticles { get; set; }
        #endregion

        #region classes
        public Commande(int id_commande, DateTime date, Boolean payee, Client client, ClientPro clientPro)
        {
            this.cmd_id = id_commande;
            this.date = date;
            this.payee = payee;
            this.client = client;
            this.clientpro = clientPro;
        }
        #endregion
    }
}
