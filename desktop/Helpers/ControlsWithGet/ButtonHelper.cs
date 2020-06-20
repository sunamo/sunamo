using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
public static partial class ButtonHelper
{

    public static void SaveTransparentImageAsContent(ContentControl button, System.Windows.Media.Color color, string imageRelPath)
    {
        BitmapSource bi = BitmapImageHelper.MsAppx(imageRelPath);
        SaveTransparentImageAsContent(button, color, bi);
    }

    /// <summary>
    /// Not working, but it was maybe because color is not exactly as was specified
    /// Not use Lunapic or my code to create favicon. Always download image from net
    /// </summary>
    /// <param name="button"></param>
    /// <param name="color"></param>
    /// <param name="bi"></param>
    public static void SaveTransparentImageAsContent(ContentControl button, System.Windows.Media.Color color, BitmapSource bi)
    {
        bi = PicturesDesktop.MakeTransparentWindowsFormsButton(bi, color);
        Image image = ImageHelper.ReturnImage(bi);
        image.Width = 20;
        image.Height = 20;
        button.Content = image;
    }
}