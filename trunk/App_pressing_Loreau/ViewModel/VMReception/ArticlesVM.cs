using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;
using App_pressing_Loreau.Data.DAO;



namespace App_pressing_Loreau.ViewModel
{
    /// <summary>
    /// Classe permettant de récupérer le/les article(s) sélectionné(s) et de les lister pour former la commande
    /// </summary>
    class ArticlesVM : ObservableObject
    {
        #region Attributes 

        
        private String _changedPhoto;

        private ComboComm _selected_Articles_Commentaire;
        ComboComm comboComm = new ComboComm();

        public TypeArticle typeArticle;   
        private PlaceConvoyeur _placeConvoyeur;


        #endregion

        public ArticlesVM()
        {
            Cbb_Articles_Commentaire = comboComm.ListeComm();
        }

        #region Propietés et commandes 

        public List<ComboComm> Cbb_Articles_Commentaire { get; set; }
        public string ArticlesName
        {
            get
            {
                return this.typeArticle.nom;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.typeArticle.nom = value;
                    OnPropertyChanged("ArticlesName");
                }
            }
        }

        public PlaceConvoyeur PlaceConvoyeur { get; set; }

        public ComboComm Selected_Articles_Commentaire
        {
            get { return _selected_Articles_Commentaire; }
            set
            {
                if (value != _selected_Articles_Commentaire)
                {
                    _selected_Articles_Commentaire = value;
                    RaisePropertyChanged("Selected_Articles_Commentaire");
                }
                
            }
        }
        public ICommand Btn_Articles_ChargerPhoto
        { get { return new RelayCommand(P => ExecuteOpenFileDialog()); } }


        public float Btn_Articles_PrixArticles
        {
            get { return typeArticle.TTC; }
            set
            {
                if (value != typeArticle.TTC)
                {
                    typeArticle.TTC = value;
                    OnPropertyChanged("Btn_Articles_PrixArticles");
                }
            }
        } 
        public String SelectedPhoto
        {
            get {  return _changedPhoto; }
            set
            {
                if (_changedPhoto != value)
                {
                    _changedPhoto = value;
                    OnPropertyChanged("SelectedPhoto");
                }
            }
        }
        private void ExecuteOpenFileDialog()
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Image Files|*.jpg;*.png;*.bmp";
            dialog.ShowDialog();
            SelectedPhoto = dialog.SafeFileName;
        }
      
        #endregion


        #region methodes
        public Article getArticle(int cmd_id)
        {
            String selection = null;

            if (_selected_Articles_Commentaire == null) selection = null;
            else selection = _selected_Articles_Commentaire.NameCbbArt;

            Article article = new Article(_changedPhoto, selection, typeArticle, cmd_id);
            //Article article = new Article
            article.convoyeur = PlaceConvoyeur;
            return article;
        }
        #endregion
    }

    #region Class

    class ComboComm
    {
        public String NameCbbArt { get; set; }
        public int cbbArtId { get; set; }
        List<Commentaire> com = (List<Commentaire>) CommentaireDAO.selectCommentaire(); 
        public List<ComboComm> ListeComm()
        {
            List<ComboComm> lstCb = new List<ComboComm>();

            foreach (Commentaire cc in com)
            {
                lstCb.Add(new ComboComm() {NameCbbArt=cc.com,cbbArtId=cc.id });
            }
         
            return lstCb;
        }
    }


    #endregion


}
