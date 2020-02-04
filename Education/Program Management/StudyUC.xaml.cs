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
    /// Logika interakcji dla klasy StudyUC.xaml
    /// </summary>
    public partial class StudyUC : UserControl
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
        public StudyUC(Profile_Management.UserCredentials UA, ManageWindow MW)
        {
            InitializeComponent();
            ManagWin = MW;
            UserAccount = UA;
            InitializeComponent();
            ComboLevel.SelectedIndex = 0;
            DurationBox.SelectedIndex = 0;

            for (int i = -50; i < 50; i++)
            {
                YearList.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            YearList.SelectionChanged += (o, e) => UpdateEstiminatedYear();
            DurationBox.SelectionChanged += (o, e) => UpdateEstiminatedYear();
            ComboLevel.SelectionChanged += (o, e) => UpdateEstiminatedYear();
        }
        void UpdateEstiminatedYear()
        {
            ComboBoxItem Item = (ComboBoxItem)DurationBox.SelectedItem;
            int number = (int)Math.Ceiling(Convert.ToDouble((string)Item.Content));
            Item = (ComboBoxItem)ComboLevel.SelectedItem;
            estiminatedyear = (int)YearList.SelectedItem + number;
            EstiminatedTime.Content = estiminatedyear.ToString();
            query = $"INSERT INTO LEARNING_PROGRAM (TYPE, START, FINISH, LEVEL, DIRECTION) VALUES('STUDY', '{estiminatedyear - 3}', '{estiminatedyear}', '{(string)Item.Content}', '{Directiontxt.Text}')";
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
