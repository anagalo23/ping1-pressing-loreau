using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Input;

using App_pressing_Loreau.Helper;


namespace App_pressing_Loreau.ViewModel
{
    class ArticlesVM : ObservableObject, IPageViewModel
    {
        #region Attributes 

        private string _articlesName;
        private string _txb_Articles_Commentaire;
        private string _selectFile;
        #endregion

        #region Propietés et commandes 
        public string ArticlesName
        {
            get
            {
                return this._articlesName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._articlesName = value;
                    OnPropertyChanged("ArticlesName");
                }
            }
        }

        public string Txb_Articles_Commentaire
        {
            get
            {
                return this._txb_Articles_Commentaire;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._txb_Articles_Commentaire = value;
                    OnPropertyChanged("Txb_Articles_Commentaire");
                }
            }

        }

        public ICommand Btn_Articles_ChargerPhoto
        { get { return new RelayCommand(P => ExecuteOpenFileDialog()); } }

        public String SelectFile
        {
            get { return _selectFile; }
            set
            {
                _selectFile = value;
                OnPropertyChanged("SelectFile");
            }
        }


        private void ExecuteOpenFileDialog()
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            SelectFile = dialog.FileName;
        }
       
        #endregion



        public string Name
        {
            get { return ""; }
        }
    }
}
