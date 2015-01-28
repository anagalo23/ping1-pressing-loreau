using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.Model
{
    class LectureExcel
    {

        #region parametres
        //autres paramètres
        int type; //0 : LectureX // 1 : LectureZ

        //informations départements
        List<Departement> listUsedDepartements;
        List<float> caTTCDep;

        //informations moyen de paiement
        List<TypePayement> listUsedTypePaiement;
        List<float> caTTCpaiement;

        //informations type article
        List<TypeArticle> listUsedTypeArticle;
        List<int> reçutArticle;
        List<int> renduArticle;

        //autres paramètres statistiques
        int nbNewCommande;
        int nbNewClient;

        //Excel Interop
        public static String pattern_path = "I:\\PING\\App_pressing_loreau\\App_pressing_Loreau\\Resources\\PatternExcel\\LecturePattern";
        private Microsoft.Office.Interop.Excel.Application oXL;
        private Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet mWorkSheets;
        private static object misValue = System.Reflection.Missing.Value;

        #endregion

        #region methodes

        public LectureExcel(int type)
        {
            this.type = type;
            listUsedTypePaiement = PayementDAO.;
        }

        
        #endregion
    }
}
