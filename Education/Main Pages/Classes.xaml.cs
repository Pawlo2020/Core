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

namespace Education.Main_Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Classes.xaml
    /// </summary>
    public partial class Classes : Page
    {
        public Program_Management.ProgramClass Programs;
        Profile_Management.UserCredentials UserAccount;
        public Classes(Profile_Management.UserCredentials UA)
        {
            UserAccount = UA;
            InitializeComponent();

            Programs = new Program_Management.ProgramClass(UA);
            Programs.UpdateProgramList(programlist);
            programlist.SelectedIndex = 0;
        }

        private void programlist_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ADDProg_Click(object sender, RoutedEventArgs e)
        {
            Program_Management.ManageWindow NewProjectWindow = new Program_Management.ManageWindow(UserAccount, programlist, Programs);
            NewProjectWindow.Show();
            
        }
    }
}
