using System;
using System.Xml;

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