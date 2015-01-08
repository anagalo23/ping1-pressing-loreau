using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;
using System.Windows.Input;

namespace App_pressing_Loreau.View
{
    class FactureVM : ObservableObject, IPageViewModel
    {
        public String Name
        {
            get { return ""; }
        }

        #region Attributes
        private int _txb_factures_idCommande;

        private FactureFinaleVM _apercu_facture;
        #endregion


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

        public FactureFinaleVM Apercu_facture
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
                    OnPropertyChanged("Apercu_facture");
                }
            }
        }


        public ICommand Btn_Factures_Recherche
        { get { return new RelayCommand(
            p =>FactureApercu(), 
            p => Txb_factures_idCommande > 0); } }
        #endregion
        
        
        
        #region methods 

        public void FactureApercu()
        {
            FactureFinaleVM ffVM = new FactureFinaleVM();

            ffVM.LabelReferenceFacture = "Ref: " +12 ;
            ffVM.LabelDetailTotal=23 + "€";

            ffVM.ListBoxDetailFacture.Add(new CategoryArticle() { LabelNameArticle = "Pantalon", LabelPrixArticle = 100 + "€" });

            Apercu_facture = ffVM;
        }
        
        
        #endregion


    }
}
