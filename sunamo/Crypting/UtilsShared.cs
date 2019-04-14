﻿using System;
using System.Text;

public partial class Utils{
    /// <summary>
    /// converts an array of bytes to a string Hex representation
    /// P�evedu pole byt� A1 na hexadecim�ln� �et�zec.
    /// </summary>
    public static string ToHex(byte[] ba)
    {
        if (ba == null || ba.Length == 0)
        {
            return "";
        }

        const string HexFormat = "{0:X2}";
        StringBuilder sb = new StringBuilder();
        foreach (byte b in ba)
        {
            sb.Append(SH.Format2(HexFormat, b));
        }

        return sb.ToString();
    }

    /// <summary>
    /// converts from a string Base64 representation to an array of bytes
    /// pokud je A1 null/L0, GN. Jinak se pokus�m p�ev�st na pole byt�-pokud A1 nebbude Base64 �et�zec, VV
    /// </summary>
    public static byte[] FromBase64(string base64Encoded)
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
    public static string ToBase64(byte[] b)
    {
        if (b == null || b.Length == 0)
        {
            return "";
        }

        return Convert.ToBase64String(b);
    }

    /// <summary>
    /// converts from a string Hex representation to an array of bytes
    /// P�evedu �et�zec v hexadexim�ln� form�tu A1 na pole byt�. Pokud nebude hex form�t(nap��kal nebude m�t sud� po�et znak�), VV
    /// </summary>
    public static byte[] FromHex(string hexEncoded)
    {
        if (hexEncoded == null || hexEncoded.Length == 0)
        {
            return null;
        }

        try
        {
            hexEncoded = hexEncoded.TrimStart('#');
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