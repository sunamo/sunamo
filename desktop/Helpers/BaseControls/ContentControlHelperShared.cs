using desktop;
using desktop.AwesomeFont;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public partial class ContentControlHelper{
    public static object Content(CheckBox v)
    {
        var r = WpfApp.cd.Invoke(() => v.Content);
        return r;
    }
    public static string ExtractContent(object sp)
    {
        var sp2 = (StackPanel)sp;
        var result = ExtractContent( sp2);
        return result;
    }

    public static string ExtractContent(StackPanel sp)
    {
        /*
Je zde ta věc že 
         */

        StringBuilder sb = new StringBuilder();

        WpfApp.cd.Invoke(() =>
        {
            foreach (var item in sp.Children)
            {
                //if (item is StackPanel)
                //{
                    var sp2 = item as TextBlock;
                if (sp2 != null)
                {
                    if (AwesomeFontControls.IsFamilyFontFontAwesome(sp2.FontFamily))
                    {
                        continue;
                    }
                    sb.Append(sp2.Text);
                }
                
                //}
            }
        }, System.Windows.Threading.DispatcherPriority.ContextIdle);
        return sb.ToString();
    }

    public static StackPanel GetContent(ControlInitData d)
    {
        var img = d.imagePath;
        var text = d.text;
        bool isImg = img != null;
        bool isText = text != null;

        if (!isText)
        {
            isText = d.xlfKey != null;
            text = sess.i18n(d.xlfKey);
        }

        StackPanel sp = new StackPanel();
        sp.Orientation = Orientation.Horizontal;
        //10*2 padding
        sp.Height = d.imageHeight + d.addPadding;
        if (isImg && isText)
        {
            var tbHeight = AddImg(img, sp, d.imageWidth, d.imageHeight);
            AddTextBlock(text, sp, tbHeight);
        }
        else if (isImg)
        {
            AddImg(img, sp, d.imageWidth, d.imageHeight);
        }
        else if (isText)
        {
            AddTextBlock(text, sp);
        }

        return sp;
    }



    /// <summary>
    /// Return height which 
    /// </summary>
    /// <param name="img"></param>
    /// <param name="sp"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
private static double AddImg(object img, StackPanel sp, double w, double h)
    {
        bool isAwesome = false;
        var imgS = img.ToString();

        if (img.GetType() == Types.tString)
        {
            if (imgS.Length == 1)
            {
                var ch = imgS[0];
                if (ch >= AwesomeFontControls.low && ch <= AwesomeFontControls.high)
                {
                    isAwesome = true;
                }
            }
        }

        if (isAwesome)
        {
            TextBlock tb = new TextBlock();

            tb.FontSize = h;
            tb.Padding = new System.Windows.Thickness(10);

            sp.Height = h + tb.Padding.Top + tb.Padding.Bottom;

            AwesomeFontControls.SetAwesomeFontSymbol(tb, imgS);
            sp.Children.Add(tb);
        }
        else
        {
            var img2 = ImageHelperDesktop.Get(img);
            img2.Margin = new System.Windows.Thickness( 10);

            sp.Children.Add(img2);
        }

        var r = AwesomeFontControls.ReturnFontSizeForTextNextToAwesomeIconWithSize(sp.Height);
        return r;
    }

    private static void AddTextBlock(string text, StackPanel sp, double tbHeight = double.NaN)
    {
        var tb = TextBlockHelper.Get(new ControlInitData { text = text });
        if (!double.IsNaN( tbHeight ))
        {
            tb.FontSize = tbHeight;
        }

        // Must be vertical alignment
        tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        sp.Children.Add(tb);
    }
}