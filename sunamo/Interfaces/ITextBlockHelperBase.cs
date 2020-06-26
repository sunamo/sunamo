using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITextBlockHelperBase<FontWeight, Italic, Inline,Bold, Run, InlineUIContainer, FontArgs>
{
    FontWeight GetFontWeight(FontWeight2 fontWeight);
    Italic GetItalic(string run, FontArgs fa);
    Inline GetBullet(string p, FontArgs fa);
    Bold GetError(string p, FontArgs fa);
    Bold GetBold(string p, FontArgs fa);
    Run GetRun(string text, FontArgs fa);
    InlineUIContainer GetHyperlink(string text, string uri, FontArgs fa);
}