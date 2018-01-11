using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for Span
/// </summary>
public class Span : BaseControl
{
    /// <summary>
    /// Variabilní řetězec(ačkoliv Span nemá DelegateInnerHtml).
    /// </summary>
    public string innerHtml = null;

	public Span()
	{
	}

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttrs("span", BTS.GetOnlyNonNullValues( "id", ID, "class", Class, "style", Style));

        hg.WriteRaw(SH.ReplaceVariables( innerHtml, _dataBinding, actualRow));
        hg.TerminateTag("span");
        return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
