using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class FactureFinaleVM : ObservableObject
    {
        #region Attributes

        private string _labelReferenceFacture;
        private string _labelDetailTotal;
        private string _labelDetailTauxTVA;
        private string _labelDetailMontantHT;
        private string _labelDetailMontantTVA;

        private List<CategoryArticle> _listBoxDetailFacture;
        #endregion 

        #region Propriétés et commandes
        public string LabelReferenceFacture
        {
            get
            {
                return _labelReferenceFacture;
            }
            set
            {
                if (_labelReferenceFacture!= value)
                {
                    this._labelReferenceFacture = value;
                    OnPropertyChanged("LabelReferenceFacture");
                }
            }
        }

        public string LabelDetailTotal
        {
            get
            {
                return _labelDetailTotal;
            }
            set
            {
                if (_labelDetailTotal != value)
                {
                    this._labelDetailTotal = value;
                    OnPropertyChanged("LabelDetailTotal");
                }
            }
        }


        public string LabelDetailTauxTVA
        {
            get
            {
                return _labelDetailTauxTVA;
            }
            set
            {
                if (_labelDetailTauxTVA != value)
                {
                    this._labelDetailTauxTVA = value;
                    OnPropertyChanged("LabelDetailTauxTVA");
                }
            }
        }

        public string LabelDetailMontantHT
        {
            get
            {
                return _labelDetailMontantHT;
            }
            set
            {
                if (_labelDetailMontantHT != value)
                {
                    this._labelDetailMontantHT = value;
                    OnPropertyChanged("LabelDetailMontantHT");
                }
            }
        }

        public string LabelDetailMontantTVA
        {
            get
            {
                return _labelDetailMontantTVA;
            }
            set
            {
                if (_labelDetailMontantTVA != value)
                {
                    this._labelDetailMontantTVA = value;
                    OnPropertyChanged("LabelDetailMontantTVA");
                }
            }
        }


        public List<CategoryArticle> ListBoxDetailFacture
        {
            get
            {
                return _listBoxDetailFacture;
            }
            set
            {
                _listBoxDetailFacture = value;
                RaisePropertyChanged("ListBoxDetailFacture");
            }
        }
        #endregion
    }
    
    
    
    #region Class
    public class CategoryArticle
    {
          public string LabelNameArticle { get; set;}
          public string LabelPrixArticle { get; set; }
    }
    
    
    #endregion

}
