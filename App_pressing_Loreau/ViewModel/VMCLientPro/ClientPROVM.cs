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
       
        #endregion

        #region constructeur
        public ClientPROVM()
        {
            //clientsPro();
            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
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


        public ICommand TestImprimante
        {
            get { return new RelayCommand(p => testPrint()); }
        }
        #endregion

        #region Methods

        //public  PrinterSettings.StringCollection inPrint { get; }

        private void testPrint()
        {

            Client c = new Client(1, "TRAORE", "Mami", "", "", null, DateTime.Now, "", DateTime.Now, 10, false, false, 0);
            Commande comTest = new Commande(2, DateTime.Now, false, 0, c);
            Departement dep = new Departement(1, "Classique");
            TypeArticle t = new TypeArticle(1, "jupe", 1, 20, 5, dep);
            TypeArticle t2 = new TypeArticle(2, "robe", 1, 20, 50, dep);
            TypeArticle t3 = new TypeArticle(3, "habit", 1, 20, 25, dep);

            Article a1 = new Article(1, "", "", false, 20, 5, t, null, 2);
            Article a2 = new Article(2, "", "", false, 20, 50, t2, null, 2);
            Article a3 = new Article(3, "", "", false, 20, 25, t3, null, 2);
            comTest.addArticle(a1);
            comTest.addArticle(a2);
            comTest.addArticle(a3);


            RecuPaiement rp = new RecuPaiement(comTest);
            rp.printRecu();

           // MessageBox.Show( + "");
            //TicketVetement tv = new TicketVetement(comTest);
            //tv.printRecu(a1, comTest.id, c);
            //String s=null;

            //for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            //{
            //    s += PrinterSettings.InstalledPrinters[i] + "\n";
               
            //}

            //MessageBox.Show(s);
        }

        private void ExecuteCommandeClientPro(UnClientPROVM obj)
        {
            //MessageBox.Show(obj.commande.date.ToString());
            ClasseGlobale._renduCommandeClientPro = obj.commande;
            ClasseGlobale.Client = obj.clt;
        }
        private void ExecuteClientProCommandeCourante(UnClientPROVM obj)
        {
            DetailCommandeClientPro = new List<UnClientPROVM>();

            if (obj.clt.type == 1)
            {
                List<Commande> listeCommandeClientPro = (List<Commande>)CommandeDAO.selectCommandesByClient(obj.clt.id, false, true, true);

                foreach (Commande com in listeCommandeClientPro)
                {
                    DetailCommandeClientPro.Add(new UnClientPROVM() { commande = com });
                }

            }
            //MessageBox.Show(obj.clt.id + " " + obj.clt.nom);
        }


        public void clientsPro()
        {
            ListeClientPro = new List<UnClientPROVM>();
            List<Client> listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();

            if (listedesclientproDTO != null)
            {
                foreach (Client cl in listedesclientproDTO)
                {
                    ListeClientPro.Add(new UnClientPROVM() { clt = cl, NombreCommande_clientPro = 10 + "" });
                }

            }
            else
            {
                //ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "Esigelec", NombreCommande_clientPro = 10 + "" });
                //ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "SNCF", NombreCommande_clientPro = 10 + "" });

            }
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
