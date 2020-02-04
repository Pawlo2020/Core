using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Data;
using System.Windows.Media.Imaging;
using Dynamics;
namespace Core
{


    class DB : IDisposable
    {
        private static string ID;
        string Datapath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public object CheckLogin(string query)
        {
            using (SQLiteConnection _con = new SQLiteConnection($"Data Source= {Datapath}" + @"\PawloCore\DBUSERS.sqlite"))
            {
                _con.Open();
                using (var command = new SQLiteCommand(_con))
                {
                    try
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        ID = command.ExecuteScalar().ToString();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                _con.Close();
                                return ID;
                            }
                            else
                            {
                                _con.Close();
                                return false;
                            }
                        }
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

        public void Register(string query, string path = null)
        {
            using (SQLiteConnection _con = new SQLiteConnection($"Data Source= {Datapath}" + @"\PawloCore\DBUSERS.sqlite"))
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
                    catch
                    { }
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

        public ArrayList Query(string query, string path = null)
        {
            ArrayList LIST = new ArrayList();
            using (SQLiteConnection _con = new SQLiteConnection($"Data Source= {Datapath}" + @"\PawloCore\DBUSERS.sqlite"))
            {
                _con.Open();
                using (var command = new SQLiteCommand(_con))
                {
                    //if (path != null)
                    //{
                    //    set_path(path, command);
                    //}
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    SQLiteDataReader Reader = command.ExecuteReader();
                    int i = 0;
                    if (Reader.Read())
                    {
                        while (i < Reader.FieldCount)
                        {
                            LIST.Insert(i, Convert.ToString(Reader.GetValue(i)));
                            i++;
                        }
                    }
                    Reader.Dispose();
                    return LIST;
                }
            }
        }

        BitmapSource Value;
        public BitmapSource Load_Image(string ID)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source= {Datapath}" + @"\PawloCore\DBUSERS.sqlite"))
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

        public void Dispose()
        {}
    }
}

