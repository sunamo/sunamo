using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace shared.Crypting
{
    /// <summary>
    /// Friend class for shared utility methods used by multiple Encryption classes
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// converts an array of bytes to a string Hex representation
        /// P�evedu pole byt� A1 na hexadecim�ln� �et�zec.
        /// </summary>
        static public string ToHex(byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return "";
            }
            const string HexFormat = "{0:X2}";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(string.Format(HexFormat, b));
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts from a string Hex representation to an array of bytes
        /// P�evedu �et�zec v hexadexim�ln� form�tu A1 na pole byt�. Pokud nebude hex form�t(nap��kal nebude m�t sud� po�et znak�), VV
        /// </summary>
        static public byte[] FromHex(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                int l = Convert.ToInt32(hexEncoded.Length / 2);
                byte[] b = new byte[l];
                for (int i = 0; i <= l - 1; i++)
                {
                    b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from a string Base64 representation to an array of bytes
        /// pokud je A1 null/L0, GN. Jinak se pokus�m p�ev�st na pole byt�-pokud A1 nebbude Base64 �et�zec, VV
        /// </summary>
        static public byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0)
            {
                return null;
            }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from an array of bytes to a string Base64 representation
        /// Pokud A1 null nebo L0, G SE. Jinak mi p�evede na Base64
        /// </summary>
        static public string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return "";
            }
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// retrieve an element from an XML string
        /// V XML A1 najde p�rov� prvek A2 a vr�t� jeho obsah. Pokud nenajde, VV.
        /// </summary>
        static public string GetXmlElement(string xml, string element)
        {
            Match m = null;
            m = Regex.Match(xml, "<" + element + ">(?<Element>[^>]*)</" + element + ">", RegexOptions.IgnoreCase);
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
        static public string GetConfigString(string key, bool isRequired)
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
        /// Vr�t� mi �et�zec <add key=\"A1\" value=\"A2\" />
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string WriteConfigKey(string key, string value)
        {
            string s = "<add key=\"{0}\" value=\"{1}\" />" + Environment.NewLine;
            return string.Format(s, key, value);
        }

        /// <summary>
        /// G element A1 s hodnotou A2
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string WriteXmlElement(string element, string value)
        {
            string s = "<{0}>{1}</{0}>" + Environment.NewLine;
            return string.Format(s, element, value);
        }

        /// <summary>
        /// Pokud A2, vr�t� mi ukon. tag A1, jinak po�. tag A1.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="isClosing"></param>
        /// <returns></returns>
        static public string WriteXmlNode(string element, bool isClosing)
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
            return string.Format(s, element);
        }

    }
}
