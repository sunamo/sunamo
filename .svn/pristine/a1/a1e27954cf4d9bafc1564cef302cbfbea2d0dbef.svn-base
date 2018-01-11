using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for InputButton
/// </summary>
public class InputButton : BaseControl
{
	public InputButton()
	{
	}

    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onclick = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string value = null;


    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "button", "id", ID, "class", Class, "style", Style,
            "value", value, "onclick", onclick));
        return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
