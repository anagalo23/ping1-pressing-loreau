using System.Windows;
using System.Windows.Controls;


namespace App_pressing_Loreau.View
{
 
    /// <summary>
    /// Logique d'interaction pour IndentificationClient.xaml
    /// 
    /// </summary>
    public partial class IdentificationClient : UserControl
    {
       

        public IdentificationClient()
        {
            InitializeComponent();

        }


        private void btn_identClient_nouveau_client_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouveauClient());
        }

        private void btn_identClient_nouvelle_commande_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouvelleCommande());
        }

    }
}
