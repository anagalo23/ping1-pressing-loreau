using App_pressing_Loreau.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace App_pressing_Loreau
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        Button btn;
      
        public Accueil()
        {    
            InitializeComponent();
        }
      
        void btn_user1_Click(Object sender, EventArgs e)
        {
            btn = ((Button)(sender));
            switch (btn.Name)
            {

                case "btn_user1":
                    btn_user2.ClearValue(Button.BackgroundProperty);
                    btn_user3.ClearValue(Button.BackgroundProperty);
                    btn_user4.ClearValue(Button.BackgroundProperty);
                    btn_user5.ClearValue(Button.BackgroundProperty);
                    btn_user6.ClearValue(Button.BackgroundProperty);
                    btn_user1.Background = Brushes.Red;
                    break;
                case "btn_user2":
                    btn_user2.Background = Brushes.Red;
                    btn_user1.ClearValue(Button.BackgroundProperty);
                    btn_user3.ClearValue(Button.BackgroundProperty);
                    btn_user4.ClearValue(Button.BackgroundProperty);
                    btn_user5.ClearValue(Button.BackgroundProperty);
                    btn_user6.ClearValue(Button.BackgroundProperty);

                    break;
                case "btn_user3":
                    btn_user3.Background = Brushes.Red;
                    btn_user2.ClearValue(Button.BackgroundProperty);
                    btn_user1.ClearValue(Button.BackgroundProperty);
                    btn_user4.ClearValue(Button.BackgroundProperty);
                    btn_user5.ClearValue(Button.BackgroundProperty);
                    btn_user6.ClearValue(Button.BackgroundProperty);
                    break;
                case "btn_user4":
                    btn_user4.Background = Brushes.Red;
                    btn_user2.ClearValue(Button.BackgroundProperty);
                    btn_user3.ClearValue(Button.BackgroundProperty);
                    btn_user1.ClearValue(Button.BackgroundProperty);
                    btn_user5.ClearValue(Button.BackgroundProperty);
                    btn_user6.ClearValue(Button.BackgroundProperty);
                    break;
                case "btn_user5":
                    btn_user5.Background = Brushes.Red;
                    btn_user2.ClearValue(Button.BackgroundProperty);
                    btn_user3.ClearValue(Button.BackgroundProperty);
                    btn_user4.ClearValue(Button.BackgroundProperty);
                    btn_user1.ClearValue(Button.BackgroundProperty);
                    btn_user6.ClearValue(Button.BackgroundProperty);
                    break;
                case "btn_user6":
                    btn_user6.Background = Brushes.Red;
                    btn_user2.ClearValue(Button.BackgroundProperty);
                    btn_user3.ClearValue(Button.BackgroundProperty);
                    btn_user4.ClearValue(Button.BackgroundProperty);
                    btn_user5.ClearValue(Button.BackgroundProperty);
                    btn_user1.ClearValue(Button.BackgroundProperty);
                    break;
            }

        }


      

    }


}




