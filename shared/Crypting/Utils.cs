
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
    /// V XML A1 najde parove prvek A2 a vrata jeho obsah. Pokud nenajde, VV.
    /// </summary>
    public static string GetXmlElement(string xml, string element)
    {
        Match m = null;
        m = Regex.Match(xml, AllStrings.lt + element + ">(?<Element>[^>]*)</" + element + AllStrings.gt, RegexOptions.IgnoreCase);
        if (m == null)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.CouldNotFind) + " " + element + "></" + element + "  " + sess.i18n(XlfKeys.inProvidedPublicKeyXML) + ".");
        }

        return m.Groups["Element"].ToString();
    }

    /// <summary>
    /// Returns the specified string value from the application .config file
    /// G retezec z ConfigurationManager.AppSettings klice A1. Pokud se nepodari ziskat a A2, VV
    /// </summary>
    public static string GetConfigString(string key, bool isRequired)
    {
        string s = Convert.ToString(ConfigurationManager.AppSettings.Get(key));
        if (s == null)
        {
            if (isRequired)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"key " + key + "  is missing from .config file");
                return string.Empty;
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

    static Type type = typeof(Utils);

    /// <summary>
    /// Vrati mi retezec <add key =  \ " A1 \ " value  =  \ " A2 \ "/>
    /// </summary>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
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
    public static string WriteXmlElement(string element, string value)
    {
        string s = "<{0}>{1}</{0}>" + Environment.NewLine;
        return SH.Format2(s, element, value);
    }

    /// <summary>
    /// Pokud A2, vrati mi ukon. tag A1, jinak poc. tag A1.
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "isClosing"></param>
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