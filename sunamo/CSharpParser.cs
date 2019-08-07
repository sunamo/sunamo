using System;
using System.Collections.Generic;
using System.Text;


public class CSharpParser
{
    static Type type = typeof(CSharpParser);
    public const string c = "const string";
    public const string eq = "=";
    public const string p = "public" + " ";

    public static List<string> ParseConsts(List<string> lines, out int first)
    {
        List<string> keys = new List<string>();

        first = -1;

        for (int i = 0; i < lines.Count; i++)
        {
            var text = lines[i].Trim();

            if (text.Contains(CSharpParser.c))
            {
                if (first == -1)
                {
                    first = i;
                }

                string key = SH.GetTextBetween(text, CSharpParser.c, CSharpParser.eq);

                keys.Add(key);
            }
        }

        return keys;
    }

    /// <summary>
    /// Directly save to file
    /// In A2 will be what can't be deleted, when will be > 0, ThrowException
    /// </summary>
    /// <param name="file"></param>
    /// <param name="remove"></param>
    /// <returns></returns>
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
            ThrowExceptions.Custom(type, RH.CallingMethod(), "Cant be deleted in XlfKeys: " + SH.Join(",", remove));
        }
    }
}

