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
using System.Windows.Shapes;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques
    {
        public Statistiques()
        {
            InitializeComponent();
        }

        private void btn_Statistique_Du_Jour_Click(object sender, RoutedEventArgs e)
        {
            NouveauClient nouveauClient = new NouveauClient();
            dp.Children.Clear();
            dp.Children.Add(nouveauClient);
        }

        private void btn_Statistique_De_La_Semaine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Statistique_Du_Mois_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Statistique_Des_14_Derniers_Mois_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
