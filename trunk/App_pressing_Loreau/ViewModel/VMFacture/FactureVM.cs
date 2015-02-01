using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.View;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;

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

       
        
        private FactureFinaleVM _apercu_facture;

        Commande commande;
        #endregion


        public FactureVM()
        {
            
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
            FactureFinaleVM ffVM = new FactureFinaleVM();
            float prixTTCTotal = 0;
            float prixHTTotal = 0;
            

            commande = (Commande)CommandeDAO.selectCommandeById(Txb_factures_idCommande,false, true, true);
            if (commande != null)
            {
                foreach (Article art in commande.listArticles)
                {
                    prixTTCTotal += art.TTC;
                   
                }
                prixHTTotal = prixTTCTotal * (1 - commande.listArticles[0].TVA / 100);

                ffVM.commande = commande;
                ffVM.LabelDetailPrixTotalTTC = prixTTCTotal;
                ffVM.LabelDetailMontantHT = prixHTTotal;
                ffVM.LabelDetailMontantTVA = prixTTCTotal - prixHTTotal;
                ffVM.RemplirArticles(commande);
            }

            
            ApercuFacture = ffVM;

        }

        public void impression()
        {
            //PrintDialog dialog = new PrintDialog();
            //FactureFinale fenetreFacture = new FactureFinale();
            //if (dialog.ShowDialog() == true)
            //{
            //    dialog.PrintVisual(fenetreFacture, "fenetreFacture");
            //}
            if (commande != null)
            {
                FactureExcel fe = new FactureExcel(commande);

                fe.printFacture();
            }
        }
        
        #endregion


    }


}
