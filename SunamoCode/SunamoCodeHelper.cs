using System;
using System.Collections.Generic;
using System.Text;


public class SunamoCodeHelper
{
    private static bool IsNameOfHtmlAttrValue(string between)
    {
        return AllHtmlAttrsValues.list.Contains(between.Trim());
    }

    private static bool IsNameOfHtmlAttr(string between)
    {
        return AllHtmlAttrs.list.Contains(between.Trim());
    }

    public static bool IsNameOfHtmlTag(string between, bool add)
    {
        string element = null;
        var startWithTag = CA.StartWith(AllHtmlTags.list, between, out element);
        startWithTag = element;
        if (startWithTag != null)
        {
            if (startWithTag == between)
            {
                add = true;
            }
            else
            {
                var remain = between.Substring(startWithTag.Length);
                add = BTS.IsInt(remain);
            }
        }

        return add;
    }

    /// <summary>
    /// A1 normal, not lower
    /// </summary>
    /// <param name="between"></param>
    public static bool IsNameOfControl(string between)
    {
        var add = false;
        add = IsNameOfHtmlTag(between, add);

        if (!add)
        {
            add = IsNameOfHtmlAttr(between);
        }

        if (!add)
        {
            add = IsNameOfHtmlAttrValue(between);
        }

        if (!add)
        {
            #region Check whether contains only lower and no letters after first number
            int firstInt = -1;

            var i = 0;
            foreach (var item in between)
            {
                if (char.IsLower(item))
                {
                    if (firstInt != -1)
                    {
                        add = false;
                        break;
                    }
                }
                else if (char.IsNumber(item))
                {
                    if (firstInt == -1)
                    {
                        firstInt = i;
                    }
                }
                else
                {
                    add = false;
                    break;
                }
                i++;
            }
            #endregion

            string prefix = between;
            if (firstInt != -1)
            {
                prefix = between.Substring(0, firstInt);
            }
            add = SystemWindowsControls.IsShortcutOfControl(prefix);
        }

        return add;
    }
}