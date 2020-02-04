using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Database_Handler
{
    class EducationHelper
    {
        #region CLASSES SCHEMA

        private string ClassSchema = "CREATE TABLE CLASSES(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "CLASS_NAME VARCHAR(60) NOT NULL," +
            "STATUS VARCHAR (20) NOT NULL DEFAULT 'IN PROGRESS'," +
            "FINAL_NOTE VARCHAR(4)," +
            "ECTS VARCHAR(2)," +
            "TYPE VARCHAR(20)," +
            "SEMESTER VARCHAR(1)," +
            "PASS_MODE VARCHAR(20)," +
            "YEAR_ID INTEGER NOT NULL," +
            "SESSION_ID INTEGER," +
            "FOREIGN KEY(`SESSION_ID`) REFERENCES SESSIONS(ID)," +
            "FOREIGN KEY(`YEAR_ID`) REFERENCES LEARNING_YEAR(ID));";

        private string ExamSchema = "CREATE TABLE EXAMS(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "EXAM_NAME VARCHAR(60) NOT NULL," +
            "TYPE VARCHAR(20)," +
            "NOTE VARCHAR(4)," +
            "DATE TEXT," +
            "STATUS VARCHAR(20) DEFAULT 'IN PROGRESS'," +
            "CLASS_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`CLASS_ID`) REFERENCES CLASSES(ID));";

        private string LearningYearSchema = "CREATE TABLE LEARNING_YEAR(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "MODE VARCHAR(20) NOT NULL," +
            "YEAR_NO VARCHAR(5) NOT NULL," +
            "SEMESTERS_NO VARCHAR(1) NOT NULL," +
            "PROGRAM_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`PROGRAM_ID`) REFERENCES LEARNING_PROGRAM(ID));";

        private string LearningProgram = "CREATE TABLE LEARNING_PROGRAM(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "TYPE VARCHAR(30) NoT NULL," +
            "START Text," +
            "FINISH Text," +
            "LEVEL VARCHAR(2)," +
            "DIRECTION VARCHAR(60));";

        private string SessionSchema = "CREATE TABLE SESSIONS(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "DATE_START TEXT," +
            "DATE_FINISH TEXT," +
            "YEAR_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`YEAR_ID`) REFERENCES LEARNING_YEAR(ID));";

        private string TopicsSchema = "CREATE TABLE TOPICS(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "TOPIC_NAME VARCHAR(60) NOT NULL," +
            "DATE TEXT NOT NULL," +
            "CLASS_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`CLASS_ID`) REFERENCES CLASSES(ID));";

        private string NotesSchema = "CREATE TABLE NOTES(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "VALUE VARCHAR(1) NOT NULL," +
            "DESCRIPTION VARCHAR(20)," +
            "CLASS_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`CLASS_ID`) REFERENCES CLASSES(ID));";
        #endregion
        #region COURSES SCHEMA
        private string COURSES = "CREATE TABLE COURSES(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "COURSE_NAME VARCHAR(60) NOT NULL," +
            "DATE_START TEXT," +
            "DATE_FINISH TEXT," +
            "STATUS VARCHAR(20) DEFAULT 'IN PROGRESS');";

        private string COURSE_TRAINING = "CREATE TABLE COURSE_TRAINING(" +
            "ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
            "TRAINING_NAME VARCHAR(60) NOT NULL," +
            "DATE TEXT," +
            "COURSE_ID INTEGER NOT NULL," +
            "FOREIGN KEY(`COURSE_ID`) REFERENCES COURSES(ID));";
        #endregion

        #region BUILD DATABASE
        public void CreateContent(string path)
        {
            using (SQLiteConnection con = new SQLiteConnection(path))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = ClassSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = ExamSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = LearningYearSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = SessionSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = TopicsSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = NotesSchema;
                    command.ExecuteNonQuery();

                    command.CommandText = COURSES;
                    command.ExecuteNonQuery();

                    command.CommandText = COURSE_TRAINING;
                    command.ExecuteNonQuery();

                    command.CommandText = LearningProgram;
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }

        }
        #endregion

    }
}
