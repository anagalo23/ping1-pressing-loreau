using App_pressing_Loreau.View;
using App_pressing_Loreau.ViewModel;
using System.Windows;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// Lancement de l'application avec le chargement du VM
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
           
            base.OnStartup(e);
            // chargement de la page accueil

            Accueil acc = new Accueil();
            // liaison avec la page AccueilVM
            AccueilVM context = new AccueilVM();
            // Permet de traiter les données dans AccueilVM
            acc.DataContext = context;
            acc.Show();

            //PageDemarrage acc = new PageDemarrage();

            //PageDemarrageVM pVM = new PageDemarrageVM();
            //acc.DataContext = pVM;
            //acc.Show();
        }
        
    }
}
