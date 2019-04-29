
using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Friend public class for shared utility methods used by multiple Encryption classes
/// </summary>
public partial class UtilsNonNetStandard
{
    

    /// <summary>
    /// retrieve an element from an XML string
    /// V XML A1 najde p�rov� prvek A2 a vr�t� jeho obsah. Pokud nenajde, VV.
    /// </summary>
    public static string GetXmlElement(string xml, string element)
    {
        Match m = null;
        m = Regex.Match(xml, AllStrings.lt + element + ">(?<Element>[^>]*)</" + element + AllStrings.gt, RegexOptions.IgnoreCase);
        if (m == null)
        {
            throw new Exception("Could not find <" + element + "></" + element + "> in provided Public Key XML.");
        }

        return m.Groups["Element"].ToString();
    }

    /// <summary>
    /// Returns the specified string value from the application .config file
    /// G �et�zec z ConfigurationManager.AppSettings kl��e A1. Pokud se nepoda�� z�skat a A2, VV
    /// </summary>
    public static string GetConfigString(string key, bool isRequired)
    {
        string s = Convert.ToString(ConfigurationManager.AppSettings.Get(key));
        if (s == null)
        {
            if (isRequired)
            {
                throw new ConfigurationErrorsException("key <" + key + "> is missing from .config file");
            }
            else
            {
                return "";
            }
        }
        else
        {
            return s;
        }
    }

    /// <summary>
    /// Vr�t� mi �et�zec <add key =  \ " A1 \ " value  =  \ " A2 \ "/>
    /// </summary>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    /// <returns></returns>
    public static string WriteConfigKey(string key, string value)
    {
        string s = "<add key=\"{0}\" value=\"{1}\" />" + Environment.NewLine;
        return SH.Format2(s, key, value);
    }

    /// <summary>
    /// G element A1 s hodnotou A2
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "value"></param>
    /// <returns></returns>
    public static string WriteXmlElement(string element, string value)
    {
        string s = "<{0}>{1}</{0}>" + Environment.NewLine;
        return SH.Format2(s, element, value);
    }

    /// <summary>
    /// Pokud A2, vr�t� mi ukon. tag A1, jinak po�. tag A1.
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "isClosing"></param>
    /// <returns></returns>
    public static string WriteXmlNode(string element, bool isClosing)
    {
        string s = null;
        if (isClosing)
        {
            s = "</{0}>" + Environment.NewLine;
        }
        else
        {
            s = "<{0}>" + Environment.NewLine;
        }

        return SH.Format2(s, element);
    }
}