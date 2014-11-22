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
using ProjetLoreau.View;

namespace ProjetLoreau
{

    
    /// <summary>
    /// Logique d'interaction pour BaniereAccueil.xaml
    /// </summary>
    /// 
    public partial class BanniereAccueil
    {

        Accueil accueil;
        public BanniereAccueil()
        {
            InitializeComponent();
        }

       /* private void btn_banniereaccueil_reception_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("msg");
            accueil = new Accueil();
            accueil.DP.Children.Clear(); 
            accueil.DP.Children.Add(new IdentificationClient());
        }*/

        private void btn_banniereaccueil_rendu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_banniereaccueil_facture_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_banniereaccueil_convoyeur_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_banniereaccueil_client_pro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_banniereaccueil_impression_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_banniereaccueil_administrateur_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
