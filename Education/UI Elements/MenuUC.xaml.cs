using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Education.UI_Elements
{
    /// <summary>
    /// Logika interakcji dla klasy MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        public List<string> GetCredentials(string query, string Datapath)
        {
            using (Database_Handler.EduDB qu = new Database_Handler.EduDB())
            {
                return qu.GetColumnList(query, Datapath);
            }
        }

        List<string> CredentialsList;

        string Datapath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\DBUSERS.sqlite";

        MainWindow _Window;
        Profile_Management.ProfileView ProfilePage;
        public MenuUC(MainWindow Window)
        {
            _Window = Window;
            InitializeComponent();
            ProfilePage = new Profile_Management.ProfileView(Window.UserAccount);

            CredentialsList = new List<string>();
            CredentialsList = GetCredentials($"SELECT LOGIN, PASS FROM USERS WHERE ID = {Window.UserAccount.Identity}", $"Data Source=" + Datapath);
        }

        private void PROF_BUT_Click(object sender, RoutedEventArgs e)
        {
            _Window.frame.Content = ProfilePage;
            _Window.UserMenuStack.Visibility = Visibility.Hidden;
            _Window.menuflag = false;
        }

        private void CLOSE_BUT_Click(object sender, RoutedEventArgs e)
        {
            _Window.Close();
            var applicationPath = System.IO.Directory.GetCurrentDirectory() + @"\Core.exe";
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(applicationPath);
            process.StartInfo.Arguments = $"{CredentialsList[0]} {CredentialsList[1]}";
            process.Start();
        }

        private void LOGOUT_BUT_Click(object sender, RoutedEventArgs e)
        {
            _Window.Close();
            var applicationPath = System.IO.Directory.GetCurrentDirectory() + @"\Core.exe";
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(applicationPath);
            process.Start();
        }
    }
}
