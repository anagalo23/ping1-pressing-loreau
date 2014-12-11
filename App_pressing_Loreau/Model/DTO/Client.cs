using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{


    class Client
    {

        #region attributs
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string telfix { get; set; }
        public string telmob { get; set; }
        public Adresse adresse { get; set; }
        public DateTime dateNaissance { get; set; }
        public string email { get; set; }
        public DateTime dateInscription { get; set; }
        public int idCleanWay { get; set; }
        public bool contactMail { get; set; }
        public bool contactSms { get; set; }
        public int type { get; set; }
        public List<Commande> listCommandes { get; set; }


        #endregion

        #region classes
        //Constructeurs
        public Client()
        {
        }
        public Client(string nom, string prenom, string telfix, string telmob, Adresse adresse, DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay, bool contactMail, bool contactSms, int type)
        {
            id = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.telfix = telfix;
            this.telmob = telmob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
            this.contactMail = contactMail;
            this.contactSms = contactSms;
            this.type = type;
            listCommandes = new List<Commande>();
        }
        public Client(int id, string nom, string prenom, string telfix, string telmob, Adresse adresse, DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay, bool contactMail, bool contactSms, int type)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.telfix = telfix;
            this.telmob = telmob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
            this.contactMail = contactMail;
            this.contactSms = contactSms;
            this.type = type;
            listCommandes = new List<Commande>();
        }
        public Client(string nom, string prenom, string telfix, string telmob, Adresse adresse,  DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay, int contactMail, int contactSms, int type)
        {
            id = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.telfix = telfix;
            this.telmob = telmob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
            setContactMail(contactMail);
            setContactSms(contactSms);
            this.type = type;
            listCommandes = new List<Commande>();
        }
        public Client(int id, string nom, string prenom, string telfix, string telmob, Adresse adresse, DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay, int contactMail, int contactSms, int type)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.telfix = telfix;
            this.telmob = telmob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
            setContactMail(contactMail);
            setContactSms(contactSms);
            this.type = type;
            listCommandes = new List<Commande>();
        }
        //Méthodes de listes
        public void addCommande(Commande commande)
        {
            listCommandes.Add(commande);
        }

        public void setContactMail(int contactInt)
        {
            switch (contactInt)
            {
                case 0:
                    contactMail = false;
                    break;
                case 1:
                    contactMail = true;
                    break;
                default:
                    //EERRRRRRRRRRRRROOOOOOOOOORRRRRRRR
                    break;
            }
        }

        public void setContactSms(int contactInt)
        {
            switch (contactInt)
            {
                case 0:
                    contactSms = false;
                    break;
                case 1:
                    contactSms = true;
                    break;
                default:
                    //EERRRRRRRRRRRRROOOOOOOOOORRRRRRRR
                    break;
            }
        }
        #endregion


    }
}