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
using System.IO;

namespace Dynamics.Projects.ProfileElements
{
    /// <summary>
    /// Logika interakcji dla klasy SingleContentChange.xaml
    /// </summary>
    public partial class SingleContentChange : UserControl
    {
        public void EX_QUERY(string query, string Path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, Path);
            }
        }

        string query;
        int _mode;
        string _ID;
        string Data = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\DBUSERS.sqlite";

        ChangeWin _Window;
        PersonalInfoUC _PUC;
        MainWindow _main;
        ProfileView _PV;

        string[] fullname;
        public SingleContentChange(string Label, string ID, int mode, ChangeWin Window, PersonalInfoUC PUC, ProfileView PV, MainWindow main)
        {
            _PUC = PUC;
            _PV = PV;
            InitializeComponent();
            _Window = Window;
            _main = main;
            _mode = mode;
            _ID = ID;
            ContentLBL.Content = Label;
        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case 1:
                    query = $"UPDATE USERS SET LOGIN = '{ChangeBox.Text}' WHERE ID = {Int32.Parse(_ID)}";
                    EX_QUERY(query, $"Data Source={Data}");
                    _PUC.Content = ChangeBox.Text;
                    _Window.Close();
                    break;

                case 3:
                    query = $"UPDATE USERS SET FIRST_NAME = '{ChangeBox.Text}' WHERE ID = {Int32.Parse(_ID)}";
                    EX_QUERY(query, $"Data Source={Data}");
                    _PUC.FNLBL.Content = ChangeBox.Text;
                    _PV.FN_man.Content = ChangeBox.Text;

                    fullname = ((_main.FN_LNlbl.Content).ToString()).Split();
                    _main.FN_LNlbl.Content = ChangeBox.Text + " " + fullname[1];
                    _Window.Close();
                    break;

                case 4:
                    query = $"UPDATE USERS SET LAST_NAME = '{ChangeBox.Text}' WHERE ID = {Int32.Parse(_ID)}";
                    EX_QUERY(query, $"Data Source={Data}");
                    _PUC.LNLBL.Content = ChangeBox.Text;
                    _PV.LN_man.Content = ChangeBox.Text;

                    fullname = ((_main.FN_LNlbl.Content).ToString()).Split();
                    _main.FN_LNlbl.Content =fullname[0] + " " + ChangeBox.Text;
                    _Window.Close();
                    break;

                case 5:
                    query = $"UPDATE USERS SET E_MAIL = '{ChangeBox.Text}' WHERE ID = {Int32.Parse(_ID)}";
                    EX_QUERY(query, $"Data Source={Data}");
                    _PUC.EMAILLBL.Content = ChangeBox.Text;
                    _Window.Close();
                    break;

                case 7:
                    query = $"UPDATE USERS SET CITY = '{ChangeBox.Text}' WHERE ID = {Int32.Parse(_ID)}";
                    EX_QUERY(query, $"Data Source={Data}");
                    _PUC.CityLBL.Content = ChangeBox.Text;
                    _Window.Close();
                    break;
            }
        }
    }
}
