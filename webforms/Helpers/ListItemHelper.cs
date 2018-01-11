
using System.Web.UI.WebControls;

public class ListItemHelper
{
    public static ListItem[] CastStringsToListItems(string[] v)
    {
        ListItem[] vr = new ListItem[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            vr[i] = new ListItem(v[i]);
        }
        return vr;
    }
}
