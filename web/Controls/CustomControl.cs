using System;
/// <summary>
/// Prvně vypisuje pomocí delegáta pokud není null a když bude tak outerHtml
/// ---Zde cesta nevede, pokud ti nějaký prvek nevyhovuje, 
/// </summary>
using System.Collections.Generic;
public class CustomControl : BaseControl
{
    /// <summary>
    /// Naplňuje se pomocí metody Delegate a hodnoty proměnné DelegateArgs, která může být proměnná {x}
    /// </summary>
    public string outerHtml = null;

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        if (Delegate != null)
        {
            return CallDelegate(hg, ref outerHtml);
        }
        return RenderHtml(hg);
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        return outerHtml;
    }
}
