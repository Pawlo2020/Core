using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy DOBUI.xaml
    /// </summary>
    public partial class DOBUI : UserControl
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
        ProfileManagement UCDOB;
        ChangeWindow PopUp;

        public DOBUI(ProfileManagement main, ChangeWindow main2)
        {
            UCDOB = main;
            PopUp = main2;
            identity = main.identity;
            InitializeComponent();
        }

        private void LChange_Click(object sender, RoutedEventArgs e)
        {
            EX_QUERY($"UPDATE USERS SET DOB_YEAR = '{comboYear.Text}', DOB_MONTH = '{comboMonth.Text}', DOB_DAY = '{comboDay.Text}' WHERE ID = {Int32.Parse(identity)}");

            UCDOB.List_Credentials = EX_QUERY($"SELECT DOB_YEAR, DOB_MONTH, DOB_DAY FROM USERS WHERE ID = {Int32.Parse(identity)}");
            UCDOB.DOBChange.Content = UCDOB.List_Credentials[2] + "." + UCDOB.List_Credentials[1] + "." + UCDOB.List_Credentials[0];
            PopUp.Close();
        }
    }
}
