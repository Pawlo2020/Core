using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections;
using System.IO;

namespace Core
{
    class DBUSERS_HELPER : IDisposable
    {

        public void EX_ADMIN(string query)
        {
            using(DB IA = new DB())
            {
                IA.Register(query);
            }
        }

        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        string createBase = "Create Table USERS(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "FIRST_NAME varchar(60)," +
            "LAST_NAME varchar(60)," +
            "LOGIN varchar(60) NOT NULL," +
            "PASS varchar(60) NOT NULL," +
            "E_MAIL varchar(60) NOT NULL," +
            "DOB_YEAR varchar(4) NOT NULL," +
            "DOB_MONTH varchar(2) NOT NULL," +
            "DOB_DAY varchar(2) NOT NULL," +
            "CITY varchar(60)," +
            "DATE_OF_REGIST varchar(10)," +
            "STATUS varchar(15)," +
            "PHONE varchar(12)," +
            "Employment varchar(30)," +
            "Website varchar(30),"+
            "PHOTO BLOB);";

        string insertAdmin = $"INSERT INTO USERS (FIRST_NAME, LAST_NAME, LOGIN, PASS, E_MAIL, DOB_YEAR, DOB_MONTH, DOB_DAY, DATE_OF_REGIST, STATUS, PHOTO)" +
            $" VALUES('admin', 'admin', 'admin', 'admin', 'admin@pawlo.com', '2018', '7', '13', '{DateTime.Now.ToShortDateString()}', 'ROOT', @0)";

        public void createNewDatabase()
        {
           
            SQLiteConnection.CreateFile(path + @"\PawloCore\DBUSERS.sqlite");

            using (SQLiteConnection con = new SQLiteConnection($"Data Source= {path}" + @"\PawloCore\DBUSERS.sqlite"))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    command.CommandText = createBase;
                    command.ExecuteNonQuery();
                }

            }
            EX_ADMIN(insertAdmin);
        }

        public void Dispose()
        {}
    }
}
