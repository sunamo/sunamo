
using System.Xml;
using System.Text;
using System;
//using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
public class XH
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static string InnerXml(string xml)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(xml);
        return xdoc.DocumentElement.InnerXml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string ReplaceSpecialHtmlEntity(string vstup)
    {
        vstup = vstup.Replace("&rsquo;", "'");//�
        vstup = vstup.Replace("&lsquo;", "'"); //�
        return vstup;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static string ReplaceAmpInString(string xml)
    {
        Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
        const string goodAmpersand = "&amp;";
        return badAmpersand.Replace(xml, goodAmpersand);
    }

    /// <summary>
    /// Do A2 se vkl�d� ji� hotov� xml, nikoliv soubor.
    /// G posledn� dite, to znamen� �e p�i parsov�n� cel�ho dokumentu vrac� root.
    /// </summary>
    /// <param name="soubor"></param>
    /// <returns></returns>
    public static XmlNode ReturnXmlRoot(string xml)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(xml);
        return (XmlNode)xdoc.LastChild;
    }

    /// <summary>
    /// Vrac� FirstChild, p�i parsaci cel�ho dokumentu tak vrac� xml deklaraci.
    /// </summary>
    /// <param name="soubor"></param>
    /// <param name="xnm"></param>
    /// <returns></returns>
    public static XmlNode ReturnXmlNode(string xml)
    {
        //XmlTextReader xtr = new XmlTextReader(
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(xml);

        //xdoc.Load(soubor);
        return (XmlNode)xdoc.FirstChild;
    }

    /// <summary>
    /// Remove illegal XML characters from a string.
    /// </summary>
    public static string SanitizeXmlString(string xml)
    {
        if (xml == null)
        {
            throw new ArgumentNullException("Atributte xml is null");
        }
        //xml = xml.Replace("&", " and ");
        StringBuilder buffer = new StringBuilder(xml.Length);

        foreach (char c in xml)
        {
            if (IsLegalXmlChar(c))
            {
                buffer.Append(c);
            }
        }

        return buffer.ToString();
    }

    public static XmlDocument xd = new XmlDocument();

    

    /// <summary>
    /// Whether a given character is allowed by XML 1.0.
    /// </summary>
    static bool IsLegalXmlChar(int character)
    {
        return
        (
             character == 0x9 /* == '\t' == 9   */          ||
             character == 0xA /* == '\n' == 10  */          ||
             character == 0xD /* == '\r' == 13  */          ||
            (character >= 0x20 && character <= 0xD7FF) ||
            (character >= 0xE000 && character <= 0xFFFD) ||
            (character >= 0x10000 && character <= 0x10FFFF)
        );
    }



    
}
