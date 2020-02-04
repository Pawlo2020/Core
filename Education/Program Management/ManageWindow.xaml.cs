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

namespace Education.Program_Management
{
    /// <summary>
    /// Logika interakcji dla klasy ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        Program_Management.ElementaryUC ElementaryView;
        Middle MiddleUC;
        TechUC TechView;
        StudyUC StudyView;
        BranchUC BranchView;
        Profile_Management.UserCredentials UserAccount;
        public ComboBox _List { get; set; }
        public ProgramClass _PC { get; set; }
        public ManageWindow(Profile_Management.UserCredentials UA, ComboBox List, Program_Management.ProgramClass PC)
        {
            _PC = PC;
            _List = List;
            UserAccount = UA;
            InitializeComponent();
        }

        private void closeBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBut_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void MiddleRadio_Checked(object sender, RoutedEventArgs e)
        {
            STKUPDATE.Children.Clear();
            MiddleUC = new Middle(UserAccount, this);
            STKUPDATE.Children.Add(MiddleUC);
        }

        private void ElementaryRadio_Checked(object sender, RoutedEventArgs e)
        {
            STKUPDATE.Children.Clear();
            ElementaryView = new ElementaryUC(UserAccount, this);
            STKUPDATE.Children.Add(ElementaryView);
        }

        private void TechnicianRadio_Checked(object sender, RoutedEventArgs e)
        {
            STKUPDATE.Children.Clear();
            TechView = new TechUC(UserAccount, this);
            STKUPDATE.Children.Add(TechView);
        }

        private void BrancheRadio_Checked(object sender, RoutedEventArgs e)
        {
            STKUPDATE.Children.Clear();
            BranchView = new BranchUC(UserAccount, this);
            STKUPDATE.Children.Add(BranchView);
        }

        private void StudyRadio_Checked(object sender, RoutedEventArgs e)
        {
            STKUPDATE.Children.Clear();
            StudyView = new StudyUC(UserAccount, this);
            STKUPDATE.Children.Add(StudyView);
        }

        private void CustonRadio_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Custom");
        }
    }
}
