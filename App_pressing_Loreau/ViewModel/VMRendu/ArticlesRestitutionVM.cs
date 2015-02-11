using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class ArticlesRestitutionVM : ObservableObject, IPageViewModel
    {
        #region Attributes

        private string _articlesNameRes;
        private string _txb_ArticlesRes_etat;

        private string _txb_Emplacement_conv;

        private bool _isSelectedArticle;
        private bool _isEnabledArticles;


        public Article ar;
        #endregion

        #region Constructeur

        public ArticlesRestitutionVM()
        {
            ar = new Article();
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }

        #region Propietés et commandes
        public string ArticlesNameRes
        {
            get
            {
                return this._articlesNameRes;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._articlesNameRes = value;
                    OnPropertyChanged("ArticlesNameRes");
                }
            }
        }

        public string Txb_ArticlesRes_CommentaireArticles
        {
            get
            {
                return this.ar.commentaire;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ar.commentaire = value;
                    OnPropertyChanged("Txb_ArticlesRes_CommentaireArticles");
                }
            }

        }

        public string Txb_ArticlesRes_photo
        {
            get
            {
                return this.ar.photo;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ar.photo = value;
                    OnPropertyChanged("Txb_ArticlesRes_photo");
                }
            }

        }
        public string Txb_ArticlesRes_etat
        {
            get
            {
                return this._txb_ArticlesRes_etat;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._txb_ArticlesRes_etat = value;
                    OnPropertyChanged("Txb_ArticlesRes_etat");
                }
            }

        }

        public string Txb_Emplacement_conv
        {
            get
            {
                //return this._txb_Emplacement_conv.ToString();
                _txb_Emplacement_conv = ar.convoyeur.emplacement.ToString();
                if (_txb_Emplacement_conv.Equals("0"))
                {
                    return "HC";
                }
                else
                {
                    return _txb_Emplacement_conv;
                }
                
            }

            //set
            //{
            //    this._txb_Emplacement_conv = float.Parse(value);
            //    OnPropertyChanged("Txb_Emplacement_conv");
            //}

        }


        public bool IsSelectedArticle
        {
            get { return _isSelectedArticle; }
            set
            {
                if (value != _isSelectedArticle)
                {
                    _isSelectedArticle = value;
                    OnPropertyChanged("IsSelectedArticle");
                }
            }

        }

        public bool IsEnabledArticles
        {
            get { return _isEnabledArticles; }
            set
            {
                if (value != _isEnabledArticles)
                {
                    _isEnabledArticles = value;
                    OnPropertyChanged("IsEnabledArticles");
                }
            }
        }
        #endregion
    }
}
