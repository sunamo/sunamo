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
    public static BitmapImage Bitmap2BitmapImage(Image bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }
    }

    public static BitmapImage Resize(BitmapImage source, int width, int height)
    {
        source.BeginInit();
        source.DecodePixelHeight = width;
        source.DecodePixelWidth = height;
        source.EndInit();
        return source;
    }

    public static BitmapImage Resize(BitmapImage source, int rate, bool init)
    {
        if (init)
        {
            source.BeginInit();
        }
        
        source.DecodePixelHeight = rate;
        source.DecodePixelWidth = rate;
        if (init)
        {
            source.EndInit();
        }
        
        return source;
    }

    public static void Save(BitmapSource renderTarget, string fn)
    {
        PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
        bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
        using (Stream stm = File.Create(fn))
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(fn);
            bitmapEncoder.Save(stm);

            // Cant be, otherwise could be visible on another screenshot
            //ThisApp.SetStatus(TypeOfMessage.Success, "File written to " + fn);
        }
    }
    #endregion
}