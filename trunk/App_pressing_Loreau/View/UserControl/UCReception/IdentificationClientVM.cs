using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class IdentificationClientVM : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get { return "Reception"; }
        }


    }
}
