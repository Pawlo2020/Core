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

namespace Education.Program_Management
{
    /// <summary>
    /// Logika interakcji dla klasy BranchUC.xaml
    /// </summary>
    public partial class BranchUC : UserControl
    {
        private void Execute(string query, string path)
        {
            using (Database_Handler.EduDB EX = new Database_Handler.EduDB())
            {
                EX.Execute(query, path);
            }
        }
        private string query;
        private int estiminatedyear;
        Profile_Management.UserCredentials UserAccount;
        ManageWindow ManagWin;
        public BranchUC(Profile_Management.UserCredentials UA, ManageWindow MW)
        {
            InitializeComponent();
            ManagWin = MW;
            UserAccount = UA;
            InitializeComponent();
            ComboLevel.SelectedIndex = 0;

            for (int i = -50; i < 50; i++)
            {
                YearList.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            YearList.SelectionChanged += (o, e) => UpdateEstiminatedYear();
            ComboLevel.SelectionChanged += (o, e) => UpdateEstiminatedYear();
        }

        void UpdateEstiminatedYear()
        {
            if (ComboLevel.SelectedIndex == 0)
            {
                estiminatedyear = (int)YearList.SelectedItem + 3;
                EstiminatedTime.Content = estiminatedyear.ToString();
                query = $"INSERT INTO LEARNING_PROGRAM (TYPE, START, FINISH, LEVEL) VALUES('BRANCH', '{estiminatedyear - 3}', '{estiminatedyear}', '1')";

            }
            else if(ComboLevel.SelectedIndex == 1)
            {
                estiminatedyear = (int)YearList.SelectedItem + 2;
                EstiminatedTime.Content = estiminatedyear.ToString();
                query = $"INSERT INTO LEARNING_PROGRAM (TYPE, START, FINISH, LEVEL) VALUES('BRANCH', '{estiminatedyear - 2}', '{estiminatedyear}', '2')";
            }
        }

        private void SaveProjectBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Execute(query, $"Data Source={UserAccount.ServiceDataPath}");
                MessageBox.Show("Program został dodany");
                ManagWin.Close();
                ManagWin._PC.UpdateProgramList(ManagWin._List);
                ManagWin._List.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Wszystkie pola nie zostały uzupełnione");
            }
        }
    }
}
