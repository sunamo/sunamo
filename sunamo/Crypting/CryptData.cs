using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for CryptData
/// </summary>
public class CryptData
{
    public static CryptHelper ci = new CryptHelper(Provider.Rijndael, s4, ivRijn, pp);
    /// <summary>
    /// Použít pouze pro hashovací, nikoliv šifrovací funkce
    /// </summary>
    public static List<byte> s2 = new List<byte> { 164, 145 };
    /// <summary>
    /// Použít pouze pro hashovací, nikoliv šifrovací funkce
    /// </summary>
    public static List<byte> s4 = new List<byte> { 164, 145, 24, 189 };
    public static List<byte> s8 = new List<byte> { 164, 145, 24, 189, 37, 86, 236, 54 };
    public static List<byte> s16 = CA.ToList<byte>(7, 210, 229, 235, 85, 133, 12, 208, 89, 168, 141, 229, 244, 27, 189, 192 );
    public static List<byte> s32 = CA.ToList<byte>(72, 94, 143, 120, 144, 136, 164, 247, 177, 71, 246, 73, 138, 96, 136, 17, 170, 87, 222, 131, 23, 21, 30, 82, 69, 143, 247, 150, 170, 17, 54, 43 );
    /// <summary>
    /// 128b = 16B
    /// </summary>
    public static List<byte> ivRijn = CA.ToList<byte>(3, 255, 83, 66, 241, 237, 129, 212, 238, 227, 101, 71, 13, 112, 248, 75 );
    public static List<byte> ivRc2 = CA.ToList<byte>(2, 121, 254, 123, 28, 199, 48, 112 );
    public static List<byte> ivTrip = CA.ToList<byte>(45, 176, 79, 111, 207, 173, 195, 182 );
    public const string pp = "olsehpwheslo";

    public CryptData()
    {
    }
}
