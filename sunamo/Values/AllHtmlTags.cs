using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

/// <summary>
/// Must be in shared due to HtmlTextWriterTag in System.Web
/// All is lower
/// </summary>
public class AllHtmlTags
{
    /// <summary>
    /// Sorted from longest to shortest due to comparing and finding right string
    /// </summary>
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

            list.Sort(new SunamoComparerICompare.StringLength.Desc(SunamoComparer.StringLength.Instance));
        }
    }
}

