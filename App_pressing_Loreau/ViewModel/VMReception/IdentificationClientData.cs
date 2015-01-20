using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientData : ObservableObject
    {
        #region Variables

        private String _label_idenClient_nom;
        private String _label_idenClient_prenom;

        public String ButtonClientContent { get; set; }
        public int ButtonClientTag { get; set; }
        #endregion


        public String Label_idenClient_nom
        {
            get { return _label_idenClient_nom; }
            set
            {
                if (value != _label_idenClient_nom)
                {
                    _label_idenClient_nom = value;
                    RaisePropertyChanged("Label_idenClient_nom");
                }
            }
        }

        public String Label_idenClient_prenom
        {
            get { return _label_idenClient_prenom; }
            set
            {
                if (value != _label_idenClient_prenom)
                {
                    _label_idenClient_prenom = value;
                    RaisePropertyChanged("Label_idenClient_prenom");
                }
            }
        }
    }
}
