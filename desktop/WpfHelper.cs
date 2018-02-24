using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
public class WpfHelper
{
    // TODO: Merge with ButtonHelper

    public static Button GetButton(string tooltip, string imagePath)
    {
        Button vr = new Button();
        BitmapImage btm = new BitmapImage(new System.Uri(imagePath, System.UriKind.Relative));
        Image img = new Image();
        img.Source = btm;
        img.Width = 16;
        img.Height = 16;
        img.Stretch = Stretch.Fill;
        vr.Content = img;
        vr.ToolTip = tooltip;
        return vr;
    }

    
}
