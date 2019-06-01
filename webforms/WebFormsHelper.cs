using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

public class WebFormsHelper
{
    public static void SetDataCaptionAttr(System.Web.UI.HtmlControls.HtmlControl c, string v)
    {
        c.Attributes["data-caption"] = v;
    }

    /// <summary>
    /// Add with space
    /// </summary>
    /// <param name="c"></param>
    /// <param name="v"></param>
    public static void AppendToInnerHtml(HtmlContainerControl c, string v)
    {
        c.InnerHtml += " " + v;
    }

    public static void EnPostColon(HtmlContainerControl c, string key)
    {
        c.InnerHtml = RLData.EnPostColon(key);
    }

    public static void InnerHtml(HtmlContainerControl c, string key)
    {
        c.InnerHtml = RLData.en[key];
    }

    public static void SetAttr(HtmlControl c, string attr, string key)
    {
        c.Attributes[attr] = RLData.en[key];
    }
}

