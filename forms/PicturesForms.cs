using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace forms
{
    public class PicturesForms
    {
        public static Icon ConvertToIcon(Image imgFavorites)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmp = new Bitmap(imgFavorites);
            bmp.Save(ms, ImageFormat.Icon);
            return new Icon(ms);
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
