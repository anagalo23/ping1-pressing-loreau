using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using App_pressing_Loreau.Helper;
using Microsoft.Practices.Prism.Commands;

namespace App_pressing_Loreau.ViewModel
{
    class DetailCommandeVM : ObservableObject
    {
        #region attributs

        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_selectionner_tout;
        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_deselectionner_tout;
        private List<ArticlesRestitutionVM> _afficheDetailCommande;

        private float _label_prixTTC;
        #endregion

        #region Constructeur
        public DetailCommandeVM()
        {
            LaCommande();
        }
            
        #endregion

        #region properties and Commands

        public float Label_prixTTC
        {
            get { return _label_prixTTC; }
            set
            {
                if (value != _label_prixTTC)
                {
                    _label_prixTTC = value;
                    RaisePropertyChanged("Label_prixTTC");
                }
            }
        }
        public DelegateCommand<ArticlesRestitutionVM> Btn_detailCommande_selectionner_tout
        {
            get
            {
                return this._btn_detailCommande_deselectionner_tout ?? (this._btn_detailCommande_deselectionner_tout = new DelegateCommand<ArticlesRestitutionVM>(
                                                                     this.ExecuteSelectAllArticles,
                                                                     (arg) => true));
            }
        }

        public DelegateCommand<ArticlesRestitutionVM> Btn_detailCommande_deselectionner_tout
        {
            get
            {
                return this._btn_detailCommande_selectionner_tout ?? (this._btn_detailCommande_selectionner_tout = new DelegateCommand<ArticlesRestitutionVM>(
                                                                     this.ExecuteDeselectAllArticles,
                                                                     (arg) => true));
            }
        }
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

        private void ExecuteSelectAllArticles(ArticlesRestitutionVM obj)
        {
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = true;
            }
        }

        private void ExecuteDeselectAllArticles(ArticlesRestitutionVM obj)
        {
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = false;
            }
        }
        private void LaCommande()
        {
            AfficheDetailCommande = new List<ArticlesRestitutionVM>();

            AfficheDetailCommande.Add(new ArticlesRestitutionVM() { ArticlesNameRes = "Bonjour", IsSelectedArticle = false });
            AfficheDetailCommande.Add(new ArticlesRestitutionVM() { ArticlesNameRes = "Slt", IsSelectedArticle = false });
            Label_prixTTC = 10;
        }
        #endregion

    }
}
