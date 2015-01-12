using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.ViewModel
{
    class DetailCommandeVM : ObservableObject, IPageViewModel
    {
        #region attributs
        private String _afficheDetailCommande;

        #endregion

        public String Name
        {
            get { return ""; }
        }

    }
}
