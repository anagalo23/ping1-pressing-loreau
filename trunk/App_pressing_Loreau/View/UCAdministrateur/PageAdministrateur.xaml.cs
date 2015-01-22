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
    /// Logique d'interaction pour PageAdministrateur.xaml
    /// </summary>
    public partial class PageAdministrateur : UserControl
    {
        public PageAdministrateur()
        {
            InitializeComponent();
        }

       private void btn_pageAdministrateur_statistiques_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new Statistiques());
        }

     

        private void btn_pageAdministrateur_gestion_utilisateur_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new GestionUtilisateurs());
        }

        private void btn_pageAdministrateur_administration_commandes_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new AdministrationCommandes());
        }

        private void btn_pageAdministrateur_administration_convoyeur_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new AdministrationConvoyeur());
        }

        private void btn_pageAdministrateur_administration_caisse_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new AdministrationCaisse());
        }

        private void btn_pageAdministrateur_administration_clients_Click(object sender, RoutedEventArgs e)
        {

        }

 

        private void btn_pageAdministrateur_parametre_utilisation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_pageAdministrateur_parametre_affichage_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new ParametreAffichage());
        }

        private void btn_pageAdministrateur_administration_articles_departements_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new AdministrationArticlesDepartements());
        }

        private void btn_pageAdministrateur_administration_client_pro_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new AdministrationClientPro());
        }

    }
}
 