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
    /// Logique d'interaction pour AdministrationConvoyeur.xaml
    /// </summary>
    public partial class AdministrationConvoyeur 
    {
        public AdministrationConvoyeur()
        {
            InitializeComponent();
        }
        public void btn_admin_commandes_editer_Click(Object sender, RoutedEventArgs e)
        {

        }

        private void btn_admin_convoyeur_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }
    }
}
