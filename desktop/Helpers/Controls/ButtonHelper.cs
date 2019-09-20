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
        ControlHelper.SetForeground(vr, d.foreground);
        vr.Content = ContentControlHelper.GetContent(d);
        if (d.OnClick != null)
        {
            vr.Click += d.OnClick;
        }
        vr.Tag = d.tag;
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