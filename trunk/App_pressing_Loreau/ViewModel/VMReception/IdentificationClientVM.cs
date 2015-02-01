using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientVM : ObservableObject, IPageViewModel
    {

        #region Variables

        private List<IdentificationClientData> _resultatRecherche_identificationClient;


        private String _label_identClient_choix;
        private DelegateCommand<IdentificationClientData> _resultatRechercheClient;

        #endregion

        public string Name
        {
            get { return ""; }
        }

        public IdentificationClientVM()
        {
            ClasseGlobale.Client = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._renduCommande = null;
            ClasseGlobale._renduCommandeClientPro = null;
            ClasseGlobale._contentDetailCommande = null;

        }

        #region Properties

        public String Label_identClient_choix
        {
            get { return _label_identClient_choix; }
            set
            {
                if (value != _label_identClient_choix)
                {
                    _label_identClient_choix = value;
                    OnPropertyChanged("Label_identClient_choix");
                }
            }
        }

       
        public ICommand Btn_idenClient_recherche
        {
            get { return new RelayCommand(p => rechercheBDD()); }
        }

        public List<IdentificationClientData> ResultatRecherche_identificationClient
        {
            get { return _resultatRecherche_identificationClient; }
            set
            {
                if (value != _resultatRecherche_identificationClient)
                {
                    _resultatRecherche_identificationClient = value;
                    OnPropertyChanged("ResultatRecherche_identificationClient");
                }
            }
        }

        public DelegateCommand<IdentificationClientData> ResultatRechercheClient
        {
            get
            {
                return this._resultatRechercheClient ?? (this._resultatRechercheClient = new DelegateCommand<IdentificationClientData>(
                                                                       this.ExecuteAddClient,
                                                                       (arg) => true));
            }
        }

     
       

        #endregion


        #region methodes

        private void ExecuteAddClient(IdentificationClientData obj)
        {

            if (ClasseGlobale.Client!=obj.clt)
                
            {
                ClasseGlobale.Client = obj.clt;
                
            }

            if (ClasseGlobale.Client != null)
            {
                Label_identClient_choix = "Choix = " + ClasseGlobale.Client.nom + " " + ClasseGlobale.Client.prenom;
            }
        }


        public void rechercheBDD()
        {
            //On recherche dans la bdd en fonction des champs que l'utilisateur à entré
            Fields fields = AutoComplete.getFields();
            ResultatRecherche_identificationClient = new List<IdentificationClientData>();

            List<Client> resultat = ClientDAO.seekClients(fields.nom, fields.prenom, fields.portable,fields.idCleaWay);


            //On affiche le résultat dans le doc Panel
            if (resultat != null)
            {
                foreach (Client c in resultat)
                {
                    ResultatRecherche_identificationClient.Add(new IdentificationClientData() {clt=c});
                }
            }
            else
            {
                MessageBox.Show("recherche infructueuse");
            }
        }
        #endregion

    }


    #region Class

    public class AutoComplete
    {

        static Fields fields;
        public static Fields getFields()
        {

            if (fields == null)
            {
                fields = new Fields();
                return fields;
            }
            else
            {
                return fields;
            }

        }

        public static string parseFromClassToMessage(string classeuh)
        {
            string[] salutTableau = classeuh.Split(':');
            string contenuDuChampDeText = salutTableau[salutTableau.Length - 1];
            return contenuDuChampDeText;
        }

    }

    public class Fields
    {
        public String nom { get; set; }
        public String prenom { get; set; }
        public String portable { get; set; }
        public int idCleaWay { get; set; }
        public String adresse { get; set; }
        public String dateDeNaissance { get; set; }
    }
    #endregion
}
