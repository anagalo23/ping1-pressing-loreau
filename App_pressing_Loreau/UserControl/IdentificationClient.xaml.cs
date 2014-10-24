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

using App_pressing_Loreau.Interfaces;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour IndentificationClient.xaml
    /// </summary>
    public partial class IdentificationClient
    {
        public IdentificationClient()
        {
            InitializeComponent();
            

        }

        private void btn_identClient_valider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_identClient_nouveau_client_Click(object sender, RoutedEventArgs e)
        {
            NouveauClient nouveauClient = new NouveauClient();
            dp.Children.Clear();
            dp.Children.Add(nouveauClient);

           
            
        }

    }
}
