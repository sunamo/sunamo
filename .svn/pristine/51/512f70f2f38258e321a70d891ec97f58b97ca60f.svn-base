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

	public InputText()
	{
	}

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
                HtmlGenerator hg = new HtmlGenerator();
                hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "text", "id", ID, "class", Class, "style", Style,
                    "value", value, "maxlegth", maxlength,
                    "title", title, "onchange", onchange,
                    "onkeypress", onkeypress));
                return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
