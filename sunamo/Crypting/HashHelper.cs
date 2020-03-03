
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
/// <summary>
/// Pokud se zde pracuje s řetězci, jsou v kódování UTF8
/// Je to jednosměrné, může se používat pouze při přihlašování, kdy uživatel zadává password a sůl mám uloženou
/// </summary>
public partial class HashHelper
{
    /// <summary>
    /// Zřetězím A3+A2, vytvořím Hash a porovnám s A1
    /// </summary>
    /// <param name = "hash"></param>
    /// <param name = "salt"></param>
    /// <param name = "pass"></param>
    public static bool PairHashAndPassword(byte[] hash, byte[] salt, byte[] pass)
    {
        byte[] hash2 = GetHash(CA.JoinBytesArray(pass, salt).ToArray());
        if (hash == hash2)
        {
            return true;
        }

        return false;
    }
}