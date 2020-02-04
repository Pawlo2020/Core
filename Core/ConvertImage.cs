using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Core
{
    class ConvertImage
    {
        string path = @"E:\Projekty\Core\Core\images\user.jpg";

        public byte[] ConvertIma(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public void main()
        {
            System.Drawing.Image photo = new Bitmap(path);
            byte[] pic = ConvertIma(photo, System.Drawing.Imaging.ImageFormat.Jpeg);

            File.WriteAllBytes(@"E:\Projekty\Core\Core\images\ByteImage.dat", pic);


        }

        


    }
}
