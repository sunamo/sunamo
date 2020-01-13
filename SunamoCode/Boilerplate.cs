using System;
using System.Collections.Generic;
using System.Text;


public class Boilerplate
{
    public static string CSharpCmd(string innerMain)
    {
        var c = @"using System;

namespace ConsoleStandardApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            {0}
        }
    }
}";

        StringBuilder sb = new StringBuilder();

        try
        {
            sb.AppendLine(SH.Format4(c, innerMain));

        }
        catch (Exception)
        {
        }

        try
        {
            sb.AppendLine(SH.Format3(c, innerMain));

        }
        catch (Exception)
        {
        }

        return sb.ToString();
    }
}

