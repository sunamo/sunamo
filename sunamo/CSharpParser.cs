using System;
using System.Collections.Generic;
using System.Text;
public partial class CSharpParser
{
    
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

                string key = XmlLocalisationInterchangeFileFormatSunamo.GetConstsFromLine(text);
                keys.Add(key);
            }
        }

        return keys;
    }

    public static List<string> ParseConstsAllLines(List<string> lines)
    {
        List<string> keys = new List<string>();
        //first = -1;
        for (int i = 0; i < lines.Count; i++)
        {
            var text = lines[i].Trim();
            //if (text.Contains(CSharpParser.c))
            //{
                //if (first == -1)
                //{
                //    first = i;
                //}

                string key = XmlLocalisationInterchangeFileFormatSunamo.GetConstsFromLine(text);
                keys.Add(key);
            //}
        }

        return keys;
    }

}