using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;

namespace App_pressing_Loreau.ViewModel
{
    class PaiementVM : ObservableObject, IPageViewModel
    {
        #region Attributs
        private String _label_paiement_prixHT;
        private String _label_paiement_prixTTC;
        private String _label_paiement_montant;

        private int _txb_paiement_montantRemise;
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
            int x = 18;
            Label_paiement_prixHT = x + "  €";
            Label_paiement_prixTTC = x + x / 5 + "  €";
            Label_paiement_montant = x + x / 5 - Txb_paiement_montantRemise + "  €";
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
