using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for InputText
/// </summary>
public class InputText : BaseControl
{
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string value = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string maxlength = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string title = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onchange = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onkeypress = null;
    public string runat;
    

    public InputText()
	{
        dataRole = HtmlAttrValue.text;
	}

    /// <summary>
    /// A1 only if generate mass row 
    /// A2 NSN
    /// </summary>
    /// <param name="actualRow"></param>
    /// <param name="_dataBinding"></param>
    /// <returns></returns>
    public override string Render(int actualRow, List<String[]> _dataBinding = null)
    {
        this.actualRow = actualRow;
                HtmlGenerator hg = new HtmlGenerator();
                hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "text", "id", ID, "class", Class, "style", Style,
                    "value", value, "maxlegth", maxlength,
                    "title", title, "onchange", onchange,
                    "onkeypress", onkeypress, HtmlAttrs.runat, runat, HtmlAttrs.dataRole, dataRole));
                return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
