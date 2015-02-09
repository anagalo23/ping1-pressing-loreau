using App_pressing_Loreau.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App_pressing_Loreau.ViewModel
{
    class PageDemarrageVM: ObservableObject
    {

        #region Attributes
        private int _billet100EURO;
        private int _billet50EURO;
        private int _billet20EURO;
        private int _billet10EURO;
        private int _billet5EURO;
        private int _piece2Euro;
        private int _piece1Euro;

        private double _piece50c;
        private double _piece20c;
        private double _piece10c;
        private double _piece5c;
        private double _piece2c;

        private double _label_pageDemmarrage_total;

        public static double Somme { get; set; }
        #endregion

        #region Constructeur
        public PageDemarrageVM()
        {
            Label_pageDemmarrage_total = new Double();

            Label_pageDemmarrage_total = 0;
            Somme = 0;
        }
        #endregion

        #region Properties and Command

        public int Billet100EURO
        {
            get { return _billet100EURO; }
            set
            {
                if (value != _billet100EURO)
                {
                    _billet100EURO = value;
                    //Somme += value*100;
                    OnPropertyChanged("Billet100EURO");
                }
            }
        }

        public int Billet50EURO
        {
            get { return _billet50EURO; }
            set
            {
                if (value != _billet50EURO)
                {
                    _billet50EURO = value;
                    //Somme += value*50;
                    OnPropertyChanged("Billet50EURO");
                }
            }
        }

        public int Billet20EURO
        {
            get { return _billet20EURO; }
            set
            {
                if (value != _billet20EURO)
                {
                    _billet20EURO = value;
                    //Somme += value*20;
                    OnPropertyChanged("Billet20EURO");
                }
            }
        }

        public int Billet10EURO
        {
            get { return _billet10EURO; }
            set
            {
                if (value != _billet10EURO)
                {
                    _billet10EURO = value;
                    //Somme += value*10;
                    OnPropertyChanged("Billet10EURO");
                }
            }
        }

        public int Billet5EURO
        {
            get { return _billet5EURO; }
            set
            {
                if (value != _billet5EURO)
                {
                    _billet5EURO = value;
                    //Somme += value*5;
                    OnPropertyChanged("Billet5EURO");
                }
            }
        }

        public int Piece2Euro
        {
            get { return _piece2Euro; }
            set
            {
                if (value != _piece2Euro)
                {
                    _piece2Euro = value;
                    //Somme += value*2;
                    OnPropertyChanged("Piece2Euro");
                }
            }
        }

        public int Piece1Euro
        {
            get { return _piece1Euro; }
            set
            {
                if (value != _piece1Euro)
                {
                    _piece1Euro = value;
                    //Somme += value*1;
                    OnPropertyChanged("Piece1Euro");
                }
            }
        }

        public double Piece50c
        {
            get { return _piece50c; }
            set
            {
                if (value != _piece50c)
                {
                    _piece50c = value;
                    //Somme += value* 0.5;
                    OnPropertyChanged("Piece50c");
                }
            }
        }

        public double Piece20c
        {
            get { return _piece20c; }
            set
            {
                if (value != _piece20c)
                {
                    _piece20c = value;
                    //Somme += value*0.2;
                    OnPropertyChanged("Piece20c");
                }
            }
        }

        public double Piece10c
        {
            get { return _piece10c; }
            set
            {
                if (value != _piece10c)
                {
                    _piece10c = value;
                    //Somme += value*0.1;
                    OnPropertyChanged("Piece10c");
                }
            }
        }

        public double Piece5c
        {
            get { return _piece5c; }
            set
            {
                if (value != _piece5c)
                {
                    _piece5c = value;
                    //Somme += value*0.05;
                    OnPropertyChanged("Piece5c");
                }
            }
        }

        public double Piece2c
        {
            get { return _piece2c; }
            set
            {
                if (value != _piece2c)
                {
                    _piece2c = value;
                    //Somme += value*0.02;
                    OnPropertyChanged("Piece2c");
                }
            }
        }

        public double Label_pageDemmarrage_total
        {
            get { return _label_pageDemmarrage_total; }
            set
            {
                if (value != _label_pageDemmarrage_total)
                {
                    _label_pageDemmarrage_total = value;    
                    OnPropertyChanged("Label_pageDemmarrage_total");
                }
            }
        }


        public ICommand Btn_pageDemarrage_valider
        {
            get { return new RelayCommand(p => totalFondCaisse()); }
        }
        #endregion

        #region Methods
        public void totalFondCaisse()
        {

            Somme = (double)((decimal)(Billet100EURO * 100) + (decimal)(Billet50EURO * 50) + (decimal)(Billet20EURO *20) +
                (decimal)(Billet10EURO * 10) + (decimal)(Billet5EURO * 5) + (decimal)(Piece2Euro * 2) + (decimal)(Piece1Euro) +
                (decimal)(Piece50c * 0.5) + (decimal)(Piece20c * 0.2) + (decimal)(Piece10c * 0.1) + (decimal)(Piece2c * 0.02) +
                (decimal)(Piece5c * 0.05));

            Label_pageDemmarrage_total = Somme;
            Somme=0;
        }
        #endregion
    }
}
