using System;
using System.Collections.Generic;
using System.Text;

public partial class CSharpParser{
    static Type type = typeof(CSharpParser);
    public const string c = "const string";
    public const string eq = "=";
    public const string p = "public" + " ";

    /// <summary>
    /// Directly save to file
    /// In A2 will be what can't be deleted, when will be > 0, ThrowException
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "remove"></param>
    public static void RemoveConsts(string file, List<string> remove)
    {
        var lines = TF.GetLines(file);
        //first = -1;
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            var text = lines[i].Trim();
            if (text.Contains(CSharpParser.c))
            {
                string key = SH.GetTextBetween(text, CSharpParser.c, CSharpParser.eq);
                var dx = remove.IndexOf(key);
                if (dx != -1)
                {
                    lines.RemoveAt(i);
                    remove.RemoveAt(dx);
                }
            }
        }

        TF.WriteAllLines(file, lines);
        if (remove.Count > 0)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(),type, RH.CallingMethod(), "Cant be deleted in XlfKeys: " + SH.Join(",", remove));
        }
    }
}