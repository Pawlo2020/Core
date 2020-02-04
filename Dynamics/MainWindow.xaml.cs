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
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Dynamics
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> GetDataValues(string query, string DBpath)
        {
            using(DynDB qu = new DynDB())
            {
                return qu.GetColumnList(query, DBpath);
            }
        }

        public object GetValue(string query, string DBpath)
        {
            using(DynDB qu = new DynDB())
            {
                return qu.GetDataScalar(query, DBpath);
            }
        }
        public string path = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\DBUSERS.sqlite";
        string JsonPath;
        public string _identity;
        private List<string> DataList = new List<string>();

        EventClass IPHelper;

        DynamicsSplash Screen;
        public void StartSplashScreen()
        {

            Screen = new DynamicsSplash(_identity);
            Screen.Show();
            Thread.Sleep(5000);
            IPHelper = Screen.IPHelper;
            Screen.Close();
        }

        UI_Elements.UserMenuUC MenuUC;
        public MainWindow(string ID)
        {

            _identity = ID;
            Thread Thr = new Thread(StartSplashScreen);
            Thr.SetApartmentState(ApartmentState.STA);
            Thr.Start();
            Thr.Join();
            
            JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data\DynamicsData\dynamicsEv.json";


            
            _identity = ID;
            this.DataContext = this;

            DataList = GetDataValues($"SELECT FIRST_NAME, LAST_NAME FROM USERS WHERE ID = {Int32.Parse(_identity)}", path);
            InitializeComponent();

            MenuUC = new UI_Elements.UserMenuUC(this);
            UserMenuStack.Children.Add(MenuUC);

            FN_LNlbl.Content = DataList[0] + " " + DataList[1];
            Dynamics.Projects.Dashboard_page Page1 = new Projects.Dashboard_page(_identity, IPHelper, JsonPath);
            frame.Content = Page1;
            
            


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

        private void Project_But_Click(object sender, RoutedEventArgs e)
        {
           Projects.Project_page Page2 = new Projects.Project_page(_identity, IPHelper);
           frame.Content = Page2;
        }

        private void Dash_But_Click(object sender, RoutedEventArgs e)
        {
            Projects.Dashboard_page Page1 = new Projects.Dashboard_page(_identity, IPHelper, JsonPath);
            frame.Content = Page1;
        }

        private void Calendar_But_Click(object sender, RoutedEventArgs e)
        {
            Projects.Calendar_page Page3 = new Projects.Calendar_page(_identity, IPHelper, JsonPath);
            frame.Content = Page3;
        }

        private void Help_But_Click(object sender, RoutedEventArgs e)
        {
            Projects.Help_page Page4 = new Projects.Help_page();
            frame.Content = Page4;
        }
        public bool menuflag=false;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (menuflag == false)
            {
                menuflag = true;
                UserMenuStack.Visibility = Visibility.Visible;
            }
            else {
                menuflag = false;
                UserMenuStack.Visibility = Visibility.Hidden;

            }
        }
    }
}