using System.Windows;
using System.Windows.Controls;



namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Accueil : UserControl 
    {
        //Button btn;
      
        public Accueil()
        {    
            InitializeComponent();
            DataContext = new AccueilVM();
        }

    }


}




