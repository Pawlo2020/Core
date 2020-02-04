using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;
using System.Collections;
using System.Windows.Controls;

namespace Dynamics
{
    public class EventClass
    {
        //private List<string> _ProjectList = new List<string>(); 
        protected ScriptEngine Engine;
        protected ScriptSource Source;
        protected ScriptScope Scope;
        protected string _JsonPath;

        public string PID { get; set; }
        public string PNAME { get; set; }
        public string PSTART { get; set; }
        public string PFINISH { get; set; }
        public int EventNumber { get; set; }
        //string IPath = System.IO.Directory.GetCurrentDirectory() + "";


        Dictionary<object, List<object>> ELIST = new Dictionary<object, List<object>>();


        public EventClass(string JsonPath)
        {
            _JsonPath = JsonPath;
            Engine = IronPython.Hosting.Python.CreateEngine();
            Scope = Engine.CreateScope();
            Engine.SetSearchPaths(new string[] { System.IO.Directory.GetCurrentDirectory(),
                                                 System.IO.Directory.GetCurrentDirectory() + @"\packages\"});
            


        }

        public void ColdStart()
        {
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\ColdStart.py");
            Source.Execute(Scope);
        }

        public void AddEvent(List<string> ProjectList)
        {
            

            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\AddEvent.py");
            Scope.SetVariable("PNAME", ProjectList[1]);
            Scope.SetVariable("PSTART", ProjectList[3]);
            Scope.SetVariable("PID", ProjectList[0]);
            Scope.SetVariable("PFINISH", ProjectList[4]);
            Scope.SetVariable("PATH", _JsonPath);
            Source.Execute(Scope);
        }

        public void DeleteProjectEvent(string PID)
        {
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\DeleteEvent.py");
            Scope.SetVariable("PATH", _JsonPath);
            Scope.SetVariable("PID", PID);
            Source.Execute(Scope);
        }

        public void GetEvents(ItemsControl Container, DateTime CorrectDate)
        {
            List<object> TestList = new List<object>();
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\GetEvents.py");
            Scope.SetVariable("PATH", _JsonPath);

            ELIST.Clear();
            Source.Execute(Scope);
            TestList = Scope.GetVariable("IDdyn");
            ELIST = Scope.GetVariable("PDICT");

            object[] Event = new object[4];

            
                int j;
                for(int i = 0; i<TestList.Count; i++)
                {
                    j = 0;
                    if(ELIST.TryGetValue("PID", out TestList))
                    {
                        Event[j] = TestList[i];
                        
                    }j++;
                    if (ELIST.TryGetValue("PNAME", out TestList))
                    {
                        Event[j] = TestList[i];
                        
                    }
                    j++;
                    if (ELIST.TryGetValue("PSTART", out TestList))
                    {
                        Event[j] = TestList[i];
                        
                    }
                    j++;
                    if (ELIST.TryGetValue("PFINISH", out TestList))
                    {
                        Event[j] = TestList[i];
                       
                    }

                //Console.WriteLine("END EVENT");

                //Binding

                if ((Container.Items.Count < 2 && (CorrectDate >= Convert.ToDateTime(Event[2]) && CorrectDate<=Convert.ToDateTime(Event[3]))))
                {
                    if(CorrectDate!= Convert.ToDateTime(Event[2]))
                    {
                        Event[1] = "-----------------------------------------------";
                    }
                    Container.Items.Add(new EventClass(_JsonPath) { PID = Event[0].ToString(), PNAME = Event[1].ToString(), PSTART = Event[2].ToString(), PFINISH = Event[3].ToString() });
                }
                }

            EventNumber = TestList.Count;
        }

        public void AddEventsFile()
        {
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\AddEventsScript.py");
            Scope.SetVariable("PATH", _JsonPath);
            Source.Execute(Scope);
        }

        public void AddTask(List<string> TaskList, string projID)
        {
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\AddTask.py");
            Scope.SetVariable("PID", projID);
            Scope.SetVariable("PATH", _JsonPath);
            Scope.SetVariable("TID", TaskList[0]);
            Scope.SetVariable("TNAME", TaskList[1]);
            Scope.SetVariable("TSTART", TaskList[2]);
            Scope.SetVariable("TFINISH", TaskList[3]);
            Scope.SetVariable("TFKID", projID);
            Source.Execute(Scope);
        }

        public void DeleteTask(string TID, string projID)
        {
            Source = Engine.CreateScriptSourceFromFile(System.IO.Directory.GetCurrentDirectory() + @"\EventScripts\DeleteTask.py");
            Scope.SetVariable("PID", projID);
            Scope.SetVariable("PATH", _JsonPath);
            Scope.SetVariable("TID", TID);
            Source.Execute(Scope);
        }

        
        }

        
        


    }

