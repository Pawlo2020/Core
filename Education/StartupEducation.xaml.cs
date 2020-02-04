using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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
using System.Windows.Shapes;

namespace Education
{
    /// <summary>
    /// Logika interakcji dla klasy StartupEducation.xaml
    /// </summary>
    public partial class StartupEducation : Window
    {
        string Datapath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);


        Profile_Management.UserCredentials UserAccount;
        public StartupEducation(string ID, BitmapSource image)
        {
            UserAccount = new Profile_Management.UserCredentials(ID);
            UserAccount.Identity = ID;
            UserAccount.Photo = image;
            UserAccount.GetCredentials();
            if (File.Exists(Datapath + @"\PawloCore\" + UserAccount.Identity + @"_user_data" + @"\EducationData\education.sqlite"))
            {
                MainWindow MyWindow = new MainWindow(UserAccount);
                MyWindow.Show();
                this.Close();
            }
            else
            {
                InitializeComponent();

            }
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void StartupBut_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = Directory.CreateDirectory(Datapath + @"\PawloCore\" + UserAccount.Identity + @"_user_data\EducationData");
            Datapath = Datapath + @"\PawloCore\" + UserAccount.Identity + @"_user_data" + @"\EducationData\education.sqlite";
            SQLiteConnection.CreateFile(Datapath);
            Database_Handler.EducationHelper EH = new Database_Handler.EducationHelper();
            EH.CreateContent($"Data Source=" + Datapath);

            MainWindow MyWindow = new MainWindow(UserAccount);
            MyWindow.Show();
            this.Close();
        }
    }
}
