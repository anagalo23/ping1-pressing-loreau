using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour Paiement.xaml
    /// </summary>
    public partial class Paiement{
        public Paiement()
        {
            InitializeComponent();
        }

        private void btn_paiement_RRR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_paiement_mode_paiement_supp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_paiment_payer_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new IdentificationClient());
        }
    }
}
