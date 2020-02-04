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
using System.Collections;

namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy NameUI.xaml
    /// </summary>
    public partial class NameUI : UserControl
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


        ProfileManagement UCName;
        ChangeWindow PopUp;
        Launcher launch;
        public NameUI(ProfileManagement main, ChangeWindow main2, Launcher main3)
        {
            UCName = main;
            PopUp = main2;
            launch = main3;
            identity = main.identity;
            InitializeComponent();
        }

        private void LChange_Click(object sender, RoutedEventArgs e)
        {

            EX_QUERY($"UPDATE USERS SET FIRST_NAME = '{FN_Box.Text}', LAST_NAME = '{LN_Box.Text}' WHERE ID = {Int32.Parse(identity)}");

            UCName.List_Credentials = EX_QUERY($"SELECT FIRST_NAME, LAST_NAME FROM USERS WHERE ID = {Int32.Parse(identity)}");
            UCName.FN_man.Content = UCName.List_Credentials[0];
            UCName.LN_man.Content = UCName.List_Credentials[1];
            launch.username_lbl.Content = UCName.List_Credentials[0];
            PopUp.Close();

        }
    }
}
