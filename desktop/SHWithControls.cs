using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

public static class SHWithControls
{
    static TextBlock tb = new TextBlock();

    public static string DivideStringToRows(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        StringBuilder vystup = new StringBuilder();
        foreach (var item in DivideStringToRowsList(fontFamily, fontSize, fontStyle, fontStretch, fontWeight, text, maxSize))
        {
            vystup.AppendLine(item);
        }
        return vystup.ToString();
    }

    #region wsf


    public static double MeasureString(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        //tb.Height = Double.PositiveInfinity;
        tb.FontFamily = fontFamily;
        tb.FontSize = fontSize;
        tb.FontStyle = fontStyle;
        tb.FontStretch = fontStretch;
        tb.FontWeight = fontWeight;
        tb.Text = text;
        tb.Measure(maxSize);
        //tb.Arrange(new Rect(new Point(0, 0), maxSize));
        return tb.DesiredSize.Width;
    }

    public static List<string> DivideStringToRowsList(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        maxSize.Width = maxSize.Width * 0.95d;
        List<string> vr = new List<string>();
        double maxWidth = maxSize.Width; //  (fontSize * 3);
        StringBuilder sb = new StringBuilder();
        StringBuilder sbCelaSlova = new StringBuilder();
        foreach (char item in text)
        {
            if (item == ' ')
            {
                sbCelaSlova.Clear();
                sbCelaSlova.Append(sb);
            }
            sb.Append(item);

            double measureString = MeasureString(fontFamily, fontSize, fontStyle, fontStretch, fontWeight, sb.ToString(), maxSize);
            ////Debug.WriteLine(measureString.ToString());
            if (measureString > maxWidth)
            {
                // Získat řetězec z sb
                string sb2 = sb.ToString();
                // Nahradit v tomto řetězci a substringovat od prvního znaku
                sb2 = sb2.Replace(sbCelaSlova.ToString(), "").Substring(1);
                vr.Add(sbCelaSlova.ToString());
                //vystup.AppendLine(sbCelaSlova.ToString());
                sb.Clear();
                sbCelaSlova.Clear();
                sbCelaSlova.Append(sb2);
                sb.Append(sb2);
            }
        }
        vr.Add(sb.ToString());
        return vr;
    }


    #endregion
}
