using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Class.DAO;
using App_pressing_Loreau.Class.DTO;


namespace App_pressing_Loreau.Controler
{
    class ControlerRendu
    {
        ArticleDAO ad = new ArticleDAO();

        public static List<Article> getCommandeByNumeroFacture(int num)
        {
           return ArticleDAO.getArticlesById(num); 
        }
    }

}
