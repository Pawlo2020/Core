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
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Data;
using Dynamics;

namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy Launcher.xaml
    /// </summary>
    public partial class Launcher : Window, IDisposable
    {
        public void Dispose()
        {
            this.Close();
        }
        

        public BitmapSource EX_Load_Image(string _ID)
        {
            using(DB LI = new DB())
            {
                return LI.Load_Image(_ID);
            }
        }

        public ArrayList EX_QUERY(string query)
        {
            using (DB qu = new DB())
            {
                return qu.Query(query);
            }
        }
        BitmapSource _image;
        public ArrayList list = new ArrayList();
        string identity;
        
        public Launcher(string ID)
        {
            identity = ID;
            InitializeComponent();

            
            list = EX_QUERY($"SELECT FIRST_NAME, LOGIN FROM USERS WHERE ID = {Int32.Parse(ID)}");
            username_lbl.Content = list[0];
            _image = EX_Load_Image(identity);
            image_prof.Source = _image;
            
            }
               
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void profilemanagementBut_Click(object sender, RoutedEventArgs e)
        {
            ProfileManagement UC2 = new ProfileManagement(identity,this);
            stkmanagement.Children.Add(UC2);
            UC2.image_man.Source = image_prof.Source;
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Window = new MainWindow();
            this.Close();
            Window.Show();
        }

        private void dynBut_Click(object sender, RoutedEventArgs e)
        {


            

            Dynamics.StartupDynamics Start = new Dynamics.StartupDynamics(identity, list[1], _image);
            try
            {
                Start.Show();
                this.Dispose();
            }
            catch(Exception)
            {
                this.Dispose();
            }
        }

        private void eduBut_Click(object sender, RoutedEventArgs e)
        {
            Education.StartupEducation Start = new Education.StartupEducation(identity, _image);
            try
            {
                Start.Show();
                this.Dispose();
            }
            catch
            {
                this.Dispose();
            }
        }
    }
}