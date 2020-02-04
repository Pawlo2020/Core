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

namespace Dynamics.Projects.ProfileElements
{
    /// <summary>
    /// Logika interakcji dla klasy PersonalInfoUC.xaml
    /// </summary>
    public partial class PersonalInfoUC : UserControl
    {
        string ID;
        public string _DynData;
        MainWindow _Window;
        ProfileView _PV;
        private string pass;
        public PersonalInfoUC(List<string> Credentials,ProfileView PV ,MainWindow Window)
        {
            InitializeComponent();
            _PV = PV;
            ID = Credentials[0];
            pass = Credentials[4];
            _Window = Window;
            LoginLBL.Content = Credentials[3];

            for(int i=0;i< Credentials[4].Count(); i++)
            {
                PassLBL.Content += "*";
            }

            FNLBL.Content = Credentials[1];
            LNLBL.Content = Credentials[2];
            EMAILLBL.Content = Credentials[5];
            DOBLBL.Content = Credentials[8] + "." + Credentials[7] + "." + Credentials[6];
            CityLBL.Content = Credentials[9];
        }

        private void MainPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginCH.Visibility = Visibility.Hidden;
            PassCH.Visibility = Visibility.Hidden;
            FNCH.Visibility = Visibility.Hidden;
            LNCH.Visibility = Visibility.Hidden;
            EmailCH.Visibility = Visibility.Hidden;
            DOBCH.Visibility = Visibility.Hidden;
            CityCH.Visibility = Visibility.Hidden;
        }

        private void BR1_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginCH.Visibility = Visibility.Visible;
        }

        private void BR2_MouseEnter(object sender, MouseEventArgs e)
        {
            PassCH.Visibility = Visibility.Visible;
        }


        private void BR4_MouseEnter(object sender, MouseEventArgs e)
        {
            LNCH.Visibility = Visibility.Visible;
        }

        private void BR5_MouseEnter(object sender, MouseEventArgs e)
        {
            EmailCH.Visibility = Visibility.Visible;
        }

        private void BR6_MouseEnter(object sender, MouseEventArgs e)
        {
            DOBCH.Visibility = Visibility.Visible;
        }

        private void BR7_MouseEnter(object sender, MouseEventArgs e)
        {
            CityCH.Visibility = Visibility.Visible;
        }

        private void BR3_MouseEnter(object sender, MouseEventArgs e)
        {
            FNCH.Visibility = Visibility.Visible;
        }

        private void LoginCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin LoginCHange = new ChangeWin(ID, 1, this, _PV,_Window);
            LoginCHange.Show();
        }

        private void PassCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin PassCHange = new ChangeWin(ID, 2, this, _PV,_Window);
            PassCHange.Show();
        }

        private void FNCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin FNCHange = new ChangeWin(ID, 3 ,this, _PV,_Window);
            FNCHange.Show();

        }

        private void LNCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin LNCHange = new ChangeWin(ID, 4,this, _PV,_Window);
            LNCHange.Show();

        }

        private void EmailCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin EmailCHange = new ChangeWin(ID, 5, this, _PV,_Window);
            EmailCHange.Show();

        }

        private void DOBCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin DOBCHange = new ChangeWin(ID, 6, this, _PV,_Window);
            DOBCHange.Show();
        }

        private void CityCH_Click(object sender, RoutedEventArgs e)
        {
            ProfileElements.ChangeWin CityCHange = new ChangeWin(ID, 7, this, _PV,_Window);
            CityCHange.Show();

        }
    }
}
