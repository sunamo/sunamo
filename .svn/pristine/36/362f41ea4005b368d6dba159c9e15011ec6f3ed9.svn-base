using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
public class Anchor : BaseControl
{
    /// <summary>
    /// Fixní proměnná která když bude nastavená, se přidá ke všem anchorům v sloupci
    /// </summary>
    public string onclick = null;
    /// <summary>
    /// Pokud Delegate nebude null, naplní tuto proměnnou
    /// Může se do ní zadat i mnohonásobná proměnná
    /// </summary>
    public string href = null;
    string hrefRender = null;
    /// <summary>
    /// Do tohoto se uloží z delegátu DelegateInnerHtml
    /// </summary>
    public string innerHtml = null;
    /// <summary>
    /// Metoda do které bude předán argument DelegateInnerHtmlArgs
    /// </summary>
    public StringStringHandler DelegateInnerHtml = null;
    /// <summary>
    /// Jediný argument který lze předat do DelegateInnerHtml. 
    /// Pokud chceš více formátů, musíš je naformátovat do jednoho a rozparsovat si je pak v DelegateInnerHtml
    /// Můžeš zde používat odkazy např. {0}
    /// </summary>
    public string DelegateInnerHtmlArgs = null;



    public override string Render(int actualRow, List<String[]> _dataBinding)
    {
        this.actualRow = actualRow;
        HtmlGenerator hg = new HtmlGenerator();
        if (Delegate != null || DelegateInnerHtml != null)
        {
            if (actualRow != -1)
            {
                if (Delegate != null)
                {
                    hrefRender = Delegate.Invoke(_dataBinding[int.Parse(DelegateArgs.TrimStart('{').TrimEnd('}'))][actualRow]);
                    if (hrefRender == null)
                    {
                        return "";
                    }
                }
                else if (DelegateInnerHtml != null)
                {
                    innerHtml = DelegateInnerHtml.Invoke(_dataBinding[int.Parse(DelegateInnerHtmlArgs.TrimStart('{').TrimEnd('}'))][actualRow]);
                }
                //return Render(hg);
            }
            else
            {
                
                //repeteDuringRow = true;
                return Render(hg);
            }
        }
        if (SH.ContainsVariable(href))
        {
            hrefRender = SH.ReplaceVariables(href, _dataBinding, actualRow);
        }
        
        return Render(hg);
    }

    private string Render(HtmlGenerator hg)
    {
        hg.WriteTagWithAttrs("a", BTS.GetOnlyNonNullValues( "id", ID, "class", Class, "style", Style, "href", hrefRender,
        "onclick", onclick));
        if (innerHtml != null)
        {
            if (innerHtml.IndexOf(BaseControl.p) != -1 && innerHtml.IndexOf(BaseControl.k) != -1)
            {
                if (SH.ContainsVariable(innerHtml))
                {
                    //Anchor aa = this;
                    hg.WriteRaw(SH.ReplaceVariables( innerHtml, _dataBinding, actualRow));
                }
                else
                {
                    hg.WriteRaw(innerHtml);
                }
            }
            else
            {
                hg.WriteRaw(innerHtml);
            }
            
        }

        hg.TerminateTag("a");

        return hg.ToString();
    }

    

    protected override string RenderHtml(HtmlGenerator hg)
    {
        throw new System.NotImplementedException();
    }
}
