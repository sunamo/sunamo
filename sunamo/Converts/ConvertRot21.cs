using System.Collections.Generic;
using System.Text;
public static class ConvertRot21
{
    /// <summary>
    /// V klíči jsou všechny znaky které se mohou vyskytovat ve přezdívce
    /// V hodnotě jsou ty samé znaky, akorát zpřeházené
    /// </summary>
    static List<ABT<char, char>> abc = new List<ABT<char, char>>();


    static ConvertRot21()
    {
        abc.Add(new ABT<char, char>('1', 'F'));
        abc.Add(new ABT<char, char>('2', 'c'));
        abc.Add(new ABT<char, char>('3', 'G'));
        abc.Add(new ABT<char, char>('4', 'D'));
        abc.Add(new ABT<char, char>('5', 'J'));
        abc.Add(new ABT<char, char>('6', 'w'));
        abc.Add(new ABT<char, char>('7', 'L'));
        abc.Add(new ABT<char, char>('8', 'Y'));
        abc.Add(new ABT<char, char>('9', 'W'));
        abc.Add(new ABT<char, char>('0', 'C'));
        abc.Add(new ABT<char, char>('a', 'Q'));
        abc.Add(new ABT<char, char>('b', 't'));
        abc.Add(new ABT<char, char>('c', 'd'));
        abc.Add(new ABT<char, char>('d', 'i'));
        abc.Add(new ABT<char, char>('e', '0'));
        abc.Add(new ABT<char, char>('f', '*'));
        abc.Add(new ABT<char, char>('g', 'T'));
        abc.Add(new ABT<char, char>('h', 'h'));
        abc.Add(new ABT<char, char>('i', '2'));
        abc.Add(new ABT<char, char>('j', '7'));
        abc.Add(new ABT<char, char>('k', 'n'));
        abc.Add(new ABT<char, char>('l', 'l'));
        abc.Add(new ABT<char, char>('m', 'p'));
        abc.Add(new ABT<char, char>('n', '~'));
        abc.Add(new ABT<char, char>('o', 'u'));
        abc.Add(new ABT<char, char>('p', 'g'));
        abc.Add(new ABT<char, char>('q', 'M'));
        abc.Add(new ABT<char, char>('r', 'S'));
        abc.Add(new ABT<char, char>('s', 'K'));
        abc.Add(new ABT<char, char>('t', '8'));
        abc.Add(new ABT<char, char>('u', 'O'));
        abc.Add(new ABT<char, char>('v', 'v'));
        abc.Add(new ABT<char, char>('w', '6'));
        abc.Add(new ABT<char, char>('x', 'x'));
        abc.Add(new ABT<char, char>('y', 'B'));
        abc.Add(new ABT<char, char>('z', 'm'));
        abc.Add(new ABT<char, char>('A', 'E'));
        abc.Add(new ABT<char, char>('B', 'Z'));
        abc.Add(new ABT<char, char>('C', 'f'));
        abc.Add(new ABT<char, char>('D', 'V'));
        abc.Add(new ABT<char, char>('E', 'a'));
        abc.Add(new ABT<char, char>('F', 'H'));
        abc.Add(new ABT<char, char>('G', '^'));
        abc.Add(new ABT<char, char>('H', '!'));
        abc.Add(new ABT<char, char>('I', '&'));
        abc.Add(new ABT<char, char>('J', '5'));
        abc.Add(new ABT<char, char>('K', '$'));
        abc.Add(new ABT<char, char>('L', 'N'));
        abc.Add(new ABT<char, char>('M', '@'));
        abc.Add(new ABT<char, char>('N', 's'));
        abc.Add(new ABT<char, char>('O', 'e'));
        abc.Add(new ABT<char, char>('P', 'P'));
        abc.Add(new ABT<char, char>('Q', 'j'));
        abc.Add(new ABT<char, char>('R', '9'));
        abc.Add(new ABT<char, char>('S', '#'));
        abc.Add(new ABT<char, char>('T', 'z'));
        abc.Add(new ABT<char, char>('U', 'U'));
        abc.Add(new ABT<char, char>('V', 'I'));
        abc.Add(new ABT<char, char>('W', 'r'));
        abc.Add(new ABT<char, char>('X', '4'));
        abc.Add(new ABT<char, char>('Y', 'k'));
        abc.Add(new ABT<char, char>('Z', 'y'));
        abc.Add(new ABT<char, char>('!', 'X'));
        abc.Add(new ABT<char, char>('@', 'q'));
        abc.Add(new ABT<char, char>('#', '%'));
        abc.Add(new ABT<char, char>('$', '1'));
        abc.Add(new ABT<char, char>('%', '?'));
        abc.Add(new ABT<char, char>('^', 'b'));
        abc.Add(new ABT<char, char>('&', 'o'));
        abc.Add(new ABT<char, char>('*', '_'));
        abc.Add(new ABT<char, char>('?', 'R'));
        abc.Add(new ABT<char, char>('_', '3'));
        abc.Add(new ABT<char, char>('~', 'A'));




    }

    public static string From(string rot12)
    {
        StringBuilder sb = new StringBuilder(rot12.Length);
        foreach (char item in rot12)
        {
            foreach (var item2 in abc)
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
            foreach (var item2 in abc)
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
