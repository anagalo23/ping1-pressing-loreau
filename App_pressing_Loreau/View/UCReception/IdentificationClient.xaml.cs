﻿using App_pressing_Loreau.ViewModel;
using System.Windows;
using System.Windows.Controls;


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

        }


        private void btn_identClient_nouveau_client_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouveauClient());
        }

        private void btn_identClient_nouvelle_commande_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new NouvelleCommande());
        }

        private void txb_identificationClient_nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("salut");
            Fields fields = AutoComplete.getFields();
            //MessageBox.Show(sender.ToString());

            fields.nom = AutoComplete.parseFromClassToMessage(sender.ToString());
            //message();

        }

        private void txb_identificationClient_prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.prenom = AutoComplete.parseFromClassToMessage(sender.ToString());
            //message();
        }

        private void txb_identificationClient_portable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.portable = AutoComplete.parseFromClassToMessage(sender.ToString());
            //MessageBox.Show(fields.nom);
            //message();

        }

        private void txb_identificationClient_adresse_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.adresse = AutoComplete.parseFromClassToMessage(sender.ToString());
            //tactacttac
            //message();
        }

        private void txb_identificationClient_date_naissance_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fields fields = AutoComplete.getFields();
            fields.dateDeNaissance = AutoComplete.parseFromClassToMessage(sender.ToString());
            message();
        }

        private void message(){
            Fields fields = AutoComplete.getFields();
            MessageBox.Show("Champs de recherche désirés :\nnom : " + fields.nom +
                "\nprenom : " + fields.prenom + 
                "\nportable : " + fields.portable +
                "\nadresse : " + fields.adresse +
                "\ndateDeNaissance : " + fields.dateDeNaissance);
        }


        
    }
}