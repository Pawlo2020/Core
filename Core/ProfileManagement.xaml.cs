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
using Microsoft.Win32;

namespace Core
{
    public class MyDictionary : Dictionary<string, int> { }
    /// <summary>
    /// Logika interakcji dla klasy ProfileManagement.xaml
    /// </summary>
    public partial class ProfileManagement : UserControl
    {
        public string identity;
        int a;

        public ArrayList EX_QUERY(string query)
        {
            using (DB qu = new DB())
            {
                return qu.Query(query);
            }
        }

        public ArrayList List_Credentials = new ArrayList();

        public Launcher launch;
        public ProfileManagement(string ID, Launcher main)
        {
            launch = main;
            identity = ID;
            string query = $"SELECT FIRST_NAME, LAST_NAME, LOGIN, PASS, E_MAIL, DOB_YEAR, DOB_MONTH, DOB_DAY, CITY FROM USERS WHERE ID = {Int32.Parse(ID)}";
            InitializeComponent();

            List_Credentials = EX_QUERY(query);

            FN_man.Content = List_Credentials[0];
            LN_man.Content = List_Credentials[1];
            LoginChange.Content = List_Credentials[2];


            PassChange.Content = List_Credentials[3];


            EmailChange.Content = List_Credentials[4];
            CityChange.Content = List_Credentials[8];
            DOBChange.Content = List_Credentials[7] + "." + List_Credentials[6] + "." + List_Credentials[5];
        }

        private void profilemanagementBut_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BR1_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginCH.Visibility = Visibility.Visible;
        }

        private void panel_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginCH.Visibility = Visibility.Hidden;
            PassCH.Visibility = Visibility.Hidden;
            EmailCH.Visibility = Visibility.Hidden;
            CityCH.Visibility = Visibility.Hidden;
            DOBCH.Visibility = Visibility.Hidden;
            ImageCH.Visibility = Visibility.Hidden;
            fadeRec.Visibility = Visibility.Hidden;
            NameCH.Visibility = Visibility.Hidden;
        }

        private void BR2_MouseEnter(object sender, MouseEventArgs e)
        {
            PassCH.Visibility = Visibility.Visible;
        }

        private void BR3_MouseEnter(object sender, MouseEventArgs e)
        {
            EmailCH.Visibility = Visibility.Visible;
        }

        private void BR4_MouseEnter(object sender, MouseEventArgs e)
        {
            CityCH.Visibility = Visibility.Visible;
        }

        private void BR5_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageCH.Visibility = Visibility.Visible;
            fadeRec.Visibility = Visibility.Visible;
        }

        private void BR6_MouseEnter(object sender, MouseEventArgs e)
        {
            DOBCH.Visibility = Visibility.Visible;
            
        }

        private void BR7_MouseEnter(object sender, MouseEventArgs e)
        {
            NameCH.Visibility = Visibility.Visible;
        }

        private void LoginCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            LoginUI Loginview = new LoginUI(this, PopUpManagement, a = 1);
            PopUpManagement.StackContent.Children.Add(Loginview);
            Loginview.Llbl.Content = "Nowy login";
            PopUpManagement.Show();
        }
        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Usunąć konto?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {}
            else
            {
                EX_QUERY($"DELETE FROM USERS WHERE ID = {Int32.Parse(identity)}");
                MainWindow main = new MainWindow();
                main.Show();
                launch.Close();
            }
        }

        private void PassCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            PassUI Passview = new PassUI(this, PopUpManagement);
            PopUpManagement.StackContent.Children.Add(Passview);
            PopUpManagement.Show();
        }

        private void EmailCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            LoginUI Emailview = new LoginUI(this, PopUpManagement, a=3);
            PopUpManagement.StackContent.Children.Add(Emailview);
            Emailview.Llbl.Content = "Nowy e-mail";
            PopUpManagement.Show();
        }

        private void CityCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            LoginUI Cityview = new LoginUI(this, PopUpManagement, a = 4);
            PopUpManagement.StackContent.Children.Add(Cityview);
            Cityview.Llbl.Content = "Nowe miasto";
            PopUpManagement.Show();
        }

        private void DOBCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            DOBUI DOBview = new DOBUI(this, PopUpManagement);
            PopUpManagement.StackContent.Children.Add(DOBview);
            PopUpManagement.Show();
        }

        private void NameCH_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow PopUpManagement = new ChangeWindow();
            NameUI Nameview = new NameUI(this, PopUpManagement, launch);
            PopUpManagement.StackContent.Children.Add(Nameview);
            PopUpManagement.Show();
        }

        private void ImageCH_Click(object sender, RoutedEventArgs e)
        {
            string image_path = System.IO.Directory.GetCurrentDirectory() + @"images\user.jpg";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                image_path = openFileDialog.FileName;
                using (DB db = new DB())
                {
                    db.Register($"UPDATE USERS SET PHOTO = @0 WHERE ID = {Int32.Parse(identity)}", image_path);
                    image_man.Source = db.Load_Image(identity);
                    launch.image_prof.Source = db.Load_Image(identity);
                }
            }
        }
    }
}