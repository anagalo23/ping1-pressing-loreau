using App_pressing_Loreau.Helper;
using App_pressing_Loreau.ViewModel;
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
    /// Logique d'interaction pour NouvelleCommande.xaml
    /// </summary>
    public partial class NouvelleCommande : UserControl
    {
        public NouvelleCommande()
        {
            InitializeComponent();
            DataContext = new NouvelleCommandeVM();
        }

        private void btn_nouvelleCommande_paiement_immediat_Click(object sender, RoutedEventArgs e)
        {
            if (ClasseGlobale._contentDetailCommande.Count != 0)
            {
                dp.Children.Clear();
                dp.Children.Add(new Paiement());
            }
            else { MessageBox.Show("Ajoutez des articles"); }
            
        }

        private void btn_nouvelleCommande_paiement_differe_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(ClasseGlobale._contentDetailCommande[0].SelectedPhoto);
            //AccueilVM acvm = new AccueilVM();
            //acvm.accueilVM();
        }

        private void btn_nouvelleCommande_paiement_differe_Click_1(object sender, RoutedEventArgs e)
        {
            if (NouvelleCommandeVM.payeDifferer != 0)
            {
                dp.Children.Clear();
            }
        }

    }
}
