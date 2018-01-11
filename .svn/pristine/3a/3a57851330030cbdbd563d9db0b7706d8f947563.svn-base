using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for InputFile
/// </summary>
public class InputFile : BaseControl
{
	public InputFile()
	{
	}


    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string accept = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string size = null;
    /// <summary>
    /// Fixní řetězec(metoda nemá Delegate který by ho naplnil), přidá do výstupního HTML pokud bude nastaven
    /// </summary>
    public string onchange = null;

    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();

        
        hg.WriteNonPairTagWithAttrs("input", BTS.GetOnlyNonNullValues( "type", "file", "id", ID, "class", Class, "style", Style,
            "accept", accept, "size", size, "onchange", onchange));
        return hg.ToString();
    }

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new NotImplementedException();
    }
}
