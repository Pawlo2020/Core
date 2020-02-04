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
using System.Windows.Threading;

namespace Education.Main_Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard(Profile_Management.UserCredentials UA)
        {
            InitializeComponent();

            TimeLbl.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.TimeLbl.Content = DateTime.Now.ToString("HH:mm");
            }, this.Dispatcher);

            FullDate.Content = DateTime.Now.ToLongDateString();
        }
    }
}
