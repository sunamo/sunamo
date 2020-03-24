using System;
using System.Collections.Generic;
using System.Text;


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
    static List<string> withLeftArrow;
    public static List<string> WithLeftArrow
    {
        get
        {
            if (withLeftArrow == null)
            {
                Initialize();
                withLeftArrow = new List<string>(list.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    withLeftArrow.Add(AllStrings.lt + list[i] + AllStrings.space);
                }
            }

            return withLeftArrow;
                }
    }

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