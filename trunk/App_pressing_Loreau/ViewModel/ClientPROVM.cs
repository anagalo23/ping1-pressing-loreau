using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class ClientPROVM : ObservableObject, IPageViewModel
    {
        public String Name
        {
            get { return ""; }
        }
    }
}
