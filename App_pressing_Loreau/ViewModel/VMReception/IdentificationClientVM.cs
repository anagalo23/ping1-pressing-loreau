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
        //private String _txt_identificationClient_prenom;
        //private String _txt_identificationClient_portable;
        //private String _txt_identificationClient_adresse;
        //private String _txt_identificationClient_date_naissance;

        private DelegateCommand<IdentificationClientData> _resultatRechercheClient;

        #endregion

        public string Name
        {
            get { return ""; }
        }

        public IdentificationClientVM()
        {
           

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

        //public String Txb_identificationClient_prenom
        //{
        //    get { return _txt_identificationClient_prenom; }
        //    set
        //    {
        //        if (value != _txt_identificationClient_prenom)
        //        {
        //            _txt_identificationClient_prenom = value;
        //            OnPropertyChanged("Txb_identificationClient_prenom");
        //        }
        //    }
        //}

        //public String Txb_identificationClient_portable
        //{
        //    get { return _txt_identificationClient_portable; }
        //    set
        //    {
        //        if (value != _txt_identificationClient_portable)
        //        {
        //            _txt_identificationClient_portable = value;
        //            OnPropertyChanged("Txb_identificationClient_portable");
        //        }
        //    }
        //}

        //public String Txb_identificationClient_adresse
        //{
        //    get { return _txt_identificationClient_adresse; }
        //    set
        //    {
        //        if (value != _txt_identificationClient_adresse)
        //        {
        //            _txt_identificationClient_adresse = value;
        //            OnPropertyChanged("Txb_identificationClient_adresse");
        //        }
        //    }
        //}

        //public String Txb_identificationClient_date_naissance
        //{
        //    get { return _txt_identificationClient_date_naissance; }
        //    set
        //    {
        //        if (value != _txt_identificationClient_date_naissance)
        //        {
        //            _txt_identificationClient_date_naissance = value;
        //            OnPropertyChanged("Txb_identificationClient_date_naissance");
        //        }
        //    }
        //}


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
            //Label_identClient_choix = new String(obj.clt.nom + " " + obj.clt.prenom , 10);
            if (ClasseGlobale.client!=obj.clt)
                
            {
                ClasseGlobale.client = obj.clt;
                
            }

            if (ClasseGlobale.client != null)
            {
                Label_identClient_choix = "Choix = " + ClasseGlobale.client.nom + " " + ClasseGlobale.client.prenom;
            }
        }


        private void choixClient(object button)
        {
            Button clickedbutton = button as Button;

            if (clickedbutton != null)
            {
                string msg = string.Format("You Pressed : {0} button", clickedbutton.Tag);
                MessageBox.Show(msg);
                clickedbutton.Background = Brushes.Red;
            }
        }

        public void rechercheBDD()
        {
            //On recherche dans la bdd en fonction des champs que l'utilisateur à entré
            Fields fields = AutoComplete.getFields();
            ResultatRecherche_identificationClient = new List<IdentificationClientData>();

            List<Client> resultat = ClientDAO.seekClients(fields.nom, fields.prenom, fields.portable);

            //MessageBox.Show(fields.nom);
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



        //Suite 
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
