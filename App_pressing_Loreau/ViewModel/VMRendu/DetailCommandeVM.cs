using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.ViewModel
{
    class DetailCommandeVM : ObservableObject
    {
        #region attributs
       

        private List<ArticlesRestitutionVM> _afficheDetailCommande;

        #endregion

        #region Constructeur
        public DetailCommandeVM()
        {
            LaCommande();
        }
            
        #endregion

        #region properties and Commands

        public List<ArticlesRestitutionVM> AfficheDetailCommande
        {
            get { return _afficheDetailCommande; }
            set
            {
                if (value != _afficheDetailCommande)
                {
                    _afficheDetailCommande = value;
                    RaisePropertyChanged("AfficheDetailCommande");
                }
            }
        }
        #endregion

        #region Methods
        public void LaCommande()
        {
            AfficheDetailCommande = new List<ArticlesRestitutionVM>();

            _afficheDetailCommande.Add(new ArticlesRestitutionVM() { ArticlesNameRes = "Bonjour" });

        }
        #endregion

    }
}
