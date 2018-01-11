
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

public class FontArgs
{
    public FontArgs()
    {
        
    }

    public FontArgs(FontFamily fontFamily, double fontSize, System.Windows.FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight)
    {
        this.fontFamily = fontFamily;
        this.fontSize = fontSize;
        this.fontStretch = fontStretch;
        this.fontStyle = fontStyle;
        this.fontWeight = fontWeight;
    }

    public FontArgs(FontArgs fa)
    {
        this.fontFamily = fa.fontFamily;
        this.fontSize = fa.fontSize;
        this.fontStretch = fa.fontStretch;
        this.fontStyle = fa.fontStyle;
        this.fontWeight = fa.fontWeight;
    }

    public FontFamily fontFamily = null;
    public double fontSize = 0;
    public FontStyle fontStyle = FontStyles.Normal;
    /// <summary>
    /// Hodnota mezi 1-9, průměrná je 5
    /// </summary>
    public FontStretch fontStretch = FontStretch.FromOpenTypeStretch(5);
    public System.Windows.FontWeight fontWeight = new System.Windows.FontWeight();

    public static FontArgs DefaultRun()
    {
        Run r = new Run();
        return new FontArgs(r.FontFamily, r.FontSize, r.FontStyle, r.FontStretch, r.FontWeight);
    }
}
