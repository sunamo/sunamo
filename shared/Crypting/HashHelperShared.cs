using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class HashHelper{ 
public static byte[] GetHash(byte[] pass, byte[] salt)
    {
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        return GetHash(joined.ToArray());
    }
    /// <summary>
    /// Pouze vypočte Hash bez soli - resp. sůl musí být v A1 společně s bajty které chci zakódovat s ní.
    /// </summary>
    /// <param name = "vstup"></param>
    /// <returns></returns>
    public static byte[] GetHash(byte[] vstup)
    {
        SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
        byte[] b = sha.ComputeHash(vstup);
        return b;
    }

/// <summary>
    /// Získá 10 náhodných bajtů jako heslo a vloží do A3
    /// Spojí A1 a A3 a získaný hash uloží do A2
    /// </summary>
    /// <param name = "pass"></param>
    /// <param name = "hash"></param>
    /// <param name = "salt"></param>
    public static void GetHashAndSalt(byte[] pass, out byte[] hash, out byte[] salt)
    {
        salt = RandomHelper.RandomBytes(10);
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        hash = GetHash(joined.ToArray());
    }
public static void GetHashAndSalt(byte[] pass, out byte[] hash, out byte[] salt, int pocetBajtuSoli)
    {
        salt = RandomHelper.RandomBytes(pocetBajtuSoli);
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        hash = GetHash(joined.ToArray());
    }
}