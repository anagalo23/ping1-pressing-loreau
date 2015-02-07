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
            //ClasseGlobale._rendreArticlesSelectionnes = ArtSelec;
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

        public ICommand Btn_ValiderSelect
        {
            get { return new RelayCommand(p => ValiderSelection()); }
        } 
        #endregion

        #region Methods

        private void ValiderSelection()
        {
            ArtSelec = new List<Article>();

            foreach (ArticlesRestitutionVM artVM in AfficheDetailCommande)
            {
               
                if (artVM.IsSelectedArticle)
                {
     
                    ArtSelec.Add(artVM.ar);
                }
               
            }


            if (ClasseGlobale._renduCommande.payee == false)
            {


                if (ArtSelec.Count != 0)
                {
                    Label_prixTTC = 0;
                    foreach (Article arti in ArtSelec)
                    {
                        Label_prixTTC += (arti.TTC);
                    }
                }
                else
                {
                    Label_prixTTC = 0;
                }

                //Label_prixTTC = prixTotal;
            }
            else
            {
                Label_prixTTC = 0;
            }


            ClasseGlobale._rendreArticlesSelectionnes = ArtSelec;
            //MessageBox.Show(ClasseGlobale._rendreArticlesSelectionnes.Count +"");
            
        }
        private void ExecuteSelectAllArticles(ArticlesRestitutionVM obj)
        {
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = true;
                //ArtSelec.Add(art.ar);
            }
        }

        private void ExecuteDeselectAllArticles(ArticlesRestitutionVM obj)
        {
            foreach (ArticlesRestitutionVM art in AfficheDetailCommande)
            {
                art.IsSelectedArticle = false;
                //ArtSelec.Remove(art.ar);

            }
        }

        
        private void LaCommande()
        {
            AfficheDetailCommande = new List<ArticlesRestitutionVM>();

            Commande com = ClasseGlobale._renduCommande;

            //Commande comdSelec = ClasseGlobale._rendreArticlesSelectionnes;
            //foreach(Article art in com.)
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
                }



            }
            if (com.payee == false)
            {
                Label_EtatPaiementCommande = "Commande non réglée";
                //Label_prixTTC = 0;
                //float prixTotal = 0;

                ////float prixPaye = 0;
                //if (ArtSelec.Count != 0)
                //{
                //    foreach (Article arti in ArtSelec)
                //    {
                //        prixTotal += (arti.TTC);
                //    }
                //}
               

                ////foreach (Payement p in comPaye.listPayements)
                ////{
                ////    prixPaye += p.montant;
                ////}

                //Label_prixTTC = prixTotal;
            }
            else
            {
                Label_EtatPaiementCommande = "Commande  réglée";
              

            }


        }
        #endregion

    }
}
