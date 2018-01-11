
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
/// <summary>
/// Pokud se zde pracuje s řetězci, jsou v kódování UTF8
/// Je to jednosměrné, může se používat pouze při přihlašování, kdy uživatel zadává password a sůl mám uloženou
/// </summary>
public class HashHelper
{
    /// <summary>
    /// Zřetězím A3+A2, vytvořím Hash a porovnám s A1
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="salt"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public static bool PairHashAndPassword(byte[] hash, byte[] salt, byte[] pass)
    {
        byte[] hash2 = GetHash(CA.JoinBytesArray(pass, salt).ToArray());
        if (hash == hash2)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Získá 10 náhodných bajtů jako heslo a vloží do A3
    /// Spojí A1 a A3 a získaný hash uloží do A2
    /// </summary>
    /// <param name="pass"></param>
    /// <param name="hash"></param>
    /// <param name="salt"></param>
    public static void GetHashAndSalt(byte[] pass, out byte[] hash, out byte[] salt)
    {
        salt = RandomHelper.RandomBytes(10) ;
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        hash = GetHash(joined.ToArray());
    }

    public static void GetHashAndSalt(byte[] pass, out byte[] hash, out byte[] salt, int pocetBajtuSoli)
    {
        salt = RandomHelper.RandomBytes(pocetBajtuSoli);
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        hash = GetHash(joined.ToArray());
    }

    public static string GetMd5Hash(string text)
    {
        return GetMd5Hash(text, Encoding.UTF8);
    }

    public static string GetMd5Hash(string text, Encoding e)
    {
        MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider();
    //http://www.gravatar.com/avatar/c9b424b73b969e217693c401a40db390.png
        byte[] data = hash.ComputeHash(e.GetBytes( text));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();  // Return the hexadecimal string. 
        
    }

    public static byte[] GetHash(byte[] pass,  byte[] salt)
    {
        List<byte> joined = CA.JoinBytesArray(pass, salt);
        return GetHash(joined.ToArray());
    }

    /// <summary>
    /// Pouze vypočte Hash bez soli - resp. sůl musí být v A1 společně s bajty které chci zakódovat s ní.
    /// </summary>
    /// <param name="vstup"></param>
    /// <returns></returns>
    public static byte[] GetHash(byte[] vstup)
    {
        SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
        byte[] b = sha.ComputeHash(vstup);
        return b;
    }
}
