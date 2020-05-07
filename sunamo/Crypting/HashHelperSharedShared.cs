using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

public partial class HashHelper
{
    public static string GetHashString(string s)
    {
        var hash = GetHash( UTF8Encoding.UTF8.GetBytes(s));
        return Encoding.UTF8.GetString(hash);
    }

    /// <summary>
    /// Is used only in HostingManager
    /// 
    /// </summary>
    /// <param name="pass"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static byte[] GetHash(byte[] pass, byte[] salt)
    {
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        return GetHash(joined.ToArray());
    }
    /// <summary>
    /// Pouze vypočte Hash bez soli - resp. sůl musí být v A1 společně s bajty které chci zakódovat s ní.
    /// </summary>
    /// <param name = "vstup"></param>
    public static byte[] GetHash(byte[] vstup)
    {
        SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
        byte[] b = sha.ComputeHash(vstup);
        return b;
    }
}