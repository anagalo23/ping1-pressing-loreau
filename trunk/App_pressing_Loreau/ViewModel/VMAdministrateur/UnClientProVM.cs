using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_pressing_Loreau.ViewModel
{
    class UnClientProVM:ObservableObject
    {
        public Client client;
        public String Label_idenClientPRO_nomSociete 
        {
            get
            {
                return client.nom;
            }
            set
            {
                client.nom = value;
                RaisePropertyChanged("Label_idenClientPRO_nomSociete");
            }
        }
        public String Label_identCleint_siret { get; set; }
        public int idClientPro { get; set; }
       

        //public Client clientPro()
        //{
        //    client.nom = Label_idenClientPRO_nomSociete;
        //    client.id = idClientPro;

        //    return client;
        //}
    }
}
