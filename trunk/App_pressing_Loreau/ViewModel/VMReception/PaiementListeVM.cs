using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// Classe permettant de payer une commande avec les différents modes de paiement suivant :
    /// espèces
    /// carte bancaire
    /// id cleanway
    /// virement
    /// american express
    /// chèque
    /// Traitement des remises également
    /// </summary>
    class PaiementListeVM : ObservableObject
    {
        #region Attributes
        private String _modeDePaiement;
        private String _montant;
        #endregion

        public PaiementListeVM()
        {
            //Cbb_Articles_Commentaire = comboComm.ListeComm();
        }

        public String ModeDePaiement
        {
            get
            {
                return _modeDePaiement;
            }
            set
            {
                _modeDePaiement = value;
            }
        }

        public String Montant
        {
            get
            {
                return _montant;
            }
            set
            {
                _montant = value;
            }
        }
    }
}
