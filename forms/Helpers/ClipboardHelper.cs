using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
namespace forms
{
    public class ClipboardHelper
    {
        public static List<string> GetLines()
        {
            return SH.GetLines(Clipboard.GetText());
        }

        public static void SetText(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                Clipboard.SetText(p);
            }
            else
            {
                Clipboard.SetText("SE or NULL");
            }
        }

        public static void SetLines(IEnumerable p)
        {
            Clipboard.SetText(SH.JoinString(Environment.NewLine, p));
        }
    }
}
