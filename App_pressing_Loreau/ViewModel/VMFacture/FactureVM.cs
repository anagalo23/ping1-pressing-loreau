using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;
using System.Windows.Controls;

namespace App_pressing_Loreau.ViewModel
{
    class FactureVM : ObservableObject, IPageViewModel
    {
        public String Name
        {
            get { return ""; }
        }

        #region Attributes
        private int _txb_factures_idCommande;

        FactureFinaleVM ffVM = null;
        
        private FactureFinaleVM _apercu_facture;
        #endregion


        public FactureVM()
        {
            ffVM = new FactureFinaleVM();
        }


        #region Properties and commands
        public int Txb_factures_idCommande
        {
            get
            {
                return _txb_factures_idCommande;
            }
            set
            {
                if (_txb_factures_idCommande != value)
                {
                    this._txb_factures_idCommande = value;
                    OnPropertyChanged("Txb_factures_idCommande");
                }
            }
        }

        public FactureFinaleVM ApercuFacture
        {
            get
            {
                return _apercu_facture;
            }
            set
            {
                if (_apercu_facture != value)
                {
                    this._apercu_facture = value;
                    OnPropertyChanged("ApercuFacture");
                }
            }
        }


        public ICommand Btn_Factures_Recherche
        {
            get
            {
                return new RelayCommand(
                    p => FactureApercu(), 
                    p=>Txb_factures_idCommande>0);
            }
        }


        public ICommand Btn_factures_imprimer_facture
        {
            get
            {
                return new RelayCommand(
                    p => impression());
            }
        }
        #endregion
        
        
        
        #region methods 

        public void FactureApercu()
        {
            int ht=20;
            List<CategoryArticle> listeArticles= new List<CategoryArticle>();
            ffVM.LabelReferenceFacture = "Ref: " + 277777 + Txb_factures_idCommande;
            ffVM.LabelDetailTotal= ht*1.2 +" €";

            listeArticles.Add(new CategoryArticle() { LabelNameArticle = "Pantalon", LabelPrixArticle = 100 + "€" });
            listeArticles.Add(new CategoryArticle() { LabelNameArticle = "Pantalon", LabelPrixArticle = 100 + "€" });

            ffVM.ListBoxDetailFacture = listeArticles;
            ffVM.LabelDetailTauxTVA = 20 + "%";
            ffVM.LabelDetailMontantHT = ht + "€";
            ffVM.LabelDetailMontantTVA = ht * 20 / 100 + " €";
            ApercuFacture= ffVM;
        }

        public void impression()
        {
            PrintDialog dialog = new PrintDialog();
            FactureFinale fenetreFacture = new FactureFinale();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(fenetreFacture, "fenetreFacture");
            }
        }
        
        #endregion


    }


}
