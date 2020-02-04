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
using System.ComponentModel;
using Microsoft.Scripting.Hosting;

namespace Dynamics.Projects
{
    /// <summary>
    /// Logika interakcji dla klasy Project_page.xaml
    /// </summary>
    public partial class Project_page : Page
    {
        public List<string> GetRowsValues(string query, string DBpath)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetRowList(query, DBpath);
            }
        }

        public object GetValue(string query, string DBpath)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetDataScalar(query, DBpath);
            }
        }


        ProjectClass ProjectHelper;
        private string _identity;
        private string filtr;
        public bool Contains(object de)
        {
            ProjectClass order = de as ProjectClass;
            //Return members whose Orders have not been filled
            return (order.STATUS == "ACTIVE");
        }

        EventClass _IPHelper;

        public Project_page(string ID, EventClass IPHelper)

        {
            _IPHelper = IPHelper;
 
            InitializeComponent();
            
            _identity = ID;
            comboBox.SelectedIndex = 1;

            ProjectHelper = new ProjectClass();
            ProjectHelper.GetProjectList(_identity,ProjectList, filtr = $"WHERE STATUS = 'ACTIVE'");

            comboBox.SelectionChanged += (o, e) => RefreshList();

        }


        private void NewProjectBut_Click(object sender, RoutedEventArgs e)
        {
            Projects.ProjectPages.NewProjectPage ProjectPage = new Projects.ProjectPages.NewProjectPage(_identity, _IPHelper);
            NavigationService.Navigate(ProjectPage);
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ProjectClass s = (ProjectClass)ProjectList.SelectedItems[0];
                Projects.ProjectPages.ProjectView ProjectSite = new Projects.ProjectPages.ProjectView(_identity, s, ProjectList, _IPHelper);
                NavigationService.Navigate(ProjectSite);
            }
            catch { }
            ProjectList.SelectedItem = false;

        }


        public void RefreshList()
        {
            if (comboBox.SelectedIndex == 0)
            {
                
                ProjectHelper.GetProjectList(_identity ,ProjectList, filtr = null);
            }
            if (comboBox.SelectedIndex == 1)
            {
                ProjectHelper.GetProjectList(_identity ,ProjectList, filtr = $"WHERE STATUS = 'ACTIVE'");
            }
            if (comboBox.SelectedIndex == 2)
            {
                ProjectHelper.GetProjectList(_identity ,ProjectList, filtr = $"WHERE STATUS = 'INACTIVE'");
            }
        }

        //private void comboBox_Loaded(object sender, RoutedEventArgs e)
        //{

        //    ProjectHelper = new ProjectClass();
        //    ProjectHelper.GetProjectList(_login, ProjectList, filtr = $"WHERE STATUS = 'INACTIVE'");

        //}

        
        }
    
    }




