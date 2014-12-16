using System;
using System.Windows.Input;
using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;
using System.Windows.Controls;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour NouvelleCommande.xaml
    /// </summary>
    public partial class NouvelleCommande
    {
        public NouvelleCommande()
        {
            InitializeComponent();
        }

        private void btn_nouvelleCommande_paiement_immediat_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new Paiement());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_nouvelleCommande_Deppartement_Click(object sender, RoutedEventArgs e)
        {
            
            //List<string> list = new List<string>();
            //List<Departement> listDep = DepartementDAO.getListeDepartement();
            //for(int i=0; i<listDep.Count(); i++){
            //    list.Add( listDep[i].nom);
            //}
           

            //DataGrid dgdep = new DataGrid();
            //dgdep.FrozenColumnCount = 1;
            
            
         
            ////dgdep.Items.Add("Departement");
            //dgdep.DataContext = list;

            //dp_nouvelleCommande_cmd.Children.Clear();
            //dp_nouvelleCommande_cmd.Children.Add(dgdep);

        }

        private void btn_nouvellecommande_modifier_Click(object sender, RoutedEventArgs e)
        {

        }

        
      
    }
}
