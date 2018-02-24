using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
namespace desktop
{
    public class TextBlockHelperBase
    {
        protected List<MeasureStringArgs> texts = new List<MeasureStringArgs>();

        protected FontWeight GetFontWeight(FontWeight2 fontWeight)
        {
            System.Windows.FontWeight fw = System.Windows.FontWeight.FromOpenTypeWeight((int)fontWeight);
            return fw;
        }

        public Italic GetItalic(string run, FontArgs fa)
        {
            Italic b = new Italic();
            FontArgs fa2 = new FontArgs(fa);
            fa.fontStyle = FontStyles.Italic;
            b.Inlines.Add(GetRun(run, fa));

            return b;
        }
        public Inline GetBullet(string p, FontArgs fa)
        {
            return GetRun("• " + p, fa);
        }

        public Bold GetError(string p, FontArgs fa)
        {
            Bold b = GetBold(p, fa);
            b.Foreground = new SolidColorBrush(Colors.Red);
            b.FontSize += 5;
            return b;
        }

        public Bold GetBold(string p, FontArgs fa)
        {
            Bold b = new Bold();
            FontArgs fa2 = new FontArgs(fa);
            System.Windows.FontWeight fw = System.Windows.FontWeight.FromOpenTypeWeight(700);
            fa2.fontWeight = fw;
            b.Inlines.Add(GetRun(p, fa2));
            return b;
        }

        public Run GetRun(string text, FontArgs fa)
        {
            Run run = new Run();
            run.FontFamily = fa.fontFamily;
            run.FontSize = fa.fontSize;
            run.FontStretch = fa.fontStretch;
            run.FontStyle = fa.fontStyle;
            run.FontWeight = fa.fontWeight;
            run.Text = text;
            
            texts.Add(new MeasureStringArgs(run.FontFamily, run.FontSize, run.FontStyle, run.FontStretch, run.FontWeight, run.Text));
            return run;
        }

        public InlineUIContainer GetHyperlink(string text, string uri, Thickness margin, Thickness padding, FontArgs fa)
        {
            Label tb = new Label();
            Hyperlink link = new Hyperlink();

            //link.Inlines.Add(tb);
            tb.Content = link;
            tb.FontFamily = fa.fontFamily;
            tb.FontSize = fa.fontSize;
            tb.FontStretch = fa.fontStretch;
            tb.FontStyle = fa.fontStyle;
            tb.FontWeight = fa.fontWeight;
            link.NavigateUri = new Uri(uri);
            tb.Padding = padding;
            tb.Margin = margin;
            //link.Name = ControlNameGenetator.GetSeries(link.GetType());
            InlineUIContainer inlines = new InlineUIContainer();

            //inlines.Name = ControlNameGenetator.GetSeries(inlines.GetType());
            tb.Content = text;
            texts.Add(new MeasureStringArgs(tb.FontFamily, tb.FontSize, tb.FontStyle, tb.FontStretch, tb.FontWeight, text));
            inlines.Child = tb;

            return inlines;
        }
    }
}
