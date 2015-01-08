using System;
using System.Windows.Input;
using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;
using System.Windows.Controls;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour NouvelleCommande.xaml
    /// </summary>
    public partial class NouvelleCommande
    {
        public NouvelleCommande()
        {
            InitializeComponent();
        }

        private void btn_nouvelleCommande_paiement_immediat_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new Paiement());
        }

       
       
      
    }
}
