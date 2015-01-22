using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using App_pressing_Loreau.Helper;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Interactivity;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationClientProVM : ObservableObject
    {
        #region Atrributs
        private List<UnClientProItem> _listeClientPro;
        private List<Client> listedesclientproDTO = null;

        #endregion

        #region
        public AdministrationClientProVM()
        {
            clientsPro();
        }
        #endregion
        #region Properties and commands

        ICommand commandeCLientPro;
        public ICommand CommandeCLientPro
        {
            get { return commandeCLientPro ?? (commandeCLientPro = new RelayCommand(validerClientPro)); }
        }
        public List<UnClientProItem> ListeClientPro
        {
            get { return _listeClientPro; }

            set
            {
                _listeClientPro = value;
                RaisePropertyChanged("ListeClientPro");

            }
        }

        #endregion
        public void validerClientPro(object cltpro)
        {
            InvokeCommandAction clikebutton = cltpro as InvokeCommandAction;

            if (clikebutton != null)
            {
                MessageBox.Show("Bon jour");
               // clikebutton. = Brushes.Red;
                
            }
        }
        public void clientsPro()
        {
            ListeClientPro = new List<UnClientProItem>();

            listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();
            if (listedesclientproDTO != null)
            {
                foreach (Client cl in listedesclientproDTO)
                {
                    ListeClientPro.Add(new UnClientProItem() { Label_idenClientPRO_nomSociete = cl.nom, idClientPro = 10 });
                }

            }
            else
            {
                ListeClientPro.Add(new UnClientProItem() { Label_idenClientPRO_nomSociete = "Esigelec", idClientPro = 10  });
                ListeClientPro.Add(new UnClientProItem() { Label_idenClientPRO_nomSociete = "SNCF", idClientPro = 10  });

            }
        }
       
    }



    #region Class
    class UnClientProItem
    {
        public String Label_idenClientPRO_nomSociete { get; set; }
        public String Label_identCleint_siret { get; set; }
        public int idClientPro { get; set; }
    }
    #endregion
}
