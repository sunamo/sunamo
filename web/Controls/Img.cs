using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for Img
/// </summary>
public class Img :BaseControl
{
	public Img()
	{
	}

    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string alt = null;
    /// <summary>
    /// Pokud Delegate není null, naplní právě tuto proměnnou src
    /// Pokud je Delegate null, chová se src jako statický argument
    /// </summary>
    public string src = null;
    /// <summary>
    /// Zadává se pouze číslem bez px
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string height = null;
    /// <summary>
    /// Zadává se pouze číslem bez px
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string width = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onclick = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string href = null;
    
    /// <summary>
    /// Vygeneruje atribut img, možno i s obalovacím tagem -a- pokud je vyplněn atribut href
    /// </summary>
    /// <returns></returns>
    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        if (href != null)
        {
            hg.WriteTagWithAttr("a", "href", href);
        }
        if (Delegate != null)
        {
            return CallDelegate(hg, ref src);
        }
        return RenderHtml(hg);
    }

   

    protected override string RenderHtml(HtmlGenerator hg)
    {
        hg.WriteNonPairTagWithAttrs("img", BTS.GetOnlyNonNullValues( "id", ID, "class", Class, "style", Style,
        "alt", alt, "src", src,
        "height", height, "width", width,
        "onclick", onclick));

        if (href != null)
        {
            hg.TerminateTag("a");
        }
        return hg.ToString();
    }
}
