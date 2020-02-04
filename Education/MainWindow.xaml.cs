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
using System.Threading;

namespace Education
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Profile_Management.UserCredentials UserAccount { get; set; }
        EduSplashScreen Screen;

        public void StartSplashScreen()
        {

            Screen = new EduSplashScreen();
            Screen.Show();
            Thread.Sleep(5000);
            Screen.Close();
        }

        Main_Pages.Dashboard Dash;
        Main_Pages.Classes Classes;
        Main_Pages.Exams Exams;
        Main_Pages.Calendar_Page Calendar;
        Main_Pages.Courses Courses;
        UI_Elements.MenuUC Menu;

        //CONSTRUCTOR
        public MainWindow(Profile_Management.UserCredentials UA)
        {      
            UserAccount = UA;
            Thread Thr = new Thread(StartSplashScreen);
            Thr.SetApartmentState(ApartmentState.STA);
            Thr.Start();
            Thr.Join();
            InitializeComponent();
            FN_LNlbl.Content = UA.FirstName + " " + UA.LastName;
            image_prof.Source = UA.Photo;

            Dash = new Main_Pages.Dashboard(UA);
            frame.Content = Dash;

            Menu = new UI_Elements.MenuUC(this);
            UserMenuStack.Children.Add(Menu);

            Classes = new Main_Pages.Classes(UA);
            Exams = new Main_Pages.Exams(UA);
            Calendar = new Main_Pages.Calendar_Page(UA);
            Courses = new Main_Pages.Courses(UA);

        }




        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public bool menuflag = false;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (menuflag == false)
            {
                menuflag = true;
                UserMenuStack.Visibility = Visibility.Visible;
            }
            else
            {
                menuflag = false;
                UserMenuStack.Visibility = Visibility.Hidden;

            }
        }

        private void Dash_But_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = Dash;
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Classes_But_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = Classes;
        }

        private void Exams_But_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = Exams;
        }

        private void Calendar_But_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = Calendar;
        }

        private void Courses_But_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = Courses;
        }
    }
}
