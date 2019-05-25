using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WebFormsHelper
{
    public static void SetDataCaptionAttr(System.Web.UI.HtmlControls.HtmlControl c, string v)
    {
        c.Attributes["data-caption"] = v;
    }
}

