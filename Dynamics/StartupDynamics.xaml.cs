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
using System.Windows.Shapes;
using System.IO;
using System.Data.SQLite;

namespace Dynamics
{
    /// <summary>
    /// Logika interakcji dla klasy StartupDynamics.xaml
    /// </summary>
    public partial class StartupDynamics : Window
    {
        string Datapath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        BitmapSource _image;
        string _identity;
        public StartupDynamics(string ID, object login, BitmapSource image)
        {
            _image = image;
            _identity = ID;
            
            if (File.Exists(Datapath + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite"))
            {
                
                MainWindow MyWindow = new MainWindow(_identity);
                MyWindow.image_prof.Source = _image;
                MyWindow.Show();
                this.Close();
            }
            else{
                InitializeComponent();   
            }
        }

        private void StartupBut_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = Directory.CreateDirectory(Datapath + @"\PawloCore\" + _identity + @"_user_data\DynamicsData");
            Datapath = Datapath + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite";
            SQLiteConnection.CreateFile(Datapath);
            DynamicsHelper DH = new DynamicsHelper();
            DH.CreateContent($"Data Source=" + Datapath);

            MainWindow MyWindow = new MainWindow(_identity);
            MyWindow.Show();
            this.Close();
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
