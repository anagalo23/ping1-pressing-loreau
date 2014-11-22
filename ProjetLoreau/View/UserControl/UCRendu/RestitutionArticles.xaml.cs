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
using System.Data;
using ProjetLoreau.Model.DTO;
using ProjetLoreau.Model.DAO;


namespace ProjetLoreau
{
    /// <summary>
    /// Logique d'interaction pour RestitutionClient.xaml
    /// </summary>
    public partial class RestitutionArticles
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
          //  listArt= ArticleDAO.getArticlesById(Int32.Parse(txb_restitutionArticles_nuemro_facture.Text));
            //listArt= ControlerRendu.getCommandeByNumeroFacture(Int32.Parse(txb_restitutionArticles_nuemro_facture.Text));
            //listArt.photo
           //Art.Add(listArt[Int32.Parse(txb_restitutionArticles_nuemro_facture.Text)].photo);
           //Art.Add(listArt[Int32.Parse(txb_restitutionArticles_nuemro_facture.Text)].commentaire);
           art.Add("bonjour");
           art.Add("bonjour");
           art.Add("bonjour");



            this.dtgrid_restitution_article_affiche.ItemsSource = null;

            //this.dtgrid_restitution_article_affiche.ItemsSource =art;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



    }
}
