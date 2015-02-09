using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows.Input;
using App_pressing_Loreau.Model;
using System.Windows;


namespace App_pressing_Loreau.ViewModel
{

    /// <summary>
    /// Classe impression
    /// @param: Pour la méthode: 0: lecture x et 1:lecture Z
    /// Impression d un ticket lecture X en cours de journée et d un ticket lecture z pour la fin de journée
    /// </summary>
    class ImpressionVM : ObservableObject, IPageViewModel
    {

        #region Properties and commands
        public String Name
        {
            get { return ""; }
        }

        public ICommand Btn_impression_imprimerTicketZ
        {
            get { return new RelayCommand(p => printTicketZ(1)); }
        }

        public ICommand Btn_impression_imprimerTicketX
        {
            get { return new RelayCommand(p => printTicketZ(0)); }
        }

        #endregion

        #region Methods
        private void printTicketZ(int var)
        {
            try
            {
                LectureExcel le = new LectureExcel(var);
                le.printLecture();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e);
            }

        }
        #endregion
    }
}
