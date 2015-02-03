﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.ViewModel;
using System.Collections.ObjectModel;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;

namespace App_pressing_Loreau.Helper
{
    class ClasseGlobale
    {
        public static void INITIALIZE_ALL()
        {
            initializeContentDetailCommande();
            initializeClient();
            initializeContenuListePaiement();
            _initializePlacesLibres();
        }

        public static void SET_ALL_NULL()
        {
            _contentDetailCommande = null;
            Client = null;
            _contenuListePaiement = null;
            _renduCommandeClientPro = null;
            _renduCommande = null;
            _rendreArticlesSelectionnes = null;
            PlacesLibres = null;
        }

        //*********************************************************************************GESTION DE LA LISTE DE COMMANDE
        public static ObservableCollection<ArticlesVM> _contentDetailCommande { get; set; }

        public static void initializeContentDetailCommande()
        {
            _contentDetailCommande = new ObservableCollection<ArticlesVM>();
        }

        //*********************************************************************************GESTION DU CLIENT EN COURS
        private static Client _client;
        public static Client Client
        {
            get
            {
                if (_client == null)
                {
                    initializeClient();
                }
                return _client;
            }
            set
            {
                _client = value;
            }
        }


        public static void initializeClient()
        {
            Client = new Client();
        }



        //*******************************************************************************GESTION DE LA LISTE DES EMPLOYES

        public static List<Employe> listeEmployes { get; set; }
        public static void getAllEmployee()
        {
            listeEmployes = (List<Employe>)EmployeDAO.selectEmployes();
        }


        //*********************************************************************************GESTION DE LA LISTE DES PAIEMENTS DESIRES
        public static ObservableCollection<PaiementListeVM> _contenuListePaiement;
        internal static void initializeContenuListePaiement()
        {
            _contenuListePaiement = new ObservableCollection<PaiementListeVM>();
        }

        //*********************************************************************************GESTION DES RENDU COMMANDE

        public static Commande _renduCommande { get; set; }


        public static CommandeConcernantRA_DATA _rendreArticlesSelectionnes { get; set; }


        //*********************************************************************************GESTION COMMANDES CLIENT PRO

        public static Commande _renduCommandeClientPro { get; set; }

        //*********************************************************************************GESTION DES PLACES LIBRES DU CONVOYEUR
        private static ConvoyeurPlacesLibres _placesLibres;

        public static ConvoyeurPlacesLibres PlacesLibres
        {
            get
            {
                if (_placesLibres == null)
                {
                    _initializePlacesLibres();
                }
                return _placesLibres;
            }
            set
            {
                if (_placesLibres == null)
                {
                    _initializePlacesLibres();
                }
                _placesLibres = value;
            }
        }
        public static void _initializePlacesLibres()
        {
            _placesLibres = new ConvoyeurPlacesLibres();
        }



    }

    //Contient la liste des places convoyeur qui sont libres est permet la manipulation dans la liste
    class ConvoyeurPlacesLibres
    {

        public static List<PlaceConvoyeur> _placesConvoyeurLibres;

        public static void _initializePlacesLibres()
        {
            _placesConvoyeurLibres = new List<PlaceConvoyeur>();
        }

        public PlaceConvoyeur this[int index]
        {
            get
            {
                return _placesConvoyeurLibres[index];
            }
            set
            {
                _placesConvoyeurLibres[index] = value;
            }
        }

        public List<PlaceConvoyeur> getList()
        {
            return _placesConvoyeurLibres;
        }
        public void setList(List<PlaceConvoyeur> list)
        {
            _placesConvoyeurLibres = list;
        }
    }
}
