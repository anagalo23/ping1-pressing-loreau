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
    /// Logique d'interaction pour GestionUtilasteurs.xaml
    /// </summary>
    public partial class GestionUtilisateurs 
    {
        public GestionUtilisateurs()
        {
            InitializeComponent();
        }

        public void btn_gest_utilisateur_supprimer_Click(Object sender, RoutedEventArgs e)
        {

        }
        public void btn_gest_utilisateur_editer_Click(Object sender, RoutedEventArgs e)
        {

        }
        public void btn_gest_utilisateur_ajouter_Click(Object sender, RoutedEventArgs e)
        {

        }

        private void btn_gest_utilisateur_retour_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new PageAdministrateur());
        }

       

    }
}
