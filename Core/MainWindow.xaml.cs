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
using System.Data.SQLite;
using Core;
using System.IO;
using System.Reflection;

namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public object Check(string query)
        {
            using (DB login = new DB())
            {
                return login.CheckLogin(query);
            }
        }

        public void Create()
        {
            using(DBUSERS_HELPER help = new DBUSERS_HELPER())
            {
                help.createNewDatabase();
            }
        }
        string parampass;
        string paramlogin;
        string Datapath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public MainWindow()
        {
            InitializeComponent();
            
            

            if (Directory.Exists(Datapath + @"\PawloCore")){
                if(File.Exists(Datapath + @"\PawloCore\DBUSERS.sqlite"))
                {}
                else
                {
                    Create();
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(Datapath + @"\PawloCore");
                Create();
            }

            
         
            string _imageDirectory = System.IO.Directory.GetCurrentDirectory() + @"\images\login\";
            Random _random = new Random();
            string[] imagePaths = System.IO.Directory.GetFiles(_imageDirectory, "*.jpg");
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[_random.Next(imagePaths.Length)]));
            image.Source = bitmapImage;

        //    try
        //    {
        //        //paramlogin = App.Current.Properties["login"].ToString();
        //        //parampass = App.Current.Properties["pass"].ToString();

        //        paramlogin = "admin";
        //        parampass = "admin";
        //        Login();
                

        //    }
        //    catch { }
            
        }
        
        private void signinBut_Click_1(object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Login(string plogin =null, string ppassword=null)
        {
            if(paramlogin == null && parampass == null) {
                plogin = loginBox.Text;
                ppassword = passBox.Password.ToString();
            }
            else
            {
                plogin = paramlogin;
                ppassword = parampass;
            }
            
            string query_login = $"SELECT ID, LOGIN, PASS FROM USERS WHERE LOGIN = '{plogin}' AND PASS = '{ppassword}'";

            var state = Check(query_login);


            if (state is String)
            {
                Console.WriteLine(state);
                correctness.Content = "";
                correctnessbq.Visibility = System.Windows.Visibility.Hidden;
                Launcher launcher = new Launcher(state.ToString());
                launcher.Show();
                this.Close();
            }
            else
            {
                correctness.Content = "Nieprawdłowe dane logowania";
                correctnessbq.Visibility = System.Windows.Visibility.Visible;
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

        private void registerBut_Click(object sender, RoutedEventArgs e)
        {
            UserControl1 UC1 = new UserControl1();
            stkTest.Children.Add(UC1);
        }

       
    }
}


     

