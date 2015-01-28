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
    class ArticlesVM : ObservableObject
    {
        #region Attributes 

     
        private String _changedPhoto;
              
        private ComboComm _selected_Articles_Commentaire;
        ComboComm comboComm = new ComboComm();

        public TypeArticle typeArticle;        
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


        public ComboComm Selected_Articles_Commentaire
        {
            get { return _selected_Articles_Commentaire; }
            set
            {
                _selected_Articles_Commentaire = value;
                RaisePropertyChanged("Selected_Articles_Commentaire");
            }
        }
        public ICommand Btn_Articles_ChargerPhoto
        { get { return new RelayCommand(P => ExecuteOpenFileDialog()); } }

   

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
            Article article = new Article(_changedPhoto, "Commentaire", typeArticle, cmd_id);

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
