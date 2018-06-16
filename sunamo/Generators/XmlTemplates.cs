using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for XmlTemplates
/// </summary>
public class XmlTemplates
{
    public const string xml = "<?xml version='1.0' encoding='utf-8'?>";

    public static string GetXml2(string n1, string n2)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2></sunamo>";
    }

    public static string GetXml5(string n1, string n2, string n3, string n4, string n5)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3><n4><![CDATA[" + n4 + "]]></n4><n5><![CDATA[" + n5 + "]]></n5></sunamo>";
    }

    public static string GetXml4(string n1, string n2, string n3, string n4)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3><n4><![CDATA[" + n4 + "]]></n4></sunamo>";
    }

    public static string GetXml3(string n1, string n2, string n3)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3></sunamo>";
    }

    public static string GetXml1(string n1)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1></sunamo>";
    }
}
