using App_pressing_Loreau.ViewModel;
using System;
using System.Windows.Controls;


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

    }
}
