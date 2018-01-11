using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public class SelectHelper
{

    /// <summary>
    /// Vrátí 0 pokud se index nepodaří nalézt
    /// </summary>
    /// <param name="selectTypeSoftware"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static int GetIndexOfValue(HtmlSelect selectTypeSoftware, string p)
    {
        for (int i = 0; i < selectTypeSoftware.Items.Count; i++)
        {
            if (selectTypeSoftware.Items[i].Value == p)
            {
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// Dá do hodnoty i textu A2
    /// </summary>
    /// <param name="select"></param>
    /// <param name="item"></param>
    public static void AddItems(HtmlSelect select, params string[] item)
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
    public static void AddItem(HtmlSelect select, string item)
    {
        select.Items.Add(new ListItem(item, item));
    }
}
