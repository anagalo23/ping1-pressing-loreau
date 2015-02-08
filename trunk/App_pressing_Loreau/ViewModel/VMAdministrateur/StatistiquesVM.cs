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
    class StatistiquesVM : ObservableObject
    {
        #region Attributs
        private float _label_statistique_catotal;
        private float _label_statistique_cadep;
        private float _label_statistique_nbrClientsDepoArt;
        private float _label_statistique_nbrClientsRecupArt;
        private float _label_statistique_nbrClientspayeimediatement;

        private float _label_statistique_nbrClientspayediffere;
        private float _label_statistique_nbrArticlesPressBlanchi;
        private float _label_statistique_nbrCouettes;
        private float _label_statistique_nbrChemises;

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
        }

        #endregion

        #region Properties and command
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
        public float Label_statistique_nbrArticlesPressBlanchi
        {
            get { return _label_statistique_nbrArticlesPressBlanchi; }
            set
            {
                if (value != _label_statistique_nbrArticlesPressBlanchi)
                {
                    _label_statistique_nbrArticlesPressBlanchi = value;
                    RaisePropertyChanged("Label_statistique_nbrArticlesPressBlanchi");
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
                return new RelayCommand(p => statisticsByDays());
            }
        }

        public ICommand Btn_statistique_de_la_semaine
        {
            get { return new RelayCommand(p => statisticsByWeek()); }
        }
        public ICommand Btn_statistique_du_mois
        {
            get { return new RelayCommand(p => statisticsByMonth()); }
        }
        public ICommand Btn_statistique_de_lannee
        {
            get { return new RelayCommand(p => statisticsByYear()); }
        }
        #endregion

        #region methods
        public void statisticsByDays()
        {
            float ChiffreAffaireDuJour = 0;

            List<Payement> listePaiement = (List<Payement>)PayementDAO.listSommePaiementToday(1);
            foreach (Payement paye in listePaiement)
            {
                if (!paye.typePaiement.Equals("CleanWay"))
                    ChiffreAffaireDuJour = (float)((decimal)ChiffreAffaireDuJour + (decimal)paye.montant);
            }

            Label_statistique_catotal = ChiffreAffaireDuJour;
            //MessageBox.Show("" + _label_statistique_catotal);
        }

        public void statisticsByWeek()
        {
            float ChiffreAffaireDuJour = 0;

            List<Payement> listePaiement = (List<Payement>)PayementDAO.listSommePaiementToday(2);
            foreach (Payement paye in listePaiement)
            {
                if (!paye.typePaiement.Equals("CleanWay"))
                    ChiffreAffaireDuJour = (float)((decimal)ChiffreAffaireDuJour + (decimal)paye.montant);
            }

            Label_statistique_catotal = ChiffreAffaireDuJour;
        }

        public void statisticsByMonth()
        {
            float ChiffreAffaireDuJour = 0;

            List<Payement> listePaiement = (List<Payement>)PayementDAO.listSommePaiementToday(3);
            foreach (Payement paye in listePaiement)
            {
                if (!paye.typePaiement.Equals("CleanWay"))
                    ChiffreAffaireDuJour = (float)((decimal)ChiffreAffaireDuJour + (decimal)paye.montant);
            }

            Label_statistique_catotal = ChiffreAffaireDuJour;
        }

        public void statisticsByYear()
        {
            float ChiffreAffaireDuJour = 0;

            List<Payement> listePaiement = (List<Payement>)PayementDAO.listSommePaiementToday(4);
            foreach (Payement paye in listePaiement)
            {
                if (!paye.typePaiement.Equals("CleanWay"))
                    ChiffreAffaireDuJour = (float)((decimal)ChiffreAffaireDuJour + (decimal)paye.montant);
            }

            Label_statistique_catotal = ChiffreAffaireDuJour;
        }
        #endregion

        #region methodes de calcul

        public void DepartmentTTC(List<Article> articlesrendu)
        {
            Boolean ifExist;

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
       
        #endregion
    }
}
