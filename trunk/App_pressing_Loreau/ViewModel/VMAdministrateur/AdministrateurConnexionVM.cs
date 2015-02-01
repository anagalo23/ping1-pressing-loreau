using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrateurConnexionVM : ObservableObject, IPageViewModel
    {

        public AdministrateurConnexionVM()
        {
            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._contentDetailCommande = null;

        }
        public String Name
        {
            get { return ""; }
        }




    }
}
