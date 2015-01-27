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
using System.Windows;
using System.Windows.Controls;

//using System.Windows.Controls.Button;
//using System.Windows.Forms;

namespace App_pressing_Loreau.ViewModel
{
    class PaiementVM : ObservableObject
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

        private ListePaiement montantParMoyenPaiement = new ListePaiement();
        private String _mode_de_paiement;

        private List<GetPaiement> _itemsMontantParMoyenPaiement;

        private ObservableCollection<PaiementListeVM> _contenuListePaiement;

        private float _reste_a_payer;
        //Payement paiement;
        #endregion


        #region constructeur
        public PaiementVM()
        {
            lePaiement();
            _txb_paiement_montantParMoyenPaiement = new float();

            _itemsMontantParMoyenPaiement = new List<GetPaiement>();

            //Label_NouvelleCommande_prixTotal = 0;
            //for (int i = 0; i < ContentDetailCommande.Count; i++)
            //{
            //    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.TTC);
            //}


            //Txb_paiement_montantParMoyenPaiement = Label_NouvelleCommande_prixTotal;
            Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
            Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
            //Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
            //float.Parse(_label_paiement_prixTTC);
            _mode_de_paiement = "";
            
        }
        #endregion

        #region Properties and command

        public List<GetPaiement> ItemsMontantParMoyenPaiement
        {
            get { return _itemsMontantParMoyenPaiement; }
            set
            {
                _itemsMontantParMoyenPaiement = value;
                OnPropertyChanged("ItemsMontantParMoyenPaiement");
            }
        }

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


        public String Mode_de_paiement
        {
            get { return _mode_de_paiement; }
            set
            {
                if (value != _mode_de_paiement)
                {
                    _mode_de_paiement = value;
                    OnPropertyChanged("Mode_de_paiement");
                }
            }
        }




        public float Reste_a_payer
        {
            get { return _reste_a_payer; }
            set
            {
                if (value != _reste_a_payer)
                {
                    _reste_a_payer = value;
                    OnPropertyChanged("Reste_a_payer");
                }
            }
        }

        public String Reste_a_payer_String
        {
            get { return _reste_a_payer.ToString();  }
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


        public ListePaiement MontantParMoyenPaiement
        {
            get { return montantParMoyenPaiement; }
            set
            {
                if (value != montantParMoyenPaiement)
                {
                    montantParMoyenPaiement = value;
                    OnPropertyChanged("MontantParMoyenPaiement");
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


        //Gère les boutons de mode de paiement
        ICommand btn_paiement;
        public ICommand Btn_paiement
        {
            get { return btn_paiement ?? (btn_paiement = new RelayCommand(getModeDePaiement)); }
        }
        private void getModeDePaiement(object button)
        {
            System.Windows.Controls.Button clickedbutton = button as System.Windows.Controls.Button;
            _mode_de_paiement = clickedbutton.Tag.ToString();
            //System.Windows.Forms.MessageBox.Show(_mode_de_paiement);
        }


        //Gère les boutons de mode de paiement
        ICommand btn_paiement_valider;
        public ICommand Btn_paiement_valider
        {
            get { return btn_paiement_valider ?? (btn_paiement_valider = new RelayCommand(completeLaListeDesModesDePaiement)); }
        }
        private void completeLaListeDesModesDePaiement(object button)
        {
            Button clickedbutton = button as Button;
            //_mode_de_paiement = clickedbutton.Tag.ToString();
            //System.Windows.Forms.MessageBox.Show(_mode_de_paiement);

            //get le mode de paiement


            //get Le montant

            //MontantParMoyenPaiement = new ListePaiement();
            //Ajouter à la liste
            if (Mode_de_paiement != "" && clickedbutton != null)
            {
                //MontantParMoyenPaiement[_mode_de_paiement] = Txb_paiement_montantParMoyenPaiement;


                //ItemsMontantParMoyenPaiement.Add(new GetPaiement()
                //{
                //    Label_MontantPayerParMode = Txb_paiement_montantParMoyenPaiement,
                //    Label_nomModePaiement = _mode_de_paiement
                //});

                ClasseGlobale._contenuListePaiement.Add(new PaiementListeVM()
                {
                    ModeDePaiement = Mode_de_paiement,
                    Montant = Txb_paiement_montantParMoyenPaiement.ToString(),

                });

                //Réinitialisation des champs pour éviter les erreurs et doublons
                Txb_paiement_montantParMoyenPaiement = Reste_a_payer - Txb_paiement_montantParMoyenPaiement;
                Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
                Mode_de_paiement = "";

            }
            else
            {
                String erreur="Veuillez s'il vous plait : ";
                if (Mode_de_paiement == "")
                {
                    erreur += "\t- sélectionner un mode de paiement";
                }
                if (clickedbutton == null)
                {
                    erreur += "\tERREUR LOGICIEL : le bouton cliqué est null";
                }
                MessageBox.Show(erreur);
            }

        }


        public ObservableCollection<PaiementListeVM> ContenuListePaiement
        {
            get
            {
                if (_contenuListePaiement == null)//ClasseGlobale.
                {
                    ClasseGlobale.initializeContenuListePaiement();
                    //_contenuListePaiement = new ObservableCollection<PaiementListeVM>();
                }
                return ClasseGlobale._contenuListePaiement;

            }

            set
            {
                if (value != null)
                {
                    ClasseGlobale._contenuListePaiement = value;
                    //_contenuListePaiement = value;
                    OnPropertyChanged("ContenuListePaiement");
                }
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

    }
    public class GetPaiement
    {

        public String Label_nomModePaiement { get; set; }

        public float Label_MontantPayerParMode { get; set; }
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
