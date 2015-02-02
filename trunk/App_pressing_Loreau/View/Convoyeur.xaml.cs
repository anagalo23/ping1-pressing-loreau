using System;
using System.Windows.Controls;
using System.Windows.Input;


namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour Convoyeur1.xaml
    /// </summary>
    public partial class Convoyeur : UserControl
    {
        public Convoyeur()
        {
            InitializeComponent();
        }

        private void txb_Convoyeur_idCommande_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9) ; // it`s number
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ; // it`s number
             else
                e.Handled = true; // the key will sappressed
        }
    }
}
