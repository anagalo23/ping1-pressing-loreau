using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using App_pressing_Loreau;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
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
        }
        
    }
}
