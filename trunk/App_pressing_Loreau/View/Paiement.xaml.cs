using App_pressing_Loreau.Data;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour Paiement.xaml
    /// </summary>
    public partial class Paiement : UserControl
    {
        public Paiement()
        {
            InitializeComponent();
            DataContext = new PaiementVM();
        }

        private void btn_paiment_valider_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
