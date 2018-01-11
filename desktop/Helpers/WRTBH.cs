using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace desktop
{
    class WRTBH : TextBlockHelperBase
    {
        FontArgs fa = null;
        Thickness margin = new Thickness(0);
        Thickness padding = new Thickness(0, 1, 5, 1);
        double maxWidth = 0;
        double paddingLeftFirst = 0;
        bool nextIsFirts = true;
        double width = 0;
        public List<StackPanel> uis = new List<StackPanel>();

        public WRTBH(double maxWidth, double paddingLeftFirst, FontArgs fa)
        {
            this.fa = fa;
            this.paddingLeftFirst = paddingLeftFirst;
            this.maxWidth = maxWidth;
            uis.Add(NewStackPanel());
        }

        private StackPanel NewStackPanel()
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            return sp;
        }

        public void Run(string text)
        {
            string[] slova = GetWords(text);
            foreach (var item in slova)
            {
                Add(GetTextBlock(GetRun(item, fa)));
            }
        }



        private string[] GetWords(string text)
        {
            return SH.SplitNone(text, " ");
        }

        /// <summary>
        /// Automaticky přidá jednomezerový run za hyperlink
        /// </summary>
        /// <param name="text"></param>
        /// <param name="uri"></param>
        public void HyperLink(string text, string uri)
        {
            Add(GetTextBlock(GetHyperlink(text, uri, margin, padding, fa)));
            //Add(GetRichTextBlock( GetHyperlink(text, uri, margin, padding, fa)));
            Add(GetTextBlock(GetRun(" ", fa)));
        }

        public void Bold(string text)
        {
            string[] slova = GetWords(text);
            foreach (var item in slova)
            {
                Add(GetTextBlock(GetBold(item + "  ", fa)));
            }
        }

        private TextBlock GetTextBlock(Inline inline)
        {
            TextBlock txt = new TextBlock();
            txt.Inlines.Add(inline);
            txt.Padding = padding;
            txt.Margin = margin;
            return txt;
        }

        private void Add(TextBlock textBlock)
        {
            MeasureStringArgs msa = texts[texts.Count - 1];
            double width2 = SHWithControls.MeasureString(msa.fontFamily, msa.fontSize, msa.fontStyle, msa.fontStretch, msa.fontWeight, msa.text, new Size( textBlock.ActualWidth, textBlock.ActualHeight));
            double width3 = width + width2 + padding.Right;

            if (nextIsFirts)
            {
                textBlock.Padding = new Thickness(textBlock.Padding.Left + paddingLeftFirst, textBlock.Padding.Top, textBlock.Padding.Right, textBlock.Padding.Bottom);
                nextIsFirts = false;
            }

            if (maxWidth < width3)
            {
                width = 0;
                uis.Add(NewStackPanel());
                textBlock.Padding = new Thickness(textBlock.Padding.Left + paddingLeftFirst, textBlock.Padding.Top, textBlock.Padding.Right, textBlock.Padding.Bottom);
            }
            else
            {
                width = width3;
            }

            uis[uis.Count - 1].Children.Add(textBlock);
        }

        public void LineBreak()
        {
            nextIsFirts = true;
            uis.Add(NewStackPanel());

            //Add(GetTextBlock(GetLineBreak()));
        }



        public void Italic(string p)
        {
            string[] slova = GetWords(p);
            foreach (var item in slova)
            {
                Add(GetTextBlock(GetItalic(item + "  ", fa)));
            }
        }
    }
}
