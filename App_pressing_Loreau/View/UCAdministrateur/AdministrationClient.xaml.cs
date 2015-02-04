using App_pressing_Loreau.ViewModel;
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
    /// Logique d'interaction pour AdministrationClient.xaml
    /// </summary>
    public partial class AdministrationClient : UserControl
    {
        public AdministrationClient()
        {
            InitializeComponent();
            DataContext = new AdministrationClientVM();
        }


        private void txb_administrationClient_choix_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add( new PageAdministrateur());
        }
    }
}
