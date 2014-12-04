using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class IdentificationClientData : ObservableObject
    {
        #region Variables

        private String _txt_identificationClient_nom;
        #endregion


        #region Properties 

        public String txb_identificationClient_nom
        {
            get { return _txt_identificationClient_nom; }
            set
            {
                if (value != _txt_identificationClient_nom)
                {
                    _txt_identificationClient_nom = value;
                    OnPropertyChanged("txb_identificationClient_nom");
                }
            }
        }

        #endregion

     
    }
}
