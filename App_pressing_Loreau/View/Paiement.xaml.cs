using App_pressing_Loreau.ViewModel;
using System;
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
            if (true)
            {
                dp.Children.Clear();
                //On retourne à l'accueil
                //AccueilVM ac = new AccueilVM();
                MessageBox.Show("salut");
                try
                {
                    dp.Children.Add(new Accueil());
                }
                catch (InvalidOperationException ioe)
                {

                }
                
                //ac.Btn_receptionColor = Brushes.Teal;
            }
        }

    }
}
