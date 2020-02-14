using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public partial class FrameworkElementHelper
{
    static Type type = typeof(FrameworkElementHelper);

    

    public static Size GetMaxContentSize(FrameworkElement fe)
    {
        return new Size(fe.ActualWidth, fe.ActualHeight);
    }

     public static Size GetContentSize(FrameworkElement fe)
    {
        return new Size(fe.Width, fe.Height);
    }

    public static void SetMaxContentSize(FrameworkElement fe, Size s)
    {
        fe.MaxWidth = s.Width;
        fe.MaxHeight = s.Height;
        fe.Width = s.Width;
        fe.Height = s.Height;
    }

    public static void SetWidthAndHeight(FrameworkElement fe, Size s)
    {
        fe.Width = s.Width;
        fe.Height = s.Height;
        fe.UpdateLayout();
    }

    public static T FindName<T>(FrameworkElement element, string controlNamePrefix, int serie)
    {
        return FindName<T>(element, controlNamePrefix + serie);
    }

    static bool IsContentControl(object customControl)
    {
        if (RH.IsOrIsDeriveFromBaseClass(customControl.GetType(), typeof(ContentControl)))
        {
            var contentControl = (ContentControl)customControl;
            if (RH.IsOrIsDeriveFromBaseClass(contentControl.Content.GetType(), typeof(FrameworkElement)))
            {
                return true;
            }
        }

        return false;
    }

    static bool IsPanel(object customControl)
    {
        return RH.IsOrIsDeriveFromBaseClass(customControl.GetType(), typeof(Panel));
    }

    public static T FindByTag<T>(object customControl, object v)
        where T : FrameworkElement
    {
        if (IsContentControl(customControl))
        {
            ContentControl c = (ContentControl)customControl;
            return FindByTag<T>(c.Content, v);
        }
        else if (IsPanel(customControl))
        {
            Panel c = (Panel)customControl;
            foreach (var item in c.Children)
            {
                if (IsPanel(item) || IsContentControl(item))
                {
                    return FindByTag<T>(item, v);
                }

                if (RH.IsOrIsDeriveFromBaseClass(item.GetType(), typeof(FrameworkElement)))
                {
                    FrameworkElement fw = (FrameworkElement)item;
                    if (BTS.CompareAsObjectAndString(fw.Tag, v))
                    {
                        return (T)fw;
                    }
                }
            }
        }
        else
        {
            ThrowExceptions.Custom(type, "", "customControl is not ContentControl or Panel");
        }

        return default(T);
    }

    public static T FindName<T>(FrameworkElement element, string controlName)
    {
        return (T)element.FindName(controlName);
    }
}