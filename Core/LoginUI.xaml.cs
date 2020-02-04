using System;
using System.Collections.Generic;
using System.Collections;
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

namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy LoginUI.xaml
    /// </summary>
    public partial class LoginUI : UserControl
    {

        public ArrayList EX_QUERY(string query)
        {
            using (DB qu = new DB())
            {
                return qu.Query(query);
            }

        }

        public string identity
        {
            get;
            set;
        }

        private int _a;

        ProfileManagement UC;
        ChangeWindow PopUp;

        public LoginUI(ProfileManagement main, ChangeWindow main2, int a)
        {
            _a = a;
            UC = main;
            PopUp = main2;
            identity = main.identity;
            InitializeComponent();
            
            

        }

        private void LChange_Click(object sender, RoutedEventArgs e)
        {
            switch (_a)
            {


                case 1:
                    EX_QUERY($"UPDATE USERS SET Login = '{Box.Text}' WHERE ID = {Int32.Parse(identity)}");

                    UC.List_Credentials = EX_QUERY($"SELECT LOGIN FROM USERS WHERE ID = {Int32.Parse(identity)}");
                    UC.LoginChange.Content = UC.List_Credentials[0];
                    PopUp.Close();
                    break;

                case 3:
                    EX_QUERY($"UPDATE USERS SET E_MAIL = '{Box.Text}' WHERE ID = {Int32.Parse(identity)}");

                    UC.List_Credentials = EX_QUERY($"SELECT E_MAIL FROM USERS WHERE ID = {Int32.Parse(identity)}");
                    UC.EmailChange.Content = UC.List_Credentials[0];
                    PopUp.Close();
                    break;

                case 4:
                    EX_QUERY($"UPDATE USERS SET CITY = '{Box.Text}' WHERE ID = {Int32.Parse(identity)}");

                    UC.List_Credentials = EX_QUERY($"SELECT CITY FROM USERS WHERE ID = {Int32.Parse(identity)}");
                    UC.CityChange.Content = UC.List_Credentials[0];
                    PopUp.Close();
                    break;

                default:
                    MessageBox.Show("Błąd");
                    break;
            }


            
        }
    }
}
