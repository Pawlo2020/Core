using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Dynamics
{
    class DynamicsHelper
    {
        string ProjectSchema = "CREATE TABLE PROJECTS(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "PROJECT_NAME varchar(60) NOT NULL," +
            "STATUS varchar(20) NOT NULL DEFAULT 'ACTIVE'," +
            "DATE_START varchar(15) NOT NULL," +
            "DATE_FINISH varchar(15) NOT NULL," +
            "DESCRIPTION varchar(256));";

        string ProjectTaskSchema = "CREATE TABLE `TASKS` ("+
	        "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
	        "TASK_NAME TEXT NOT NULL," +
	        "DATE_START TEXT,"+
	        "DATE_FINISH TEXT," +
	        "DESCRIPTION TEXT," +
            "STATUS varchar(20) NOT NULL DEFAULT 'ACTIVE'," +
	        "FK_ID INTEGER NOT NULL," +
	        "FOREIGN KEY(`FK_ID`) REFERENCES  PROJECTS(ID));";

        #region Build Database

        public void CreateContent(string path)
        {
            using(SQLiteConnection con = new SQLiteConnection(path))
            {
                using(SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = ProjectSchema;
                    command.ExecuteNonQuery();
                    command.CommandText = ProjectTaskSchema;
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        #endregion
    }
}
