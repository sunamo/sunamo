using System.Web.UI.HtmlControls;
public static class ShareButtons
{
    public static void PlusOneCasdMladezPhoto(HtmlButton hb, bool bilyObtah)
    {
        hb.Style.Clear();
        hb.Style.Value = "background: orange;color: white;padding: 10px;border-radius: 50%;border-color: transparent;width:50px;height:50px;";
        hb.InnerHtml = "<span class='fa-heart fa'></span>";
        if (bilyObtah)
        {
            hb.Attributes.Add("class", "bilyObtah");
        }
        else
        {
            if (hb.Attributes["class"] != null)
            {
                hb.Attributes["class"] = hb.Attributes["class"].Replace("cernyObtah", "");    
            }
        }
        hb.Attributes.Add("type", "button");
    }
}
