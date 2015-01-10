using System;
using System.Windows;
using System.Windows.Controls;

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour NouveauClientPro.xaml
    /// </summary>
    public partial class NouveauClientPro : UserControl
    {
        public NouveauClientPro()
        {
            InitializeComponent();
        }

        private void btn_nouveauClientPro_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new ClientProUC());
        }
    }
}
