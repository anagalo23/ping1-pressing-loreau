using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System;

using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;



namespace App_pressing_Loreau.View
{
    /// <summary>
    /// Logique d'interaction pour RestitutionClient.xaml
    /// </summary>
    public partial class RestitutionArticles : UserControl
    {


        public RestitutionArticles()
        {
            InitializeComponent();
        }


        private void btn_restitutionArticles_suivant_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            dp.Children.Add(new DetailCommande());
        }

        private void btn_restitutionArticles_ok_Click(object sender, RoutedEventArgs e)
        {
            List<Article> listArt;
            List<String> art = new List<String>();
          //  listArt= ArticleDAO.getArticlesById(Int32.Parse(txb_restitutionArticles_numero_facture.Text));
            //listArt= ControlerRendu.getCommandeByNumeroFacture(Int32.Parse(txb_restitutionArticles_numero_facture.Text));
            //listArt.photo
           //Art.Add(listArt[Int32.Parse(txb_restitutionArticles_numero_facture.Text)].photo);
           //Art.Add(listArt[Int32.Parse(txb_restitutionArticles_numero_facture.Text)].commentaire);
           art.Add("bonjour");
           art.Add("bonjour");
           art.Add("bonjour");



    

            //this.dtgrid_restitution_article_affiche.ItemsSource =art;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




    }
}
