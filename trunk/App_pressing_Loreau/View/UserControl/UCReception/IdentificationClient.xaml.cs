using App_pressing_Loreau;
using App_pressing_Loreau.Model.DAO;
using App_pressing_Loreau.Model.DTO;
using System.Windows;
using System.Windows.Controls;


namespace App_pressing_Loreau
{
 
    /// <summary>
    /// Logique d'interaction pour IndentificationClient.xaml
    /// 
    /// </summary>
    public partial class IdentificationClient : System.Windows.Controls.UserControl
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
     
        //    //nouveauClient.parameter = 5;
        //    //dp.Children.Clear();
        //    //dp.Children.Add(nouveauClient);

           
            
        }

        private void datagridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
