using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

/// <summary>
/// Have StackPanel and NewTextBlock()
/// </summary>
public class ParagraphBuilderTextBlock : IInlineBuilder
{
    InlineBuilderTextBlock t = null;
    public Thickness padding = new Thickness();
    public Thickness margin = new Thickness();

    public ParagraphBuilderTextBlock() 
    {
        sp.Orientation = Orientation.Vertical;
        NewTextBlock();
    }

    StackPanel sp = new StackPanel();

    public void NewTextBlock()
    {
        if (t != null)
        {
            sp.Children.Add(t.tb);
        }

        t = new InlineBuilderTextBlock();
    }

    public StackPanel Final()
    {
        sp.Children.Add(t.tb);

        foreach (TextBlock item in sp.Children)
        {
            item.Margin = margin;
            item.Padding = padding;
        }

        return sp;
    }

    #region IInlineBuilder members
    public void Bold(string text)
    {
        t.Bold(text);
    }

    public void Bullet(string p)
    {
        t.Bullet(p);
    }

    public void Error(string p)
    {
        t.Error(p);
    }

    public void H1(string text)
    {
        t.H1(text);
    }

    public void H1(string text, double maxWidth)
    {
        t.H1(text, maxWidth);
    }

    public void H2(string text)
    {
        t.H2(text);
    }

    public void H3(string text)
    {
        t.H3(text);
    }

    public void Hyperlink(string text, string uri)
    {
        t.Hyperlink(text, uri);
    }

    public void Italic(string p)
    {
        t.Italic(p);
    }

    public void KeyValue(string p1, string p2)
    {
        t.KeyValue(p1, p2);
    }

    public void LineBreak()
    {
        t.LineBreak();
    }

    public void Run(string p)
    {
        t.Run(p);
    } 
    #endregion
}