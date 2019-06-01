using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

/// <summary>
/// Must be in shared due to HtmlTextWriterTag in System.Web
/// </summary>
public class AllHtmlTags
{
    public static List<string> list = null;

    

    public static void Initialize()
    {
        if (list == null)
        {

            list = new List<string>();

            foreach (var item in Enum.GetNames(typeof(HtmlTextWriterTag)))
            {
                list.Add(item.ToLower());
            }
        }
    }
}

