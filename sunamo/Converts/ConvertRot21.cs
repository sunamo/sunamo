using System.Collections.Generic;
using System.Text;
public static class ConvertRot21
{
    /// <summary>
    /// In key is all chars which can occured in nick
    /// Ïn value is the same chars, just swaped
    /// </summary>
    private static List<ABT<char, char>> s_abc = new List<ABT<char, char>>();


    static ConvertRot21()
    {
        s_abc.Add(new ABT<char, char>('1', 'F'));
        s_abc.Add(new ABT<char, char>('2', 'c'));
        s_abc.Add(new ABT<char, char>('3', 'G'));
        s_abc.Add(new ABT<char, char>('4', 'D'));
        s_abc.Add(new ABT<char, char>('5', 'J'));
        s_abc.Add(new ABT<char, char>('6', 'w'));
        s_abc.Add(new ABT<char, char>('7', 'L'));
        s_abc.Add(new ABT<char, char>('8', 'Y'));
        s_abc.Add(new ABT<char, char>('9', 'W'));
        s_abc.Add(new ABT<char, char>('0', 'C'));
        s_abc.Add(new ABT<char, char>('a', 'Q'));
        s_abc.Add(new ABT<char, char>('b', 't'));
        s_abc.Add(new ABT<char, char>('c', 'd'));
        s_abc.Add(new ABT<char, char>('d', 'i'));
        s_abc.Add(new ABT<char, char>('e', '0'));
        s_abc.Add(new ABT<char, char>('f', AllChars.asterisk));
        s_abc.Add(new ABT<char, char>('g', 'T'));
        s_abc.Add(new ABT<char, char>('h', 'h'));
        s_abc.Add(new ABT<char, char>('i', '2'));
        s_abc.Add(new ABT<char, char>('j', '7'));
        s_abc.Add(new ABT<char, char>('k', 'n'));
        s_abc.Add(new ABT<char, char>('l', 'l'));
        s_abc.Add(new ABT<char, char>('m', 'p'));
        s_abc.Add(new ABT<char, char>('n', '~'));
        s_abc.Add(new ABT<char, char>('o', 'u'));
        s_abc.Add(new ABT<char, char>('p', 'g'));
        s_abc.Add(new ABT<char, char>('q', 'M'));
        s_abc.Add(new ABT<char, char>('r', 'S'));
        s_abc.Add(new ABT<char, char>('s', 'K'));
        s_abc.Add(new ABT<char, char>('t', '8'));
        s_abc.Add(new ABT<char, char>('u', 'O'));
        s_abc.Add(new ABT<char, char>('v', 'v'));
        s_abc.Add(new ABT<char, char>('w', '6'));
        s_abc.Add(new ABT<char, char>('x', 'x'));
        s_abc.Add(new ABT<char, char>('y', 'B'));
        s_abc.Add(new ABT<char, char>('z', 'm'));
        s_abc.Add(new ABT<char, char>('A', 'E'));
        s_abc.Add(new ABT<char, char>('B', 'Z'));
        s_abc.Add(new ABT<char, char>('C', 'f'));
        s_abc.Add(new ABT<char, char>('D', 'V'));
        s_abc.Add(new ABT<char, char>('E', 'a'));
        s_abc.Add(new ABT<char, char>('F', 'H'));
        s_abc.Add(new ABT<char, char>('G', '^'));
        s_abc.Add(new ABT<char, char>('H', AllChars.excl));
        s_abc.Add(new ABT<char, char>('I', '&'));
        s_abc.Add(new ABT<char, char>('J', '5'));
        s_abc.Add(new ABT<char, char>('K', '$'));
        s_abc.Add(new ABT<char, char>('L', 'N'));
        s_abc.Add(new ABT<char, char>('M', '@'));
        s_abc.Add(new ABT<char, char>('N', 's'));
        s_abc.Add(new ABT<char, char>('O', 'e'));
        s_abc.Add(new ABT<char, char>('P', 'P'));
        s_abc.Add(new ABT<char, char>('Q', 'j'));
        s_abc.Add(new ABT<char, char>('R', '9'));
        s_abc.Add(new ABT<char, char>('S', '#'));
        s_abc.Add(new ABT<char, char>('T', 'z'));
        s_abc.Add(new ABT<char, char>('U', 'U'));
        s_abc.Add(new ABT<char, char>('V', 'I'));
        s_abc.Add(new ABT<char, char>('W', 'r'));
        s_abc.Add(new ABT<char, char>('X', '4'));
        s_abc.Add(new ABT<char, char>('Y', 'k'));
        s_abc.Add(new ABT<char, char>('Z', 'y'));
        s_abc.Add(new ABT<char, char>(AllChars.excl, 'X'));
        s_abc.Add(new ABT<char, char>('@', 'q'));
        s_abc.Add(new ABT<char, char>('#', AllChars.percnt));
        s_abc.Add(new ABT<char, char>('$', '1'));
        s_abc.Add(new ABT<char, char>(AllChars.percnt, AllChars.q));
        s_abc.Add(new ABT<char, char>('^', 'b'));
        s_abc.Add(new ABT<char, char>('&', 'o'));
        s_abc.Add(new ABT<char, char>(AllChars.asterisk, AllChars.lowbar));
        s_abc.Add(new ABT<char, char>(AllChars.q, 'R'));
        s_abc.Add(new ABT<char, char>(AllChars.lowbar, '3'));
        s_abc.Add(new ABT<char, char>('~', 'A'));
    }

    public static string From(string rot12)
    {
        StringBuilder sb = new StringBuilder(rot12.Length);
        foreach (char item in rot12)
        {
            foreach (var item2 in s_abc)
            {
                if (item2.B == item)
                {
                    sb.Append(item2.A);
                }
            }
        }
        return sb.ToString();
    }

    public static string To(string s)
    {
        StringBuilder sb = new StringBuilder(s.Length);
        foreach (char item in s)
        {
            foreach (var item2 in s_abc)
            {
                if (item2.A == item)
                {
                    sb.Append(item2.B);
                }
            }
        }
        return sb.ToString();
    }
}