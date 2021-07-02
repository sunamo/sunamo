using System;
using System.Collections.Generic;
using System.Text;


public class EmoticonsHelper
{
    public static List<string> GetAllEmotions()
    {
        Emoticons emoticons = new Emoticons();
        var fields = RH.GetFields(emoticons);

        

        List<string> result = new List<string>();

        foreach (var item in fields)
        {
            var value = item.GetValue(emoticons);
            var ts = value.ToString();

            result.AddRange(SH.SplitByWhiteSpaces(ts));
        }

        return result;
    }
}