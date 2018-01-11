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

/// <summary>
/// Všechny šifrování v této třídě fungují.
/// 
/// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
/// decrypt data. As long as encryption and decryption routines use the same
/// parameters to generate the keys, the keys are guaranteed to be the same.
/// The class uses static functions with duplicate code to make it easier to
/// demonstrate encryption and decryption logic. In a real-life application, 
/// this may not be the most efficient way of handling encryption, so - as
/// soon as you feel comfortable with it - you may want to redesign this class.
/// </summary>
public class CryptHelper2
    {
    const int velikostKliceAsym = 1024;
    /// <summary>
    /// Před použitím jednoduchých metod musíš nastavit tuto proměnnou
    /// </summary>
    public static byte[] _s16 = null;
    public static string _pp = null;
    public static byte[] _ivRijn = null;
    public static byte[] _ivRc2 = null;
    public static byte[] _ivTrip = null;

    public static string EncryptRSA(string inputString, int dwKeySize,
                             string xmlString)
    {
        // TODO: Add Proper Exception Handlers
        RSACryptoServiceProvider rsaCryptoServiceProvider =
                                      new RSACryptoServiceProvider(dwKeySize);
        rsaCryptoServiceProvider.FromXmlString(xmlString);
        int keySize = dwKeySize / 8;
        byte[] bytes = Encoding.UTF32.GetBytes(inputString);
        int maxLength = keySize - 42;
        int dataLength = bytes.Length;
        int iterations = dataLength / maxLength;
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i <= iterations; i++)
        {
            byte[] tempBytes = new byte[
                    (dataLength - maxLength * i > maxLength) ? maxLength :
                                                  dataLength - maxLength * i];
            Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                              tempBytes.Length);
            byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                      true);
            Array.Reverse(encryptedBytes);
            stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
        }
        return stringBuilder.ToString();
    }

    public static string DecryptRSA(string inputString, int dwKeySize,
                                 string xmlString)
    {
        // TODO: Add Proper Exception Handlers
        RSACryptoServiceProvider rsaCryptoServiceProvider
                                 = new RSACryptoServiceProvider(dwKeySize);
        rsaCryptoServiceProvider.FromXmlString(xmlString);
        int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ?
          (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
        int iterations = inputString.Length / base64BlockSize;
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < iterations; i++)
        {
            byte[] encryptedBytes = Convert.FromBase64String(
                 inputString.Substring(base64BlockSize * i, base64BlockSize));
            Array.Reverse(encryptedBytes);
            arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                encryptedBytes, true));
        }
        return null;
    }










    public static byte[] EncryptRC2(byte[] plainTextBytes)
    {
        return EncryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }

    public static byte[] DecryptRC2(byte[] plainTextBytes)
    {
        return DecryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }

    public static string EncryptRC2(string p)
    {
        return Encoding.UTF8.GetString(EncryptRC2(Encoding.UTF8.GetBytes(p)));
    }

    public static string DecryptRC2(string p)
    {
        return Encoding.UTF8.GetString(DecryptRC2(Encoding.UTF8.GetBytes(p)));
    }

    public static byte[] EncryptTripleDES(byte[] plainTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        // Finish encrypting.
        cryptoStream.FlushFinalBlock();

        // Convert our encrypted data from a memory stream into a byte array.
        byte[] cipherTextBytes = memoryStream.ToArray();

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return cipherTextBytes;
    }

    public static byte[] DecryptTripleDES(byte[] cipherTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                   0,
                                                   plainTextBytes.Length);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return plainTextBytes;
    }

    public static byte[] EncryptTripleDES(byte[] plainTextBytes)
    {
        return EncryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }

    public static byte[] DecryptTripleDES(byte[] plainTextBytes)
    {
        return DecryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }

    public static string EncryptTripleDES(string p)
    {
        return Encoding.UTF8.GetString(EncryptTripleDES(Encoding.UTF8.GetBytes(p)));
    }

    public static string DecryptTripleDES(string p)
    {
        return Encoding.UTF8.GetString(DecryptTripleDES(Encoding.UTF8.GetBytes(p)));
    }

    public static byte[] EncryptRijndael(byte[] plainTextBytes, byte[] salt)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }

    public static byte[] EncryptRijndael(byte[] plainTextBytes)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }

    public static byte[] DecryptRijndael(byte[] plainTextBytes)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }

    /// <summary>
    /// Pokud chci bajty, musím si je znovu převést a nebo odkomentovat metodu níže
    /// </summary>
    /// <param name="plainTextBytes"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static String DecryptRijndael(string plainText, byte[] salt)
    {

        return Encoding.UTF8.GetString(DecryptRijndael(BTS.ClearEndingsBytes(Encoding.UTF8.GetBytes(plainText)), CryptHelper2._pp, salt, CryptHelper2._ivRijn));
    }

    public static byte[] DecryptRijndael(byte[] plainTextBytes, byte[] salt)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }

    public static string EncryptRijndael(string p)
    {
        return Encoding.UTF8.GetString(EncryptRijndael(Encoding.UTF8.GetBytes(p)));
    }

    public static string DecryptRijndael(string p)
    {
        return Encoding.UTF8.GetString(DecryptRijndael(Encoding.UTF8.GetBytes(p)));
    }

    /// <summary>
    /// RSA není uspůsobeno pro velké bloky dat, proto max, ale opravdu MAXimální velikost je 64bajtů
    /// </summary>
    const int RSA_BLOCKSIZE = 64;
    static bool OAEP = false;

    public static byte[] EncryptRSA(byte[] plainTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes, string xmlSouborKlíče, int velikostKliče)
    {
        CspParameters csp = new CspParameters();
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(velikostKliče, VratCspParameters(true));

        rsa.PersistKeyInCsp = false;

        rsa.FromXmlString(TF.ReadFile(xmlSouborKlíče));
        //int nt = rsa.ExportParameters(true).Modulus.Length;
        int lastBlockLength = plainTextBytes.Length % RSA_BLOCKSIZE;
        decimal bc = plainTextBytes.Length / RSA_BLOCKSIZE;
        int blockCount = (int)Math.Floor(bc);
        bool hasLastBlock = false;
        if (lastBlockLength != 0)
        {
            //We need to create a final block for the remaining characters
            blockCount += 1;
            hasLastBlock = true;
        }
        List<byte> vr = new List<byte>();
        for (int blockIndex = 0; blockIndex <= blockCount - 1; blockIndex++)
        {
            int thisBlockLength = 0;

            //If this is the last block and we have a remainder, then set the length accordingly
            if ((blockCount == (blockIndex + 1)) && hasLastBlock)
            {
                thisBlockLength = lastBlockLength;
            }
            else
            {
                thisBlockLength = RSA_BLOCKSIZE;
            }
            int startChar = blockIndex * RSA_BLOCKSIZE;

            //Define the block that we will be working on
            byte[] currentBlock = new byte[thisBlockLength];
            Array.Copy(plainTextBytes, startChar, currentBlock, 0, thisBlockLength);

            byte[] encryptedBlock = rsa.Encrypt(currentBlock, OAEP);
            vr.AddRange(encryptedBlock);
        }
        rsa.Clear();
        return vr.ToArray();
        //return rsa.Encrypt(plainTextBytesBytes, false);
    }
    #region Z původní třídy CryptHelper, kterou jsem nahradil jiným obsahem
    public static RSAParameters GetRSAParametersFromXml(string p)
    {
        RSAParameters rp = new RSAParameters();
        XmlDocument xd = new XmlDocument();
        xd.Load(p);
        // Je lepší to číst v Ascii protože to bude po jednom bytu číst
        Encoding kod = Encoding.UTF8;
        rp.D = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/D").InnerText);
        rp.DP = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/DP").InnerText);
        rp.DQ = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/DQ").InnerText);
        rp.Exponent = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/Exponent").InnerText);
        rp.InverseQ = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/InverseQ").InnerText);
        rp.Modulus = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/Modulus").InnerText);
        rp.P = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/P").InnerText);
        rp.Q = Convert.FromBase64String(xd.SelectSingleNode("RSAKeyValue/Q").InnerText);

        return rp;
    }
    #endregion

    /// <summary>
    /// Encrypts specified plaintext using Rijndael symmetric key algorithm
    /// and returns a base64-encoded result.
    /// </summary>
    /// <param name="plainText">
    /// Plaintext value to be encrypted.
    /// </param>
    /// <param name="passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name="saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name="passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name="initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged class IV must be 
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Encrypted value formatted as a base64-encoded string.
    /// </returns>
    public static byte[] EncryptRijndael(byte[] plainTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        #region Old
        #endregion

        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        // Finish encrypting.
        cryptoStream.FlushFinalBlock();

        // Convert our encrypted data from a memory stream into a byte array.
        byte[] cipherTextBytes = memoryStream.ToArray();

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return cipherTextBytes;
    }

    /// <summary>
    /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    /// </summary>
    /// <param name="cipherText">
    /// Base64-formatted ciphertext value.
    /// </param>
    /// <param name="passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name="saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name="passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name="initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged class IV must be
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
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
    public static byte[] DecryptRijndael(byte[] cipherTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        #region old - aspnet.cz
        #endregion

        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                   0,
                                                   plainTextBytes.Length);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return plainTextBytes;
    }

    public static byte[] EncryptRC2(byte[] plainTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        // Finish encrypting.
        cryptoStream.FlushFinalBlock();

        // Convert our encrypted data from a memory stream into a byte array.
        byte[] cipherTextBytes = memoryStream.ToArray();

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return cipherTextBytes;
    }

    public static byte[] DecryptRC2(byte[] cipherTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                   0,
                                                   plainTextBytes.Length);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return plainTextBytes;

    }

    // TODO: Umožnit export do key containery a v případě potřeby to z něho vytáhnout.
    public static byte[] DecryptRSA(byte[] cipherTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes, string xmlSouborKlíče, int velikostKliče)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(velikostKliče, VratCspParameters(false));
        rsa.PersistKeyInCsp = false;
        rsa.FromXmlString(File.ReadAllText(xmlSouborKlíče));
        //bool b = rsa.PublicOnly;
        if ((cipherTextBytes.Length % RSA_BLOCKSIZE) != 0)
        {
            throw new System.Security.Cryptography.CryptographicException("Encrypted text is an invalid length");
            return null;
        }

        //Calculate the number of blocks we will have to work on
        int blockCount = cipherTextBytes.Length / RSA_BLOCKSIZE;

        List<byte> vr = new List<byte>();
        for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
        {
            int startChar = blockIndex * RSA_BLOCKSIZE;

            //Define the block that we will be working on
            byte[] currentBlockBytes = new byte[RSA_BLOCKSIZE];
            Array.Copy(cipherTextBytes, startChar, currentBlockBytes, 0, RSA_BLOCKSIZE);




            byte[] currentBlockDecrypted = rsa.Decrypt(currentBlockBytes, OAEP);
            vr.AddRange(currentBlockDecrypted);
        }

        //Release all resources held by the RSA service provider
        rsa.Clear();
        return vr.ToArray();
        //return rsa.Decrypt(cipherTextBytes, false);
    }

    private static CspParameters VratCspParameters(bool p)
    {
        CspParameters csp = new CspParameters();
        return csp;
    }
}
