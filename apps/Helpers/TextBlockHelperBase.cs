using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class TextBlockHelperBase
    {
        protected List<MeasureStringArgs> texts = new List<MeasureStringArgs>();

        protected FontWeight GetFontWeight(FontWeight2 fontWeight)
        {
            FontWeight fw = new FontWeight();
            fw.Weight = (ushort)fontWeight;
            return fw;
        }

        public Italic GetItalic(string run, FontArgs fa)
        {
            Italic b = new Italic();
            FontArgs fa2 = new FontArgs(fa);
            fa.fontStyle = Windows.UI.Text.FontStyle.Italic;
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
            Windows.UI.Text.FontWeight fw = new Windows.UI.Text.FontWeight();
            fw.Weight = 700;
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
            HyperlinkButton link = new HyperlinkButton();
            link.FontFamily = fa.fontFamily;
            link.FontSize = fa.fontSize;
            link.FontStretch = fa.fontStretch;
            link.FontStyle = fa.fontStyle;
            link.FontWeight = fa.fontWeight;
            link.NavigateUri = new Uri(uri);
            link.Padding = padding;
            link.Margin = margin;
            //link.Name = ControlNameGenetator.GetSeries(link.GetType());
            InlineUIContainer inlines = new InlineUIContainer();
            
            //inlines.Name = ControlNameGenetator.GetSeries(inlines.GetType());
            link.Content = text;
            texts.Add(new MeasureStringArgs(link.FontFamily, link.FontSize, link.FontStyle, link.FontStretch, link.FontWeight, link.Content.ToString()));
            inlines.Child = link;
            
            return inlines;
        }
    }
}
