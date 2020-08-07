using sunamo;
using System;
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
}