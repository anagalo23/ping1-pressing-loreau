using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App_pressing_Loreau.Class.DTO
{
    class ClientPro
    {
        #region attributs
        public int id {get; set;}
        public string nomEnt { get; set; }
        public string fix { get; set; }
        public string mob { get; set; }
        public string adresse { get; set; }
        public string email { get; set; }
        public DateTime dateInscription { get; set; }
        #endregion

        #region classes
        public ClientPro(string nomEnt, string fix, string mob, string adresse, string email, DateTime dateInscription)
        {
            this.nomEnt = nomEnt;
            this.fix = fix;
            this.mob = mob;
            this.adresse = adresse;
            this.email = email;
            this.dateInscription = dateInscription;
        }
        #endregion
    }
}
