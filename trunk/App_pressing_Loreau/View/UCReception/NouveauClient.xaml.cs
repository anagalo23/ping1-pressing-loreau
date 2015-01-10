﻿using System;
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
    /// Logique d'interaction pour NouveauClient.xaml
    /// </summary>
    public partial class NouveauClient : UserControl
    {

        public int parameter {get; set;}
        public NouveauClient()
        {
            InitializeComponent();
        }

        private void btn_nouveauClient_valider_inscription_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            
            dp.Children.Add(new NouvelleCommande());
        }

       

      

    }
}
