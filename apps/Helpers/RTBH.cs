using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace apps
{
    public class RTBH : TextBlockHelperBase
    {
        public FontArgs fa = FontArgs.DefaultRun();
        RichTextBlock rtb = null;

        public RTBH(RichTextBlock rtb)
        {
            this.rtb = rtb;
        }

        public void Run(string p)
        {
            rtb.Blocks.Add(GetParagraph(GetRun(p, fa)));
        }

        public void Bold(string p)
        {
            rtb.Blocks.Add(GetParagraph( GetBold(p, fa)));
        }

        private Paragraph GetParagraph(Inline bold)
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(bold);
            return p;
        }
    }
}
