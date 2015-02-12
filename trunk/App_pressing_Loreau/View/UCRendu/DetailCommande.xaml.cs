using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.View;
using App_pressing_Loreau.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour DetailCommande.xaml
    /// </summary>
    public partial class DetailCommande : UserControl
    {
        public DetailCommande()
        {
            InitializeComponent();
            DataContext = new DetailCommandeVM();
        }

        private void btn_detailCommande_rendre_articles_selectionnes_Click(object sender, RoutedEventArgs e)
        {
            dp.Children.Clear();
            //dp.Children.Add(new Accueil());
            //dp.Children.Add(new Paiement());
            //dp.Children.Clear();
            ////Si la commande a déjà été payée je ne pase pas par la page de paiement
            if (ClasseGlobale._renduCommande.payee == false)
            {
                dp.Children.Add(new Paiement());
            }
            else
            {
                //Il faut nettoyer le convoyeur et update les articles

                List<Article> ListeSelectArt = ClasseGlobale._rendreArticlesSelectionnes;
                Commande comdRendu = ClasseGlobale._renduCommande;

                foreach (Article art in ListeSelectArt)
                {
                    //Mise à jour de la place convoyeur
                    //1 - dans la table convoyeur : on soustrait l'encombrement
                    //2 - dans la table article : id convoyeur devient nul

                    art.convoyeur.encombrement = (float)((decimal)art.convoyeur.encombrement - (decimal)art.type.encombrement);
                    //Si un article est à la même place, il faut modifier sa place convoyeur pour qu'elle corresponde au changement appliqué
                    //Permet la mise à jour correcte de la table convoyeur
                    foreach (Article art2 in ListeSelectArt)
                    {
                        //Si j'ai un autre article au même emplacement convoyeur
                        if (art2.convoyeur.id == art.convoyeur.id && art2.id != art.id)
                        {
                            //Je lui attribut le bon encombrement
                            art2.convoyeur.encombrement = art.convoyeur.encombrement;
                        }
                    }

                    PlaceConvoyeurDAO.updatePlaceConvoyeur(art.convoyeur);

                    Article artAdd = new Article(art.id, art.photo, art.commentaire, true, art.TVA, art.TTC, art.type, null, comdRendu.id);
                    artAdd.date_rendu = DateTime.Now;
                    //artAdd.date_payee = DateTime.Now;

                    ArticleDAO.updateArticle(artAdd);
                }


                //Mise à jour de la commande
                comdRendu = CommandeDAO.selectCommandeById(comdRendu.id, true, true, true);
                bool tousLesArticlesSontIlsRendus = true;
                //Si j'ai rendu tous les articles de la commande je dois update le champ de la commande
                foreach (Article art in comdRendu.listArticles)
                {
                    //Si l'un au moins des articles n'est pas rendu je ne change pas la date rendu de la commande
                    if (art.ifRendu == false)
                    {
                        tousLesArticlesSontIlsRendus = false;
                        break;
                    }
                }
                if (tousLesArticlesSontIlsRendus == true)
                {
                    comdRendu.date_rendu = DateTime.Now;
                    CommandeDAO.updateCommande(comdRendu);
                }

                ClasseGlobale.SET_ALL_NULL();

            }

        }
    }
}
