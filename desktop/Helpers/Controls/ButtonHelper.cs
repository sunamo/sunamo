using System.Windows.Controls;
using System.Windows.Media.Imaging;
public static class ButtonHelper
{
    public static Button GetButton(string tooltip, string imagePath)
    {
        return WpfHelper.GetButton(tooltip, imagePath);
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
        bi = shared.Pictures.MakeTransparentWindowsFormsButton(bi);
        Image image = ImageHelper.ReturnImage(bi);
        image.Width = 20;
        image.Height = 20;
        button.Content = image;
    }

    
}
