﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;
using App_pressing_Loreau.Model;
using System.Windows;

namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// Cette classe permet de modifier le fond de caisse et de l'afficher 
    /// </summary>
    class AdministrationCaisseVM : ObservableObject
    {
        #region Attributes
        //private float _label_administrationCaisse_fonCaisse;
        private float _txb_AdminCaisse_modifFondCaisse;
        #endregion

        #region Constructeurs

        public AdministrationCaisseVM()
        {
            try
            {
                Label_administrationCaisse_fonCaisse = new float();

                CashProperties.openProperties();

                //ClasseGlobale.fondCaisse = new float();
                //ClasseGlobale.fondCaisse = 150;

                Label_administrationCaisse_fonCaisse = CashProperties.fondCaisse;

            }
            catch (Exception e)
            {
                MessageBox.Show("error:  " + e);
            }
           
        }
        #endregion

        #region Properties and commands

        public float Label_administrationCaisse_fonCaisse
        {
            get { return CashProperties.fondCaisse; }
            set
            {
                if (value != CashProperties.fondCaisse)
                {
                    CashProperties.fondCaisse = value;
                    OnPropertyChanged("Label_administrationCaisse_fonCaisse");
                }
            }
        }

        public float Txb_AdminCaisse_modifFOndCaisse
        {
            get { return _txb_AdminCaisse_modifFondCaisse; }
            set
            {
                if (value != _txb_AdminCaisse_modifFondCaisse)
                {
                    _txb_AdminCaisse_modifFondCaisse = value;
                    OnPropertyChanged("Txb_AdminCaisse_modifFOndCaisse");
                }
            }
        }

        public ICommand Btn_adminCaisse_valider
        {
            get { return new RelayCommand(p => ValiderFOndCaisse(), p => Txb_AdminCaisse_modifFOndCaisse>0); }
        }
        #endregion

        #region Methods

        private void ValiderFOndCaisse()
        {
            CashProperties.fondCaisse = Txb_AdminCaisse_modifFOndCaisse;
            Label_administrationCaisse_fonCaisse = Txb_AdminCaisse_modifFOndCaisse;

            Txb_AdminCaisse_modifFOndCaisse = 0;
        }
        #endregion
    }
}
