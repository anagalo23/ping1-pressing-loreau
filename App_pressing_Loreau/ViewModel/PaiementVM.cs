using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace App_pressing_Loreau.ViewModel
{
    class PaiementVM : ObservableObject, IPageViewModel
    {
        #region Attributs
        private String _label_paiement_prixHT;
        private String _label_paiement_prixTTC;
        private String _label_paiement_montant;

        private int _txb_paiement_montantParmoyenPaiement;
        private int _txb_paiement_montantRemise;
        #endregion


        #region constructeur

        public PaiementVM()
        {
            lePaiement();
        }
        #endregion

        #region Properties and command

        public String Label_paiement_prixHT
        {
            get { return _label_paiement_prixHT; }
            set
            {
                if (value != _label_paiement_prixHT)
                {
                    _label_paiement_prixHT = value;
                    OnPropertyChanged("Label_paiement_prixHT");
                }
            }
        }

        public String Label_paiement_prixTTC
        {
            get { return _label_paiement_prixTTC; }
            set
            {
                if (value != _label_paiement_prixTTC)
                {
                    _label_paiement_prixTTC = value;
                    OnPropertyChanged("Label_paiement_prixTTC");
                }
            }
        }

        public String Label_paiement_montant
        {
            get { return _label_paiement_montant; }
            set
            {
                if (value != _label_paiement_montant)
                {
                    _label_paiement_montant = value;
                    OnPropertyChanged("Label_paiement_montant");
                }
            }
        }

        public int Txb_paiement_montantRemise
        {
            get { return _txb_paiement_montantRemise; }
            set
            {
                if (value != _txb_paiement_montantRemise)
                {
                    _txb_paiement_montantRemise = value;
                    OnPropertyChanged("Txb_paiement_montantRemise");
                }
            }
        }

        public int Txb_paiement_montantParMoyenPaiement
        {
            get { return _txb_paiement_montantParmoyenPaiement; }
            set
            {
                if (value != _txb_paiement_montantParmoyenPaiement)
                {
                    _txb_paiement_montantParmoyenPaiement = value;
                    OnPropertyChanged("Txb_paiement_montantParMoyenPaiement");
                }
            }
        }
        public ICommand Btn_paiment_valider
        {
            get
            {
                return new RelayCommand(
                    p => lePaiement(),
                    p => Txb_paiement_montantRemise > 0 & Txb_paiement_montantRemise < 6);
            }
        }
        #endregion


        #region Methods
        public void lePaiement()
        {
           // NouvelleCommandeVM nc = new NouvelleCommandeVM();

            //ObservableCollection<ArticlesVM> cmdDetail = (ObservableCollection<ArticlesVM>)NouvelleCommandeVM.getContentDetailCommande().ContentDetailCommande;

            ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;

            float prixHT = 0;
            float prixTTC = 0;
            try
            {
                foreach (ArticlesVM art in cmdDetail)
                {
                    prixTTC += art.article.TTC;
                    prixHT += art.article.TTC * (1 - art.article.TVA / 100);
                }
                Label_paiement_prixHT = prixHT + "  €";
                Label_paiement_prixTTC = prixTTC + "  €";
                Label_paiement_montant = prixTTC - Txb_paiement_montantRemise + "  €";
            }
            catch (Exception e)
            {
                //Inscription en log
            }
            
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
