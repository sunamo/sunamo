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
        var sw = new StringWriter(stringBuilder);
        var itw = new IndentedTextWriter(sw);
        // System.NullReference 'Object reference not set to an instance of an object.'
        serializer.Serialize(sw, o2[0]);
        return stringBuilder.ToString();
    }
}