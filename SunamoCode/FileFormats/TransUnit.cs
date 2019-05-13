using System;
using System.Collections.Generic;
using System.Text;


public class TransUnit
{
    public string id;
    public bool translate;
    public string xml_space;
    public string source;
    public string target;

    const string tTransUnit = "trans-unit";

    public override string ToString()
    {
        XmlGenerator g = new XmlGenerator();
        g.WriteTagWithAttrs(tTransUnit, "id", id, "translate", BTS.BoolToString(translate, true), "xml:space", "preserve");
        g.WriteElement("source", source);

        g.WriteTagWithAttr("target", "state", "translated");
        g.WriteRaw(target);
        g.TerminateTag("target");

        g.TerminateTag(tTransUnit);

        return g.ToString();
    }
}

