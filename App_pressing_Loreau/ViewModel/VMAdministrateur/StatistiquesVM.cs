using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.Practices.Prism.Commands;

namespace App_pressing_Loreau.ViewModel
{

    /// <summary>  Classe Statistique
    /// Classe statistique
    /// *Chiffre d affaires total par jour, semaine , mois et année
    /// *Chiffre d'affaires par departement et par jour, semaine , mois et année
    /// *Nombre de clients ayant deposé des articles par jour, semaine , mois et année
    /// *Nombre de clients ayant recupéré des articles par  jour, semaine , mois et année
    /// *Nombre de clients ayant payé immediatement par jour, semaine , mois et année
    /// *Nombre de client ayant payé en differé par jour, semaine , mois et année
    /// *Nombre d'articles dans le pressing par jour, semaine , mois et année
    /// *Nombre d'articles dans la blanchisserie par jour, semaine , mois et année
    /// *Nombre de couettes par jour, semaine , mois et année
    /// *Nombre de chemises par jour, semaine , mois et année
    /// </summary>
    class StatistiquesVM : ObservableObject
    {
        #region Attributs
        private float _label_statistique_catotal;
        private float _label_statistique_cadep;
        private float _label_statistique_nbrClientsDepoArt;
        private float _label_statistique_nbrClientsRecupArt;
        private float _label_statistique_nbrClientspayeimediatement;

        private float _label_statistique_nbrClientspayediffere;
        private float _label_statistique_nbrArticlesPressing;
        private float _label_statistique_nbrArticlesBlanchisserie;
        private float _label_statistique_nbrCouettes;
        private float _label_statistique_nbrChemises;

        private ComboDepartStat _selected_stat_ChoixDepart;
        ComboDepartStat comboDepartStat = new ComboDepartStat();

        //chiffre d'affaire par département
        private List<Departement> listUsedDepartements = new List<Departement>();
        private List<float> caTTCDep = new List<float>();



        //private DelegateCommand<StatistiquesVM> _btn_statistique_du_jour;
        #endregion

        #region Constructeur
        public StatistiquesVM()
        {

            Label_statistique_cadep = new float();
            Label_statistique_catotal = new float();
            Label_statistique_nbrArticlesBlanchisserie = new float();
            Label_statistique_nbrArticlesPressing = new float();
            Label_statistique_nbrChemises = new float();
            Label_statistique_nbrCouettes = new float();
            Label_statistique_nbrClientsDepoArt = new float();
            Label_statistique_nbrClientsRecupArt = new float();

            ListeDepartementStatChoix = comboDepartStat.ListeDep();
        }

        #endregion

        #region Properties and command

        public List<ComboDepartStat> ListeDepartementStatChoix { get; set; }
        public ComboDepartStat Selected_stat_ChoixDepart
        {
            get { return _selected_stat_ChoixDepart; }
            set
            {
                if (value != _selected_stat_ChoixDepart)
                {
                    _selected_stat_ChoixDepart = value;
                    OnPropertyChanged("Selected_stat_ChoixDepart");
                }
            }
        }
        public float Label_statistique_catotal
        {
            get { return _label_statistique_catotal; }
            set
            {
                if (value != _label_statistique_catotal)
                {
                    _label_statistique_catotal = value;
                    OnPropertyChanged("Label_statistique_catotal");
                }
            }
        }
        public float Label_statistique_cadep
        {
            get { return _label_statistique_cadep; }
            set
            {
                if (value != _label_statistique_cadep)
                {
                    _label_statistique_cadep = value;
                    RaisePropertyChanged("Label_statistique_cadep");
                }
            }
        }


        public float Label_statistique_nbrClientsDepoArt
        {
            get { return _label_statistique_nbrClientsDepoArt; }
            set
            {
                if (value != _label_statistique_nbrClientsDepoArt)
                {
                    _label_statistique_nbrClientsDepoArt = value;
                    RaisePropertyChanged("Label_statistique_nbrClientsDepoArt");
                }
            }
        }
        public float Label_statistique_nbrClientsRecupArt
        {
            get { return _label_statistique_nbrClientsRecupArt; }
            set
            {
                if (value != _label_statistique_nbrClientsRecupArt)
                {
                    _label_statistique_nbrClientsRecupArt = value;
                    RaisePropertyChanged("Label_statistique_nbrClientsRecupArt");
                }
            }
        }
        public float Label_statistique_nbrClientspayeimediatement
        {
            get { return _label_statistique_nbrClientspayeimediatement; }
            set
            {
                if (value != _label_statistique_nbrClientspayeimediatement)
                {
                    _label_statistique_nbrClientspayeimediatement = value;
                    RaisePropertyChanged("Label_statistique_nbrClientspayeimediatement");
                }
            }
        }
        public float Label_statistique_nbrClientspayediffere
        {
            get { return _label_statistique_nbrClientspayediffere; }
            set
            {
                if (value != _label_statistique_nbrClientspayediffere)
                {
                    _label_statistique_nbrClientspayediffere = value;
                    RaisePropertyChanged("Label_statistique_nbrClientspayediffere");
                }
            }
        }
        public float Label_statistique_nbrArticlesPressing
        {
            get { return _label_statistique_nbrArticlesPressing; }
            set
            {
                if (value != _label_statistique_nbrArticlesPressing)
                {
                    _label_statistique_nbrArticlesPressing = value;
                    RaisePropertyChanged("Label_statistique_nbrArticlesPressing");
                }
            }
        }


        public float Label_statistique_nbrArticlesBlanchisserie
        {
            get { return _label_statistique_nbrArticlesBlanchisserie; }
            set
            {
                if (value != _label_statistique_nbrArticlesBlanchisserie)
                {
                    _label_statistique_nbrArticlesBlanchisserie = value;
                    RaisePropertyChanged("Label_statistique_nbrArticlesBlanchisserie");
                }
            }
        }
        public float Label_statistique_nbrCouettes
        {
            get { return _label_statistique_nbrCouettes; }
            set
            {
                if (value != _label_statistique_nbrCouettes)
                {
                    _label_statistique_nbrCouettes = value;
                    RaisePropertyChanged("Label_statistique_nbrCouettes");
                }
            }
        }
        public float Label_statistique_nbrChemises
        {
            get { return _label_statistique_nbrChemises; }
            set
            {
                if (value != _label_statistique_nbrChemises)
                {
                    _label_statistique_nbrChemises = value;
                    RaisePropertyChanged("Label_statistique_nbrChemises");
                }
            }
        }
        public ICommand Btn_statistique_du_jour
        {
            get
            {
                return new RelayCommand(p => statisticsByDate(1));
            }
        }
        public ICommand Btn_statistique_de_la_semaine
        {
            get { return new RelayCommand(p => statisticsByDate(2)); }
        }
        public ICommand Btn_statistique_du_mois
        {
            get { return new RelayCommand(p => statisticsByDate(3)); }
        }
        public ICommand Btn_statistique_de_lannee
        {
            get { return new RelayCommand(p => statisticsByDate(4)); }
        }


        public ICommand Btn_Statistique_validerDep
        {
            get { return new RelayCommand(p => validerComboBox()); }
        }
        #endregion

        #region methods

        /// <summary>
        /// Methodes Non complete. Aurelien, si tu peux faire kelk chose 
        /// </summary>
        public void validerComboBox()
        {
            if (Selected_stat_ChoixDepart != null)
            {
                Departement dep = DepartementDAO.selectDepartementById(Selected_stat_ChoixDepart.cbbDepId);
                DepartmentTTC(ArticleDAO.selectArticleRenduByDate(2));
                foreach (float f in caTTCDep)
                {
                    Label_statistique_cadep = (float)((Decimal)Label_statistique_cadep + (decimal)f);
                }

            }

        }

        public void statisticsByDate(int typeDate)
        {
            try
            {
                float ChiffreAffaireDuJour = 0;
                //DepartmentTTC(ArticleDAO.selectArticleRenduByDate(typeDate));

                List<Payement> listePaiement = (List<Payement>)PayementDAO.listSommePaiementToday(typeDate);
                foreach (Payement paye in listePaiement)
                {
                    if (!paye.typePaiement.Equals("CleanWay"))
                        ChiffreAffaireDuJour = (float)((decimal)ChiffreAffaireDuJour + (decimal)paye.montant);
                }
                //Chiffre d affaire total
                Label_statistique_catotal = ChiffreAffaireDuJour;
                //Nombre d articles dans la blanchisserie 
                Label_statistique_nbrArticlesBlanchisserie = (float)ArticleDAO.articlesInBlanchisserieByDate(typeDate);
                //Nombre d articles dans le pressing
                Label_statistique_nbrArticlesPressing = (float)ArticleDAO.articlesByDate(typeDate);
                //Nombre de chemises 
                Label_statistique_nbrChemises = (float)ArticleDAO.chemisesByDate(typeDate);
                //Nombre de couette
                Label_statistique_nbrCouettes = (float)ArticleDAO.couetteByDate(typeDate);
                //Nombre de clients ayant deposés des articles
                Label_statistique_nbrClientsDepoArt = (float)ClientDAO.nbClientDepot(typeDate);
                //Nombre de clients ayant recuperés des artilces
                Label_statistique_nbrClientsRecupArt = (float)ClientDAO.nbClientRecup(typeDate);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur :" + e);
            }
        }
        #endregion

        #region methodes de calcul

        public void DepartmentTTC(List<Article> articlesrendu)
        {
            Boolean ifExist;

            try
            {


                foreach (Article art in articlesrendu)
                {
                    //Vérifie que l'article n'a pas été payé en CleanWay
                    if (!CommandeDAO.isPayedByCleanWay(art.fk_commande))
                    {
                        ifExist = false;
                        //Jusque là on a déroulé tout les articles
                        //recherche de départements déja entrés
                        for (int i = 0; i < listUsedDepartements.Count; i++)
                        {
                            if (listUsedDepartements[i].nom.Contains(art.type.departement.nom))
                            {
                                caTTCDep[i] = caTTCDep[i] + art.TTC;
                                ifExist = true;
                                break;
                            }
                        }

                        if (!ifExist)
                        {
                            listUsedDepartements.Add(art.type.departement);
                            caTTCDep.Add(art.TTC);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        #endregion
    }


    #region Class

    public class ComboDepartStat
    {
        public String NameDepartStat { get; set; }
        public int cbbDepId { get; set; }

        List<Departement> depart = (List<Departement>)DepartementDAO.selectDepartements();
        public List<ComboDepartStat> ListeDep()
        {
            List<ComboDepartStat> listDep = new List<ComboDepartStat>();

            foreach (Departement cc in depart)
            {
                listDep.Add(new ComboDepartStat() { NameDepartStat = cc.nom, cbbDepId = cc.id });
            }

            return listDep;
        }
    }
    #endregion
}
