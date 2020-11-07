using sunamo.Data;
using System.Collections.Generic;
using System.Text;
public static class ConvertRot12
{
    /// <summary>
    /// In key is all chars which can occured in nick
    /// Ïn value is the same chars, just swaped
    /// </summary>
    private static List<ABT<char, char>> s_abc = new List<ABT<char, char>>();


    static ConvertRot12()
    {
        s_abc.Add(new ABT<char, char>('1', 'v'));
        s_abc.Add(new ABT<char, char>('2', 'f'));
        s_abc.Add(new ABT<char, char>('3', '7'));
        s_abc.Add(new ABT<char, char>('4', 'Z'));
        s_abc.Add(new ABT<char, char>('5', AllChars.dot));
        s_abc.Add(new ABT<char, char>('6', '4'));
        s_abc.Add(new ABT<char, char>('7', AllChars.lowbar));
        s_abc.Add(new ABT<char, char>('8', 'L'));
        s_abc.Add(new ABT<char, char>('9', 't'));
        s_abc.Add(new ABT<char, char>('0', '0'));
        s_abc.Add(new ABT<char, char>('a', '8'));
        s_abc.Add(new ABT<char, char>('b', 'o'));
        s_abc.Add(new ABT<char, char>('c', '9'));
        s_abc.Add(new ABT<char, char>('d', 'J'));
        s_abc.Add(new ABT<char, char>('e', '3'));
        s_abc.Add(new ABT<char, char>('f', 'x'));
        s_abc.Add(new ABT<char, char>('g', 'g'));
        s_abc.Add(new ABT<char, char>('h', 'h'));
        s_abc.Add(new ABT<char, char>('i', 'z'));
        s_abc.Add(new ABT<char, char>('j', 'S'));
        s_abc.Add(new ABT<char, char>('k', 'M'));
        s_abc.Add(new ABT<char, char>('l', 'w'));
        s_abc.Add(new ABT<char, char>('m', 'G'));
        s_abc.Add(new ABT<char, char>('n', 'u'));
        s_abc.Add(new ABT<char, char>('o', 'n'));
        s_abc.Add(new ABT<char, char>('p', 'p'));
        s_abc.Add(new ABT<char, char>('q', 'q'));
        s_abc.Add(new ABT<char, char>('r', 'E'));
        s_abc.Add(new ABT<char, char>('s', 'N'));
        s_abc.Add(new ABT<char, char>('t', 'l'));
        s_abc.Add(new ABT<char, char>('u', '6'));
        s_abc.Add(new ABT<char, char>('v', 'c'));
        s_abc.Add(new ABT<char, char>('w', 'K'));
        s_abc.Add(new ABT<char, char>('x', 'A'));
        s_abc.Add(new ABT<char, char>('y', '5'));
        s_abc.Add(new ABT<char, char>('z', 'O'));
        #region These 3 letters here could be, UH.UrlEncode and HttpUtility.HtmlEncode not encode it
        s_abc.Add(new ABT<char, char>(AllChars.lowbar, 'b'));
        s_abc.Add(new ABT<char, char>(AllChars.dot, 'm'));
        s_abc.Add(new ABT<char, char>(AllChars.dash, AllChars.dash));
        #endregion
        s_abc.Add(new ABT<char, char>('A', 'Q'));
        s_abc.Add(new ABT<char, char>('B', 'e'));
        s_abc.Add(new ABT<char, char>('C', 'y'));
        s_abc.Add(new ABT<char, char>('D', 'B'));
        s_abc.Add(new ABT<char, char>('E', 'R'));
        s_abc.Add(new ABT<char, char>('F', 'F'));
        s_abc.Add(new ABT<char, char>('G', 'i'));
        s_abc.Add(new ABT<char, char>('H', 'd'));
        s_abc.Add(new ABT<char, char>('I', 'r'));
        s_abc.Add(new ABT<char, char>('J', '2'));
        s_abc.Add(new ABT<char, char>('K', 'H'));
        s_abc.Add(new ABT<char, char>('L', 'k'));
        s_abc.Add(new ABT<char, char>('M', 'U'));
        s_abc.Add(new ABT<char, char>('N', 's'));
        s_abc.Add(new ABT<char, char>('O', 'W'));
        s_abc.Add(new ABT<char, char>('P', 'P'));
        s_abc.Add(new ABT<char, char>('Q', '1'));
        s_abc.Add(new ABT<char, char>('R', 'I'));
        s_abc.Add(new ABT<char, char>('S', 'V'));
        s_abc.Add(new ABT<char, char>('T', 'T'));
        s_abc.Add(new ABT<char, char>('U', 'j'));
        s_abc.Add(new ABT<char, char>('V', 'a'));
        s_abc.Add(new ABT<char, char>('W', 'C'));
        s_abc.Add(new ABT<char, char>('X', 'X'));
        s_abc.Add(new ABT<char, char>('Y', 'D'));
        s_abc.Add(new ABT<char, char>('Z', 'Y'));
    }

    public static string From(string rot12)
    {
        return rot12;
    }

    public static string To(string s)
    {
        return s;
    }
}