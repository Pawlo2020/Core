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
    /// Logika interakcji dla klasy DescriptionUpdate.xaml
    /// </summary>
    public partial class DescriptionUpdate : UserControl
    {
        public void EX_Query(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, path);
            }
        }
        string _projectID;
        static string Datapath;
        ProjectView _Data;
        UpdateWindow _Update;
        string _taskID;
        public DescriptionUpdate(ProjectView Data,string projectID, string ID, UpdateWindow Update, string taskID)
        {
            _taskID = taskID;
            _Update = Update;
            _projectID = projectID;
            _Data = Data;
            Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data" + @"\DynamicsData\dynamics.sqlite";

            InitializeComponent();
        }

        private void SaveProjectBut_Click(object sender, RoutedEventArgs e)
        {
            string query = $"UPDATE PROJECTS SET DESCRIPTION = '{NewDescriptionTasktxt.Text}' WHERE ID = {_projectID}";
            EX_Query(query, Datapath);
            _Data.DescriptionBlock.Text = NewDescriptionTasktxt.Text;
            _Update.Close();
            MessageBox.Show("Zaktualizowano dane");
            
        }
    }
}
