using System;
using System.Text;
using System.Collections.Generic;

public class HtmlGenerator : XmlGenerator
{
    public void WriteBr()
    {
        base.WriteNonPairTag("br");
    }

    public void Boilerplate()
    {
    }

    public void WriteNonPairTagWithAttrs(object hr, object style, string v1, string c, string v2)
    {
        throw new NotImplementedException();
    }
}
