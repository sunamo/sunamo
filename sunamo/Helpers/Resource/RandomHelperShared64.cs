using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class RandomHelper
{
    public static List<char> vsZnaky = null;
    private static Random s_rnd = new Random();

    public static string RandomString(int delka)
    {
        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            sb.Append(RandomChar());
        }
        return sb.ToString();
    }

    

    public static char RandomChar()
    {
        return RandomElementOfCollection(vsZnaky)[0];
    }

    public static string RandomElementOfCollection(IList ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt].ToString();
    }

    /// <summary>
    /// Vr�t� ��slo mezi 0 a A1-1
    /// </summary>
    /// <param name="to"></param>
    public static int RandomInt(int to)
    {
        return s_rnd.Next(0, to);
    }

    /// <summary>
    /// upper, lower and digits
    /// </summary>
    public static List<char> vsZnakyWithoutSpecial = null;
    static RandomHelper()
    {
        vsZnaky = new List<char>(AllChars.lowerChars.Count + AllChars.numericChars.Count + AllChars.specialChars.Count + AllChars.upperChars.Count);
        vsZnaky.AddRange(AllChars.lowerChars);
        vsZnaky.AddRange(AllChars.numericChars);
        vsZnaky.AddRange(AllChars.specialChars);
        vsZnaky.AddRange(AllChars.upperChars);

        vsZnakyWithoutSpecial = new List<char>(AllChars.lowerChars.Count + AllChars.numericChars.Count + AllChars.upperChars.Count);
        vsZnakyWithoutSpecial.AddRange(AllChars.lowerChars);
        vsZnakyWithoutSpecial.AddRange(AllChars.numericChars);
        vsZnakyWithoutSpecial.AddRange(AllChars.upperChars);
    }
}
