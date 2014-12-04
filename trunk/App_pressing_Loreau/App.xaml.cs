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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Accueil acc = new Accueil();
            AccueilVM context = new AccueilVM();
            acc.DataContext = context;
            acc.Show();
        }
        
    }
}
