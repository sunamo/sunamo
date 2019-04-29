using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class HtmlAssistant{ 
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

        return string.Empty;
    }
}