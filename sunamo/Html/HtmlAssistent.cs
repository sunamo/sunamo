using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Html
{
    public class HtmlAssistent
    {
        public static string GetValueOfAttribute(string p, HtmlNode divMain, bool trim = false)
        {
            object o = divMain.Attributes[p];
            if (o != null)
            {
                string st = ((HtmlAttribute)o).Value;
                if (trim)
                {
                    st = st.Trim();
                }
                return st;
            }
            return "";
        }
    }
}
