using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;

namespace Core
{
   public sealed class Sqlcon
    {
        private String _con;
        public SQLiteCommand command;
        private SQLiteConnection con;
        public SQLiteDataReader Reader;
        public void Getcon(String value)
        {
            _con = value;
        }

        

        public void SqlQuery(String query_text)
        {
            using (con = new SQLiteConnection(_con))
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Connection Established");
                    command = con.CreateCommand();
                    command.CommandText = query_text;
                    command.ExecuteNonQuery();
                    Reader = command.ExecuteReader();
                    if(Reader.Read())
                    {
                        MessageBox.Show("Jest admin");
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowy login");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
            

        }

    }


