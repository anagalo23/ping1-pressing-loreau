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

namespace App_pressing_Loreau.Interfaces
{
    /// <summary>
    /// Logique d'interaction pour ReglementPro.xaml
    /// </summary>
    public partial class ReglementPro : Window
    {
        public ReglementPro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void huguette( Object sender, EventArgs e)
        {
        DragMove();
        }
    }
}
