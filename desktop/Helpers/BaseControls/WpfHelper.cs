using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using sunamo;
using System.Windows;
using sunamo.Values;

public class WpfHelper
{


    public static string GetText(Visual control)
    {
        Type type = control.GetType();
        if (type == Types.TextBlockType)
        {
            return ((TextBlock)control).Text;
        }
        return "";
    }

    public static Type GetDefaultValue(FrameworkElement fw, DependencyProperty dp)
    {
        object defaultValue = dp.DefaultMetadata.DefaultValue;
        
        if (defaultValue != null)
        {
            return defaultValue.GetType();
        }

        defaultValue = dp.GetMetadata(fw).DefaultValue;

        if (defaultValue != null)
        {
            return defaultValue.GetType();
        }

        return Consts.tObject;
    }

    public static Button GetButton(string tooltip, string imagePath)
    {
        // TODO: Merge with ButtonHelper
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

    public static void SetIsEnabled(bool value, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.IsEnabled = value;
        }
    }
}
