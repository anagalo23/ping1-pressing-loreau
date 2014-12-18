using App_pressing_Loreau.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.View
{

    /// <summary>
    /// Listes des commandes d'un client
    /// Cette classe permettra de constituer les articles a afficher par client 
    /// </summary>
    

    class CommandeConcernantRA_DATA : ObservableObject
    {
        private String _label_restitutionArticles_type;
        private String _label_restitutionArticles_departement;
        private String _label_restitutionArticles_name;

        public CommandeConcernantRA_DATA()
        {

        }

        public String Label_restitutionArticles_type
        {
            get { return _label_restitutionArticles_type; }
            set
            {
                if (value!=_label_restitutionArticles_type)
                {
                    _label_restitutionArticles_type = value;
                    OnPropertyChanged("Label_restitutionArticles_type");
                }
            }
        }


        public String Label_restitutionArticles_departement
        {
            get { return _label_restitutionArticles_departement; }
            set
            {
                if (value != _label_restitutionArticles_departement)
                {
                    _label_restitutionArticles_departement = value;
                    OnPropertyChanged("Label_restitutionArticles_departement");
                }
            }
        }

        public String Label_restitutionArticles_name
        {
            get { return _label_restitutionArticles_name; }
            set
            {
                if (value != _label_restitutionArticles_name)
                {
                    _label_restitutionArticles_name = value;
                    OnPropertyChanged("Label_restitutionArticles_name");
                }
            }
        }
    }
}
