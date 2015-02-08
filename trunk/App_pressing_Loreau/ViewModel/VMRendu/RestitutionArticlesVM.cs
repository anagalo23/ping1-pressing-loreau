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

        private int _txb_restitutionArticles_idFactures;
        private string _txb_restitutionArticles_choix;

        ChoixBox choixbox = new ChoixBox();
        private ChoixBox _selected_restitutionClient_choix_theme;
        public List<ChoixBox> Cbb_restitutionClient_choix_theme { get; set; }

        public List<CommandeConcernantRA_DATA> _contentCommandeConcernant;
        public List<CommandeConcernantRA_DATA> _listeRechercheClient;
        private DelegateCommand<CommandeConcernantRA_DATA> commandeParIdFacture;
        private DelegateCommand<CommandeConcernantRA_DATA> _getButtonRecherche;

        private String _label_commandeSelectionner;
        #endregion

        #region Accesseurs et mutateurs des attributs qui ne sont pas propriétés
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
        #endregion

        public String Name
        {
            get { return ""; }

        }

        #region Constructeur
        public RestitutionArticlesVM()
        {
            Cbb_restitutionClient_choix_theme = choixbox.ListeChamp();
            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._contentDetailCommande = null;

        }


        #endregion


        #region Propriétés
        public int Txb_restitutionArticles_idFactures
        {
            get { return _txb_restitutionArticles_idFactures; }
            set
            {

                _txb_restitutionArticles_idFactures = value;
                OnPropertyChanged("Txb_restitutionArticles_idFactures");


            }
        }

        public String Label_CommandeSelectionner
        {
            get { return _label_commandeSelectionner; }
            set
            {
                if (value != _label_commandeSelectionner)
                {
                    _label_commandeSelectionner = value;
                    OnPropertyChanged("Label_CommandeSelectionner");
                }
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

        #endregion


        #region Commandes

        #region Champ de recherche par id de commande

        public ICommand Btn_restitutionArticles_ok
        {
            get
            {
                return new RelayCommand(
                    p => ContenuDeLaCommande(),
                    p => Txb_restitutionArticles_idFactures > 0);
            }

        }
        public void ContenuDeLaCommande()
        {
            if (Txb_restitutionArticles_idFactures > 0)
            {
                Commande commandeRendre = (Commande)CommandeDAO.selectCommandeById(Txb_restitutionArticles_idFactures, false, true, true);
                if (commandeRendre.id != 0)
                {
                    ContentCommandeConcernant = new List<CommandeConcernantRA_DATA>();
                    ContentCommandeConcernant.Add(new CommandeConcernantRA_DATA()
                    {
                        Label_restitutionArticles_Reference = commandeRendre.id,
                        Label_restitutionArticles_DateCommande = commandeRendre.date.ToString(),
                        commande = commandeRendre,
                        Label_restitutionArticles_nomDuClient = commandeRendre.client.nom + "  " + commandeRendre.client.prenom
                    });
                }
                else
                {
                    MessageBox.Show("Cette Commande n'existe pas");
                }
            }
            else
            {
                MessageBox.Show("Un id de commande ne peut pas être négatif ou nul");
            }
        }

        #endregion

        #region Recherche par nom ou prénom pour afficher une liste de clients

        
        public ICommand Btn_restitutionArticles_valider
        {
            get
            {
                return new RelayCommand(
                    p => ContenuDeLaRecherche(),
                    p => Txb_restitutionArticles_choix != null);
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
                            clt = clt
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



        #region Recherche des commandes en cours d'un client
        public DelegateCommand<CommandeConcernantRA_DATA> GetButtonRecherche
        {
            get
            {
                return this._getButtonRecherche ?? (this._getButtonRecherche = new DelegateCommand<CommandeConcernantRA_DATA>(
                                                                       this.ExecuteResultatRechercheClient,
                                                                       (arg) => true));
            }
        }
        private void ExecuteResultatRechercheClient(CommandeConcernantRA_DATA obj)
        {
            //MessageBox.Show("resultat: " + obj.clt.nom + " et Id = " + obj.clt.id);

            ContentCommandeConcernant = new List<CommandeConcernantRA_DATA>();

            if (obj.clt.type == 0)
            {
                List<Commande> listeCommande = (List<Commande>)CommandeDAO.selectCommandesByClient(obj.clt.id, false, true, true);
                if (listeCommande != null)
                {
                    foreach (Commande com in listeCommande)
                    {
                        ContentCommandeConcernant.Add(new CommandeConcernantRA_DATA()
                        {
                            Label_restitutionArticles_Reference = com.id,
                            Label_restitutionArticles_DateCommande = com.date.ToString(),
                            commande = com,
                            Label_restitutionArticles_nomDuClient = com.client.nom + "  " + com.client.prenom
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Ce client n'a pas de commande");
                }
            }

        }
        #endregion


        #region Sélection d'une commande
        public DelegateCommand<CommandeConcernantRA_DATA> CommandeParIdFacture
        {
            get
            {
                return this.commandeParIdFacture ?? (this.commandeParIdFacture = new DelegateCommand<CommandeConcernantRA_DATA>(
                   this.ValiderCetteCommande,
                   (arg) => true));
            }
        }
        private void ValiderCetteCommande(CommandeConcernantRA_DATA obj)
        {
            //MessageBox.Show(obj.commande.id +"");
            ClasseGlobale._renduCommande = obj.commande;
            ClasseGlobale.Client = obj.clt;
            Label_CommandeSelectionner = ClasseGlobale._renduCommande.id.ToString();
            //MessageBox.Show();
        }
        #endregion

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
