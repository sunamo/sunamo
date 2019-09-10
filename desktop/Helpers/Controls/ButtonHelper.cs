using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
public static partial class ButtonHelper
{
    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="tooltip"></param>
    /// <param name="imagePath"></param>
    /// <returns></returns>
    public static Button Get(ControlInitData d)
    {
        Button vr = new Button();
        if (d.imagePath != null)
        {
            BitmapImage btm = new BitmapImage(new System.Uri(d.imagePath, System.UriKind.Relative));
            Image img = new Image();
            img.Source = btm;
            img.Width = 16;
            img.Height = 16;
            img.Stretch = Stretch.Fill;
            vr.Content = img;
        }
        else
        {
            vr.Content = d.text;
            
        }
        vr.Tag = d.tag;
        vr.Click += d.OnClick;
        vr.ToolTip = d.tooltip;
        return vr;
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