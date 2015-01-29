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
using Microsoft.Practices.Prism.Commands;


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

        public List<CommandeConcernantRA_DATA> _contentCommandeConcernant;
        public List<CommandeConcernantRA_DATA> _listeRechercheClient;

        private DelegateCommand<CommandeConcernantRA_DATA> _getButtonRecherche;
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

        public List<CommandeConcernantRA_DATA> ContentCommandeConcernant
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

        public DelegateCommand<CommandeConcernantRA_DATA> GetButtonRecherche
        {
            get
            {
                return this._getButtonRecherche ?? (this._getButtonRecherche = new DelegateCommand<CommandeConcernantRA_DATA>(
                                                                       this.ExecuteResultatRechercheClient,
                                                                       (arg) => true));
            }
        }

        private DelegateCommand<CommandeConcernantRA_DATA> commandeParIdFacture;
        public DelegateCommand<CommandeConcernantRA_DATA> CommandeParIdFacture
        {
            get
            {
                return this.commandeParIdFacture ?? (this.commandeParIdFacture = new DelegateCommand<CommandeConcernantRA_DATA>(
                   this.ValiderCetteCommande,
                   (arg) => true));
            }
        }
        #endregion

        #region Méthodes
        private void ExecuteResultatRechercheClient(CommandeConcernantRA_DATA obj)
        {
            MessageBox.Show("resultat: " + obj.clt.nom + " et Id = " + obj.clt.id);
        }


        private void ValiderCetteCommande(CommandeConcernantRA_DATA obj)
        {
            MessageBox.Show(obj.command.id +"");
        }
        public void ContenuDeLaCommande()
        {
            ContentCommandeConcernant = new List<CommandeConcernantRA_DATA>();

            Commande commandeRendre = (Commande)CommandeDAO.selectCommandeById(Txb_restitutionArticles_idFactures, false,true,true);

            if (commandeRendre != null)
            {
                ContentCommandeConcernant.Add(new CommandeConcernantRA_DATA() { Label_restitutionArticles_Reference = commandeRendre.id,
                    Label_restitutionArticles_Name =commandeRendre.client.nom,Label_restitutionArticles_DateCommande=commandeRendre.date.ToString() });

            }
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
                            clt=clt
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
