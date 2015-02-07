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

        public Adresse()
        {
            complement = "";
            numero = "";
            rue = "";
            codePostal = "";
            ville = "";
        }

        public string giveAdresse()
        {
            return String.Format("{0}\\{1}\\{2}\\{3}\\{4}", numero, rue, codePostal, ville, complement);
        }

        public static Adresse Parse(string adresse)
        {
            Adresse retour = new Adresse();
            string[] parameters = adresse.Split('\\');
            try
            {
                retour.numero = parameters[0];
                retour.rue = parameters[1];
                retour.codePostal = parameters[2];
                retour.ville = parameters[3];
                retour.complement = parameters[4];
            }
            catch (Exception e)
            {
                retour.numero = "12";
                retour.rue = "salut";
                retour.codePostal = "76000";
                retour.ville = "salut";
                retour.complement = "salut";
            }


            return retour;
        }

        public Adresse ToLower()
        {
            complement = complement.ToLower();
            rue = rue.ToLower();
            ville = ville.ToLower();
            return this;
        }


        public override String ToString()
        {
            return numero+", "+rue+"\n"+codePostal+" "+ville;
        }
    }
}
