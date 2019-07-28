using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

public class TransUnit
{
    public string id;
    public bool translate;
    public string xml_space;
    

    string _source;

    public string source
    {
        get
        {
            return _source;
        }
        set
        {
            value = SHNotTranslateAble.DecodeSlashEncodedString(value);
            value = HtmlAssistant.TrimInnerHtml(value);
            value = HttpUtility.HtmlEncode(value);
            
            _source = value;
        }
    }

    string _target;
    public string target
    {
        get
        {
            return _target;
        }
        set
        {
            value = SHNotTranslateAble.DecodeSlashEncodedString(value);
            value = HtmlAssistant.TrimInnerHtml(value);
            value = HttpUtility.HtmlEncode(value);
            _target = value; 
        }
    }

    public const string tTransUnit = "trans-unit";

    public override string ToString()
    {
        XmlGenerator g = new XmlGenerator();
        g.WriteTagWithAttrs( tTransUnit, "id", id, "translate", BTS.BoolToString(translate, true), "xml:space", "preserve");
        g.WriteElement("source", source);

        g.WriteTagWithAttr("target", "state", "translated");
        g.WriteRaw(target);
        g.TerminateTag("target");

        g.TerminateTag(tTransUnit);

        return g.ToString();
    }
}

