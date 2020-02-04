using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Education.Database_Handler
{
    class EduDB : IDisposable
    {

        public void Execute(string query, string DatabasePath)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public List<string> GetRowList(string query, string DatabasePath)
        {

            List<string> List = new List<string>();
            using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    SQLiteDataReader Reader = command.ExecuteReader();

                    int i = 0;
                    while (Reader.Read())
                    {
                        List.Add(Convert.ToString(Reader.GetValue(0)));
                    }
                    Reader.Dispose();
                    con.Close();
                    return List;
                }
            }
        }

        public List<string> GetColumnList(string query, string DatabasePath)
        {

            List<string> List = new List<string>();
            using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    SQLiteDataReader Reader = command.ExecuteReader();

                    int i = 0;
                    if (Reader.Read())
                    {

                        while (i < Reader.FieldCount)
                        {
                            List.Insert(i, Convert.ToString(Reader.GetValue(i)));
                            i++;
                        }
                    }
                    Reader.Dispose();
                    con.Close();
                    return List;
                }
            }
        }

        void IDisposable.Dispose()
        {
            
        }
    }

    
}
