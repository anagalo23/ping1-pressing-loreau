using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;


namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// ViewModel pour la vue RestitutionArticles.xaml
    /// 
    /// </summary>
    class RestitutionArticlesVM : ObservableObject, IPageViewModel
    {
        #region Attributs
        Departement dep;

        private int _txb_restitutionArticles_idFactures;
        private string _txb_restitutionArticles_choix;

        ChoixBox choixbox = new ChoixBox();
        private ChoixBox _selected_restitutionClient_choix_theme;
        public List<ChoixBox> Cbb_restitutionClient_choix_theme { get; set; }

        public CommandeConcernantRA_DATA _contentCommandeConcernant;
        public List<CommandeConcernantRA_DATA> _listeRechercheClient;

        #endregion
        public String Name
        {
            get { return ""; }

        }
        #region Constructeur
        public RestitutionArticlesVM()
        {
            Cbb_restitutionClient_choix_theme = choixbox.ListeChamp();
        }


        #endregion


        #region Propriétés et commandes
        public int Txb_restitutionArticles_idFactures
        {
            get { return _txb_restitutionArticles_idFactures; }
            set
            {

                _txb_restitutionArticles_idFactures = value;
                OnPropertyChanged("Txb_restitutionArticles_idFactures");


            }
        }

        public String Txb_restitutionArticles_choix
        {
            get { return _txb_restitutionArticles_choix; }
            set
            {
                _txb_restitutionArticles_choix = value;
                OnPropertyChanged("Txb_restitutionArticles_choix");
            }

        }


        public ChoixBox Selected_restitutionClient_choix_theme
        {
            get { return _selected_restitutionClient_choix_theme; }
            set
            {
                _selected_restitutionClient_choix_theme = value;
                RaisePropertyChanged("Selected_restitutionClient_choix_theme");
            }
        }

        public ICommand Btn_restitutionArticles_ok
        {
            get
            {
                return new RelayCommand(
                    p => ContenuDeLaCommande(),
                    p => Txb_restitutionArticles_idFactures > 0);
            }

        }

        public ICommand Btn_restitutionArticles_valider
        {
            get
            {
                return new RelayCommand(
                    p => ContenuDeLaRecherche(),
                    p => Txb_restitutionArticles_choix != null);
            }
        }

        public CommandeConcernantRA_DATA ContentCommandeConcernant
        {
            get { return _contentCommandeConcernant; }
            set
            {
                if (value != _contentCommandeConcernant)
                {
                    _contentCommandeConcernant = value;
                    OnPropertyChanged("ContentCommandeConcernant");
                }
            }

        }

        public List<CommandeConcernantRA_DATA> ListeRechercheClient
        {
            get { return _listeRechercheClient; }
            set
            {
                _listeRechercheClient = value;
                OnPropertyChanged("ListeRechercheClient");
            }
        }

        #endregion

        #region Méthodes
        public void ContenuDeLaCommande()
        {
            ObservableCollection<ArticlesRestitutionVM> listeArt = new ObservableCollection<ArticlesRestitutionVM>();

            dep = (Departement)DepartementDAO.selectDepartementById(2);
            CommandeConcernantRA_DATA ccd = new CommandeConcernantRA_DATA();
            ccd.Label_restitutionArticles_Name = "Alexis";
            ccd.Label_restitutionArticles_Reference = "23678";
            listeArt.Add(new ArticlesRestitutionVM() { ArticlesNameRes = Txb_restitutionArticles_idFactures + "pt", Txb_ArticlesRes_etat = "Fini" });
            listeArt.Add(new ArticlesRestitutionVM() { ArticlesNameRes = Txb_restitutionArticles_idFactures + "pj", Txb_ArticlesRes_etat = "encours" });

            ccd.ListeArticlesRestitution = listeArt;
            ccd.Label_restitutionArticles_NombreArticles = Txb_restitutionArticles_idFactures;
            ContentCommandeConcernant = ccd;
        }

        public void ContenuDeLaRecherche()
        {
            ListeRechercheClient = new List<CommandeConcernantRA_DATA>();
            List<Client> resultat = null;
            if (Selected_restitutionClient_choix_theme != null)
            {


                if (Selected_restitutionClient_choix_theme.NameCbb.Equals("Nom"))
                {
                    resultat = ClientDAO.seekClients(Txb_restitutionArticles_choix, null, null, 0);
                }
                else if (Selected_restitutionClient_choix_theme.NameCbb.Equals("Prenom"))
                {
                    resultat = ClientDAO.seekClients(null, Txb_restitutionArticles_choix, null, 0);
                }
                else resultat = null;

                if (resultat != null)
                {
                    foreach (Client clt in resultat)
                    {
                        ListeRechercheClient.Add(new CommandeConcernantRA_DATA()
                        {
                            ContentButtonClientRA = clt.nom + " " + clt.prenom,
                            TagButtonClientRA = clt.id
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Pas de resultat ");
                }
            }
            else
            {
                MessageBox.Show("Choisissez un élement ");
            }


        }

        #endregion


        #region Classe

        public class ChoixBox
        {
            public String NameCbb { get; set; }
            public int cbbId { get; set; }

            public List<ChoixBox> ListeChamp()
            {
                List<ChoixBox> lstCb = new List<ChoixBox>();

                lstCb.Add(new ChoixBox() { cbbId = 1, NameCbb = "Nom" });
                lstCb.Add(new ChoixBox() { cbbId = 2, NameCbb = "Prenom" });

                return lstCb;
            }
        }



        #endregion



    }
}
