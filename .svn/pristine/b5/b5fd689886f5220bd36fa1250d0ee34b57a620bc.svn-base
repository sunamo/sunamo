using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for InputCheckBox
/// </summary>
public class InputCheckBox : BaseControl
{
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string name = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string value = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string Checked = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onchange = null;

    public InputCheckBox()
    {
    }

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
                HtmlGenerator hg = new HtmlGenerator();
                hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "checkbox", "id", SH.ReplaceVariables( ID, _dataBinding, actualRow), "class", Class, "style", Style,
                    "checked", Checked, "onchange", onchange, "value", value, "name", name));
                return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
