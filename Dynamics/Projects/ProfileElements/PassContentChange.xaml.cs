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
    /// Logika interakcji dla klasy PassContentChange.xaml
    /// </summary>
    public partial class PassContentChange : UserControl
    {
        private List<string> GetPass(string query, string path)
        {
            using (DynDB qu = new DynDB())
            {
                return qu.GetRowList(query, path);
            }
        }

        public void EX_QUERY(string query, string Path)
        {
            using (DynDB qu = new DynDB())
            {
                qu.Execute(query, Path);
            }
        }
        string query;
        string Data = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\DBUSERS.sqlite";
        private List<string> CrePass;
        private string _identity;
        ChangeWin _Window;
        public PassContentChange(string ID, ChangeWin Window)
        {
            _identity = ID;
            InitializeComponent();
            CrePass = new List<string>();
            CrePass = GetPass($"SELECT PASS FROM USERS WHERE ID = {ID}", $"Data Source={Data}");
            
        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            if (CrePass[0] == OLDPass.Password.ToString())
            {
                query = $"UPDATE USERS SET PASS = '{NEWPass.Password.ToString()}' WHERE ID = {Int32.Parse(_identity)}";
                EX_QUERY(query, $"Data Source={Data}");
                _Window.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe hasło");
            }
        }
    }
}
