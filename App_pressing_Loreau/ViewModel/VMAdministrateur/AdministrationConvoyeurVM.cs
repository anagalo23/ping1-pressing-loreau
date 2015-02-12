using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationConvoyeurVM : ObservableObject
    {

        #region Attributes
        private int _label_AdminConv_NbrePlace;
        private int _label_AdminConv_Disponibles;
        private int _txb_AdminConv_ModifPlace;

        List<PlaceConvoyeur> listePlace = null;

        #endregion

        #region Constructeur
        public AdministrationConvoyeurVM()
        {
            try
            {
                listePlace = (List<PlaceConvoyeur>)PlaceConvoyeurDAO.selectConvoyeurs();
                Label_AdminConv_NbrePlace = listePlace.Count;
             
                

                List<PlaceConvoyeur> comptePlaceLibre = (List<PlaceConvoyeur>)PlaceConvoyeurDAO.selectConvoyeursNotEmpty();

                Label_AdminConv_Disponibles = comptePlaceLibre.Count;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }
        }
        #endregion

        #region Properties and commands

        public int Label_AdminConv_NbrePlace
        {
            get { return _label_AdminConv_NbrePlace; }
            set
            {
                if (value != _label_AdminConv_NbrePlace)
                {
                    _label_AdminConv_NbrePlace = value;
                    OnPropertyChanged("Label_AdminConv_NbrePlace");
                }
            }
        }
        public int Label_AdminConv_Disponibles
        {
            get { return _label_AdminConv_Disponibles; }
            set
            {
                if (value != _label_AdminConv_Disponibles)
                {
                    _label_AdminConv_Disponibles = value;
                    OnPropertyChanged("Label_AdminConv_Disponibles");
                }
            }
        }

        public int Txb_AdminConv_ModifPlace
        {
            get { return _txb_AdminConv_ModifPlace; }
            set
            {
                if (value != _txb_AdminConv_ModifPlace)
                {
                    _txb_AdminConv_ModifPlace = value;
                    OnPropertyChanged("Txb_AdminConv_ModifPlace");
                }
            }
        }

        public ICommand Btn_AdminConv_EnregistrerPlace
        {
            get { return new RelayCommand(p => enregistrerPlace(),p=>Txb_AdminConv_ModifPlace>20); }
        }
        #endregion

        #region Methods

        public void enregistrerPlace()
        {


            try
            {
                int nbrPlace = PlaceConvoyeurDAO.selectConvoyeurs().Count;

                if (Txb_AdminConv_ModifPlace > nbrPlace)
                {
                    for (int i = nbrPlace; i <Txb_AdminConv_ModifPlace; i++)
                    {
                        PlaceConvoyeur p = new PlaceConvoyeur(i,0);
                        PlaceConvoyeurDAO.insertConvoyeur(p);
                    }
                }
                else 
                {

                    MessageBox.Show("Impossible de diminuer le nombre d'emplacement dans le convoyeur");
                }

                Label_AdminConv_NbrePlace = PlaceConvoyeurDAO.selectConvoyeurs().Count;
                Label_AdminConv_Disponibles = PlaceConvoyeurDAO.selectConvoyeursNotEmpty().Count;

                Txb_AdminConv_ModifPlace = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(""+e);
            }


        }
        #endregion
    }
}
