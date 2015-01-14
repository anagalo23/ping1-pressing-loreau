﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class IdentificationClientVM : ObservableObject, IPageViewModel
    {

        #region Variables

        private List<IdentificationClientData> _resultatRecherche_identificationClient;
     

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

        public IdentificationClientVM(){
            resultatName();
        }

         #region Properties 
     
        public String txb_identificationClient_nom
        {
            get { return _txt_identificationClient_nom; }
            set
            {
                if (value != _txt_identificationClient_nom)
                {
                    _txt_identificationClient_nom = value;
                    OnPropertyChanged("txb_identificationClient_nom");
                }
            }
        }

        public String txb_identificationClient_prenom
        {
            get { return _txt_identificationClient_prenom; }
            set
            {
                if (value != _txt_identificationClient_prenom)
                {
                    _txt_identificationClient_prenom = value;
                    OnPropertyChanged("txb_identificationClient_prenom");
                }
            }
        }

        public String txb_identificationClient_portable
        {
            get { return _txt_identificationClient_portable; }
            set
            {
                if (value != _txt_identificationClient_portable)
                {
                    _txt_identificationClient_portable = value;
                    OnPropertyChanged("txb_identificationClient_portable");
                }
            }
        }

        public String txb_identificationClient_adresse
        {
            get { return _txt_identificationClient_adresse; }
            set
            {
                if (value != _txt_identificationClient_adresse)
                {
                    _txt_identificationClient_adresse = value;
                    OnPropertyChanged("txb_identificationClient_adresse");
                }
            }
        }

        public String txb_identificationClient_date_naissance
        {
            get { return _txt_identificationClient_date_naissance; }
            set
            {
                if (value != _txt_identificationClient_date_naissance)
                {
                    _txt_identificationClient_date_naissance = value;
                    OnPropertyChanged("txb_identificationClient_date_naissance");
                }
            }
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

    
        //public ICommand ResultatRechercheClient
        //{
        //    get { return new RelayCommand(P => resultatName()); }
        //}
       
         #endregion


        #region methodes
        private void resultatName()
        {
            ResultatRecherche_identificationClient = new List<IdentificationClientData>();

            ResultatRecherche_identificationClient.Add(new IdentificationClientData() { ButtonClientContent = "NAGALO", ButtonClientTag = 2 });
            ResultatRecherche_identificationClient.Add(new IdentificationClientData() { ButtonClientContent = "NAGALO", ButtonClientTag = 2 });
            ResultatRecherche_identificationClient.Add(new IdentificationClientData() { ButtonClientContent = "NAGALO", ButtonClientTag = 2 });

        }
        #endregion



        //Suite 
    }
}
