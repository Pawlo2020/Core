using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Data;

namespace Dynamics
{
    class DynDB : IDisposable
    {



        public void Execute(string query, string DatabasePath)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
            {
                using(SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    con.Close();
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
                    while(Reader.Read())
                    {
                            List.Add(Convert.ToString(Reader.GetValue(0)));                        
                    }
                    Reader.Dispose();
                    con.Close();
                    return List;
                }
            }
        }




        public object GetDataScalar(string query, string DatabasePath)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    try
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        var value = command.ExecuteScalar().ToString();
                        SQLiteDataReader Reader = command.ExecuteReader();
                        if (Reader.Read())
                        {
                            con.Close();
                            return value;
                        }
                        else
                        {
                            con.Close();
                            Console.WriteLine("BRAK");
                            return false;
                        }

                }
                    catch (Exception ex)
                {
                    con.Close();
                    Console.WriteLine("Exception");
                    return false;
                }
            }
            }
        }

        public void Register(string query, string path = null)
        {
            using (SQLiteConnection _con = new SQLiteConnection($"Data Source= {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}" + @"\PawloCore\DBUSERS.sqlite"))
            {
                _con.Open();
                using (var command = new SQLiteCommand(_con))
                {
                    if (path != null)
                    {
                        System.Drawing.Image photo = new Bitmap(path);
                        byte[] pic = ConvertImage(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
                        set_path(pic, command);
                    }
                    else
                    {
                        byte[] pic = File.ReadAllBytes("ByteImage.dat");
                        set_path(pic, command);

                    }
                    try
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        _con.Close();

                    }
                    catch (Exception ex)
                    { }
                }
            }
        }


        BitmapSource Value;
        public BitmapSource Load_Image(string ID)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source= {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}" + @"\PawloCore\DBUSERS.sqlite"))
            {
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    con.Open();
                    string query_photo = $"SELECT PHOTO FROM USERS WHERE ID = {Int32.Parse(ID)}";
                    command.CommandText = query_photo;
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        byte[] a = (System.Byte[])reader[0];
                        Value = LoadImage(a);
                    }
                    con.Close();
                    reader.Dispose();
                    return Value;
                }
            }
            BitmapSource LoadImage(Byte[] imageData)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    var decoder = BitmapDecoder.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    return decoder.Frames[0];
                }
            }
        }

        public void set_path(byte[] BArray, SQLiteCommand cmd)
        {

            SQLiteParameter param = new SQLiteParameter("@0", System.Data.DbType.Binary);
            param.Value = BArray;
            cmd.Parameters.Add(param);
        }

        public byte[] ConvertImage(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public void Dispose()
        {}
    }
}
