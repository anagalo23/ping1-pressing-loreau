using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;
using System.Windows.Input;



namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour RestitutionClient.xaml
    /// </summary>
    public partial class RestitutionArticles : UserControl
    {


        public RestitutionArticles()
        {
            InitializeComponent();
        }


        private void btn_restitutionArticles_suivant_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new DetailCommande());
        }

        private void btn_restitutionArticles_ok_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void txb_restitutionArticles_idFactures_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key >= Key.D0 && e.Key <= Key.D9) ; // it`s number
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ; // it`s number
            //else if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.LeftCtrl ||
            //    e.Key == Key.LWin || e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.RightCtrl || e.Key == Key.RightShift ||
            //    e.Key == Key.Left || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.Return || e.Key == Key.Delete ||
            //    e.Key == Key.System) ; // it`s a system key (add other key here if you want to allow)
            else
                e.Handled = true; // the key will sappressed
        }

        private void txb_restitutionArticles_choix_KeyDown(object sender, KeyEventArgs e)
        {

            //if (cbb_restitutionClient_choix_theme.SelectedItem.ToString() != "nom")
            //{

        }




    }
}
