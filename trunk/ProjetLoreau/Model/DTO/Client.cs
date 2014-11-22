using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{

   
    class  Client
    {
        #region attributs
        public int id {get; set;}
        public string nom { get; set; }
        public string prenom { get; set; }
        public string telfix { get; set; }
        public string telmob { get; set; }
        public string adresse { get; set; }
        public DateTime dateNaissance { get; set; }
        public string email { get; set; }
        public DateTime dateInscription { get; set; }
        public int idCleanWay { get; set; }
        public bool contactMail { get; set; }
        public bool contactSms { get; set; }

        
        #endregion

        #region classes
        //Constructeur
        public Client(string nom, string prenom, string fix, string mob, string adresse, DateTime dateNaissance, string email, DateTime dateInscription, int idCleanWay)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.telfix = fix;
            this.telmob = mob;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.dateInscription = dateInscription;
            this.idCleanWay = idCleanWay;
        }
        public Client()
        {

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