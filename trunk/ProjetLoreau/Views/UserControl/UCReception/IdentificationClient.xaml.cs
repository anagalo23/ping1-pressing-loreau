using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ProjetLoreau.View;
using ProjetLoreau.Model.DAO;
using ProjetLoreau.Model.DTO;


namespace ProjetLoreau
{
 

    /// <summary>
    /// Logique d'interaction pour IndentificationClient.xaml
    /// 
    /// </summary>
    public partial class IdentificationClient
    {
       

        public IdentificationClient()
        {
            InitializeComponent();

            //List<Client> retour = ClientDAO.seekClients("", null, "");
            //Client cc = new Client("Test1", "prenom", "067890", "0374839", "Rue de la ******", new DateTime(1995, 03, 23), "Babayetu", DateTime.Now, 2);
            //ClientDAO.insertClient(cc);
            //List<Client> retour = ClientDAO.seekClients("Faye", null, null);
            //Console.Write(retour.Count);
            //List<String> data = new List<String>{retour[0].nom, retour[0].prenom, retour[0].telmob};

           // datagridClient.Items.Add(data);

        }

        private void btn_identClient_valider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_identClient_nouveau_client_Click(object sender, RoutedEventArgs e)
        {
            NouveauClient nouveauClient = new NouveauClient();
            dp.Children.Clear();
            dp.Children.Add(nouveauClient);

           
            
        }

        private void datagridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
