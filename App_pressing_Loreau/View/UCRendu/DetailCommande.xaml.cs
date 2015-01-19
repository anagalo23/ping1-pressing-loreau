using App_pressing_Loreau.View;
using System;
using System.Windows;
using System.Windows.Controls;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour DetailCommande.xaml
    /// </summary>
    public partial class DetailCommande: UserControl 
    {
        public DetailCommande()
        {
            InitializeComponent();
        }

        private void btn_detailCommande_rendre_articles_selectionnes_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add( new Paiement());
        }
    }
}
