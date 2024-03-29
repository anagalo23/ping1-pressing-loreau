﻿using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Model;
using App_pressing_Loreau.Data;

namespace App_pressing_Loreau.ViewModel
{
    class NouvelleCommandeClientProVM : ObservableObject
    {

        #region Attributs

        private List<CategoryItem> _listeDepartement;
        private List<CategoryItem> _listeArticles;
        private List<Departement> listeDepartementDTO = null;
        private List<TypeArticle> articlesByDep = null;
        private TypeArticle typeArticleDTO = null;

        private float _label_NouvelleCommande_prixTotal;
        public float prixTotal { get; private set; }

        private DelegateCommand<ArticlesVM> _deleteArticles;
        private List<Article> lArticles;
        private bool commandePayeeEnDiffere;

        #endregion


        #region Constructeur
        public NouvelleCommandeClientProVM()
        {
            commandePayeeEnDiffere = false;
            lArticles = new List<Article>();

            defileDepartementPrecedente();
            //deselectButtons();
            prixTotal = 0;
            if (ContentDetailCommande != null)
            {
                for (int i = 0; i < ContentDetailCommande.Count; i++)
                {
                    Label_NouvelleCommande_prixTotal += (ContentDetailCommande[i].typeArticle.TTC);
                }

            }
            ClasseGlobale.PlacesLibres.setList(PlaceConvoyeurDAO.selectConvoyeursEmpty());
        }

        #endregion


        #region Proprietés et Commandes



        //gestion du choix des articles

        #region Bouton departement
        //Permet de faire réagir le bouton
        ICommand onButtonClickCommand;
        public ICommand OnButtonClickCommand
        {
            get { return onButtonClickCommand ?? (onButtonClickCommand = new RelayCommand(Contenudepartement)); }
        }
    

        public ICommand CommandeSuivante
        {
            get
            {
                return new RelayCommand(p => defileDepartementSuivante(),
                    p => ListeDepartements.Count != 4);
            }
        }

        public ICommand CommandePrecedente
        {
            get
            {
                return new RelayCommand(p => defileDepartementPrecedente(),
                    p => ListeDepartements.Count != 6);
            }
        }

        public List<CategoryItem> ListeDepartements
        {
            get
            {
                return _listeDepartement;
            }
            set
            {
                _listeDepartement = value;
                RaisePropertyChanged("ListeDepartements");
            }
        }

        public List<CategoryItem> ListeArticles
        {
            get
            {
                return _listeArticles;
            }
            set
            {
                _listeArticles = value;
                RaisePropertyChanged("ListeArticles");
            }
        }
        #endregion


        #region contenu commande

        public float Label_NouvelleCommande_prixTotal
        {
            get { return _label_NouvelleCommande_prixTotal; }
            set
            {
                if (value != _label_NouvelleCommande_prixTotal)
                {
                    _label_NouvelleCommande_prixTotal = value;
                    OnPropertyChanged("Label_NouvelleCommande_prixTotal");
                }
            }
        }


        public ObservableCollection<ArticlesVM> ContentDetailCommande
        {
            get
            {
                if (ClasseGlobale._contentDetailCommande == null)
                {
                    ClasseGlobale.initializeContentDetailCommande();
                }
                return ClasseGlobale._contentDetailCommande;

            }

            set
            {
                if (value != null)
                {
                    ClasseGlobale._contentDetailCommande = value;
                    OnPropertyChanged("ContentDetailCommande");
                }
            }
        }

        public DelegateCommand<ArticlesVM> DeleteArticles
        {
            get
            {
                return this._deleteArticles ?? (this._deleteArticles = new DelegateCommand<ArticlesVM>(
                                                                       this.ExecuteDeleteArticles,
                                                                       (arg) => true));
            }
        }


        #endregion


        #region paiement diferé
        public ICommand Btn_nouvelleCommandeClientPro_EnregistrerCommande
        {
            get { return new RelayCommand(p => EnregistrerCommandeClientPro()); }
        }
        #endregion
        #endregion


        #region Méthodes


        private void EnregistrerCommandeClientPro()
        {
            if (ClasseGlobale.Client.nom != "")
            {
                if (ClasseGlobale._contentDetailCommande == null)
                {
                    MessageBox.Show("Ajoutez des articles");
                }
                else
                {

                    Commande cmd = new Commande(DateTime.Now, false, 0, ClasseGlobale.Client);

                    //ObservableCollection<ArticlesVM> listeArticles = ClasseGlobale._contentDetailCommande;
                    if (CommandeDAO.insertCommande(cmd) == 1)
                    {
                        cmd = CommandeDAO.lastCommande();
                        foreach (ArticlesVM artVM in ClasseGlobale._contentDetailCommande)
                        {
                            ArticleDAO.insertArticle(artVM.getArticle(cmd.id));
                        }

                        //Mise à jour de la table convoyeur
                        foreach (PlaceConvoyeur place in ClasseGlobale.PlacesLibres.getList())
                        {
                            PlaceConvoyeurDAO.updatePlaceConvoyeur(place);
                        }


                        //Clear l'écran et bloque l'utilisation des touches


                        try
                        {
                            cmd = CommandeDAO.selectCommandeById(cmd.id, true, true, true);
                            RecuPaiement rp = new RecuPaiement(cmd);
                            rp.printRecu();
                           
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Impression refusée");
                        }
                        finally
                        {
                            ClasseGlobale.SET_ALL_NULL();
                            commandePayeeEnDiffere = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la création de la commande. Enregistrement de la commande annulé");
                    }

                }
            }
            else
            {
                MessageBox.Show("La comande a déjà été enregistrée. Veuillez cliquer sur HOME");
            }
        }

        /**
         * Permet le défilement des départements 
         **/

        public void defileDepartementPrecedente()
        {
            ListeDepartements = new List<CategoryItem>();
            listeDepartementDTO = (List<Departement>)DepartementDAO.selectDepartements();

            if (listeDepartementDTO != null)
            {
                for (int i = 0; i < 6; i++)
                    ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
            }

        }

        public void defileDepartementSuivante()
        {
            ListeDepartements = new List<CategoryItem>();

            if (listeDepartementDTO != null)
            {
                for (int i = 6; i < listeDepartementDTO.Count; i++)
                    ListeDepartements.Add(new CategoryItem() { ButtonContent = listeDepartementDTO[i].nom, ButtonTag = listeDepartementDTO[i].id });
            }

        }

        /**
         * Permet d'afficher  les articles correspondants à un département
         * */
        private void Contenudepartement(object button)
        {
            Button clickedbutton = button as Button;
            List<CategoryItem> listedesArticles = new List<CategoryItem>();
            articlesByDep = new List<TypeArticle>();
            articlesByDep = (List<TypeArticle>)TypeArticleDAO.selectTypeByDepId(Int32.Parse(clickedbutton.Tag.ToString()));

            if (articlesByDep.Count > 0)
            {
                if (clickedbutton != null)
                {
                    int x = 15, y = 5;

                    foreach (TypeArticle type in articlesByDep)
                    {
                        listedesArticles.Add(new CategoryItem() { ButtonArticlesContent = type.nom, ButtonArticlesTag = type.id, X = x, Y = y });
                        if (x < 16) { x = 220; }
                        else if (x < 221) { x = 425; }
                        else if (x > 424)
                        {
                            x = 15;
                            y += 70;
                        }
                    }
                    ListeArticles = listedesArticles;
                }
            }
            else
            {

                //string msg = string.Format("You Pressed : {0} button", clickedbutton.Content);
                MessageBox.Show("Departement vide");
            }

        }



        //Ajouter un article a la commande 
        ICommand listesArticlesCommandes;
        public ICommand ListesArticlesCommandes
        {
            get
            {
                if (ClasseGlobale._contentDetailCommande != null)
                    return listesArticlesCommandes ?? (listesArticlesCommandes = new RelayCommand(AjouterArticles));
                else return null;
            }

        }


        public void AjouterArticles(object button)
        {
            if (ClasseGlobale._contentDetailCommande != null)
            {
                Button clickedbutton = button as Button;
                if (clickedbutton != null)
                {
                    typeArticleDTO = (TypeArticle)TypeArticleDAO.selectTypesById(Int32.Parse(clickedbutton.Tag.ToString()));

                    PlaceConvoyeur place = new PlaceConvoyeur();
                    place = null;
                    if (typeArticleDTO.encombrement == 0 || typeArticleDTO.encombrement > 3)
                    {
                        //Cet article ne va pas dans le convoyeur
                        //MessageBox.Show("Cet article ne va pas dans le convoyeur.");
                    }
                    else
                    {
                        //Je parcours la liste pour trouver une place convoyeur pouvant accueillir l'article
                        int finDeListe = ClasseGlobale.PlacesLibres.getList().Count();
                        float encombrement_occupe_pour_cette_place;
                        float encombrement_maximum = 3 - typeArticleDTO.encombrement;
                        for (int i = 0; i < finDeListe; i++)
                        {
                            //si l'encombrement du convoyeur est permet de recevoir l'article
                            encombrement_occupe_pour_cette_place = ClasseGlobale.PlacesLibres.getList()[i].encombrement;

                            if (encombrement_occupe_pour_cette_place <= encombrement_maximum)
                            {
                                //Je modifie l'encombrement de la place convoyeur
                                ClasseGlobale.PlacesLibres[i].encombrement += typeArticleDTO.encombrement;
                                //Je récupère la place convoyeur concernée
                                place = ClasseGlobale.PlacesLibres.getList()[i];
                                break;
                            }
                            if (i == finDeListe - 1)
                            {
                                MessageBox.Show("Cet article ne trouve pas sa place dans le convoyeur.\n" +
                                "Peut-être n'y a t-il plus de place ou cet article est trop volumineux pour les emplacements restants.");
                            }
                        }
                    }


                    //On construit un nouvel ArticlesVM
                    ArticlesVM articleVmAAjouter = new ArticlesVM()
                    {
                        typeArticle = typeArticleDTO,
                        ArticlesName = typeArticleDTO.nom,
                        PlaceConvoyeur = place
                    };
                    ClasseGlobale._contentDetailCommande.Add(articleVmAAjouter);


                    Label_NouvelleCommande_prixTotal = 0;
                    decimal tampon = 0;
                    foreach (ArticlesVM artVm in ClasseGlobale._contentDetailCommande)
                    {
                        //MessageBox.Show("ajout de " + artVm.typeArticle.TTC);
                        //Label_NouvelleCommande_prixTotal += (artVm.typeArticle.TTC);
                        tampon += (decimal)(artVm.typeArticle.TTC);
                    }
                    //MessageBox.Show(tampon.ToString());
                    Label_NouvelleCommande_prixTotal = (float)tampon;//(float)Math.Round(tampon, 2, MidpointRounding.AwayFromZero);// 
                }
            }


        }


        private void ExecuteDeleteArticles(ArticlesVM obj)
        {
            if (ClasseGlobale._contentDetailCommande != null)
            {
                if (ClasseGlobale._contentDetailCommande.Contains(obj))
                {
                    ClasseGlobale._contentDetailCommande.Remove(obj);
                    decimal tamp = (decimal)Label_NouvelleCommande_prixTotal;
                    tamp -= (decimal)obj.typeArticle.TTC;
                    Label_NouvelleCommande_prixTotal = (float)tamp;
                }
            }

        }


        #endregion


        #region Class
        public class CategoryItem
        {
            public string ButtonContent { get; set; }
            public string ButtonArticlesContent { get; set; }

            public Brush ButtonDepBackground { get; set; }
            public int ButtonTag { get; set; }
            public int ButtonArticlesTag { get; set; }

            public int X { get; set; }
            public int Y { get; set; }


        }

        #endregion

    }
}
