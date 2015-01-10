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

        public FactureFinaleVM()
        {

        }

        #region Propriétés et commandes
        public string LabelReferenceFacture 
        {
            get {  return this._labelReferenceFacture;}
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._labelReferenceFacture = value;
                    OnPropertyChanged("LabelReferenceFacture");
                }
            }
        }

        public string LabelDetailTotal
        {
            get { return this._labelDetailTotal;  }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._labelDetailTotal = value;
                    OnPropertyChanged("LabelDetailTotal");
                }
            }
        }


        public string LabelDetailTauxTVA
        {
            get { return this._labelDetailTauxTVA; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._labelDetailTauxTVA = value;
                    OnPropertyChanged("LabelDetailTauxTVA");
                }
            }
        }

        public string LabelDetailMontantHT
        {
            get {  return this._labelDetailMontantHT; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._labelDetailMontantHT = value;
                    OnPropertyChanged("LabelDetailMontantHT");
                }
            }
        }

        public string LabelDetailMontantTVA
        {
            get {  return this._labelDetailMontantTVA; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._labelDetailMontantTVA = value;
                    OnPropertyChanged("LabelDetailMontantTVA");
                }
            }
        }


        public List<CategoryArticle> ListBoxDetailFacture
        {
            get { return _listBoxDetailFacture; }
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
