using System;

public partial class Utils{ 
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