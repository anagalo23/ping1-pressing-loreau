using App_pressing_Loreau.Helper;
using App_pressing_Loreau.View;
using App_pressing_Loreau.ViewModel;
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
            DataContext = new DetailCommandeVM();
        }

        private void btn_detailCommande_rendre_articles_selectionnes_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            //Si la commande a déjà été payée je ne pase pas par la page de paiement
            if (ClasseGlobale._renduCommande.payee == false)
            {
                dp.Children.Add(new Paiement());
            }
            else
            {
                dp.Children.Add(new Accueil());
            }
            
        }
    }
}
