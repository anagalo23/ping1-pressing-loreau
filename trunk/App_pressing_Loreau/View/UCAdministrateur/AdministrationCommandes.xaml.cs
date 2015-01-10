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

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour AdministrationCommandes.xaml
    /// </summary>
    public partial class AdministrationCommandes 
    {
        public AdministrationCommandes()
        {
            InitializeComponent();
        }

        private void btn_admin_commandes_modifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_commandes_supprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_commandes_retour_Click_1(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }
    }
}
