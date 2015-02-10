using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model;
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
using System.Windows.Shapes;

namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour PageDemarrage.xaml
    /// </summary>
    public partial class PageDemarrage : Window
    {
        public PageDemarrage()
        {
            InitializeComponent();
            //DataContext = new PageDemarrageVM();
        }

        private void btn_accessApplication_Click(object sender, RoutedEventArgs e)
        {
            //dpDemarrage.Children.Clear();
            //dpDemarrage.Children.Add(new Accueil());
            CashProperties.openProperties();
            MessageBox.Show(CashProperties.fondCaisse +"");
        }
    }
}
