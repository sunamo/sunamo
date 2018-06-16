using sunamo.Generators.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class TextOutputGenerator
{
    readonly static string znakNadpisu = "*";
    public TextBuilder sb = new TextBuilder();
    

    #region Main

    /// <summary>
    /// 
    /// </summary>
    public  void EndRunTime()
    {
        sb.AppendLine("Thank you for using my app. Press enter to app will be terminated.");
    }

    /// <summary>
    /// Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public  void NoData()
    {
        sb.AppendLine("When you will have the input data, run the program again.");
    }

    
    #endregion

    #region Dalsi vyskyty

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

    public  void WriteLineFormat(string text, params object[] p)
    {
        sb.AppendLine();
        sb.AppendLine(string.Format(text, p));
    }

    public void Format(string text, params object[] p)
    {
        sb.AppendLine(string.Format(text, p));
    }
    
    public override string ToString()
    {
 	    return sb.ToString();
    }

    public void Header(string v)
    {
        sb.AppendLine();
        sb.AppendLine(v);
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
                sb.AppendLine(item);
            }
            sb.AppendLine();
        }
    }

    public void List(IEnumerable<string> files1, string header)
    {
        sb.AppendLine(header);
        List(files1);
    }

    public void SingleCharLine(char paddingChar, int v)
    {
        sb.AppendLine(string.Empty.PadLeft(v, paddingChar));
    }

    internal void Undo()
    {
        
    }
}
