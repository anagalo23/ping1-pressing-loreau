using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Commande
    {
        #region attributs
        public int id { get; set; }
        public DateTime date { get; set; }
        public Boolean payee { get; set; }
        public float remise { get; set; }
        public Client client { get; set; }
        public List<Article> listArticles { get; set; }
        public List<Payement> listPayements { get; set; }
        #endregion

        #region classes
        public Commande()
        {
            listArticles = new List<Article>();
            listPayements = new List<Payement>();
        }
        public Commande(DateTime date, Boolean payee, float remise, Client client)
        {
            id = 0;
            this.date = date;
            this.payee = payee;
            this.remise = remise;
            this.client = client;
            listArticles = new List<Article>();
            listPayements = new List<Payement>();
        }
        public Commande(int id, DateTime date, Boolean payee, float remise, Client client)
        {
            this.id = id;
            this.date = date;
            this.payee = payee;
            this.remise = remise;
            this.client = client;
            listArticles = new List<Article>();
            listPayements = new List<Payement>();
        }

        public void addArticle(Article article)
        {
            listArticles.Add(article);
        }

        public void addPayement(Payement payement)
        {
            listPayements.Add(payement);
        }
        #endregion
    }
}
