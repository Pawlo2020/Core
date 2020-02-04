using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Media.Imaging;

namespace Education.Profile_Management
{
    public class UserCredentials
    {
        public string Identity { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DOB { get; set; }

        public string City { get; set; }

        public string Phone {get; set;}

        public string Employment { get; set; }

        public string Website { get; set; }

        public BitmapSource Photo { get; set; }

        public string ServiceDataPath { get; set; }

        List<string> CredentialsList;
        public UserCredentials(string ID)
        {
            Identity = ID;
        }

        public void GetCredentials()
        {
            string query = $"SELECT * FROM USERS WHERE ID = {Identity}";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"/PawloCore/DBUSERS.sqlite";
            using (Database_Handler.EduDB DBHandler = new Database_Handler.EduDB())
            {
                CredentialsList = DBHandler.GetColumnList(query, $"Data Source={path}");

                Login = CredentialsList[3];
                FirstName = CredentialsList[1];
                LastName = CredentialsList[2];
                Email = CredentialsList[5];
                DOB = CredentialsList[8] + "." + CredentialsList[7] + "." + CredentialsList[6];
                City = CredentialsList[9];
                Phone = CredentialsList[12];
                Employment = CredentialsList[13];
                Website = CredentialsList[14];
                ServiceDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"/PawloCore/" + Identity + @"_user_data" + @"\EducationData\education.sqlite";
            }
        }
    }
}
