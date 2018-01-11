using System.Collections.Generic;
using System.Text;
public class HtmlSearchResult
{
    /// <summary>
    /// Relativní adresa vzhledem k /Apps/
    /// </summary>
    public string uri = "";
    /// <summary>
    /// Textový řetězec, například "Apps", "Version"
    /// Je to v kolekci aby se to mohlo snáze porovnávat
    /// </summary>
    public List<string> hiearchy = new List<string>();
    /// <summary>
    /// Název prvku který jsem vyhledal, například "Il Cammino Manager"
    /// </summary>
    public string name = "";
    /// <summary>
    /// Texto nalezený v popisu aplikace. Je HTML, aby se mohli zvýraznit nalezená slova. Vždycky bude obsahovat maximálně 2 řetězce slov po 9 slovech, vždy 4 před slovem a 4 za slovem. Pokud v těchto slovech bude více hledaných slov, budou se zobrazovat přednostně a s větší jistotou.
    /// </summary>
    public string text = "";

    public string meta = "";


    public string GetHtml(string rightUp)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttr("div", "style", "margin-bottom: 15px");
        hg.WriteTagWith2Attrs("a", "href", rightUp  + uri, "class", "vysledekHledaniOdkaz");
        hg.WriteRaw(name);
        hg.TerminateTag("a");
        hg.WriteBr();

            hg.WriteTagWithAttr("div", "class", "vysledekHledaníHiearchie");
            StringBuilder sb = new StringBuilder();
            int toh = hiearchy.Count ;
            bool zapsano = false;
            if (hiearchy.Count > 0)
            {
                zapsano = true;
                hg.WriteRaw(hiearchy[0]);
                for (int i = 1; i < toh; i++)
                {
                    hg.WriteRaw(" > " + hiearchy[i]);
                }
            }
            else
            {
                if (hiearchy.Count != 0)
                {
                    zapsano = true;
                    hg.WriteRaw(" > " + hiearchy[hiearchy.Count - 1]);
                }
            }
            if (zapsano)
            {
                hg.WriteRaw(" > ");
            }
            
            hg.WriteRaw( name);

            hg.TerminateTag("div");
        if (meta != "")
        {
            hg.WriteTagWithAttr("div", "class", "vysledekHledaniMeta");
            hg.WriteRaw(meta);
            hg.TerminateTag("div");
        }

        if (text != "")
        {
            hg.WriteTagWithAttr("div", "class", "vysledekHledaniText");
            hg.WriteRaw(text);
            hg.TerminateTag("div");
        }
        hg.TerminateTag("div");
        return hg.ToString();
    }

    public void WriteMeta(WordsCountSearchTargetInt item)
    {
        if (item.words != "")
        {
            this.meta = "Nalezeny tyto slova: " + item.words;
        }
        
    }
}
