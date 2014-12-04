using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    class   ApplicationViewModel
    {
        public ApplicationViewModel()
        {

        }

        public Object identificationView
        {
            get
            {

                return new IdentificationClient(); 
            }
        }


        public NouveauClient nouveauClientView
        {
            get
            {
                return new NouveauClient();
            }
        }

       
    }
}
