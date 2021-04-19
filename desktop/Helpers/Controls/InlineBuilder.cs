
using desktop;

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

public class InlineBuilder : InlineBuilderBase, IInlineBuilder
{
    public InlineCollection inlines = null;
    static Dictionary<int, double> averageNumberWidthOnFontSize = new Dictionary<int, double>();
    static Dictionary<int, double> averageCharWidthOnFontSize = new Dictionary<int, double>();

    public InlineBuilder(InlineCollection inlines)
    {
        this.inlines = inlines;
    }

    public InlineBuilder()
    {

    }

    public FontArgs fa = FontArgs.DefaultRun();
    public void DivideStringToRows(FontArgs fa, string text, Size maxSize)
    {
        List<string> ls = FontHelper.DivideStringToRows(fa.fontFamily, fa.fontSize, fa.fontStyle, fa.fontStretch, fa.fontWeight, text, maxSize);
        foreach (var item in ls)
        {
            inlines.Add(GetRun(item, fa));
            inlines.Add(new LineBreak());
        }
    }

    public void Bold(string text)
    {
        FontArgs fa = FontArgs.DefaultRun();
        fa.fontWeight = GetFontWeight(FontWeight2.bold);
        inlines.Add(GetBold(text, fa));
    }

    public void Hyperlink(string text, string uri)
    {
        var run = GetHyperlink(text, uri, fa);
        inlines.Add(run);

    }

    /// <summary>
    /// Return null but also add it
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public void Run(string p)
    {
        Run run = GetRun(p, fa);
        inlines.Add(run);
    }

    public void H1(string text, double maxWidth)
    {
        Bold b = new Bold();
        FontArgs fa = FontArgs.DefaultRun();
        fa.fontSize = HeaderSize.h1;
        //b.FontSize = 40;
        b.Inlines.Add(new LineBreak());
        //b.Inlines.Add(GetRun(text, fa));
        DivideStringToRows(fa, text, new Size(maxWidth, double.PositiveInfinity));
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(new LineBreak());
        inlines.Add(b);
    }

    public void H1(string text)
    {
        Bold b = new Bold();
        FontArgs fa = FontArgs.DefaultRun();
        fa.fontSize = HeaderSize.h1;
        //b.FontSize = 40;
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(GetRun(text, fa));
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(new LineBreak());
        inlines.Add(b);
    }

    public void H2(string text)
    {
        Bold b = new Bold();
        FontArgs fa = FontArgs.DefaultRun();
        //fa.fontSize = 50;
        fa.fontSize = HeaderSize.h2;
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(GetRun(text, fa));
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(new LineBreak());
        inlines.Add(b);
    }

    public void H3(string text)
    {
        Italic b = new Italic();
        FontArgs fa = FontArgs.DefaultRun();
        fa.fontSize = HeaderSize.h3;
        //b.FontSize = 30;
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(GetRun(text, fa));
        b.Inlines.Add(new LineBreak());
        b.Inlines.Add(new LineBreak());
        inlines.Add(b);
    }

    /// <summary>
    /// Tato Metoda nefunguje, protože Paragraph je odvozený od Block a ne od Inline 
    /// </summary>
    /// <param name = "italic"></param>
    public void AddParagraph(Inline italic)
    {
    }

    public void LineBreak()
    {
        inlines.Add(new LineBreak());
    }

    public void KeyValue(string p1, string p2)
    {
        if (!string.IsNullOrWhiteSpace(p2))
        {
            p2 = p2.Trim();
            p1 = p1.Trim();
            if (p2 != "" && p1 != "")
            {
                Bold(p1);
                Run(AllStrings.space + p2);
                LineBreak();
            }
        }
    }

    public void Error(string p)
    {
        inlines.Add(GetError(p, FontArgs.DefaultRun()));
        LineBreak();
    }

    public void Bullet(string p)
    {
        Inline il = GetBullet(p, fa);
        //il.Foreground = new SolidColorBrush(Colors.Black);
        inlines.Add(il);
        LineBreak();
    }

    public void Italic(string p)
    {
        inlines.Add(GetItalic(p, fa));
    }

    public void DivideStringToRows(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
    {
        FontArgs fa = new FontArgs(fontFamily, fontSize, fontStyle, fontStretch, fontWeight);
        DivideStringToRows(fa, text, maxSize);
    }
}