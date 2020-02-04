using Microsoft.Win32;
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
using System.IO;
using System.Diagnostics;

namespace Dynamics
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Page
    {
        MainWindow _Window;
        string DynDataFolder;
        List<string> _Credentials { get; set; }
        public ProfileView(MainWindow Window, List<string> Credentials)
        {
            _Window = Window;
            InitializeComponent();
            _Credentials = new List<string>();
            _Credentials = Credentials;
            DynDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PawloCore\" + _Window._identity + @"_user_data\";
            using (DynDB db = new DynDB()) {
                ImageDyn.Source = db.Load_Image(_Window._identity);
            }

            DateRegistTxt.Text = DateRegistTxt.Text + " " + _Credentials[10];
            FN_man.Content = _Credentials[1];
            LN_man.Content = _Credentials[2];


            Projects.ProfileElements.PersonalInfoUC EssentialInfoUC = new Projects.ProfileElements.PersonalInfoUC(_Credentials,this ,Window);
            PersonalStk.Children.Add(EssentialInfoUC);

            Projects.ProfileElements.AdditionalInfoUC AdditionalInfoUC = new Projects.ProfileElements.AdditionalInfoUC(_Credentials);
            AdditionalStk.Children.Add(AdditionalInfoUC);
        }

        private void ImageCH_Click(object sender, RoutedEventArgs e)
        {
            string image_path = System.IO.Directory.GetCurrentDirectory() + @"images\user.jpg";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                image_path = openFileDialog.FileName;
                using (DynDB db = new DynDB())
                {
                    db.Register($"UPDATE USERS SET PHOTO = @0 WHERE ID = {Int32.Parse(_Window._identity)}", image_path);
                    ImageDyn.Source = db.Load_Image(_Window._identity);
                    _Window.image_prof.Source = db.Load_Image(_Window._identity);
                }
            }
        }
        
        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Usunąć dane Dynamics?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                try
                {
                    Directory.Delete(DynDataFolder, true);
                    _Window.Close();
                    var applicationPath = System.IO.Directory.GetCurrentDirectory() + @"\Core.exe";
                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo(applicationPath);
                    process.StartInfo.Arguments = $"{_Credentials[3]} {_Credentials[4]}";
                    process.Start();
                }
                catch { }
            }
        }
    }
}
