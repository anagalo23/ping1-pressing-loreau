using System;
using System.Windows;
using System.Windows.Controls;


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour IdentificationAdmin.xaml
    /// Verification de la connexion a la partie administrateur
    /// </summary>
    public partial class IdentificationAdmin : UserControl 
    {
        public IdentificationAdmin()
        {
            InitializeComponent();
        }

        private void btn_identificationAdmin_connecte_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (txb_identificationAdmin_identifiant.Text == "David" & pwb_identificationAdmin_mdp.Password == "Lefevre")
            //{
                dp.Children.Clear();
                dp.Children.Add(new PageAdministrateur());
            //}
            //else
            //{
            //    MessageBox.Show("Mot de passe oublié ?");
            //}
            
        }

       
      
    }
}
