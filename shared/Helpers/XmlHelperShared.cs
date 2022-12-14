using System;
using System.Xml;

/// <summary>
/// Use System.Xml NS
/// </summary>
public static partial class XmlHelper{ 
    public static XmlNode GetAttributeWithName(XmlNode item, string p)
    {
        foreach (XmlAttribute item2 in item.Attributes)
        {
            if (item2.Name == p)
            {
                return item2;
            }
        }

        return null;
    }

    public static bool IsXml(string str)
    {
        if (!string.IsNullOrEmpty(str) && str.TrimStart().StartsWith("<"))
        {
            return true;
        }
        return false;
    }

/// <summary>
    /// Vrátí InnerXml nebo hodnotu CData podle typu uzlu
    /// </summary>
    /// <param name = "eventDescriptionNode"></param>
    public static string GetInnerXml(XmlNode eventDescriptionNode)
    {
        string eventDescription = "";
        if (eventDescriptionNode is XmlCDataSection)
        {
            XmlCDataSection cdataSection = eventDescriptionNode as XmlCDataSection;
            eventDescription = cdataSection.Value;
        }
        else
        {
            if (eventDescriptionNode != null)
            {
                eventDescription = eventDescriptionNode.InnerXml;
            }
            
        }

        return eventDescription;
    }

/// <summary>
    /// Vrátí null pokud se nepodaří nalézt
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "p"></param>
    public static XmlNode GetChildNodeWithName(XmlNode item, string p)
    {
        foreach (XmlNode item2 in item.ChildNodes)
        {
            if (item2.Name == p)
            {
                return item2;
            }
        }

        return null;
    }
}