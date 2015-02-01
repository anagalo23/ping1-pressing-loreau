﻿using App_pressing_Loreau.ViewModel;
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
    /// Logique d'interaction pour NouvelleCommandeClientPro.xaml
    /// </summary>
    public partial class NouvelleCommandeClientPro : UserControl
    {
        public NouvelleCommandeClientPro()
        {
            InitializeComponent();
            DataContext = new NouvelleCommandeClientProVM();
        }

        private void btn_nouvelleCommandeClientPro_Retour_Click(object sender, RoutedEventArgs e)
        {
            if (NouvelleCommandeClientProVM.payeDifferer != 0)
            {
                dp.Children.Clear();
                dp.Children.Add(new AdministrationClientPro());
            }
            else
                MessageBox.Show("Commande non enregistrée");
           
        }
    }
}