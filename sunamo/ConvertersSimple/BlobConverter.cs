using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sunamo
{
    public class BlobConverter : ISimpleConverter<string, byte[]>
    {

        public string ConvertTo(byte[] ba)
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
            return "X'" + sb.ToString() + "'";
        }

        public byte[] ConvertFrom(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                hexEncoded = hexEncoded.Replace("X'", "").TrimEnd('\''); ;

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
                if (AppLangHelper.currentUICulture.TwoLetterISOLanguageName == "cs")
                {
                    throw new FormatException("Zadaný řetězec se nezdá být šestnáctkově kódováný:");
                }
                else
                {
                    throw new FormatException("The provided string does not appear to be Hex encoded:" + hexEncoded, ex);
                }
            }
        }
    }
}
