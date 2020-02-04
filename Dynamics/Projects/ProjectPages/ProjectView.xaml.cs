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
using System.Windows.Media.Animation;

namespace Dynamics.Projects.ProjectPages
{
    /// <summary>
    /// Logika interakcji dla klasy ProjectView.xaml
    /// </summary>
    public partial class ProjectView : Page
    {

        public List<string> GetRowsValues(string query, string DBpath)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetRowList(query, DBpath);
            }
        }
        

        public void EX_Query(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, path);
            }
        }

        public void CheckLabel(Label Lbl1, ProjectClass Class)
        {
            if(Convert.ToInt32(Class.progressValue) == 100)
            {
                Lbl1.Visibility = Visibility.Visible;
                EX_Query($"UPDATE PROJECTS SET STATUS = 'INACTIVE' WHERE ID = {Int32.Parse(_projectID)}", Datapath);
            }
            else
            {
                EX_Query($"UPDATE PROJECTS SET STATUS = 'ACTIVE' WHERE ID = {Int32.Parse(_projectID)}", Datapath);
                Lbl1.Visibility = Visibility.Hidden;
            }

        }

        int a;
        static string userID, _projectID;
        static string _taskID;
        string query;
        ProjectClass TaskHelper = new ProjectClass();
        ProjectClass t;
        CheckBox Check;
        public string Datapath;
        string filtr;

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow NewTask = new NewTaskWindow(TaskView,userID, _projectID, _IPHelper);
            NewTask.Show();
            TaskHelper.GetProgress(Datapath, _projectID ,Progressbar);
            ProgressLbl.Content = TaskHelper.progressValue + "%";
            CheckLabel(projectDoneLbl, TaskHelper);
        }

        private void ReturnParentBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                main.Items.Clear();
                TaskHelper.GetProjectList(userID, main, filtr);
            }
            catch { }
            finally
            {
                NavigationService.GoBack();
            }
        }

        private void DeleteProjBut_Click(object sender, RoutedEventArgs e)
        {
            string query = $"DELETE FROM PROJECTS WHERE ID = {_projectID}";
            EX_Query(query, Datapath);
            
            query = $"DELETE FROM TASKS WHERE FK_ID = {_projectID}";
            EX_Query(query, Datapath);

            _IPHelper.DeleteProjectEvent(_projectID);
           
            try
            {
                main.Items.Clear();
                TaskHelper.GetProjectList(userID, main, filtr=null);
            }
            catch { }
            finally
            {
                NavigationService.GoBack();
                CheckLabel(projectDoneLbl, TaskHelper);
                main.SelectedItem = false;
            }
        }



        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TaskView.IsEnabled)
                {
                    ChangeTask.Visibility = Visibility.Visible;
                    DeleteTask.Visibility = Visibility.Visible;
                }
                t = (ProjectClass)TaskView.SelectedItems[0];
                _taskID = t.ID;
                Console.WriteLine("Takes " + _taskID);
            }
            catch {}

        }

        Dictionary<string, List<string>> TNAME = new Dictionary<string, List<string>>();

        private void ChangeDescriptionBut_Click(object sender, RoutedEventArgs e)
        {
            a = 1;
            UpdateWindow Update = new UpdateWindow(this,_projectID,userID, a, _taskID, _IPHelper);
            Update.Show();
        }
        
        private void ChangeTask_Click(object sender, RoutedEventArgs e)
        {
            a = 2;
            UpdateWindow Update = new UpdateWindow(this, _projectID, userID, a, _taskID, _IPHelper);
            Update.Show();

            
        }

        private void Event_Checked(object sender, RoutedEventArgs e)
        {
            Check = sender as CheckBox;
            ProjectClass t = Check.DataContext as ProjectClass;
            query = $"UPDATE TASKS SET STATUS = 'INACTIVE' WHERE ID = {Int32.Parse(t.ID)}";
            EX_Query(query, Datapath);
            TaskHelper.GetProgress(Datapath, _projectID, Progressbar);
            ProgressLbl.Content = TaskHelper.progressValue + "%";
            CheckLabel(projectDoneLbl, TaskHelper);
        }

        private void Event_Unchecked(object sender, RoutedEventArgs e)
        {
            Check = sender as CheckBox;
            ProjectClass t = Check.DataContext as ProjectClass;
            query = $"UPDATE TASKS SET STATUS = 'ACTIVE' WHERE ID = {Int32.Parse(t.ID)}";
            EX_Query(query, Datapath);
            TaskHelper.GetProgress(Datapath, _projectID, Progressbar);
            ProgressLbl.Content = TaskHelper.progressValue + "%";
            CheckLabel(projectDoneLbl, TaskHelper);
        }

        private void TaskView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
                TaskView.UnselectAll();

            ChangeTask.Visibility = Visibility.Hidden;
            DeleteTask.Visibility = Visibility.Hidden;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            _IPHelper.DeleteTask(_taskID, _projectID);
            query = $"DELETE FROM TASKS WHERE ID = '{Int32.Parse(_taskID)}'";
            EX_Query(query, Datapath);

            TaskView.Items.Clear();
            TaskHelper.GetTaskList(_projectID, userID, TaskView);
            TaskHelper.GetProgress(Datapath,_projectID, Progressbar);
            ProgressLbl.Content = TaskHelper.progressValue + "%";
            CheckLabel(projectDoneLbl, TaskHelper);
        }

        EventClass _IPHelper;
        ListView main;
        public ProjectView(string ID, ProjectClass Project, ListView Site, EventClass IPHelper)
        {
            _IPHelper = IPHelper;
            main = Site;
            Site.SelectedItem = false;
            InitializeComponent();
            _projectID = Project.ID;
            userID = ID;


            Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data" + @"\DynamicsData\dynamics.sqlite";


            this.DataContext = this;

            TaskHelper.GetTaskList(_projectID,ID ,TaskView);
            

            //Assignments 
            ProjectLbl.Content = Project.Name;
            ProgressLbl.Content = Progressbar.Value + "%";
            if (Project.DESCRIPTION != null)
            {
                DescriptionBlock.Text = Project.DESCRIPTION;
            }
            else
            {
                DescriptionBlock.Text = "Brak opisu";
            }

            TaskHelper.GetProgress(Datapath, _projectID, Progressbar);
            ProgressLbl.Content = TaskHelper.progressValue + "%";
            CheckLabel(projectDoneLbl, TaskHelper);
        }
    }
}
