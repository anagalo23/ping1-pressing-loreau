using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using App_pressing_Loreau.Helper;
using Microsoft.Practices.Prism.Commands;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class DetailCommandeVM : ObservableObject
    {
        #region attributs

        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_selectionner_tout;
        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_deselectionner_tout;
        private List<ArticlesRestitutionVM> _afficheDetailCommande;

        private float _label_prixTTC;
        private String _label_EtatPaiementCommande;
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

        public String Label_EtatPaiementCommande
        {
            get { return _label_EtatPaiementCommande; }
            set
            {
                if (value != _label_EtatPaiementCommande)
                {
                    _label_EtatPaiementCommande = value;
                    OnPropertyChanged("Label_EtatPaiementCommande");
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

            Commande com = ClasseGlobale._renduCommande.commande;
            if (com != null)
            {
                foreach (Article art in com.listArticles)
                {
                    AfficheDetailCommande.Add(new ArticlesRestitutionVM() { ar=art, ArticlesNameRes=art.type.nom});
                }
            }
            if (com.payee == false)
            {
                Label_EtatPaiementCommande = "Commande non réglée";
                Label_prixTTC = 0;
             
                foreach (Article arti in com.listArticles)
                {
                    Label_prixTTC += (arti.TTC);
                }
            }
            else
            {
                Label_EtatPaiementCommande = "Commande  réglée";
                 Label_prixTTC=0;

            }
          
            //AfficheDetailCommande.Add(new ArticlesRestitutionVM() { ArticlesNameRes = "Slt", IsSelectedArticle = false });
         
        }
        #endregion

    }
}
