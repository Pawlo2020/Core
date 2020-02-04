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

namespace Dynamics.Projects.ProjectPages
{
    /// <summary>
    /// Logika interakcji dla klasy UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {

        int _a;
        public UpdateWindow(ProjectView Data, string projectID, string ID, int a, string taskID, EventClass IPHelper)
        {
            _a = a;
            InitializeComponent();
            Data.TaskView.UnselectAll();
            switch (a){
                case 1:
                    DescriptionUpdate DESCUpdate = new DescriptionUpdate(Data, projectID,ID , this, taskID);
                    this.STKUPDATE.Children.Add(DESCUpdate);
                    break;
                case 2:
                    TaskUpdateUI TASKUpdate = new TaskUpdateUI(ID ,Data, projectID, this, taskID, IPHelper);
                    this.STKUPDATE.Children.Add(TASKUpdate);
                    break;
                default:
                    break;
            }
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
    }

}
