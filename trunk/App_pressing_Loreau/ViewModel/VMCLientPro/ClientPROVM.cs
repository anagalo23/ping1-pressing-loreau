using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.Windows.Input;
using App_pressing_Loreau.Model;
using System.Drawing.Printing;

namespace App_pressing_Loreau.ViewModel
{
    class ClientPROVM : ObservableObject, IPageViewModel
    {
        #region Attributs
        private DelegateCommand<UnClientPROVM> _detailCommandeCouranteClientPro;
        private DelegateCommand<UnClientPROVM> _detailButtonClientPro;

        private List<UnClientPROVM> _listeClientPro;
        private List<UnClientPROVM> _detailCommandeClientPro;

        private String _label_CommandeSelectionner;
        private float _label_ClientProUC_TotalAPayer;

        float prixApayer = 0;
        #endregion

        #region constructeur
        public ClientPROVM()
        {
            clientsPro();
            Label_CommandeSelectionner = "";
            Label_ClientProUC_TotalAPayer = new float();

            ClasseGlobale.SET_ALL_NULL();
        }
        #endregion

        #region Properties and commands
        public List<UnClientPROVM> ListeClientPro
        {
            get { return _listeClientPro; }

            set
            {
                _listeClientPro = value;
                RaisePropertyChanged("ListeClientPro");

            }
        }

        public List<UnClientPROVM> DetailCommandeClientPro
        {
            get { return _detailCommandeClientPro; }
            set
            {
                _detailCommandeClientPro = value;
                OnPropertyChanged("DetailCommandeClientPro");
            }
        }
        public DelegateCommand<UnClientPROVM> DetailCommandeCouranteClientPro
        {
            get
            {
                return this._detailCommandeCouranteClientPro ?? (this._detailCommandeCouranteClientPro = new DelegateCommand<UnClientPROVM>(
                                                                       this.ExecuteClientProCommandeCourante,
                                                                       (arg) => true));
            }
        }

        public DelegateCommand<UnClientPROVM> DetailButtonCommandeClientPro
        {
            get
            {
                return this._detailButtonClientPro ?? (this._detailButtonClientPro = new DelegateCommand<UnClientPROVM>(
                  this.ExecuteCommandeClientPro,
                (arg) => true));
            }
        }

        public String Label_CommandeSelectionner
        {
            get { return _label_CommandeSelectionner; }
            set
            {
                _label_CommandeSelectionner = value;
                OnPropertyChanged("Label_CommandeSelectionner");
            }
        }
        public float Label_ClientProUC_TotalAPayer
        {
            get { return _label_ClientProUC_TotalAPayer; }
            set
            {
                _label_ClientProUC_TotalAPayer = value;
                OnPropertyChanged("Label_ClientProUC_TotalAPayer");
            }
        }
        //public ICommand TestImprimante
        //{
        //    get { return new RelayCommand(p => testPrint()); }
        //}


        public ICommand Regler_commande_pro
        {
            get { return new RelayCommand(p => ReglerCommande()); }
        }

        private void ReglerCommande()
        {
            //Les articles et la commane passe à payés
            //TODO
            MessageBox.Show("Réglage de la commande");
        }




        public ICommand Rendre_commande_pro
        {
            get { return new RelayCommand(p => RendreCommande()); }
        }

        private void RendreCommande()
        {
            //Les articles passent en rendus
            //TODO
            //MessageBox.Show("Réglage de la commande");
            if (ClasseGlobale._renduCommandeClientPro != null)
            {
                Commande cmd = ClasseGlobale._renduCommandeClientPro;
                cmd = CommandeDAO.selectCommandeById(cmd.id, true, true, true);
                //Parcours de la liste d'article et update de la bdd
                foreach (Article art in cmd.listArticles)
                {
                    //On update l'emplacment convoyeur
                    art.convoyeur.encombrement -= art.type.encombrement;
                    PlaceConvoyeurDAO.updatePlaceConvoyeur(art.convoyeur);

                    //Puis l'article
                    art.date_rendu = DateTime.Now;
                    art.ifRendu = true;
                    art.convoyeur = null;
                    ArticleDAO.updateArticle(art);
                }
                cmd.date_rendu = DateTime.Now;
                CommandeDAO.updateCommande(cmd);
            }

        }
        #endregion

        #region Methods


        //private void testPrint()
        //{

        //    Client c = new Client(1, "TRAORE", "Mami", "", "", null, DateTime.Now, "", DateTime.Now, 10, false, false, 0);
        //    Commande comTest = new Commande(2, DateTime.Now, false, 0, c);
        //    Departement dep = new Departement(1, "Classique");
        //    TypeArticle t = new TypeArticle(1, "jupe", 1, 20, 5, dep);
        //    TypeArticle t2 = new TypeArticle(2, "robe", 1, 20, 50, dep);
        //    TypeArticle t3 = new TypeArticle(3, "habit", 1, 20, 25, dep);

        //    Article a1 = new Article(1, "", "", false, 20, 5, t, null, 2);
        //    Article a2 = new Article(2, "", "", false, 20, 50, t2, null, 2);
        //    Article a3 = new Article(3, "", "", false, 20, 25, t3, null, 2);
        //    comTest.addArticle(a1);
        //    comTest.addArticle(a2);
        //    comTest.addArticle(a3);


        //    //RecuPaiement rp = new RecuPaiement(comTest);
        //    //rp.printRecu();

        //    // MessageBox.Show( + "");
        //    TicketVetement tv = new TicketVetement(comTest);
        //    tv.printTicketVetement(a1, comTest.id, c);
        //    //String s=null;

        //    //for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        //    //{
        //    //    s += PrinterSettings.InstalledPrinters[i] + "\n";

        //    //}

        //    //MessageBox.Show(s);
        //}

        private void ExecuteCommandeClientPro(UnClientPROVM obj)
        {
            try
            {
                ClasseGlobale._renduCommandeClientPro = obj.commande;
                ClasseGlobale.Client = obj.commande.client;
                Label_CommandeSelectionner = "Client PRO: " + ClasseGlobale.Client.nom + ",     N° Commande : " + ClasseGlobale._renduCommandeClientPro.id+
                    "     TOTAL : " + obj.Label_Detail_prixAPayer;
             
                Label_ClientProUC_TotalAPayer = obj.Label_Detail_prixAPayer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        
        }
        private void ExecuteClientProCommandeCourante(UnClientPROVM obj)
        {
            DetailCommandeClientPro = new List<UnClientPROVM>();
            

            if (obj.clt.type == 1)
            {
                List<Commande> listeCommandeClientPro = (List<Commande>)CommandeDAO.selectCommandesByClient(obj.clt.id, true, true, true);

                foreach (Commande com in listeCommandeClientPro)
                {
                    prixApayer = 0;
                    foreach (Article ar in com.listArticles)
                    {
                        prixApayer = (float)((decimal)prixApayer + (decimal)ar.TTC);
                    }
                    foreach (Payement pa in com.listPayements)
                    {
                        prixApayer = (float)((decimal)prixApayer - (decimal)pa.montant);
                    }

                    DetailCommandeClientPro.Add(new UnClientPROVM()
                    {
                        commande = com,
                        Label_Detail_NombresArt = com.listArticles.Count,
                        Label_Detail_prixAPayer = prixApayer
                    });
                }
            }
        }


        public void clientsPro()
        {
            ListeClientPro = new List<UnClientPROVM>();
            try
            {

                List<Client> listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();

                if (listedesclientproDTO != null)
                {
                    foreach (Client cl in listedesclientproDTO)
                    {
                        ListeClientPro.Add(new UnClientPROVM() { clt = cl });
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "Error" });

            }
        }

        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
