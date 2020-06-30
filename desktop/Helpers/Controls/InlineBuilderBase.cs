using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
namespace desktop
{
    /// <summary>
    /// Věrná kopie z apps, všechny věci jež nejsou ve WPF josu nahrazené WPF ekvivalentem
    /// Bohužel nefunguje jako odkazy, zobrazí se pouze neklikatelný text
    /// </summary>
    public class InlineBuilderBase : ITextBlockHelperBase<FontWeight, Italic, Inline, Bold, Run, Hyperlink, FontArgs>
    {
        protected List<MeasureStringArgs> texts = new List<MeasureStringArgs>();

        public FontWeight GetFontWeight(FontWeight2 fontWeight)
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

        public Hyperlink GetHyperlink(string text, string uri, FontArgs fa)
        {
            // In WPF is not needed put it in subs. control like Label
            
            Hyperlink link = new Hyperlink(GetRun(text, fa));
            link.NavigateUri = new Uri(uri);
            link.RequestNavigate += Link_RequestNavigate;

            return link;
        }

        private void Link_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}