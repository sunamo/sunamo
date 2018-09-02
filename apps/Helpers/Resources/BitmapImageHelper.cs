using sunamo;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

/// <summary>
/// Posloupnost je BitmapImage (sealed) -> BitmapSource (abstract) -> ImageSource (abstract)
/// </summary>
public static class BitmapImageHelper
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
        /// <param name="appPic2"></param>
        /// <returns></returns>
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
                cesta = "i/d/";
            }
            else
            {
                cesta = "i/e/";
            }
            cesta += appPic.ToString() + ".png";
            return MsAppx(cesta);
        }

    public static BitmapImage PathToBitmapImage(string path)
    {
        return UriToBitmapImage(new Uri(path, UriKind.Absolute));
    }

    public static BitmapImage UriToBitmapImage(Uri uri)
    {
        BitmapImage bi = new BitmapImage(uri);
        return bi;
    }

    public static ImageSource Path(string path)
        {
            return Uri(new Uri(path, UriKind.Absolute));
        }

        public static ImageSource Uri(Uri uri)
        {
            BitmapImage bi = new BitmapImage(uri);
            return bi;
        }

        public static BitmapImage MsAppxRoot(string p)
        {
            return new BitmapImage(new Uri( ImageHelper.protocol+ p, UriKind.Absolute));
        }

    public static BitmapImage Resize(BitmapImage source, int rate)
    {
        
        
        source.DecodePixelHeight = rate;
        source.DecodePixelWidth = rate;
        
        return source;
    }

    
}
