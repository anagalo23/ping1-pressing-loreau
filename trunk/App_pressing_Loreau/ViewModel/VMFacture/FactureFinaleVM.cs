using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Model.DTO;

namespace App_pressing_Loreau.ViewModel
{
    class FactureFinaleVM : ObservableObject, IPageViewModel
    {
        #region Attributes

        //private string _labelReferenceFacture;
        private float _labelDetailPrixTotalTTC;
        //private string _labelDetailTauxTVA;
        private float _labelDetailMontantHT;
        private float _labelDetailMontantTVA;

        public Commande commande;
       private List<CategoryArticle> _listBoxDetailFacture;

        #endregion 

        public FactureFinaleVM()
        {
            //commande = null;
            LabelDetailPrixTotalTTC = new float();
            LabelDetailMontantHT = new float();
            LabelDetailMontantTVA = new float();
           //RemplirArticles(commande);
        }

        #region Propriétés et commandes
        public int LabelReferenceFacture 
        {
            get { return this.commande.id; }
            set
            {
                if (value!=this.commande.id)
                {
                    this.commande.id = value;
                    OnPropertyChanged("LabelReferenceFacture");
                }
            }
        }

        public float LabelDetailPrixTotalTTC
        {
            get { return this._labelDetailPrixTotalTTC; }
            set
            {
                if (value != _labelDetailPrixTotalTTC)
                {
                    this._labelDetailPrixTotalTTC = value;
                    OnPropertyChanged("LabelDetailPrixTotalTTC");
                }
            }
        }


        public float LabelDetailTauxTVA
        {
            get { return this.commande.listArticles[0].TVA; }
            set
            {
                if (this.commande.listArticles[0].TVA!=value)
                {
                    this.commande.listArticles[0].TVA = value;
                    OnPropertyChanged("LabelDetailTauxTVA");
                }
            }
        }

        public float LabelDetailMontantHT
        {
            get {  return this._labelDetailMontantHT; }
            set
            {
                if (value!=_labelDetailMontantHT)
                {
                    this._labelDetailMontantHT = value;
                    OnPropertyChanged("LabelDetailMontantHT");
                }
            }
        }

        public float LabelDetailMontantTVA
        {
            get {  return this._labelDetailMontantTVA; }
            set
            {
                if (value!=_labelDetailMontantTVA)
                {
                    this._labelDetailMontantTVA = value;
                    OnPropertyChanged("LabelDetailMontantTVA");
                }
            }
        }

        public String Label_Adresse
        {
            get { return this.commande.client.adresse.giveAdresse(); }
            set
            {
                String adresse =this.commande.client.adresse.giveAdresse();
                adresse = value;
                OnPropertyChanged("Label_Adresse");
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

        public void RemplirArticles(Commande com)
        {
            ListBoxDetailFacture = new List<CategoryArticle>();
            foreach (Article art in com.listArticles)
            {
                ListBoxDetailFacture.Add(new CategoryArticle() { LabelNameArticle = art.type.nom, LabelPrixArticle = art.TTC });
            }
        }

        public string Name
        {
            get { return ""; }
        }
    }
    
    
    
    #region Class
    public class CategoryArticle
    {
          public string LabelNameArticle { get; set;}
          public float LabelPrixArticle { get; set; }
    }

    #endregion

}
