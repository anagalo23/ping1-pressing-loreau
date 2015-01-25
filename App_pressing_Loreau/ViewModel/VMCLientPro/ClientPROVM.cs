using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_pressing_Loreau.Helper;
using App_pressing_Loreau.Data.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace App_pressing_Loreau.ViewModel
{
    class ClientPROVM : ObservableObject, IPageViewModel
    {
        #region Attributs
        private DelegateCommand<UnClientPROVM> _detailCommandeCouranteClientPro;

        private List<UnClientPROVM> _listeClientPro;
        private List<UnClientPROVM> _detailCommandeClientPro;

        #endregion

        #region constructeur
        public ClientPROVM()
        {
            clientsPro();
        }
        #endregion

        #region Properties and commands
        public List<UnClientPROVM> ListeClientPro
        {
            get { return _listeClientPro; }

            set
            {
                _listeClientPro = value;
                RaisePropertyChanged("ListeClientPro");

            }
        }

        public List<UnClientPROVM> DetailCommandeClientPro
        {
            get { return _detailCommandeClientPro; }
            set
            {
                _detailCommandeClientPro = value;
                OnPropertyChanged("DetailCommandeClientPro");
            }
        }
        public DelegateCommand<UnClientPROVM> DetailCommandeCouranteClientPro
        {
            get
            {
                return this._detailCommandeCouranteClientPro ?? (this._detailCommandeCouranteClientPro = new DelegateCommand<UnClientPROVM>(
                                                                       this.ExecuteClientProCommandeCourante,
                                                                       (arg) => true));
            }
        }
       
        #endregion

        #region Methods

        public void ExecuteClientProCommandeCourante(UnClientPROVM obj)
        {
            MessageBox.Show(obj.clt.id + " " + obj.clt.nom);
        }

       
        public void clientsPro()
        {
            ListeClientPro = new List<UnClientPROVM>();
            List<Client> listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();

            if (listedesclientproDTO != null)
            {
                foreach (Client cl in listedesclientproDTO)
                {
                    ListeClientPro.Add(new UnClientPROVM() { clt = cl, NombreCommande_clientPro = 10 + "" });
                }

            }
            else
            {
                //ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "Esigelec", NombreCommande_clientPro = 10 + "" });
                //ListeClientPro.Add(new UnClientPROVM() { NomSociete_clientPro = "SNCF", NombreCommande_clientPro = 10 + "" });

            }
        }
        #endregion
        public String Name
        {
            get { return ""; }
        }
    }
}
