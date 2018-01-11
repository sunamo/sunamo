using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace forms
{
    public class Pictures
    {
        public static Icon ConvertToIcon(Image imgFavorites)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmp = new Bitmap(imgFavorites);
            bmp.Save(ms, ImageFormat.Icon);
            return new Icon(ms);
        }

        public static System.Drawing.Icon ConvertToIcon(string p)
        {
            BitmapImage bi = new BitmapImage(new Uri(p));
            System.Drawing.Bitmap b = shared.Pictures.BitmapImage2Bitmap(bi);
            return forms.Pictures.ConvertToIcon(b);
        }

        public static string InfoAbout(Bitmap bmp)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Width: " + bmp.Width);
            sb.AppendLine("Height: " + bmp.Height);
            return sb.ToString();
        }
    }
}
