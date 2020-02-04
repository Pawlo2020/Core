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
using System.Data.SQLite;
using System.Collections;
using Microsoft.Scripting.Hosting;

namespace Dynamics.Projects.ProjectPages
{
    /// <summary>
    /// Logika interakcji dla klasy NewProjectPage.xaml
    /// </summary>
    public partial class NewProjectPage : Page
    {
        public void EX_OR(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, path);
            }
        }

        public List<string> GetID(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetColumnList(query, path);
            }
        }

        string _identity;
        int result;
        EventClass _IPHelper;
        List<string> PList = new List<string>();
        public NewProjectPage(string ID, EventClass IPHelper)
        {
            _IPHelper = IPHelper;
            _identity = ID;

            InitializeComponent();
        }

        private void ReturnParentBut_Click(object sender, RoutedEventArgs e)
        {
            Projects.Project_page Page2 = new Projects.Project_page(_identity, _IPHelper);
            NavigationService.Navigate(Page2);
        }

        private void SaveProjectBut_Click(object sender, RoutedEventArgs e)
        {
            string Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite";


            if (string.IsNullOrWhiteSpace(ProjectNametxt.Text) || StartTimeDP.SelectedDate == null || FinishTimeDP.SelectedDate==null)
            {
                correctnessbq.Visibility = Visibility.Visible;
                FieldsErrorLbl.Visibility = Visibility.Visible;
                DateRangeErrorLbl.Visibility = Visibility.Hidden;
            }
            else {
                
                DateTime StartDate = Convert.ToDateTime(StartTimeDP.Text);
                DateTime FinishDate = Convert.ToDateTime(FinishTimeDP.Text);

                Console.WriteLine(StartDate.ToShortDateString());
                result = DateTime.Compare(StartDate, FinishDate);

                if (result > 0)
                {
                    correctnessbq.Visibility = Visibility.Visible;
                    DateRangeErrorLbl.Visibility = Visibility.Visible;
                    FieldsErrorLbl.Visibility = Visibility.Hidden;
                }
                else
                {

                    try
                    {
                        correctnessbq.Visibility = Visibility.Hidden;
                        DateRangeErrorLbl.Visibility = Visibility.Hidden;
                        FieldsErrorLbl.Visibility = Visibility.Hidden;
                        
                            string query = $"INSERT INTO PROJECTS (PROJECT_NAME, DATE_START, DATE_FINISH, DESCRIPTION) VALUES('{ProjectNametxt.Text}', '{StartDate.ToShortDateString()}', '{FinishDate.ToShortDateString()}', '{Descriptiontxt.Text}')";
                            EX_OR(query, Datapath);
                            query = $"SELECT * FROM PROJECTS ORDER BY ID DESC LIMIT 1";
                            PList = GetID(query, Datapath);
                        
                        _IPHelper.AddEvent(PList);

                        MessageBox.Show("Utworzono projekt");
                        Projects.Project_page Page2 = new Projects.Project_page(_identity, _IPHelper);
                        NavigationService.Navigate(Page2);
                    }
                    catch
                    {}
                }
            }
        }

    }

}


