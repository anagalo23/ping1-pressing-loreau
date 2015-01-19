using System;
using System.Windows.Controls;


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour IdentificationAdmin.xaml
    /// </summary>
    public partial class IdentificationAdmin : UserControl 
    {
        public IdentificationAdmin()
        {
            InitializeComponent();
        }

        private void btn_identificationAdmin_connecte_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }

       
      
    }
}
