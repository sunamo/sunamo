using System;
using System.Xml;
public static class XmlHelper
{
    
    /// <summary>
    /// Vrátí InnerXml nebo hodnotu CData podle typu uzlu
    /// </summary>
    /// <param name="eventDescriptionNode"></param>
    /// <returns></returns>
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
            eventDescription = eventDescriptionNode.InnerXml;
        }

        return eventDescription;
    }

    /// <summary>
    /// Vrátí null pokud se nepodaří nalézt
    /// </summary>
    /// <param name="item"></param>
    /// <param name="p"></param>
    /// <returns></returns>
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
}
