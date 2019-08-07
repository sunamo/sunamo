using desktop;
using desktop.Essential;
using desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

public partial class TextBlockHelper{ 
public static TextBlock Get(string text)
    {
        TextBlock tb = new TextBlock();
        tb.Text = text;
        return tb;
    }

public static void SetText(TextBlock lblStatusDownload, string status)
    {
        if (lblStatusDownload != null)
        {
            // Must be invoke because after that I immediately load it on ListBox
            WpfApp.cd.Invoke(() =>
            {
                lblStatusDownload.Text = status;
            }

            );
        }
    }

/// <summary>
    /// A1 can be TextBlock or any object
    /// </summary>
    /// <param name = "tb"></param>
    public static string TextOrToString(object tb)
    {
        if (tb is TextBlock)
        {
            var tb2 = (TextBlock)tb;
            return tb2.Text;
        }

        return tb.ToString();
    }
}