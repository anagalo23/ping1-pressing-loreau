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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void btn_reception_Click(object sender, RoutedEventArgs e)
        {
            new Recherche_client().Show();
        }

        private void btn_rendu_Click(object sender, RoutedEventArgs e)
        {
            new RestitutionArticle().Show();
        }

        private void btn_retour_client_Click(object sender, RoutedEventArgs e)
        {
            new Identification_client().Show();
        }

        private void btn_derniere_commande_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}
