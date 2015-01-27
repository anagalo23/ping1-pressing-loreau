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
        private float _txb_paiement_montantRemise;
        private String _label_paiement_montant;//Prix ttc - remise

        //Correspond à la valeur du testbox prix à payer (pour 1 et seulement 1 moyen de paiement)
        private float _txb_paiement_montantParMoyenPaiement;

        //Prends en param : le moyen de paiement et le montant correspondant à ce moyen de paiement
        //private Dictionary<string, float> _txb_paiement_montantParmoyenPaiement = new Dictionary<string, float>();

        ListePaiement montantParmoyenPaiement = new ListePaiement();



        //Payement paiement;
        #endregion


        #region constructeur

        public PaiementVM()
        {
            lePaiement();
            _txb_paiement_montantParMoyenPaiement = new float();



            //Label_NouvelleCommande_prixTotal = 0;
            //for (int i = 0; i < ContentDetailCommande.Count; i++)
            //{
            //    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.TTC);
            //}


            //Txb_paiement_montantParMoyenPaiement = Label_NouvelleCommande_prixTotal;
            Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);

            //Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
                //float.Parse(_label_paiement_prixTTC);
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

        public float Txb_paiement_montantRemise
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

        public float Txb_paiement_montantParMoyenPaiement
        {
            get { return _txb_paiement_montantParMoyenPaiement; }
            set
            {
                if (value != _txb_paiement_montantParMoyenPaiement)
                {
                    _txb_paiement_montantParMoyenPaiement = value;
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

        //public ICommand Btn_paiement_cb
        //{

        //}

        //public ICommand Btn_paiement_Espece { }
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

    class ListePaiement
    {

        private readonly IDictionary<string, float> maListeDePaiement = new Dictionary<string, float>();

        public float this[string key]
        {
            get
            {
                return maListeDePaiement[key];
            }

            // Je mets la nouvelle valeur pour ce mode de paiement, attention le calcul (- et + une certaine somme) se fait à l'extérieur
            set
            {
                //maListeDePaiement[key] = value;
                if (maListeDePaiement.ContainsKey(key))
                {
                    maListeDePaiement[key] = value;
                }
                else
                {
                    maListeDePaiement.Add(key, value);
                }
            }
        }
    }

}
