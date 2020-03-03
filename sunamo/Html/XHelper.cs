using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using sunamo.Essential;
public partial class XHelper
{
    
    public static string GetInnerXml(XElement parent)
    {
        var reader = parent.CreateReader();
        reader.MoveToContent();
        return reader.ReadInnerXml();
    }



    public static List<XElement> GetElementsOfNameWithAttr(System.Xml.Linq.XElement xElement, string tag, string attr, string value, bool caseSensitive)
    {
        return GetElementsOfNameWithAttrWorker(xElement, tag, attr, value, false, caseSensitive);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "p"></param>
    
    public static string GetValueOfElementOfNameOrSE(XElement var, string nazev)
    {
        XElement xe = GetElementOfName(var, nazev);
        if (xe == null)
        {
            return "";
        }

        return xe.Value.Trim();
    }
}