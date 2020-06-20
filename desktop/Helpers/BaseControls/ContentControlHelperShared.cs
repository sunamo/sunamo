using desktop;
using desktop.AwesomeFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public partial class ContentControlHelper{ 
public static StackPanel GetContent(ControlInitData d)
    {
        var img = d.imagePath;
        var text = d.text;
        bool isImg = img != null;
        bool isText = text != null;
        StackPanel sp = new StackPanel();
        sp.Orientation = Orientation.Horizontal;
        if (isImg && isText)
        {
            AddImg(img, sp, d.imageWidth, d.imageHeight);
            AddTextBlock(text, sp);
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

private static void AddImg(string img, StackPanel sp, double w, double h)
    {
        bool isAwesome = false;

        if (img.Length == 1)
        {
            var ch = img[0];
            if (ch >= AwesomeFontControls.low && ch <= AwesomeFontControls.high)
            {
                isAwesome = true;
            }
        }

        if (isAwesome)
        {
            TextBlock tb = new TextBlock();
            AwesomeFontControls.SetAwesomeFontSymbol(tb, img);
            sp.Children.Add(tb);
        }
        else
        {
            sp.Children.Add(ImageHelperDesktop.Get(img));
        }
        
    }

private static void AddTextBlock(string text, StackPanel sp)
    {
        sp.Children.Add(TextBlockHelper.Get(new ControlInitData{text = text}));
    }
}