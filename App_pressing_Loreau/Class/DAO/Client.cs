using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreauApplication.Class.DAO
{
    class Client
    {
        #region attributs
        //public int id {get; set;}
        public string nom {get; set;}
        public string prenom { get; set; }
        public string fix { get; set; }
        public string mob { get; set; }
        public string adresse { get; set; }
        public DateTime dateNaissance { get; set; }
        public string email { get; set; }
        public DateTime dateInscription { get; set; }
        public int idCleanWay { get; set; }
        #endregion

        #region classes
        //Constructeur
        public Client(string nom, string prenom, string fix, string mob, string adresse, DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.fix = fix;
            this.mob = mob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
        }

        #endregion
        

    }
}
