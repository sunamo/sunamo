using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows;
using desktop.Controls;
using sunamo.Helpers;

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

    public static void SetIsEnabled(bool v, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.IsEnabled = v;
        }
    }

    //
    public static void ShowExceptionWindow( object e)
    {
        ShowExceptionWindow(e, Environment.NewLine);
    }

    public static void ShowExceptionWindow(object e, string methodName = "")
    {
        if (methodName != string.Empty)
        {
            methodName += " ";
        }
        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        dump = SunamoJson.SerializeObject(e, true);

        ShowTextResult result = new ShowTextResult(methodName + dump);
        WindowWithUserControl window = new WindowWithUserControl(result, ResizeMode.CanResizeWithGrip, true);
        window.ShowDialog();
    }
}
