﻿using App_pressing_Loreau.Helper;
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
    /// Logique d'interaction pour ClientPro.xaml
    /// </summary>
    public partial class ClientProUC : UserControl
    {
        public ClientProUC()
        {
            InitializeComponent();
            //DataContext = new ClientPROVM();
        }

        private void btn_clientProUC_nouveau_clientPro_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouveauClientPro());
        }

        private void btn_regleClientPro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClasseGlobale._renduCommandeClientPro != null)
                {
                    dp.Children.Clear();
                    dp.Children.Add(new Paiement());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex);
            }
        }

       
    }
}
