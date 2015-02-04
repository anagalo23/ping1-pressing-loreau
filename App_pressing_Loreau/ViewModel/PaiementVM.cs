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
using App_pressing_Loreau.Model;

//using System.Windows.Controls.Button;
//using System.Windows.Forms;

namespace App_pressing_Loreau.ViewModel
{
    class PaiementVM : ObservableObject
    {
        #region Attributs

        public static int commandePayee = 0;
        private String _label_paiement_prixHT;
        private String _label_paiement_prixTTC;
        private float _txb_paiement_montantRemise;
        private float[] checkRemise;
        private String _label_paiement_montant;//Prix ttc - remise

        //Correspond à la valeur du testbox prix à payer (pour 1 et seulement 1 moyen de paiement)
        private float _txb_paiement_montantParMoyenPaiement;

        //Prends en param : le moyen de paiement et le montant correspondant à ce moyen de paiement
        //private Dictionary<string, float> _txb_paiement_montantParmoyenPaiement = new Dictionary<string, float>();

        private ListePaiement listeDeMontantParMoyenPaiement = new ListePaiement();
        private String _mode_de_paiement;

        private List<GetPaiement> _itemsMontantParMoyenPaiement;

        //private ObservableCollection<PaiementListeVM> _contenuListePaiement;

        private float _reste_a_payer;

        private bool erreurDeManip;
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
            // Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].article.TTC);
            //}


            //Txb_paiement_montantParMoyenPaiement = Label_NouvelleCommande_prixTotal;
            Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
            Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
            //Txb_paiement_montantParMoyenPaiement = float.Parse(Label_paiement_montant.Split(' ')[0]);
            //float.Parse(_label_paiement_prixTTC);
            _mode_de_paiement = "";
            ClasseGlobale.initializeContenuListePaiement();
            checkRemise = new float[2] { 0F, 0F };

            erreurDeManip = false;
        }

        #endregion

        #region Accesseurs et mutateurs

        public String Reste_a_payer_String
        {
            get { return _reste_a_payer.ToString(); }
            set
            {
                _reste_a_payer = float.Parse(value);
                OnPropertyChanged("Reste_a_payer_String");
            }
        }


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
                    //OnPropertyChanged("Txb_paiement_montantParMoyenPaiement");
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

        //public ListePaiement MontantParMoyenPaiement
        //{
        // get { return listeDeMontantParMoyenPaiement; }
        // set
        // {
        // if (value != listeDeMontantParMoyenPaiement)
        // {
        // listeDeMontantParMoyenPaiement = value;
        // OnPropertyChanged("MontantParMoyenPaiement");
        // }
        // }
        //}


        #endregion

        #region Commandes

        #region Bouton valider commande
        public ICommand Btn_valider_paiement_commande
        {
            get
            {
                return new RelayCommand(
                p => enregistrerCommande());
            }
        }


        #endregion

        #region Bouton mode de paiement

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
        #endregion

        #region Bouton valider le montant par mode de paiement ou valider la remise
        //Gère les boutons de mode de paiement
        ICommand btn_paiement_valider;
        public ICommand Btn_paiement_valider
        {
            get { return btn_paiement_valider ?? (btn_paiement_valider = new RelayCommand(completeLaListeDesModesDePaiement)); }
        }
        private void completeLaListeDesModesDePaiement(object button)
        {
            Button clickedbutton = button as Button;
            if (clickedbutton != null)
            {
                //Si j'ai sélectionné un mode de paiement et que la remise n'a pas bougée
                if (Mode_de_paiement != "" && checkRemise[0] == Txb_paiement_montantRemise)
                {
                    //Ajout de mon montant et du mode de paiement dans le dico
                    listeDeMontantParMoyenPaiement[Mode_de_paiement] = Txb_paiement_montantParMoyenPaiement;
                    //On récupère tous les modes de paiements
                    System.Collections.Generic.ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;

                    ObservableCollection<PaiementListeVM> contenuListePaiementTampon = new ObservableCollection<PaiementListeVM>();
                    //initialisation de la liste de paiement en globale
                    ClasseGlobale.initializeContenuListePaiement();
                    //Reconstruction à partir du dico mis à jour
                    foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
                    {
                        contenuListePaiementTampon.Add(new PaiementListeVM()
                        {
                            ModeDePaiement = monMoyenDePaiement,
                            Montant = listeDeMontantParMoyenPaiement[monMoyenDePaiement].ToString(),
                        });
                    }
                    //On donne une nouvelle référence à notre liste globale de client checkRemise
                    ContenuListePaiement = contenuListePaiementTampon;



                    //Si la valeur du champ remise n'a pas changé, je ne la considère pas
                    //if (checkRemise[0] == Txb_paiement_montantRemise)//Si j'ai déjà appliqué la remise
                    //{
                    //Je met dans le text box le nouveau reste à payer
                    Txb_paiement_montantParMoyenPaiement = Reste_a_payer - Txb_paiement_montantParMoyenPaiement;
                    //}
                    //else//Sinon oui.
                    //{
                    ////enlève la remise précédente et applique la nouvelle remise
                    //checkRemise[1] = checkRemise[0];//met le montant de l'ancienne à la place de la nouvelle
                    //checkRemise[0] = Txb_paiement_montantRemise;
                    //Txb_paiement_montantParMoyenPaiement = Reste_a_payer - Txb_paiement_montantParMoyenPaiement + checkRemise[0] - checkRemise[1];
                    ////checkRemise[0] = checkRemise[1];
                    ////Réinitialise l'ancienne valeur
                    //checkRemise[1] = 0;

                    //}

                    //Redéfinition du reste à payer
                    Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
                    MessageBox.Show("ma remise vaut : " + Txb_paiement_montantRemise + "\nprix du paiement : " + Txb_paiement_montantParMoyenPaiement + "\n Reste_a_payer : " + Reste_a_payer);
                    Mode_de_paiement = "";
                }
                //else
                //{
                // erreurDeManip = true;
                // String erreur = "Veuillez s'il vous plait : ";
                // if (Mode_de_paiement == "")
                // {
                // erreur += "\t- sélectionner un mode de paiement";
                // }
                // if (clickedbutton == null)
                // {
                // erreur += "\tERREUR LOGICIEL : le bouton cliqué est null";
                // }
                // MessageBox.Show(erreur);
                //}
                //Si je n'ai pas sélectionné de mode de paiement mais que la remise à changée, j'applique la remise
                if (Mode_de_paiement == "" && checkRemise[0] != Txb_paiement_montantRemise)
                {
                    //enlève la remise précédente et applique la nouvelle remise
                    checkRemise[1] = checkRemise[0];//met le montant de l'ancienne à la place de la nouvelle
                    checkRemise[0] = Txb_paiement_montantRemise;
                    Txb_paiement_montantParMoyenPaiement = Reste_a_payer - checkRemise[0] + checkRemise[1];//- Txb_paiement_montantParMoyenPaiement
                    Reste_a_payer = Txb_paiement_montantParMoyenPaiement;
                    //checkRemise[0] = checkRemise[1];
                    //Réinitialise l'ancienne valeur
                    checkRemise[1] = 0;
                }
                //else
                //{
                // erreurDeManip = true;
                //}

            }


        }

        #endregion


        #region ObservableCollection
        public ObservableCollection<PaiementListeVM> ContenuListePaiement
        {
            get
            {
                if (ClasseGlobale._contenuListePaiement == null)//ClasseGlobale.
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

        #endregion

        #region Methods
        public void lePaiement()
        {
            //On récupère la classe globale contenant les articles et on calcul le prix
            ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;

            Commande CommandeRendue = ClasseGlobale._renduCommande;
            float prixHT = 0;
            float prixTTC = 0;
            float prixTTCrendu = 0;
            float prixHTrendu = 0;
            if (cmdDetail != null)
            {
                try
                {
                    foreach (ArticlesVM art in cmdDetail)
                    {
                        prixTTC += art.typeArticle.TTC;
                        prixHT += art.typeArticle.TTC * (1 - art.typeArticle.TVA / 100);
                    }
                    Label_paiement_prixHT = prixHT + " €";
                    Label_paiement_prixTTC = prixTTC + " €";
                    Label_paiement_montant = prixTTC - Txb_paiement_montantRemise + " €";
                }
                catch (Exception e)
                {
                    //Inscription en log
                }
            }
            else if (ClasseGlobale._rendreArticlesSelectionnes != null)
            {
                //Commande comRenduPaye = (Commande)CommandeDAO.selectCommandeById(CommandeRendue.id, true, true, false);

                try
                {
                    //foreach (Payement paye in comRenduPaye.listPayements)
                    //{
                    //    prixTTC += paye.montant;
                    //    prixHT += paye.montant * (1 - comRenduPaye.listArticles[0].TVA / 100);
                    //}

                    foreach (Article artic in ClasseGlobale._rendreArticlesSelectionnes)
                    {
                        prixTTCrendu += artic.TTC;
                        prixHTrendu += artic.TTC * (1 - artic.TVA / 100);
                    }
                    Label_paiement_prixHT = prixHTrendu - prixHT + " €";
                    Label_paiement_prixTTC = prixTTCrendu - prixTTC + " €";
                    Label_paiement_montant = prixTTCrendu - prixTTC - Txb_paiement_montantRemise + " €";
                }
                catch (Exception e)
                {
                    //Inscription en log
                }
            }


        }


        private void enregistrerCommande()
        {
            //Récupération des articles de la commande, du client, et du paiement et enregistrement en bdd

            int i = 0, j = 0, k = 0;
            Client client = ClasseGlobale.Client;

            //***Enregistrement en base de données***

            //Enregistrement de la commande
            bool payee = false;
            if (Reste_a_payer == 0)
            {
                payee = true;
            }
            else
            {
                payee = false;
            }
            ObservableCollection<ArticlesVM> cmdDetail = ClasseGlobale._contentDetailCommande;

            List<Article> ListeSelectArt = ClasseGlobale._rendreArticlesSelectionnes;
            //Ajouter ??????????????????????????????????????????????????????????????????????????????????????????????????????????
            if (cmdDetail != null)
            {

            Commande cmd = new Commande(DateTime.Now, payee, Txb_paiement_montantRemise, client);
            i = CommandeDAO.insertCommande(cmd);
            cmd = CommandeDAO.lastCommande();

            //Enregistrement des articles
           

           

        
            foreach (ArticlesVM artVM in cmdDetail)
            {
                j = ArticleDAO.insertArticle(artVM.getArticle(cmd.id));
            }

            //Enregistrement du/des paiement(s)
            Payement paiement;
            ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;
            foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
            {
                //stgTest += listeDeMontantParMoyenPaiement[monMoyenDePaiement] + " en " + monMoyenDePaiement + " \n";

                paiement = new Payement(DateTime.Now, listeDeMontantParMoyenPaiement[monMoyenDePaiement], monMoyenDePaiement, cmd.id);
                k = PayementDAO.insertPaiement(paiement);
            }

            //Mise à jour de la table convoyeur
            foreach (PlaceConvoyeur place in ClasseGlobale.PlacesLibres.getList())
            {
                PlaceConvoyeurDAO.updatePlaceConvoyeur(place);
            }


            //if (i != 0 & j != 0 & k != 0)
            //{
            //    MessageBox.Show("Commande enregistrée \n retour à l accueil");
            //    commandePayee = 1;
            //}
            //else if (i != 0 & j == 0 & k != 0)
            //{
            //    MessageBox.Show("Erreur d'enregistrement des articles");
            //}
            //else if (i != 0 & j != 0 & k == 0)
            //{
            //    MessageBox.Show("Erreur de paiement");
            //}
            //else if (i != 0 & j == 0 & k == 0)
            //{
            //    MessageBox.Show("Erreur d'enregistrement des articles \n Erreur de paiement");

            //}
            //else if (i == 0)
            //{
            //    MessageBox.Show("Erreur d'enregistrement de la commande");

            //}
            Commande cmdTota = CommandeDAO.selectCommandeById(cmd.id,true,true,true);

            RecuPaiement rp = new RecuPaiement(cmdTota);
            rp.printRecu();
            //paiement = new Payement(DateTime.Now, 4 / 3, TypePayementDAO.selectTypePayementById(1).nom, cmd.id);
            //PayementDAO.insertPaiement(paiement);

            //FactureExcel fe = new FactureExcel(CommandeDAO.selectCommandeById(cmd.id, true, true, true));
            //fe.printFacture();
            }
            else if (ListeSelectArt != null)
            {
                ////////
                ///////
                Commande comdRendu = ClasseGlobale._renduCommande;

                //Commande updateComande= new Commande(null,)
                foreach (Article art in ListeSelectArt)
                {
                    //int id, string photo, string commentaire, bool ifRendu, float TVA, float TTC, TypeArticle type, PlaceConvoyeur convoyeur, int fk_commande
                    //DateTime dateRendu = DateTime.Now;
                    Article artAdd = new Article(art.id,art.photo,art.commentaire,true,art.TVA,art.TTC,art.type,null,comdRendu.id);
                    artAdd.date_rendu = DateTime.Now;
                    j = ArticleDAO.updateArticle(art);
                }


                //Enregistrement du/des paiement(s)
                Payement paiement;
                ICollection<String> liste_des_moyens_de_paiement = listeDeMontantParMoyenPaiement.dico.Keys;
                foreach (String monMoyenDePaiement in liste_des_moyens_de_paiement)
                {
                    //stgTest += listeDeMontantParMoyenPaiement[monMoyenDePaiement] + " en " + monMoyenDePaiement + " \n";

                    paiement = new Payement(DateTime.Now, listeDeMontantParMoyenPaiement[monMoyenDePaiement], monMoyenDePaiement, comdRendu.id);
                    k = PayementDAO.insertPaiement(paiement);
                }

                Commande cmdTota = CommandeDAO.selectCommandeById(comdRendu.id, true, true, true);

                RecuPaiement rp = new RecuPaiement(cmdTota);
                rp.printRecu();
            }


        }



        #endregion

    }

    #region classes internes
    public class GetPaiement
    {

        public String Label_nomModePaiement { get; set; }

        public float Label_MontantPayerParMode { get; set; }
    }

    class ListePaiement
    {

        private readonly IDictionary<string, float> maListeDePaiement = new Dictionary<string, float>();

        public IDictionary<string, float> dico
        {
            get
            {
                return maListeDePaiement;
            }
        }

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
                    maListeDePaiement[key] += value;
                }
                else
                {
                    maListeDePaiement.Add(key, value);
                }

            }
        }


    }

    #endregion


}
