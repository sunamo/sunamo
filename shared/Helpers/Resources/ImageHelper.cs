using sunamo;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
public static class ImageHelper
{
    public static Image ReturnImage(ImageSource bs)
    {
        Image image = new Image();
        image.Stretch = Stretch.Uniform;
        image.Source = bs;
        return image;
    }

    public static Image ReturnImage(ImageSource bs, double width, double height)
    {
        Image image = new Image();
        image.Stretch = Stretch.Uniform;
        image.Source = bs;
        image.Width = width;
        image.Height = height;
        return image;
    }

   

    /// <summary>
    /// Toto je jediné místo kde je tato proměnná a to proto že je na něho navázaná metoda SetAssemblyNameForWpfApps, která se musí volat ve WPF(ale ne Windows Store apps) aplikacích
    /// </summary>
    public static string protocol = "pack://application:,,,";
    public static string protocolRoot = "pack://application:,,,/";

    /// <summary>
    /// Nevolá se ve Windows Store Apps aplikacích
    /// </summary>
    /// <param name="assemblyName"></param>
    public static void SetAssemblyNameForWpfApps(string assemblyName)
    {
        protocol += "/" + assemblyName+ ";component/";
        //protocol = "pack://" + assemblyName + ";component/";
    }

    /// <summary>
    /// Do A1 se vkládá člen výčtu AppPics2.TS()
    /// Přípona se doplní automaticky na .png
    /// </summary>
    /// <param name="appPic2"></param>
    /// <returns></returns>
    public static Image MsAppxI(string appPic2)
    {
        BitmapSource bs = new BitmapImage(new Uri(protocol + "i/" + appPic2 + ".png"));
        return ReturnImage(bs);
    }

    /// <summary>
    /// Pokud chceš získat jen URI, dej new Uri(ImageHelper.protocol + relPath)
    /// </summary>
    /// <param name="relPath"></param>
    /// <returns></returns>
    public static Image MsAppx(string relPath)
    {
        BitmapSource bs = new BitmapImage(new Uri(protocol + relPath));
        return ReturnImage(bs);
    }

    

    public static void RegisterPackProtocol()
    {
        if (!UriParser.IsKnownScheme("pack"))
            new System.Windows.Application();
    }

    public static Image MsAppx(bool disabled, AppPics appPic)
    {
        ///Subfolder/ResourceFile.xaml
        return ReturnImage(BitmapImageHelper.MsAppx(disabled, appPic));
    }
}
