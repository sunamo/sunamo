using sunamo;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static partial class BitmapImageHelper{ 
    public static BitmapImage PathToBitmapImage(string path)
    {
        ; return UriToBitmapImage(new Uri(path, UriKind.Absolute));
    }

    public static BitmapImage UriToBitmapImage(Uri uri)
    {
        BitmapImage bi = new BitmapImage(uri);
        return bi;
    }

    #region Convert between System.Windows and System.Drawing - same name in all helper classes
    public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }
    }
    #endregion
}