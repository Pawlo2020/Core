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

namespace Dynamics.Projects.ProfileElements
{
    /// <summary>
    /// Logika interakcji dla klasy AdditionalInfoUC.xaml
    /// </summary>
    public partial class AdditionalInfoUC : UserControl
    {
        public AdditionalInfoUC(List<string> Credentials)
        {
            InitializeComponent();

            PhoneLBL.Content = Credentials[12];
            
            JobLBL.Content = Credentials[13];
            Console.WriteLine(Credentials[13]);
            WebsiteLBL.Content = Credentials[14];
        }
    }
}
