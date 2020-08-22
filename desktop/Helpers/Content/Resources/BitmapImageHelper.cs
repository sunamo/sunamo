using sunamo;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/// <summary>
/// Posloupnost je BitmapImage (sealed) -> BitmapSource (abstract) -> ImageSource (abstract)
/// </summary>
public static partial class BitmapImageHelper
{
    public static BitmapImage MsAppx(string relPath)
    {
        BitmapImage bs = new BitmapImage(new Uri(ImageHelper.protocol + relPath, UriKind.Absolute));
        return bs;
    }

    /// <summary>
    /// Do A1 se vkládá člen výčtu AppPics2.TS()
    /// Přípona se doplní automaticky na .png
    /// Používá se tehdy, když je obrázek v nějaké specifické složce(ne e nebo d) nebo když je přímo v rootu
    /// </summary>
    /// <param name = "appPic2"></param>
    public static BitmapImage MsAppxI(string appPic2)
    {
        BitmapImage bs = new BitmapImage(new Uri(ImageHelper.protocol + "i/" + appPic2 + ".png"));
        return bs;
    }

    public static BitmapImage MsAppx(bool disabled, AppPics appPic)
    {
        string cesta = "";
        if (disabled)
        {
            cesta = "//d/";
        }
        else
        {
            cesta = "//e/";
        }

        cesta += appPic.ToString() + ".png";
        return MsAppx(cesta);
    }

    public static BitmapSource Path(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        return Uri(new Uri(path, UriKind.Absolute));
    }

    public static BitmapSource Uri(Uri uri)
    {
        BitmapImage bi = null;
        try
        {
            bi = new BitmapImage(uri);
        }
        catch (Exception)
        {
            // Image was damaged
            
        }
        return bi;
    }

    public static BitmapImage MsAppxRoot(string p)
    {
        return new BitmapImage(new Uri(ImageHelper.protocolRoot + p, UriKind.Absolute));
    }

    

}