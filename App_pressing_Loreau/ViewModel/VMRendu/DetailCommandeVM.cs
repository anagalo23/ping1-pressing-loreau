using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using App_pressing_Loreau.Helper;
using Microsoft.Practices.Prism.Commands;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;
using System.Windows.Input;
using System.Windows;

namespace App_pressing_Loreau.ViewModel
{
    class DetailCommandeVM : ObservableObject
    {
        #region attributs

        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_selectionner_tout;
        private DelegateCommand<ArticlesRestitutionVM> _btn_detailCommande_deselectionner_tout;
        private List<ArticlesRestitutionVM> _afficheDetailCommande;

        List<Article> ArtSelec=null;
            //ClasseGlobale._rendreArticlesSelectionnes;
        private float _label_prixTTC;
        private String _label_EtatPaiementCommande;
        #endregion


        #region Constructeur
        public DetailCommandeVM()
        {
            LaCommande();
            ClasseGlobale._rendreArticlesSelectionnes = new List<Article>();
        }

        #endregion


        #region Propriétés

        
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


        #region Commandes

        #region Séletionner tout

        public DelegateCommand<ArticlesRestitutionVM> Btn_detailCommande_selectionner_tout
        {
            get
            {
                return this._btn_detailCommande_deselectionner_tout ?? (this._btn_detailCommande_deselectionner_tout = new DelegateCommand<ArticlesRestitutionVM>(
                                                                     this.ExecuteSelectAllArticles,
                                                                     (arg) => true));
            }
        }
        private void ExecuteSelectAllArticles(ArticlesRestitutionVM obj)
        {
            
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = true;
                
            }
        }
        #endregion

        #region Désélectionner tout
        public DelegateCommand<ArticlesRestitutionVM> Btn_detailCommande_deselectionner_tout
        {
            get
            {
                return this._btn_detailCommande_selectionner_tout ?? (this._btn_detailCommande_selectionner_tout = new DelegateCommand<ArticlesRestitutionVM>(
                                                                     this.ExecuteDeselectAllArticles,
                                                                     (arg) => true));
            }
        }
        private void ExecuteDeselectAllArticles(ArticlesRestitutionVM obj)
        {
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = false;

            }
        }
        #endregion

        #region Valider la sélection
        public ICommand Btn_ValiderSelect
        {
            get { return new RelayCommand(p => ValiderSelection()); }
        }
        private void ValiderSelection()
        {
            ArtSelec = new List<Article>();

            //Ajout des articles sélectionnés dans une liste
            foreach (ArticlesRestitutionVM artVM in AfficheDetailCommande)
            {
                if (artVM.IsSelectedArticle)
                {
                    ArtSelec.Add(artVM.ar);
                }
            }
            ClasseGlobale._rendreArticlesSelectionnes = ArtSelec;

            Label_prixTTC = 0;
            //Si la commande séletionnée n'a pas encore été totalement payée
            if (ClasseGlobale._renduCommande.payee == false)
            {
                //Les articles rendus sont payés
                //Parcours de la liste des articles et on fait la somme des prix => prix mini de la restitution
                if (ArtSelec.Count != 0)
                {
                    foreach (Article arti in ArtSelec)
                    {
                        Label_prixTTC += (arti.TTC);
                    }
                }

                //Je calcule le reste à payer de cette commande
                //Je vais chercher tous les articles et je calcule le prix
                //Je vais chercher tous les paiements effectués pour cette commande
                //Reste à payer = totalarticle - totalPaiements
            }
            else
            {
                MessageBox.Show("La commande a déjà été réglée");
            }


        }
        #endregion





        #endregion


        #region Methodes
        
        private void LaCommande()
        {
            AfficheDetailCommande = new List<ArticlesRestitutionVM>();

            Commande com = ClasseGlobale._renduCommande;

            //Commande comdSelec = ClasseGlobale._rendreArticlesSelectionnes;
            Commande comPaye = (Commande)CommandeDAO.selectCommandeById(com.id, true, false, false);
            if (com != null)
            {
                foreach (Article art in com.listArticles)
                {
                    AfficheDetailCommande.Add(new ArticlesRestitutionVM() { ar = art, ArticlesNameRes = art.type.nom });
                }


                foreach (ArticlesRestitutionVM artRestVm in AfficheDetailCommande)
                {
                    if (artRestVm.IsSelectedArticle == true)
                    {
                        ClasseGlobale._rendreArticlesSelectionnes.Add(artRestVm.ar);
                    }
                    if (artRestVm.ar.ifRendu = true)
                    {
                        artRestVm.IsEnabledArticles = false;
                    }
                    else
                    {
                        artRestVm.IsEnabledArticles = true;
                    }
                }



            }
            if (com.payee == false)
            {
                Label_EtatPaiementCommande = "Commande non réglée";

                //Activé le check box
                foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
                {
                    art.IsEnabledArticles = true;
                }
            }
            else
            {
                Label_EtatPaiementCommande = "Commande  réglée";
                //Desactivé le check box
                foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
                {
                    art.IsEnabledArticles = false;
                }


            }


        }
        
        #endregion

    }
}
