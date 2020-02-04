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

namespace Dynamics.Projects.ProfileElements
{
    /// <summary>
    /// Logika interakcji dla klasy ChangeWin.xaml
    /// </summary>
    public partial class ChangeWin : Window
    {
        public ChangeWin(string ID, int a, PersonalInfoUC PUC,ProfileView PV ,MainWindow Window)
        {
            
            InitializeComponent();

            switch (a)
            {
                case 1:
                    SingleContentChange LoginChangeUC = new SingleContentChange("Nowy login", ID, 1, this, PUC,PV,Window);
                    StackContent.Children.Add(LoginChangeUC);
                    break;

                case 2:
                    PassContentChange PassChangeUC = new PassContentChange(ID, this);
                    StackContent.Children.Add(PassChangeUC);
                    break;

                case 3:
                    SingleContentChange FNChangeUC = new SingleContentChange("Nowe imię", ID, 3, this,PUC,PV,Window);
                    StackContent.Children.Add(FNChangeUC);
                    break;

                case 4:
                    SingleContentChange LNChangeUC = new SingleContentChange("Nowe nazwisko", ID, 4, this, PUC,PV,Window);
                    StackContent.Children.Add(LNChangeUC);
                    break;

                case 5:
                    SingleContentChange EmailChangeUC = new SingleContentChange("Nowy E-Mail", ID, 5, this, PUC,PV,Window);
                    StackContent.Children.Add(EmailChangeUC);
                    break;

                case 6:
                    DOBContentChange DOBChangeUC = new DOBContentChange(ID);
                    StackContent.Children.Add(DOBChangeUC);
                    break;

                case 7:
                    SingleContentChange CityChangeUC = new SingleContentChange("Nowe miasto", ID, 7, this, PUC,PV,Window);
                    StackContent.Children.Add(CityChangeUC);
                    break;
            }
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        
    }
}
