﻿using System.Collections.Generic;
using System.Text;

public partial class HtmlHelperText{ 
public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag  + " ", "<" + forWhat+ " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }

public static string InsertMissingEndingTags(string s, string tag)
    {
       

        StringBuilder text = new StringBuilder(s);


        var start = SH.ReturnOccurencesOfString(s, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(s, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            Dictionary<int, int> se = new Dictionary<int, int>();

            for (int i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0)
                {
                    endDx = ends.Count - 1;
                }
                var endActual = -1;
                if (endDx != -1)
                {
                    endActual = ends[endDx];
                }
                if (startActual > endActual)
                {
                    se.Add(startActual, -1);
                }
                else
                {
                    se.Add(startActual, endActual);
                    ends.RemoveAt(endDx);
                }
            }

            foreach (var item in se)
            {
                if (item.Value == -1)
                {
                    var dexEndOfStart = s.IndexOf(AllChars.gt, item.Key);

                    var space = s.IndexOf(AllChars.space, dexEndOfStart);

                    if (space != -1)
                    {

                        text.Insert(space, endingTag);
                    }
                }
            }
        }

        return text.ToString();
    }
}