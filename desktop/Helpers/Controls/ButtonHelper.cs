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
        bi = PicturesDesktop.MakeTransparentWindowsFormsButton(bi);
        Image image = ImageHelper.ReturnImage(bi);
        image.Width = 20;
        image.Height = 20;
        button.Content = image;
    }
}