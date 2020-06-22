using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using desktop;

public class XamlGeneratorDesktop //: XamlGenerator
{
    static Type type = typeof(XamlGeneratorDesktop);

    

    public T GetControl<T>(XmlGenerator x)
    {
        string vr = x.sb.ToString();
        vr = SH.ReplaceFirstOccurences(vr, ">", " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
        var vrR = (T)XamlReader.Parse(vr);
        return vrR;
    }
}