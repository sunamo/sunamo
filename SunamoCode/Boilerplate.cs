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
        static void Main(String[] args)
        {
            {0}
        }
    }
}";

        string formatted = null;

        StringBuilder sb = new StringBuilder();

        

        sb.AppendLine(formatted);


        return sb.ToString();
    }
    public static string CSharpClass(string addNamespacesLines, string className, string fields, string contentOfInitMethod)
    {
        var c = @"using System;
{0}


    public class {1}
    {
        {2}

        public static void Init()
        {
            {3}
        }
    }";

        StringBuilder sb = new StringBuilder();

        try
        {
            sb.AppendLine(SH.Format4(c, addNamespacesLines, className, fields, contentOfInitMethod));
        }
        catch (Exception ex)
        {
        }

        try
        {
            sb.AppendLine(SH.Format3(c, addNamespacesLines, className, fields, contentOfInitMethod));

        }
        catch (Exception ex)
        {
        }

        return sb.ToString();
    }
}