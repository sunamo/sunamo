
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

namespace desktop
{
    /// <summary>
    /// TBH je pro TextBox, TBH2 pro TextBlock
    /// </summary>
    public class TextBlockHelper : TextBlockHelperBase
    {
        #region Vypočítání optimální šířky dle velikosti
        static Dictionary<int, double> averageNumberWidthOnFontSize = new Dictionary<int, double>();
        static Dictionary<int, double> averageCharWidthOnFontSize = new Dictionary<int, double>();

        static TextBlockHelper()
        {
            TextBlockHelper.InicializeWidths();
        }

        public static void InicializeWidths()
        {
            StackPanel p = new StackPanel();
            TextBlock txtTest = new TextBlock();
            txtTest.MinWidth = 0;
            Dictionary<int, double> charWidth = new Dictionary<int, double>();

            double? d = null;
            for (char i = 'a'; i <= 'z'; i++)
            {
                txtTest = new TextBlock();
                txtTest.Text = i.ToString();
                txtTest.Measure(ControlHelper.SizePositiveInfinity);
                txtTest.Arrange(new Rect(0, 0, txtTest.DesiredSize.Width, txtTest.DesiredSize.Height));
                txtTest.UpdateLayout();
                charWidth.Add(i, txtTest.ActualWidth);

                if (d == null)
                {
                    d = txtTest.ActualWidth;
                }
                else
                {
                    if (txtTest.ActualWidth > d.Value)
                    {
                        d = txtTest.ActualWidth;
                    }
                }

            }
            double ave = 0;

            double sum = 0;
            // Nejdříve vypočtu průměrnou velikost při FontSize=100
            foreach (var item in charWidth)
            {
                sum += item.Value;
            }
            ave = sum / charWidth.Count;
            // Pak vydělím 100
            ave /= 100;
            // Násobím 1-100(velikost písma) předchozím výsledkem - dostanu šířku TextBlocku při velikosti písma ai
            Dictionary<int, double> aweWidthFor = new Dictionary<int, double>();
            for (int i = 1; i < 101; i++)
            {
                aweWidthFor.Add(i, i * ave);
            }

            for (int i = 1; i < 101; i++)
            {
                txtTest = new TextBlock();
                p.Children.Add(txtTest);
                txtTest.Text = "1";

                txtTest.FontSize = i;
                txtTest.Measure(ControlHelper.SizePositiveInfinity);
                averageNumberWidthOnFontSize.Add(i, txtTest.DesiredSize.Width);
                p.Children.Remove(txtTest);
            }
            p.Visibility = Visibility.Collapsed;
        }

        public static double GetOptimalWidthForCountOfChars(int count, bool alsoLetters, TextBlock txt)
        {
            double countDouble = (double)count;
            double copy = (double)(int)txt.FontSize;
            if (copy != txt.FontSize)
            {
                copy++;
            }
            int copyInt = (int)copy;
            Dictionary<int, double> dict = null;
            if (alsoLetters)
            {
                dict = averageCharWidthOnFontSize;
            }
            else
            {
                dict = averageNumberWidthOnFontSize;
            }
            if (!dict.ContainsKey(copyInt))
            {
                copyInt = dict.Count;
            }
            return dict[copyInt] * countDouble;
        }

        public static TextBlock Get(string text)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            return tb;
        }
        #endregion

        public FontArgs fa = FontArgs.DefaultRun();
        TextBlock tb = null;

        public TextBlockHelper(TextBlock tb)
        {
            this.tb = tb;
        }

        public void DivideStringToRows(FontArgs fa, string text, Size maxSize)
        {
            List<string> ls = FontHelper.DivideStringToRows(fa.fontFamily, fa.fontSize, fa.fontStyle, fa.fontStretch, fa.fontWeight, text, maxSize);
            foreach (var item in ls)
            {
                tb.Inlines.Add(GetRun(item, fa));
                tb.Inlines.Add(new LineBreak());
            }
        }

        #region Error
        public void Bold(string text)
        {
            FontArgs fa = FontArgs.DefaultRun();
            fa.fontWeight = GetFontWeight(FontWeight2.bold);
            tb.Inlines.Add(GetBold(text, fa));
        }




        #endregion

        #region Run
        public Run Run(string p)
        {
            Run run = GetRun(p, fa);
            tb.Inlines.Add(run);
            return run;
        }


        #endregion

        public void H1(string text, double maxWidth)
        {
            Bold b = new Bold();
            FontArgs fa = FontArgs.DefaultRun();
            fa.fontSize = 50;
            //b.FontSize = 40;
            b.Inlines.Add(new LineBreak());
            //b.Inlines.Add(GetRun(text, fa));
            DivideStringToRows(fa, text, new Size( maxWidth, double.PositiveInfinity));
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(new LineBreak());
            tb.Inlines.Add(b);
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

        public void H2(string text)
        {
            Bold b = new Bold();
            FontArgs fa = FontArgs.DefaultRun();
            //fa.fontSize = 50;
            fa.fontSize = 40;
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(GetRun(text, fa));
            b.Inlines.Add(new LineBreak());
            b.Inlines.Add(new LineBreak());
            tb.Inlines.Add(b);
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
            if (!string.IsNullOrWhiteSpace(p2))
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

        public void DivideStringToRows(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text, Size maxSize)
        {
            FontArgs fa = new FontArgs(fontFamily, fontSize, fontStyle, fontStretch, fontWeight);
            DivideStringToRows(fa, text, maxSize);
        }
    }
}
