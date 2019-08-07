using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
public static partial class ButtonHelper
{
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

    public static Button GetButton(string tooltip)
    {
        Button button = new Button();
        button.ToolTip = tooltip;
        button.Content = tooltip;
        return button;
    }

    public static void SaveTransparentImageAsContent(ContentControl button, System.Windows.Media.Color color, string imageRelPath)
    {
        BitmapSource bi = BitmapImageHelper.MsAppx(imageRelPath);
        bi = PicturesDesktop.MakeTransparentWindowsFormsButton(bi);
        Image image = ImageHelper.ReturnImage(bi);
        image.Width = 20;
        image.Height = 20;
        button.Content = image;
    }
}