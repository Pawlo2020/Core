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
using System.ComponentModel;

namespace Dynamics.Projects.ProjectPages
{
    /// <summary>
    /// Logika interakcji dla klasy NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {

        public void EX_Query(string query, string path)
        {
            using(DynDB qu = new DynDB())
            {
                qu.Execute(query, path);
            }
        }


        public List<string> GetInfo(string query, string path)
        {
            using(DynDB qu = new DynDB())
            {
               return qu.GetColumnList(query, path);
            }
        }
        ListView main;
        
        private string _projectID;
        string _identity;
        EventClass _IPHelper;
        public List<string> InfoList;
        public NewTaskWindow(ListView Site ,string ID, string projectNumber, EventClass IPHelper)
        {
            
            main = Site;
            _identity = ID;
            _projectID = projectNumber;
            _IPHelper = IPHelper;
            InitializeComponent();
            InfoList = new List<string>();
            InfoList = GetInfo($"SELECT PROJECT_NAME, DATE_START, DATE_FINISH FROM PROJECTS WHERE ID = {Int32.Parse(_projectID)}", ($"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data" + @"\DynamicsData\dynamics.sqlite"));

            Project_NameLbl.Content = InfoList[0];
            RangeLbl.Content = (Convert.ToDateTime(InfoList[1])).ToLongDateString() + " - " + (Convert.ToDateTime(InfoList[2])).ToLongDateString();
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SaveProjectBut_Click(object sender, RoutedEventArgs e)
        {
            string Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite";


            if (string.IsNullOrWhiteSpace(TaskNametxt.Text) || StartTaskTimeDP == null || FinishTaskTimeDP == null)
            {
                correctnessbq.Visibility = Visibility.Visible;
                FieldsErrorLbl.Visibility = Visibility.Visible;
                DateRangeErrorLbl.Visibility = Visibility.Hidden;
            }
            else {
                
            DateTime StartDate = Convert.ToDateTime(StartTaskTimeDP.Text);
            DateTime FinishDate = Convert.ToDateTime(FinishTaskTimeDP.Text);

                if (DateTime.Compare(StartDate, FinishDate) > 0 || DateTime.Compare(Convert.ToDateTime(InfoList[1]), StartDate) > 0 ||
                    DateTime.Compare(FinishDate, Convert.ToDateTime(InfoList[2])) > 0)
                {
                    correctnessbq.Visibility = Visibility.Visible;
                    DateRangeErrorLbl.Visibility = Visibility.Visible;
                    FieldsErrorLbl.Visibility = Visibility.Hidden;
                }
                else
                {
                    correctnessbq.Visibility = Visibility.Hidden;
                    DateRangeErrorLbl.Visibility = Visibility.Hidden;
                    FieldsErrorLbl.Visibility = Visibility.Hidden;
                    string query = $"INSERT INTO TASKS(TASK_NAME, DATE_START, DATE_FINISH, DESCRIPTION, FK_ID) VALUES('{TaskNametxt.Text}', '{StartDate.ToShortDateString()}', '{FinishDate.ToShortDateString()}', '{DescriptionTasktxt.Text}', {_projectID});";
                    EX_Query(query, Datapath);

                    query = $"SELECT * FROM TASKS WHERE FK_ID = {_projectID} ORDER BY ID DESC LIMIT 1";
                    
                    InfoList = GetInfo(query, Datapath);
                    _IPHelper.AddTask(InfoList, _projectID);


                    MessageBox.Show("Dodano zadanie");
                    main.Items.Clear();
                    ProjectClass ListHelp = new ProjectClass();
                    ListHelp.GetTaskList(_projectID, _identity, main);
                    this.Close();
                }
        }
        }
    }
}
