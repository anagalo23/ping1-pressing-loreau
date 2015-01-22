using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Collections.ObjectModel;

namespace App_pressing_Loreau.ViewModel
{
    class ClientPROVM : ObservableObject, IPageViewModel
    {
        #region Attributs

        private List<UnClientPROVM> _listeClientPro;
        private List<Client> listedesclientproDTO = null;
        #endregion

        #region constructeur
        public ClientPROVM()
        {
           // clientsPro();            
        }
        #endregion

        #region Properties and commands
        public List<UnClientPROVM> ListeClientPro
        { get { return _listeClientPro; }

            set
            {
                    _listeClientPro = value;
                    RaisePropertyChanged("ListeClientPro");
                
            }
        }

        #endregion

        #region Methods
        public void clientsPro()
        {
            ListeClientPro = new List<UnClientPROVM>();

            listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();
            if (listedesclientproDTO != null)
            {
                foreach (Client cl in listedesclientproDTO)
                {
                    ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = cl.nom, NombreCommande_clientPro = 10 + "" });
                }

            }
            else
            {
                ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "Esigelec", NombreCommande_clientPro = 10 + "" });
                ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "SNCF", NombreCommande_clientPro = 10 + "" });

            }
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
