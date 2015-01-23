﻿using System;
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

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientVM : ObservableObject, IPageViewModel
    {

        #region Variables

        private List<IdentificationClientData> _resultatRecherche_identificationClient;

        //Client client = ClasseGlobale.client;
        private String _txt_identificationClient_nom;
        private String _txt_identificationClient_prenom;
        private String _txt_identificationClient_portable;
        private String _txt_identificationClient_adresse;
        private String _txt_identificationClient_date_naissance;



        #endregion

        public string Name
        {
            get { return ""; }
        }

        public IdentificationClientVM()
        {
            //rechercheBDD();

        }

        #region Properties

        public String Txb_identificationClient_nom
        {
            get { return _txt_identificationClient_nom; }
            set
            {
                if (value != _txt_identificationClient_nom)
                {
                    _txt_identificationClient_nom = value;
                    OnPropertyChanged("Txb_identificationClient_nom");
                }
            }
        }

        public String Txb_identificationClient_prenom
        {
            get { return _txt_identificationClient_prenom; }
            set
            {
                if (value != _txt_identificationClient_prenom)
                {
                    _txt_identificationClient_prenom = value;
                    OnPropertyChanged("Txb_identificationClient_prenom");
                }
            }
        }

        public String Txb_identificationClient_portable
        {
            get { return _txt_identificationClient_portable; }
            set
            {
                if (value != _txt_identificationClient_portable)
                {
                    _txt_identificationClient_portable = value;
                    OnPropertyChanged("Txb_identificationClient_portable");
                }
            }
        }

        public String Txb_identificationClient_adresse
        {
            get { return _txt_identificationClient_adresse; }
            set
            {
                if (value != _txt_identificationClient_adresse)
                {
                    _txt_identificationClient_adresse = value;
                    OnPropertyChanged("Txb_identificationClient_adresse");
                }
            }
        }

        public String Txb_identificationClient_date_naissance
        {
            get { return _txt_identificationClient_date_naissance; }
            set
            {
                if (value != _txt_identificationClient_date_naissance)
                {
                    _txt_identificationClient_date_naissance = value;
                    OnPropertyChanged("Txb_identificationClient_date_naissance");
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



        ICommand resultatRechercheClient;
        public ICommand ResultatRechercheClient
        {
            get { return resultatRechercheClient ?? (resultatRechercheClient = new RelayCommand(choixClient)); }
        }
       

        #endregion


        #region methodes

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
                foreach (Client clt in resultat)
                {
                    ResultatRecherche_identificationClient.Add(new IdentificationClientData() { Label_idenClient_nom = clt.nom, 
                        ButtonClientTag = clt.id, Label_idenClient_prenom=clt.prenom,Label_identCleint_Adresse=clt.adresse.rue,
                        Label_identCleint_idCleanway=clt.idCleanWay });
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
        public String adresse { get; set; }
        public String dateDeNaissance { get; set; }
    }
    #endregion
}
