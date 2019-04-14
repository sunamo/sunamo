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

/// <summary>
/// Všechny šifrování v této třídě fungují.
/// 
/// This public class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
/// decrypt data. As long as encryption and decryption routines use the same
/// parameters to generate the keys, the keys are guaranteed to be the same.
/// The public class uses static functions with duplicate code to make it easier to
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
    public static List<byte> _s16 = null;
    public static string _pp = null;
    public static List<byte> _ivRijn = null;
    public static List<byte> _ivRc2 = null;
    public static List<byte> _ivTrip = null;

    public static string EncryptRSA(string inputString, int dwKeySize,
                             string xmlString)
    {
        // TODO: Add Proper Exception Handlers
        RSACryptoServiceProvider rsaCryptoServiceProvider =
                                      new RSACryptoServiceProvider(dwKeySize);
        rsaCryptoServiceProvider.FromXmlString(xmlString);
        int keySize = dwKeySize / 8;
        List<byte> bytes = Encoding.UTF32.GetBytes(inputString).ToList();
        int maxLength = keySize - 42;
        int dataLength = bytes.Count;
        int iterations = dataLength / maxLength;
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i <= iterations; i++)
        {
            List<byte> tempBytes = new List<byte>(
                    (dataLength - maxLength * i > maxLength) ? maxLength :
                                                  dataLength - maxLength * i);
            Buffer.BlockCopy(bytes.ToArray(), maxLength * i, tempBytes.ToArray(), 0,
                              tempBytes.Count);
            List<byte> encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes.ToArray(),
                                                                      true).ToList();
            encryptedBytes.Reverse() ;
            stringBuilder.Append(Convert.ToBase64String(encryptedBytes.ToArray()));
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
        int iterations = inputString.Count() / base64BlockSize;
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < iterations; i++)
        {
            List<byte> encryptedBytes = Convert.FromBase64String(
                 inputString.Substring(base64BlockSize * i, base64BlockSize)).ToList();
            encryptedBytes.Reverse();
            arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                encryptedBytes.ToArray(), true));
        }
        return null;
    }










    public static List<byte> EncryptRC2(List<byte> plainTextBytes)
    {
        return EncryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }

    public static List<byte> DecryptRC2(List<byte> plainTextBytes)
    {
        return DecryptRC2(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRc2);
    }

    public static string EncryptRC2(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptRC2(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static string DecryptRC2(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptRC2(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static List<byte> EncryptTripleDES(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes.ToArray(),
                                                        hashAlgorithm,
                                                        passwordIterations);

        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();

        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes.ToArray(),
                                                         initVectorBytes.ToArray());

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
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

    public static List<byte> DecryptTripleDES(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes.ToArray(),
                                                        hashAlgorithm,
                                                        passwordIterations);

        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();

        // Create uninitialized Rijndael encryption object.
        TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes.ToArray(),
                                                         initVectorBytes.ToArray());

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        List<byte> plainTextBytes = new List<byte>( cipherTextBytes.Count);

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes.ToArray(),
                                                   0,
                                                   plainTextBytes.Count);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return plainTextBytes;
    }
    

    public static List<byte> EncryptTripleDES(List<byte> plainTextBytes)
    {
        return EncryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }

    public static List<byte> DecryptTripleDES(List<byte> plainTextBytes)
    {
        return DecryptTripleDES(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivTrip);
    }

    public static string EncryptTripleDES(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptTripleDES(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static string DecryptTripleDES(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptTripleDES(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static List<byte> EncryptRijndael(List<byte> plainTextBytes, List<byte> salt)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }

    public static List<byte> EncryptRijndael(List<byte> plainTextBytes)
    {
        return EncryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }

    public static List<byte> DecryptRijndael(List<byte> plainTextBytes)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, CryptHelper2._s16, CryptHelper2._ivRijn);
    }

    /// <summary>
    /// Pokud chci bajty, musím si je znovu převést a nebo odkomentovat metodu níže
    /// </summary>
    /// <param name="plainTextBytes"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static String DecryptRijndael(string plainText, List<byte> salt)
    {

        return BTS.ConvertFromBytesToUtf8(DecryptRijndael(BTS.ClearEndingsBytes(BTS.ConvertFromUtf8ToBytes(plainText)), CryptHelper2._pp, salt, CryptHelper2._ivRijn));
    }

    public static List<byte> DecryptRijndael(List<byte> plainTextBytes, List<byte> salt)
    {
        return DecryptRijndael(plainTextBytes, CryptHelper2._pp, salt, CryptHelper2._ivRijn);
    }

    public static string EncryptRijndael(string p)
    {
        return BTS.ConvertFromBytesToUtf8(EncryptRijndael(BTS.ConvertFromUtf8ToBytes(p)));
    }

    public static string DecryptRijndael(string p)
    {
        return BTS.ConvertFromBytesToUtf8(DecryptRijndael(BTS.ConvertFromUtf8ToBytes(p)));
    }

    /// <summary>
    /// RSA není uspůsobeno pro velké bloky dat, proto max, ale opravdu MAXimální velikost je 64bajtů
    /// </summary>
    const int RSA_BLOCKSIZE = 64;
    static bool OAEP = false;

    public static List<byte> EncryptRSA(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes, string xmlSouborKlíče, int velikostKliče)
    {
        CspParameters csp = new CspParameters();
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(velikostKliče, VratCspParameters(true));

        rsa.PersistKeyInCsp = false;

        rsa.FromXmlString(TF.ReadFile(xmlSouborKlíče));
        //int nt = rsa.ExportParameters(true).Modulus.Count;
        int lastBlockLength = plainTextBytes.Count % RSA_BLOCKSIZE;
        decimal bc = plainTextBytes.Count / RSA_BLOCKSIZE;
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
            List<byte> currentBlock = new List<byte>( thisBlockLength);
            Array.Copy(plainTextBytes.ToArray(), startChar, currentBlock.ToArray(), 0, thisBlockLength);

            List<byte> encryptedBlock = rsa.Encrypt(currentBlock.ToArray(), OAEP).ToList();
            vr.AddRange(encryptedBlock);
        }
        rsa.Clear();
        return vr;
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
    /// first block of plaintext data. For RijndaelManaged public class IV must be 
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
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

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes.ToArray(),
                                                        hashAlgorithm,
                                                        passwordIterations);

        List<byte> keyBytes = new List<byte>( password.GetBytes(keySize / 8));

        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes.ToArray(),
                                                         initVectorBytes.ToArray());

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
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
    /// first block of plaintext data. For RijndaelManaged public class IV must be
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
            #region MyRegion
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes.ToArray(),
                                                                hashAlgorithm,
                                                                passwordIterations);

            List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes.ToArray(),
                                                             initVectorBytes.ToArray());

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            List<byte> plainTextBytes = new List<byte>(cipherTextBytes.Count());

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes.ToArray(),
                                                       0,
                                                       plainTextBytes.Count);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            return plainTextBytes; 
            #endregion
        }
        else
        {
            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                             passPhrase,
                                                            saltValueBytes.ToArray(),
                                                            hashAlgorithm,
                                                            passwordIterations);

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
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes.ToArray(),
                                                             initVectorBytes.ToArray());

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.


            // Here must be byte[], otherwise cryptoStream.Close() throw a exception
            byte[] plainTextBytes = new byte[cipherTextBytes.Length()];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            return plainTextBytes.ToList();
        }
    }

    public static List<byte> EncryptRC2(List<byte> plainTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes.ToArray(),
                                                        hashAlgorithm,
                                                        passwordIterations);

        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();

        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes.ToArray(),
                                                         initVectorBytes.ToArray());

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
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

    public static List<byte> DecryptRC2(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes)
    {
        string hashAlgorithm = "SHA1";
        int keySize = 128;
        int passwordIterations = 2; // Může bý jakékoliv číslo

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes.ToArray(),
                                                        hashAlgorithm,
                                                        passwordIterations);

        List<byte> keyBytes = password.GetBytes(keySize / 8).ToList();

        // Create uninitialized Rijndael encryption object.
        RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes.ToArray(),
                                                         initVectorBytes.ToArray());

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes.ToArray());

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        List<byte> plainTextBytes = new List<byte>( cipherTextBytes.Count());

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes.ToArray(),
                                                   0,
                                                   plainTextBytes.Count);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        return plainTextBytes;

    }

    // TODO: Umožnit export do key containery a v případě potřeby to z něho vytáhnout.
    public static List<byte> DecryptRSA(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes, string xmlSouborKlíče, int velikostKliče)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(velikostKliče, VratCspParameters(false));
        rsa.PersistKeyInCsp = false;
        rsa.FromXmlString(File.ReadAllText(xmlSouborKlíče));
        //bool b = rsa.PublicOnly;
        if ((cipherTextBytes.Count % RSA_BLOCKSIZE) != 0)
        {
            throw new System.Security.Cryptography.CryptographicException("Encrypted text is an invalid length");
        }

        //Calculate the number of blocks we will have to work on
        int blockCount = cipherTextBytes.Count / RSA_BLOCKSIZE;

        List<byte> vr = new List<byte>();
        for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
        {
            int startChar = blockIndex * RSA_BLOCKSIZE;

            //Define the block that we will be working on
            List<byte> currentBlockBytes = new List<byte>( RSA_BLOCKSIZE);
            Array.Copy(cipherTextBytes.ToArray(), startChar, currentBlockBytes.ToArray(), 0, RSA_BLOCKSIZE);




            List<byte> currentBlockDecrypted = rsa.Decrypt(currentBlockBytes.ToArray(), OAEP).ToList();
            vr.AddRange(currentBlockDecrypted);
        }

        //Release all resources held by the RSA service provider
        rsa.Clear();
        return vr;
        //return rsa.Decrypt(cipherTextBytes, false);
    }

    private static CspParameters VratCspParameters(bool p)
    {
        CspParameters csp = new CspParameters();
        return csp;
    }
}
