using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model.DTO
{
    class Commentaire
    {
        public int id { get; set; }
        public string com { get; set; }

        public Commentaire()
        {
        }

        public Commentaire(int id, string com)
        {
            this.id = id;
            this.com = com;
        }

        public Commentaire(string com)
        {
            this.com = com;
        }
    }
}
