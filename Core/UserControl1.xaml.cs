using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Core;
using System.IO;
using Microsoft.Win32;
namespace Core
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public string image_path = null;

        public UserControl1()
        {
            InitializeComponent(); 
        }
        
        public void Execute_register(string query, string path)
        {
            using(DB db = new DB())
            {
                db.Register(query, path);
                
            }
        }

        private void comboDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {}

        private void returntologin_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }

        private void LoadImageBut_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                image_path = openFileDialog.FileName;
            }
        }

        private void registerfinalbut_Click(object sender, RoutedEventArgs e)
        {
            ArrayList RegisterPass = new ArrayList() {
                loginreg.Text,
                passregist.Password.ToString(),
                FMreg.Text,
                LMreg.Text,
                emailreg.Text,
                comboYear.Text,
                comboMonth.Text,
                comboDay.Text,
                citynamereg.Text,
                DateTime.Now.ToShortDateString()
            };

            string haslo = passregist.Password.ToString();

            if (string.IsNullOrWhiteSpace(loginreg.Text) || string.IsNullOrWhiteSpace(haslo) || string.IsNullOrWhiteSpace(emailreg.Text))
            {
                correctbq.Visibility = Visibility.Visible;
                correctcheck.Content = "Uzupełnij dame";
            }
            else
            {
                correctbq.Visibility = Visibility.Hidden;
                correctcheck.Content = "";
                string query_register = $"INSERT INTO USERS (LOGIN, PASS, E_MAIL, FIRST_NAME, LAST_NAME, DOB_YEAR, DOB_MONTH, DOB_DAY, CITY, DATE_OF_REGIST, PHOTO) VALUES ('{RegisterPass[0]}', '{RegisterPass[1]}','{RegisterPass[4]}', '{RegisterPass[2]}','{RegisterPass[3]}', '{RegisterPass[5]}', '{RegisterPass[6]}', '{RegisterPass[7]}', '{RegisterPass[8]}', '{RegisterPass[9]}', @0);";
                Execute_register(query_register, image_path);
                MessageBox.Show("Dodano użytkownika");
            }
        }
    }
}
