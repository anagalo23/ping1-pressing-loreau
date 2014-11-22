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

namespace ProjetLoreau
{
    /// <summary>
    /// Logique d'interaction pour AdministrationCaisse.xaml
    /// </summary>
    public partial class AdministrationCaisse 
    {
        public AdministrationCaisse()
        {
            InitializeComponent();
        }

        private void btn_admin_caisse_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }

        private void btn_admin_caisse_editer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_ajouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_supprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_supprimer1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_supprimer2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_supprimer3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_caisse_supprimer4_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
