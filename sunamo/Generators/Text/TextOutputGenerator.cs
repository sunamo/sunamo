using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// Similar: InstantSB,TextBuilder,HtmlSB
/// </summary>
public class TextOutputGenerator
{
    readonly static string znakNadpisu = "*";
    public TextBuilder sb = new TextBuilder();
    public string prependEveryNoWhite
    {
        get => sb.prependEveryNoWhite;
        set => sb.prependEveryNoWhite = value;
    }

    #region Static texts
    /// <summary>
    /// 
    /// </summary>
    public void EndRunTime()
    {
        sb.AppendLine(Messages.AppWillBeTerminated);
    }

    /// <summary>
    /// Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public  void NoData()
    {
        sb.AppendLine(Messages.NoData);
    }
    #endregion

    #region Templates

    /// <summary>
    /// Napíše nadpis A1 do konzole 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public void StartRunTime(string text)
    {
        int delkaTextu = text.Length;
        string hvezdicky = "";
        hvezdicky = new string(znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
        
    }
    #endregion

    public  void AppendLineFormat(string text, params object[] p)
    {
        sb.AppendLine();
        
        AppendLine(string.Format(text, p));
    }

    public void AppendFormat(string text, params object[] p)
    {
        AppendLine(string.Format(text, p));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        sb.AppendLine(text);
    }
    
    public override string ToString()
    {
 	    return sb.ToString();
    }

    public void Header(string v)
    {
        sb.AppendLine();
        AppendLine(v);
        sb.AppendLine();
    }

    public void List(IEnumerable<string> files1)
    {
        if (files1.Count() == 0)
        {
            sb.AppendLine();
        }
        else
        {
            foreach (var item in files1)
            {
                AppendLine(item);
            }
            sb.AppendLine();
        }
    }

    public void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        string text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// For ordinary text use Append*
    /// </summary>
    /// <param name="text"></param>
    /// <param name="header"></param>
    public void Paragraph(string text, string header)
    {
        
        if (text != string.Empty)
        {
            sb.AppendLine(header + AllStrings.colon);
            sb.AppendLine(text);
            sb.AppendLine();
        }
    }

    public void List(IEnumerable<string> files1, string header)
    {
        List(files1, header, true, false);
    }

    public void List(IEnumerable<string> files1, string header, bool headerWrappedEmptyLines, bool insertCount)
    {
        if (insertCount)
        {
            header += " (" + files1.Count() + ")";
        }
        if (headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        sb.AppendLine(header + AllStrings.colon);
        if (headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        List(files1);
    }



    public void SingleCharLine(char paddingChar, int v)
    {
        sb.AppendLine(string.Empty.PadLeft(v, paddingChar));
    }

    public void Undo()
    {
        sb.Undo();
    }

    
}
