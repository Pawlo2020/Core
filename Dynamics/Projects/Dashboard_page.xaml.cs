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
using System.ComponentModel;
using System.Windows.Threading;

namespace Dynamics.Projects
{
    /// <summary>
    /// Logika interakcji dla klasy Dashboard_page.xaml
    /// </summary>
    public partial class Dashboard_page : Page
    {

        private string _identity, _login;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        ProjectClass DashContent;

        private void SelectDash(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DashContent = (ProjectClass)ProjectDashList.SelectedItems[0];
                Projects.ProjectPages.ProjectView ProjectSite = new Projects.ProjectPages.ProjectView(_identity, DashContent, ProjectDashList, _IPHelper);
                NavigationService.Navigate(ProjectSite);
            }
            catch { }
            
                ProjectDashList.SelectedItem = false;
            
        }

        private void ChangeDescriptionBut_Click(object sender, RoutedEventArgs e)
        {
            Projects.Project_page ProjectsSite = new Projects.Project_page(_identity, _IPHelper);
            NavigationService.Navigate(ProjectsSite);
        }

        EventClass _IPHelper;
        public Dashboard_page(string ID, EventClass IPHelper, string JsonPath)
        {
            _IPHelper = IPHelper;
            _identity = ID;
            InitializeComponent();
            TimeLbl.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
               this.TimeLbl.Content= DateTime.Now.ToString("HH:mm");
            }, this.Dispatcher);

            FullDate.Content = DateTime.Now.ToLongDateString();

            DashContent = new ProjectClass();

            DashContent.GetDashboardList(ID, ProjectDashList);

            ChartHelper Chart = new ChartHelper(JsonPath, WeekChart);
            Chart.SetTasksValues();
        }
    }
}
