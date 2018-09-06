using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class TBH : TextBlockHelperBase
    {
        public FontArgs fa = FontArgs.DefaultRun();
        TextBlock tb = null;
        public TBH(TextBlock tb)
        {
            this.tb = tb;
        }

        public void DivideStringToRows(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, FontWeight fontWeight, string text, Size maxSize)
        {
            FontArgs fa = new FontArgs(fontFamily, fontSize, fontStyle, fontStretch, fontWeight);
            List<string> l = SHWithControls.DivideStringToRowsList(fontFamily, fontSize, fontStyle, fontStretch, fontWeight, text, maxSize);
            foreach (var item in l)
            {
                tb.Inlines.Add(GetRun(item, fa));
                tb.Inlines.Add(new LineBreak());
            }

        }



        

        public void H1(string text)
        {
            Bold b = new Bold();
            FontArgs fa = FontArgs.DefaultRun();
            fa.fontSize = 50;
            //b.FontSize = 40;
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(GetRun(text, fa));
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(new LineBreak());
            tb.Inlines.Add(b);
        }

        internal void Run(string v)
        {
            tb.Inlines.Add(GetRun(v, fa));
        }

        internal void Bold(string v)
        {
            tb.Inlines.Add(GetBold(v, fa));
        }

        public void H3(string text)
        {
            Italic b = new Italic();
            FontArgs fa = FontArgs.DefaultRun();
            fa.fontSize = 30;
            //b.FontSize = 30;
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(GetRun(text, fa));
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(new LineBreak());
            tb.Inlines.Add(b);
        }

        /// <summary>
        /// Tato Metoda nefunguje, protože Paragraph je odvozený od Block a ne od Inline 
        /// </summary>
        /// <param name="italic"></param>
        public void AddParagraph(Inline italic)
        {
        }

        public void LineBreak()
        {
            tb.Inlines.Add(new LineBreak());
        }

        

        public void KeyValue(string p1, string p2)
        {
             p2 = p2.Trim();
             p1 = p1.Trim();
            if (p2 != "" && p1 != "")
            {
                Bold(p1);
                Run(" " + p2);
                LineBreak();
            }
        }

        

        public void Error(string p)
        {
            tb.Inlines.Add(GetError(p, FontArgs.DefaultRun()));
            LineBreak();
        }

        public void Bullet(string p)
        {
            Inline il = GetBullet(p, fa);
            //il.Foreground = new SolidColorBrush(Colors.Black);
            tb.Inlines.Add(il);
            LineBreak();
        }

        public void Italic(string p)
        {
            tb.Inlines.Add(GetItalic(p, fa));
        }
    }
}
