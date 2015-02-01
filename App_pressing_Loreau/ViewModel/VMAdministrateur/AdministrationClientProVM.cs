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
using Microsoft.Practices.Prism.Commands;

namespace App_pressing_Loreau.ViewModel
{
    class AdministrationClientProVM : ObservableObject
    {
        #region Atrributs
        private List<UnClientProVM> _listeClientPro;
        private List<Client> listedesclientproDTO = null;

        private String _label_identClientPro_choix;
        #endregion

        #region Constructeur
        public AdministrationClientProVM()
        {
            clientsPro();
        }
        #endregion

        #region Properties and commands

        public String Label_identClientPro_choix
        {
            get { return _label_identClientPro_choix; }
            set
            {
                if (value != _label_identClientPro_choix)
                {
                    _label_identClientPro_choix = value;
                    OnPropertyChanged("Label_identClientPro_choix");
                }
            }

        }

        private DelegateCommand<UnClientProVM> commandeClientPro;

        public DelegateCommand<UnClientProVM> CommandeClientPro
        {
            get
            {
                return this.commandeClientPro ?? (this.commandeClientPro = new DelegateCommand<UnClientProVM>(
                                                                       this.validerClientPro,
                                                                       (arg) => true));
            }
        }

        public List<UnClientProVM> ListeClientPro
        {
            get { return _listeClientPro; }

            set
            {
                _listeClientPro = value;
                RaisePropertyChanged("ListeClientPro");

            }
        }

        #endregion
        public void validerClientPro(UnClientProVM obj)
        {
            if (ClasseGlobale.Client != obj.client)
            {
                ClasseGlobale.Client = obj.client;

            }

            if (ClasseGlobale.Client != null)
            {
                Label_identClientPro_choix = "Choix = " + ClasseGlobale.Client.nom ;
            }

            //MessageBox.Show(ClasseGlobale.Client.nom + "  " + ClasseGlobale.Client.id);
            // clikebutton. = Brushes.Red;

        }
        public void clientsPro()
        {
            ListeClientPro = new List<UnClientProVM>();

            listedesclientproDTO = (List<Client>)ClientDAO.selectProClient();
            if (listedesclientproDTO != null)
            {
                foreach (Client cl in listedesclientproDTO)
                {
                    ListeClientPro.Add(new UnClientProVM() { client = cl });
                }

            }
            else
            {
                //ListeClientPro.Add(new UnClientProItem() { Label_idenClientPRO_nomSociete = "Esigelec", idClientPro = 10  });
                //ListeClientPro.Add(new UnClientProItem() { Label_idenClientPRO_nomSociete = "SNCF", idClientPro = 10  });

            }
        }

    }
}
