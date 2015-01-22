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
        private int _label_identCleint_idCleanway;
        private String _label_identCleint_Adresse;
        //public String ButtonClientContent { get; set; }
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

        public int Label_identCleint_idCleanway
        {
            get { return _label_identCleint_idCleanway; }
            set
            {
                if (value != _label_identCleint_idCleanway)
                {
                    _label_identCleint_idCleanway = value;
                    RaisePropertyChanged("Label_identCleint_idCleanway");
                }
            }
        }

        public String Label_identCleint_Adresse
        {
            get { return _label_identCleint_Adresse; }
            set
            {
                if (value != _label_identCleint_Adresse)
                {
                    _label_identCleint_Adresse = value;
                    RaisePropertyChanged("Label_identCleint_Adresse");
                }
            }
        }
    }
}
