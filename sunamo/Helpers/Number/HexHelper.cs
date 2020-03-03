using System;
using System.Collections.Generic;
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
    public static string ToHex(List<byte> ba)
    {
        return Utils.ToHex(ba);
    }

    /// <summary>
    /// converts from a string Hex representation to an array of bytes
    /// Převedu řetězec v hexadeximální formátu A1 na pole bytů. Pokud nebude hex formát(napříkal nebude mít sudý počet znaků), VV
    /// </summary>
    public static List<byte> FromHex(string hexEncoded)
    {
        return Utils.FromHex(hexEncoded);
    }
}