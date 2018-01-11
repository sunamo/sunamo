using System;
using System.Text;

public static class HexHelper
{
    /// <summary>
    /// Musí se zadat bez znaků jako # atd. a všechny znaky musí být lowercase
    /// </summary>
    public static bool IsInHexFormat(string r)
    {
        foreach (var item in r)
        {
            if (!"0123456789abcdef".Contains(item.ToString()))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// converts an array of bytes to a string Hex representation
    /// Převedu pole bytů A1 na hexadecimální řetězec.
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
    /// Převedu řetězec v hexadeximální formátu A1 na pole bytů. Pokud nebude hex formát(napříkal nebude mít sudý počet znaků), VV
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
}
