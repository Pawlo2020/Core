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

namespace Dynamics.Projects.ProjectPages
{
    /// <summary>
    /// Logika interakcji dla klasy TaskUpdateUI.xaml
    /// </summary>
    public partial class TaskUpdateUI : UserControl
    {
        public void EX_Query(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, path);
            }
        }

        public List<string> GetInfo(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetColumnList(query, path);
            }
        }
        List<string> InfoList;
        EventClass _IPHelper;
        ProjectView _Data;
        string _projectID;
        string  _taskID;
        UpdateWindow _Update;
        static string Datapath;
        string _identity;
        public TaskUpdateUI(string ID, ProjectView Data, string projectID, UpdateWindow Update, string taskID, EventClass IPHelper)
        {
            _identity = ID;
            _IPHelper = IPHelper;
            _taskID = taskID;
            _Data = Data;
            _projectID = projectID;
            _Update = Update;
            InitializeComponent();


        }

        private void SaveProjectBut_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartDate = Convert.ToDateTime(NewStartTaskTimeDP.Text);
            DateTime FinishDate = Convert.ToDateTime(NewFinishTaskTimeDP.Text);
            Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite";
            string query = $"UPDATE TASKS SET TASK_NAME = '{NewTaskNametxt.Text}', DATE_START = '{StartDate.ToShortDateString()}', DATE_FINISH = '{FinishDate.ToShortDateString()}', DESCRIPTION = '{NewDescriptionTasktxt.Text}' WHERE ID = {Int32.Parse(_taskID)}";
            InfoList = new List<string>();
            EX_Query(query, Datapath);
            query = $"SELECT * FROM TASKS WHERE FK_ID = {_projectID} ORDER BY ID DESC LIMIT 1";
            InfoList = GetInfo(query, Datapath);
            _IPHelper.DeleteTask(_taskID, _projectID);
            _IPHelper.AddTask(InfoList, _projectID);
            _Update.Close();
            _Data.TaskView.Items.Clear();
            ProjectClass Helper = new ProjectClass();
            Helper.GetTaskList(_projectID,_identity, _Data.TaskView);
            MessageBox.Show("Zaktualizowano dane");
        }
    }
}