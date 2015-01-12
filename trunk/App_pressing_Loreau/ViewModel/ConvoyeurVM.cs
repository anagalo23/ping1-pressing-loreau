using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data;

namespace App_pressing_Loreau.ViewModel
{
    class ConvoyeurVM : ObservableObject, IPageViewModel
    {

        
        #region Attributes 

        private int _txb_Convoyeur_nbPlace;
        private String _label_convoyeur_diponibles;
        private List<String> _contenuConvoyeur;

        List<PlaceConvoyeur> listePlace = null;

        PlaceConvoyeur place = null;

        #endregion
        public String Name
        {
            get { return ""; }
        }

        public ConvoyeurVM()
        {
            listePlace=(List<PlaceConvoyeur>)PlaceConvoyeurDAO.selectConvoyeurs();
            Label_convoyeur_diponibles = listePlace.Count.ToString();
         
           
        }

        #region Properties and commands

        public int Txb_Convoyeur_nbPlace
        {
            get { return _txb_Convoyeur_nbPlace; }
            set
            {
                if (value != _txb_Convoyeur_nbPlace)
                {
                    _txb_Convoyeur_nbPlace = value;
                    OnPropertyChanged("Txb_Convoyeur_nbPlace");
                }
            }

        }


        public String Label_convoyeur_diponibles
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

        public List<String> ContenuConvoyeur
        {
            get { return _contenuConvoyeur; }
            set
            {
                _contenuConvoyeur = value;
                RaisePropertyChanged("ContenuConvoyeur");
            }
        }

        public ICommand Btn_convoyeur_validerRecherche
        {
            get { return new RelayCommand(
                p=>convoyeurContenu(),
                p=>Txb_Convoyeur_nbPlace>0 & Txb_Convoyeur_nbPlace<30); }
        }

        #endregion



        #region methods

        public void convoyeurContenu()
        {
            place = (PlaceConvoyeur)PlaceConvoyeurDAO.selectTypeById(Txb_Convoyeur_nbPlace);
            ContenuConvoyeur = new List<string>();
            //ContenuConvoyeur.Add(place.ToString());
 
        }
        #endregion
    }
}
