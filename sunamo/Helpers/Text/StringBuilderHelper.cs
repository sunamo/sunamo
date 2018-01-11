using System.Text;
public class StringBuilderHelper
{
    readonly static string znakNadpisu = "*";
    StringBuilder sb = new StringBuilder();

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
        sb.AppendLine("When you take the input data, run the program again.");
    }

    
    #endregion

    #region Dalsi vyskyty

    /// <summary>
    /// Napíše nadpis A1 do konzole 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public  void StartRunTime(string text)
    {
        int delkaTextu = text.Length;
        string hvezdicky = "";
        hvezdicky = new string(znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
        
        sb.AppendLine();sb.AppendLine(hvezdicky);
        sb.Append(text);
        sb.AppendLine();sb.AppendLine(hvezdicky);
        
    }
    #endregion

    public  void WriteLineFormat(string text, params object[] p)
    {
        sb.AppendLine();sb.AppendLine(string.Format(text, p));
    }

    public void Format(string text, params object[] p)
    {
        sb.AppendLine(string.Format(text, p));
    }
    
    public override string ToString()
    {
 	    return sb.ToString();
    }

}
