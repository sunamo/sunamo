using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI.WebControls;


public class WLBH
{
    public static ListItem[] CastStringsToListItems(List<string> values, List<string> texts)
    {
        return DDLH.CastStringsToListItems(values, texts);
    }

    public static ListItem[] CastStringsToListItems(List<string> texts)
    {
        return DDLH.CastStringsToListItems(texts);
    }
}
