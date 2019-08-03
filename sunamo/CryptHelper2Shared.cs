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
    /// <returns></returns>
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
    /// <summary>
    /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    /// </summary>
    /// <param name = "cipherText">
    /// Base64-formatted ciphertext value.
    /// </param>
    /// <param name = "passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name = "saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name = "hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name = "passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name = "initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged public class IV must be
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name = "keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Decrypted string value.
    /// </returns>
    /// <remarks>
    /// Most of the logic in this function is similar to the Encrypt
    /// logic. In order for decryption to work, all parameters of this function
    /// - except cipherText value - must match the corresponding parameters of
    /// the Encrypt function which was called to generate the
    /// ciphertext.
    /// </remarks>d
    public static List<byte> DecryptRijndael(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        if (cipherTextBytes.Count == 0)
        {
            return new List<byte>();
        }

        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        // zkusit tuhle větev jestli funguje a jestli to nebude mršit
        if (false)
        {
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
            List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();
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
        else
        {
            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes.ToArray(), initVectorBytes.ToArray());
            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());
            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            // Here must be byte[], otherwise cryptoStream.Close() throw a exception
            byte[] plainTextBytes = new byte[cipherTextBytes.Length()];
            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();
            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            return plainTextBytes.ToList();
        }
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
    /// <summary>
    /// Encrypts specified plaintext using Rijndael symmetric key algorithm
    /// and returns a base64-encoded result.
    /// </summary>
    /// <param name = "plainText">
    /// Plaintext value to be encrypted.
    /// </param>
    /// <param name = "passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name = "saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name = "hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name = "passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name = "initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged public class IV must be 
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name = "keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Encrypted value formatted as a base64-encoded string.
    /// </returns>
    public static List<byte> EncryptRijndael(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes.ToArray(), hashAlgorithm, passwordIterations);
        List<byte> keyBytes = new List<byte>(password.GetBytes(keySize / 8));
        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();
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