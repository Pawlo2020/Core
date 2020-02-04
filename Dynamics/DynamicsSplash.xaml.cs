using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dynamics
{
    /// <summary>
    /// Logika interakcji dla klasy DynamicsSplash.xaml
    /// </summary>
    public partial class DynamicsSplash : Window
    {

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public EventClass IPHelper { get; set; }
        public string JsonPath;
        public void GetScripter(string Path)
        {
            IPHelper = new EventClass(Path);
            if (File.Exists(JsonPath) == false)
            {
                IPHelper.AddEventsFile();
            }
            IPHelper.ColdStart();
        }

        
        public DynamicsSplash(string login)
        {
          
            InitializeComponent();
            JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + login + @"_user_data\DynamicsData\dynamicsEv.json";

            Task t = Task.Run(() =>
            {
                GetScripter(JsonPath);
            
            });

            t.Wait();

        }

        
    }
}
