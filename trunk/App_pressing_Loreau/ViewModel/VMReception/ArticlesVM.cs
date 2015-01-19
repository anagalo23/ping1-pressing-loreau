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



namespace App_pressing_Loreau.ViewModel
{
    class ArticlesVM : ObservableObject, IPageViewModel
    {
        #region Attributes 

        private string _txb_Articles_Commentaire;
        private ImageSource _changedPhoto;
        private string _changedPhotoFileName = null;
        private String _selected_Articles_Commentaire;

        public List<String> Cbb_Articles_Commentaire { get; set; }


        private ConvertToImage convert = new ConvertToImage();
        public TypeArticle article;        
        #endregion

        public ArticlesVM()
        {
            
        }

        #region Propietés et commandes 
        public string ArticlesName
        {
            get
            {
                return this.article.nom;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.article.nom = value;
                    OnPropertyChanged("ArticlesName");
                }
            }
        }

      

        public String Selected_Articles_Commentaire
        {
            get { return _selected_Articles_Commentaire; }
            set
            {
                _selected_Articles_Commentaire = value;
                RaisePropertyChanged("Selected_Articles_Commentaire");
            }
        }
        public ICommand Btn_Articles_ChargerPhoto
        { get { return new RelayCommand(P => ChangePhoto()); } }

   

        public ImageSource SelectedPhoto
        {
            get {  return _changedPhoto; }
            set
            {
                if (_changedPhoto == null)
                {
                    //_changedPhoto = (ImageSource)convert.Convert(
                    //    value,
                    //    typeof(ImageSource),
                    //    null,
                    //    Thread.CurrentThread.CurrentUICulture);
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
            //SelectFile = dialog.FileName;
        }
        private void ChangePhoto()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Image Files|*.jpg;*.png;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                BitmapImage bi = new BitmapImage();
                //the input stream will be disposed at the end to avoid wpf lock / file access exceptions 
                using (Stream inputStream = File.OpenRead(dlg.FileName))
                {
                    bi.BeginInit();
                    bi.StreamSource = inputStream;
                    //load the image now so we can immediately dispose of the stream
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                }
                _changedPhotoFileName = dlg.FileName;
                _changedPhoto = bi;
                OnPropertyChanged("SelectedPhoto");
            }
        }
        #endregion



        public string Name
        {
            get { return ""; }
        }
    }


   
}
