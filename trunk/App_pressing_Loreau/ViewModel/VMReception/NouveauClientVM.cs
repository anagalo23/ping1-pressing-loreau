using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;


namespace App_pressing_Loreau.View
{
    class NouveauClientVM : ObservableObject, IPageViewModel
    {
        public String Name 
        {
            get { return "Nouveau Client"; }
        }
 
    }
}
