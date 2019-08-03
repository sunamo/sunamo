using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

public partial class YamlHelper
{
    public static string DumpAsYaml(object o)
    {
        var stringBuilder = new StringBuilder();
        var serializer = new Serializer();
        List<object> o2 = new List<object>(1);
        o2.Add(o);
        serializer.Serialize(new IndentedTextWriter(new StringWriter(stringBuilder)), o2);
        return stringBuilder.ToString();
    }
}