using desktop;
using sunamo;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public  partial class ImageHelper
{
    

    /// <summary>
	/// Pokud chceš získat jen URI, dej new Uri(ImageHelper.protocol + relPath)
	/// </summary>
	/// <param name="relPath"></param>
	public static Image MsAppx(string relPath)
    {
        return imageHelperDesktop.MsAppx(relPath);
    }

    public static Image MsAppx(bool disabled, AppPics appPic)
    {
        return imageHelperDesktop.MsAppx(disabled, appPic);
    }

    /// <summary>
	/// Do A1 se vkládá člen výčtu AppPics2.TS()
	/// Přípona se doplní automaticky na .png
	/// </summary>
	/// <param name="appPic2"></param>
	public static Image MsAppxI(string appPic2)
    {
        return imageHelperDesktop.MsAppxI(appPic2);
    }

	
}