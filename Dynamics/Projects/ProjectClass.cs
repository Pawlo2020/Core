using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace Dynamics.Projects
{
    public class ProjectClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string STATUS { get; set; }
        public string START { get; set; }
        public string FINISH { get; set; }
        public string DESCRIPTION { get; set; }
        public bool STATUS_TASK { get; set; }
        public double PROGRESS { get; set; }
        public double progressValue { get; set; }
        public List<string> GetValue(string query, string path){
            using(DynDB qu = new DynDB())
            {
                return qu.GetRowList(query, path);
            }
        }

     





        public List<string> TaskList = new List<string>();
        public List<string> ProgressList = new List<string>();
        public Dictionary<string, List<string>> DLIST = new Dictionary<string, List<string>>();
        public List<string> GetRowsValues(string query, string DBpath)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetRowList(query, DBpath);
            }
        }
        bool state;
        public void GetProjectList(string ID, ListView List, string filtr)
        {
            List.Items.Clear();
            string Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data" + @"\DynamicsData\dynamics.sqlite";

            TaskList = GetRowsValues($"SELECT ID FROM PROJECTS " + filtr, Datapath);
            DLIST.Clear();
            DLIST.Add("EID", GetRowsValues($"SELECT ID FROM PROJECTS " + filtr, Datapath));
            DLIST.Add("Name", GetRowsValues($"SELECT PROJECT_NAME FROM PROJECTS " + filtr, Datapath));
            DLIST.Add("Date_Start", GetRowsValues($"SELECT DATE_START FROM PROJECTS " + filtr, Datapath));
            DLIST.Add("Date_Finish", GetRowsValues($"SELECT DATE_FINISH FROM PROJECTS " + filtr, Datapath));
            DLIST.Add("Description", GetRowsValues($"SELECT DESCRIPTION FROM PROJECTS " + filtr, Datapath));
            DLIST.Add("Status", GetRowsValues($"SELECT STATUS FROM PROJECTS " + filtr, Datapath));

            string[] Row = new string[DLIST.Count];
            try
            {
                int j = 0;
                for (int i = 0; i < TaskList.Count; i++)
                {
                    j = 0;
                    if (DLIST.TryGetValue("EID", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Name", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Date_Start", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Date_Finish", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Description", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Status", out TaskList))
                    {
                        if (TaskList[i] == "INACTIVE")
                        {
                            Row[j] = "Ukończony";


                        }
                        else if (TaskList[i] == "ACTIVE")
                        {
                            if (DateTime.Now >= Convert.ToDateTime(Row[2]) && DateTime.Now <= Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Aktywny";
                            }

                            if (DateTime.Now > Convert.ToDateTime(Row[3]) && DateTime.Now > Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Opóźniony";
                            }
                            if (DateTime.Now < Convert.ToDateTime(Row[2]) && DateTime.Now < Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Zaplanowany";
                            }
                        }
                    }


                    List.Items.Add(new ProjectClass { ID = Row[0], Name = Row[1], START = Row[2], FINISH = Row[3], DESCRIPTION = Row[4], STATUS = Row[5] });
                }
            }
            catch { }
        }

        public void GetTaskList(string _projectID, string _identity, ListView List)
        {

            string Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + _identity + @"_user_data" + @"\DynamicsData\dynamics.sqlite";


            TaskList = GetRowsValues($"SELECT ID FROM TASKS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)}", Datapath);
            DLIST.Clear();
            DLIST.Add("EID", GetRowsValues($"SELECT TASKS.ID FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            DLIST.Add("Name", GetRowsValues($"SELECT TASKS.TASK_NAME FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            DLIST.Add("Date_Start", GetRowsValues($"SELECT TASKS.DATE_START FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            DLIST.Add("Date_Finish", GetRowsValues($"SELECT TASKS.DATE_FINISH FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            DLIST.Add("Description", GetRowsValues($"SELECT TASKS.DESCRIPTION FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            DLIST.Add("Status_Task", GetRowsValues($"SELECT TASKS.STATUS FROM TASKS, PROJECTS WHERE TASKS.FK_ID = {Int32.Parse(_projectID)} GROUP BY TASKS.ID", Datapath));
            
            string[] Row = new string[DLIST.Count];
            try
            {
                
                int j;
                for (int i = 0; i < TaskList.Count; i++)
                {
                    j = 0;
                    if (DLIST.TryGetValue("EID", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Name", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Date_Start", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Date_Finish", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Description", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Status_Task", out TaskList))
                    {
                        Row[j] = TaskList[i];
                        if(TaskList[i] == "ACTIVE")
                        {
                            state = false;
                        }
                        else
                        {
                            state = true;
                        }
                    }
                    List.Items.Add(new ProjectClass { ID = Row[0], Name = Row[1], START = Row[2], FINISH = Row[3], DESCRIPTION = Row[4], STATUS_TASK = state});
                }
            }
            catch { }
        }

        public void GetProgress(string path,  string projectID, ProgressBar Bar = null)
        {
            TaskList.Clear();
            string query = $"SELECT COUNT(ID) FROM TASKS WHERE FK_ID = {Int32.Parse(projectID)}";
            TaskList = GetValue(query, path);
            string all = TaskList[0];
            query = $"SELECT COUNT(ID) FROM TASKS WHERE FK_ID = {Int32.Parse(projectID)} AND STATUS = 'INACTIVE'";
            TaskList = GetValue(query, path);
            string inactive = TaskList[0];
            progressValue = Math.Round(((Double.Parse(inactive) / Double.Parse(all))*100), 0);

            try
            {
                Bar.Value = Convert.ToDouble(progressValue);
                
            }
            catch
            {
                Bar.Value = 0;
                progressValue = 0;
            }
            finally
            {
                Duration duration = new Duration(TimeSpan.FromSeconds(0.7));
                DoubleAnimation doubleanimation = new DoubleAnimation(Convert.ToDouble(progressValue), duration);
                doubleanimation.DecelerationRatio = 0.70;
                Bar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            }
            
        }


        public void GetDashboardList(string ID, ItemsControl IC)
        {
            string Datapath = $"Data Source= " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + ID + @"_user_data" + @"\DynamicsData\dynamics.sqlite";

            TaskList = GetRowsValues($"SELECT ID FROM PROJECTS WHERE STATUS = 'ACTIVE' LIMIT 4", Datapath);
            DLIST.Clear();
            DLIST.Add("EID", GetRowsValues($"SELECT ID FROM PROJECTS WHERE STATUS = 'ACTIVE' LIMIT 4", Datapath));
            DLIST.Add("Name", GetRowsValues($"SELECT PROJECT_NAME FROM PROJECTS WHERE STATUS = 'ACTIVE' LIMIT 4", Datapath));
            DLIST.Add("Date_Start", GetRowsValues($"SELECT DATE_START FROM PROJECTS WHERE STATUS = 'ACTIVE' LIMIT 4", Datapath));
            DLIST.Add("Date_Finish", GetRowsValues($"SELECT DATE_FINISH FROM PROJECTS WHERE STATUS = 'ACTIVE' LIMIT 4", Datapath));
            DLIST.Add("Description", GetRowsValues($"SELECT DESCRIPTION FROM PROJECTS LIMIT 4", Datapath));
            DLIST.Add("Status", GetRowsValues($"SELECT STATUS FROM PROJECTS LIMIT 4", Datapath));
            List<ProjectClass> PLIST = new List<ProjectClass>();
            string[] Row = new string[DLIST.Count];
            try
            {
                int j = 0;
                for (int i = 0; i < TaskList.Count; i++)
                {
                    j = 0;
                    if (DLIST.TryGetValue("EID", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Name", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Date_Start", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;

                    if (DLIST.TryGetValue("Date_Finish", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Description", out TaskList))
                    {
                        Row[j] = TaskList[i];
                    }
                    j++;
                    if (DLIST.TryGetValue("Status", out TaskList))
                    {
                        if (TaskList[i] == "INACTIVE")
                        {
                            Row[j] = "Ukończony";


                        }
                        else if (TaskList[i] == "ACTIVE")
                        {
                            if (DateTime.Now >= Convert.ToDateTime(Row[2]) && DateTime.Now <= Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Aktywny";
                            }

                            if (DateTime.Now > Convert.ToDateTime(Row[3]) && DateTime.Now > Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Opóźniony";
                            }
                            if (DateTime.Now < Convert.ToDateTime(Row[2]) && DateTime.Now < Convert.ToDateTime(Row[3]))
                            {
                                Row[j] = "Zaplanowany";
                            }
                        }
                    }
                    
                    string query = $"SELECT COUNT(ID) FROM TASKS WHERE FK_ID = {Int32.Parse(Row[0])}";
                    ProgressList = GetValue(query, Datapath);
                    string all = ProgressList[0];
                    query = $"SELECT COUNT(ID) FROM TASKS WHERE FK_ID = {Int32.Parse(Row[0])} AND STATUS = 'INACTIVE'";
                    ProgressList = GetValue(query, Datapath);
                    string inactive = ProgressList[0];
                    if(all == "0")
                    {
                        progressValue = 0;
                    }
                    else { 
                    progressValue = Math.Round(((Double.Parse(inactive) / Double.Parse(all)) * 100), 0);
                        }
                   
                    PLIST.Add(new ProjectClass() { ID = Row[0], Name = Row[1], START = Row[2], FINISH = Row[3], DESCRIPTION = Row[4], STATUS = Row[5], PROGRESS = progressValue });
                    
                }
                
            }
            catch { }
            IC.ItemsSource = PLIST;








        }

    }
 }

