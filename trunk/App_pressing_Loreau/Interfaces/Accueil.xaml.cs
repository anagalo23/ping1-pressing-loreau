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

namespace App_pressing_Loreau.Interfaces
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
        }
     

        private void btn_accueil_reception_Click(object sender, RoutedEventArgs e)
        {
            IdentificationClient identificationClient = new IdentificationClient();
            dp.Children.Clear();
            dp.Children.Add(identificationClient);
        }

        private void btn_accueil_rendu_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();


        }

        private void btn_accueil_facture_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();

        }

        private void btn_accueil_commandes_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();

        }

        private void btn_accueil_client_pro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_accueil_administrateur_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_accueil_convoyeur_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}
