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

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques : UserControl
    {
        public Statistiques()
        {
            InitializeComponent();
            // Compute x array of 1001 points from 0 to 100 with 0.1 step
            var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();

            // Compute y array as sin(x)/x function defined on x grid
            var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();

            // Plot data 
           // linegraph.Plot(x, y);
        }

     

        private void btn_statistique_du_jour_Click_1(object sender, RoutedEventArgs e)
        {
       
        }

        private void btn_statistiques_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }
    }
}
