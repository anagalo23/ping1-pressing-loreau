using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientData : ObservableObject
    {
        #region Variables

        private String _label_identCleint_Adresse;
        public int ButtonClientTag { get; set; }

        public Client clt;

        #endregion

        #region Properties 
       
        public String Label_idenClient_nom
        {
            get { return this.clt.nom; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.clt.nom = value;
                    OnPropertyChanged("Label_idenClient_nom");
                }
            }
        }

        public String Label_idenClient_prenom
        {
            get { return this.clt.prenom; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.clt.prenom = value;
                    OnPropertyChanged("Label_idenClient_prenom");
                }
            }
        }

        public int Label_identCleint_idCleanway
        {
            get { return this.clt.idCleanWay; }
            set
            {
                if  (value!=this.clt.idCleanWay)
                {
                    this.clt.idCleanWay = value;
                    OnPropertyChanged("Label_identCleint_idCleanway");
                }
            }
        }

        public String Label_identCleint_Adresse
        {
            get { return this.clt.adresse.ToString(); }
            set
            {
                if (value != this.clt.adresse.ToString())
                {
                    _label_identCleint_Adresse = this.clt.adresse.ToString();
                    _label_identCleint_Adresse = value;
                    OnPropertyChanged("Label_identCleint_Adresse");
                }
            }
        }
        #endregion
    }
}
