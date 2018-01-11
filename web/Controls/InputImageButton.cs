using System;
using System.Collections.Generic;
public class InputImageButton : BaseControl
{
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onclick = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string src = null;

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        string onclick2 = onclick;
        if (SH.ContainsVariable(onclick2))
        {
            onclick2 = SH.ReplaceVariables(onclick, _dataBinding, actualRow);
        }

        hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "image", "id", ID, "class", Class, "style", Style, "onclick", onclick2,
            "src", src));
        return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new System.NotImplementedException();
    }
}
