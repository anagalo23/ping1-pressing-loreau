using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;

namespace App_pressing_Loreau.ViewModel
{
    class ConvoyeurVM : ObservableObject, IPageViewModel
    {


        #region Attributes
        private int _label_Convoyeur_nbPlace;
        private int _label_convoyeur_diponibles;
        private int _txb_Convoyeur_idCommande;

        private List<ItemEmplacement> _contenuConvoyeur;

        List<PlaceConvoyeur> listePlace = null;
        #endregion
        public String Name
        {
            get { return ""; }
        }

        #region Constructeur
        public ConvoyeurVM()
        {
            try
            {
                listePlace = (List<PlaceConvoyeur>)PlaceConvoyeurDAO.selectConvoyeurs();

                List<PlaceConvoyeur> comptePlaceLibre = (List<PlaceConvoyeur>)PlaceConvoyeurDAO.selectConvoyeursEmpty();

                Label_convoyeur_diponibles = comptePlaceLibre.Count;
                Label_Convoyeur_nbPlace = listePlace.Count;
            }
            catch (Exception e)
            {

            }
        }
        #endregion


        #region Properties and commands

        public int Label_Convoyeur_nbPlace
        {
            get { return _label_Convoyeur_nbPlace; }
            set
            {
                if (value != _label_Convoyeur_nbPlace)
                {
                    _label_Convoyeur_nbPlace = value;
                    OnPropertyChanged("Label_Convoyeur_nbPlace");
                }
            }

        }

        public int Txb_Convoyeur_idCommande
        {
            get { return _txb_Convoyeur_idCommande; }
            set
            {
                if (value != _txb_Convoyeur_idCommande)
                {
                    _txb_Convoyeur_idCommande = value;
                    OnPropertyChanged("Txb_Convoyeur_idCommande");
                }
            }
        }


        public int Label_convoyeur_diponibles
        {
            get { return _label_convoyeur_diponibles; }
            set
            {
                if (value != _label_convoyeur_diponibles)
                {
                    _label_convoyeur_diponibles = value;
                    OnPropertyChanged("Label_convoyeur_diponibles");
                }
            }

        }

        public List<ItemEmplacement> ContenuConvoyeur
        {
            get { return _contenuConvoyeur; }
            set
            {
                _contenuConvoyeur = value;
                RaisePropertyChanged("ContenuConvoyeur");
            }
        }

        public ICommand Btn_Convoyeur_ok
        {
            get
            {
                return new RelayCommand(
                    p => convoyeurContenu());
            }
        }

        #endregion



        #region methods

        public void convoyeurContenu()
        {
            try
            {
                ContenuConvoyeur = new List<ItemEmplacement>();
                if (Txb_Convoyeur_idCommande != 0)
                {
                    Commande commande = CommandeDAO.selectCommandeById(Txb_Convoyeur_idCommande, false, true, false);

                    foreach (Article art in commande.listArticles)
                    {
                        ContenuConvoyeur.Add(new ItemEmplacement() { Label_Convoyeur_NomArticle = art.type.nom, 
                            Label_Convoyeur_referenceCommande = "Ref Commmande:   " + art.fk_commande, 
                            Label_Convoyeur_Emplacement = art.convoyeur.emplacement });
                    }

                }
                else
                {



                    Commande com = CommandeDAO.lastCommande();

                    com = CommandeDAO.selectCommandeById(com.id, false, true, false);
                    foreach (Article art in com.listArticles)
                    {
                        ContenuConvoyeur.Add(new ItemEmplacement() { Label_Convoyeur_NomArticle = art.type.nom, 
                            Label_Convoyeur_referenceCommande = "Ref Commmande:   " + art.fk_commande, 
                            Label_Convoyeur_Emplacement = art.convoyeur.emplacement });
                    }

                }
                Txb_Convoyeur_idCommande = 0;

            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }

        }
        #endregion
    }

    #region Class

    public class ItemEmplacement
    {
        public String Label_Convoyeur_NomArticle { get; set; }
        public int Label_Convoyeur_Emplacement { get; set; }
        public String Label_Convoyeur_referenceCommande { get; set; }

    }
    #endregion
}
