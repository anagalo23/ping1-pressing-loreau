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

using App_pressing_Loreau.ViewModel;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour AdministrateurClientPro.xaml
    /// </summary>
    public partial class AdministrationClientPro : UserControl
    {
        public AdministrationClientPro()
        {
            InitializeComponent();
            DataContext = new AdministrationClientProVM();
            ClasseGlobale.Client = null;
        }

        private void btn_administrationClientPro_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }

        private void btn_administrationClientPro_nouvelleCommande_Click(object sender, RoutedEventArgs e)
        {
            if (ClasseGlobale.Client.nom != "")
            {
                dp.Children.Clear();
                dp.Children.Add(new NouvelleCommandeClientPro());
            }
       

        }

        private void btn_adminclientPro_nouveauclientPro_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouveauClientPro());

        }

        
    }
}
