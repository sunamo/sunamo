using System.Collections.Generic;
using System.Web.UI.WebControls;
public class DDLH
{
    public static ListItem[] CastStringsToListItems(List<string> list)
    {
        List<ListItem> vr = new List<ListItem>();
        foreach (string item in list)
        {
            vr.Add(new ListItem(item));
        }
        return vr.ToArray();
    }

    public static List<ListItem> CastStringsToListItemsList(List<string> list)
    {
        List<ListItem> vr = new List<ListItem>();
        foreach (string item in list)
        {
            vr.Add(new ListItem(item));
        }
        return vr;
    }

    /// <summary>
    /// Dá do hodnoty i textu A2
    /// </summary>
    /// <param name="select"></param>
    /// <param name="item"></param>
    public static void AddItems(DropDownList select, params string[] item)
    {
        foreach (var item2 in item)
        {
            AddItem(select, item2);
        }
    }

    /// <summary>
    /// Dá do hodnoty i textu A2
    /// </summary>
    /// <param name="select"></param>
    /// <param name="item"></param>
    public static void AddItem(DropDownList select, string item)
    {
        select.Items.Add(new ListItem(item, item));
    }

    public static List<ListItem> CastStringsToListItemsList(List<string> values, List<string> texts)
    {
        List<ListItem> vr = new List<ListItem>();
        for (int i = 0; i < values.Count; i++)
        {
            if (texts[i] != "" && values[i] != "")
            {
                vr.Add(new ListItem(texts[i], values[i]));
            }
        }
        return vr;
    }

    public static ListItem[] CastStringsToListItems(List<string> values, List<string> texts)
    {
        List<ListItem> vr = new List<ListItem>();
        for (int i = 0; i < values.Count; i++)
        {
            if (texts[i] != "" && values[i] != "")
            {
                vr.Add(new ListItem(texts[i], values[i]));
            }
        }
        return vr.ToArray();
    }

    public static void CheckItem(DropDownList ddlStates, string itemValue)
    {
        if (itemValue == null)
        {
            itemValue = "";
        }
        //string stateID = profilIDState.ToString();
        foreach (ListItem item in ddlStates.Items)
        {
            if (item.Value == itemValue)
            {
                item.Selected = true;
                return;
            }
        }
    }
}
