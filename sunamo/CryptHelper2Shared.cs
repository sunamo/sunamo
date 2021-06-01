using System.Diagnostics;
using System.Text;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class CryptHelper2
{
    private const int velikostKliceAsym = 1024;
    /// <summary>
    /// Před použitím jednoduchých metod musíš nastavit tuto proměnnou
    /// </summary>
    public static List<byte> _s16 = null;
    public static string _pp = null;
    public static List<byte> _ivRijn = null;
    public static List<byte> _ivRc2 = null;
    public static List<byte> _ivTrip = null;
    public static List<byte> EncryptTripleDES(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes.ToArray(), initVectorBytes.ToArray());
        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();
        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes.ToArray(), 0, plainTextBytes.Count);
        // Finish encrypting.
        cryptoStream.FlushFinalBlock();
        // Convert our encrypted data from a memory stream into a byte array.
        List<byte> cipherTextBytes = memoryStream.ToArray().ToList();
        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();
        return cipherTextBytes;
    }
    public static List<byte> EncryptTripleDES(List<byte> plainTextBytes)
    {
        return EncryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }
    public static string EncryptTripleDES(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptTripleDES(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static List<byte> DecryptTripleDES(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes.ToArray(), initVectorBytes.ToArray());
        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());
        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        List<byte> plainTextBytes = new List<byte>(cipherTextBytes.Count);
        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes.ToArray(), 0, plainTextBytes.Count);
        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();
        return plainTextBytes;
    }
    public static List<byte> DecryptTripleDES(List<byte> plainTextBytes)
    {
        return DecryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }
    public static string DecryptTripleDES(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptTripleDES(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static List<byte> DecryptRijndael(List<byte> plainTextBytes)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }
    /// <summary>
    /// Pokud chci bajty, musím si je znovu převést a nebo odkomentovat metodu níže
    /// </summary>
    /// <param name = "plainTextBytes"></param>
    /// <param name = "salt"></param>
    public static String DecryptRijndael(string plainText, List<byte> salt)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptRijndael(BTS.ClearEndingsBytes(BTS.ConvertFromUtf8ToBytes(plainText)), CryptHelper2._pp, salt, CryptHelper2._ivRijn));
    }
    public static List<byte> DecryptRijndael(List<byte> plainTextBytes, List<byte> salt)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }
    public static string DecryptRijndael(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptRijndael(BTS.ConvertFromUtf8ToBytes(p)));
    }

    

    public static List<byte> EncryptRijndael(List<byte> plainTextBytes, List<byte> salt)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }
    public static List<byte> EncryptRijndael(List<byte> plainTextBytes)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }
    public static string EncryptRijndael(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptRijndael(BTS.ConvertFromUtf8ToBytes(p)));
    }

    

    public static List<byte> EncryptRC2(List<byte> plainTextBytes)
    {
        return EncryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }
    public static string EncryptRC2(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptRC2(BTS.ConvertFromUtf8ToBytes(p)));
    }
    public static List<byte> EncryptRC2(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes.ToArray(), initVectorBytes.ToArray());
        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();
        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes.ToArray(), 0, plainTextBytes.Count);
        // Finish encrypting.
        cryptoStream.FlushFinalBlock();
        // Convert our encrypted data from a memory stream into a byte array.
        List<byte> cipherTextBytes = memoryStream.ToArray().ToList();
        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();
        return cipherTextBytes;
    }

    public static List<byte> DecryptRC2(List<byte> plainTextBytes)
    {
        return DecryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }
    public static string DecryptRC2(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptRC2(BTS.ConvertFromUtf8ToBytes(p)));
    }
    public static List<byte> DecryptRC2(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes.ToArray(), initVectorBytes.ToArray());
        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());
        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        List<byte> plainTextBytes = new List<byte>(cipherTextBytes.Count());
        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes.ToArray(), 0, plainTextBytes.Count);
        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();
        return plainTextBytes;
    }
}