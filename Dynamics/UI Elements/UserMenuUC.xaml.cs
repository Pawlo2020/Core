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
using System.Diagnostics;
namespace Dynamics.UI_Elements
{
    /// <summary>
    /// Logika interakcji dla klasy UserMenuUC.xaml
    /// </summary>
    public partial class UserMenuUC : UserControl
    {

        public List<string> GetCredentials(string query, string Datapath)
        {
            using(DynDB qu = new DynDB())
            {
                return qu.GetColumnList(query, Datapath);
            }
        }

        List<string> CredentialsList;
        MainWindow _Window;
        ProfileView ProfilePage;
        public UserMenuUC(MainWindow Window)
        {
            _Window = Window;
            InitializeComponent();
            CredentialsList = new List<string>();
            CredentialsList = GetCredentials($"SELECT * FROM USERS WHERE ID = {_Window._identity}", _Window.path);

            ProfilePage = new ProfileView(Window, CredentialsList);

            
        }

        private void LOGOUT_BUT_Click(object sender, RoutedEventArgs e)
        {

            _Window.Close();
            var applicationPath = System.IO.Directory.GetCurrentDirectory() + @"\Core.exe";
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(applicationPath);
            process.Start();
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
            process.StartInfo.Arguments = $"{CredentialsList[3]} {CredentialsList[4]}";
            process.Start();
        }
    }
}
