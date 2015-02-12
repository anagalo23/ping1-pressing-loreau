using App_pressing_Loreau.ViewModel;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

using App_pressing_Loreau.Model;
using App_pressing_Loreau.Helper;
using System;
using System.Windows.Input;

namespace App_pressing_Loreau.View
{

    /// <summary>
    /// Logique d'interaction pour IndentificationClient.xaml
    /// 
    /// </summary>
    public partial class IdentificationClient : UserControl
    {


        public IdentificationClient()
        {
            InitializeComponent();

            //DataContext = new IdentificationClientVM();
            //clientVm = new IdentificationClientVM();
        }


        private void btn_identClient_nouveau_client_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouveauClient());
        }

        private void btn_identClient_nouvelle_commande_Click(object sender, RoutedEventArgs e)
        {
            if (ClasseGlobale.Client.nom != "")
            {
                dp.Children.Clear();
                dp.Children.Add(new NouvelleCommande());
            }
            else { MessageBox.Show("Choisissez un client ou clicquez sur un nouveau client"); }

        }

        private void txb_identificationClient_nom_TextChanged(object sender, TextChangedEventArgs e)
        {

            Fields fields = AutoComplete.getFields();
            fields.nom = txb_identificationClient_nom.Text;

        }


        private void txb_identificationClient_prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.prenom = txb_identificationClient_prenom.Text;


        }

        private void txb_identificationClient_portable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.portable = txb_identificationClient_portable.Text;

        }


        private void txb_identificationClient_id_cleanway_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Fields fields = AutoComplete.getFields();
                fields.idCleanWay = Int32.Parse(txb_identificationClient_id_cleanway.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
           

        }
        private void txb_identificationClient_id_cleanway_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key >= Key.D0 && e.Key <= Key.D9) ; // it`s number
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ; // it`s number
            else
                e.Handled = true; // the key will sappressed
        }

    }
}
