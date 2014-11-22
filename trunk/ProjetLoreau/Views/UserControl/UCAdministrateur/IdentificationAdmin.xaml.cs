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

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour IdentificationAdmin.xaml
    /// </summary>
    public partial class IdentificationAdmin
    {
        String label = "Admin = Administrateur et mp = admin";
        public IdentificationAdmin()
        {
            InitializeComponent();
        }

        private void btn_identificationAdmin_connecte_Click(object sender, RoutedEventArgs e)
        {
            if (txb_identificationAdmin_identifiant.Text == "Administrateur" && txb_identificationAdmin_mdp.Text == "admin")
            {
                dp.Children.Clear();
                dp.Children.Add(new PageAdministrateur());
            }
            else
            {
                label_identificationAdmin_erreur_affiche.Content = label;
            }

           
        }
    }
}
