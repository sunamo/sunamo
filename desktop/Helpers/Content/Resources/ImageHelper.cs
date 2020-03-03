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
	
	public static Image MsAppxI(string appPic2)
    {
        return imageHelperDesktop.MsAppxI(appPic2);
    }

	
}