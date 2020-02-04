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
    /// Logika interakcji dla klasy PassUI.xaml
    /// </summary>
    public partial class PassUI : UserControl
    {
        public ArrayList EX_QUERY(string query)
        {
            using (DB qu = new DB())
            {
                return qu.Query(query);
            }

        }

        public object Check(string query)
        {
            using(DB check = new DB())
            {
                return check.CheckLogin(query);
            }
        }

        public string identity
        {
            get;
            set;
        }
        ProfileManagement UCPass;
        ChangeWindow PopUp;
        public PassUI(ProfileManagement main, ChangeWindow main2)
        {
            UCPass = main;
            PopUp = main2;
            identity = main.identity;
            InitializeComponent();

        }

        private void PChange_Click(object sender, RoutedEventArgs e)
        {

            var state = Check($"SELECT PASS FROM USERS WHERE ID = {Int32.Parse(identity)} AND PASS = '{OLDPass.Password.ToString()}'");
            Console.WriteLine(state);
            if (state is String)
            {
                EX_QUERY($"UPDATE USERS SET PASS = '{NEWPass.Password.ToString()}' WHERE ID = {Int32.Parse(identity)}");
                UCPass.List_Credentials = EX_QUERY($"SELECT PASS FROM USERS WHERE ID = {Int32.Parse(identity)}");
                UCPass.PassChange.Content = UCPass.List_Credentials[0];
                PopUp.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe hasło");
            }
        }
    }
}
