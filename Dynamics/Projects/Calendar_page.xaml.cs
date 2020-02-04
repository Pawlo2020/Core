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
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections;

namespace Dynamics.Projects
{
    /// <summary>
    /// Logika interakcji dla klasy Calendar_page.xaml
    /// </summary>
    public partial class Calendar_page : Page
    {
        private string _identity;
        List<object> List = new List<object>();
        public Calendar_page(string ID, EventClass IPHelper, string JsonPath)
        {
            
            string a = "HELLO";
            _identity = ID;
            //Scope.SetVariable("a", a);
            //Source.Execute(Scope);
            //List = Scope.GetVariable("lista");
            
            InitializeComponent();

            //for (int i = 0; i < List.Count; i++)
            //{
            //    Console.WriteLine(Convert.ToString(List[i]));
            //}

            Calendar Cal = new Calendar(IPHelper, JsonPath, this);
            CalendarContener.Children.Add(Cal);
        }
    }
}
