using App_pressing_Loreau.ViewModel;
using System;
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
            if (PaiementVM.commandePayee != 0)
            {
                dp.Children.Clear();
                //AccueilVM ac = new AccueilVM();
                //ac.Btn_receptionColor = Brushes.Teal;
            }
        }

    }
}
