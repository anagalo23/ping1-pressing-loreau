using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class Adresse
    {


        public string complement { get; set; }
        public string numero { get; set; }
        public string rue { get; set; }
        public string codePostal { get; set; }
        public string ville { get; set; }

        public string giveAdresse()
        {
            return String.Format("{0}/{1}/{2}/{3}/{4}", numero, rue, codePostal, ville, complement);
        }

        public static Adresse Parse(string adresse)
        {
            Adresse retour = new Adresse();
            string[] parameters = adresse.Split('\\');

            retour.numero = parameters[0];
            retour.rue = parameters[1];
            retour.codePostal = parameters[2];
            retour.ville = parameters[3];
            retour.complement = parameters[4];

            return retour;
        }

    }
}
