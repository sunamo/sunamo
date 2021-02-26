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
public partial class CryptHelper2
{
    public static string EncryptRSA(string inputString, int dwKeySize, string xmlString)
    {
        // TODO: Add Proper Exception Handlers
        RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
        rsaCryptoServiceProvider.FromXmlString(xmlString);
        int keySize = dwKeySize / 8;
        List<byte> bytes = Encoding.UTF32.GetBytes(inputString).ToList();
        int maxLength = keySize - 42;
        int dataLength = bytes.Count;
        int iterations = dataLength / maxLength;
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i <= iterations; i++)
        {
            List<byte> tempBytes = new List<byte>((dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i);
            Buffer.BlockCopy(bytes.ToArray(), maxLength * i, tempBytes.ToArray(), 0, tempBytes.Count);
            List<byte> encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes.ToArray(), true).ToList();
            encryptedBytes.Reverse();
            stringBuilder.Append(Convert.ToBase64String(encryptedBytes.ToArray()));
        }

        return stringBuilder.ToString();
    }

    public static string DecryptRSA(string inputString, int dwKeySize, string xmlString)
    {
        // TODO: Add Proper Exception Handlers
        RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
        rsaCryptoServiceProvider.FromXmlString(xmlString);
        int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
        int iterations = inputString.Count() / base64BlockSize;
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < iterations; i++)
        {
            List<byte> encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize)).ToList();
            encryptedBytes.Reverse();
            arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes.ToArray(), true));
        }

        return null;
    }

    /// <summary>
    /// RSA není uspůsobeno pro velké bloky dat, proto max, ale opravdu MAXimální velikost je 64bajtů
    /// </summary>
    private const int RSA_BLOCKSIZE = 64;
    private static bool s_OAEP = false;
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
            List<byte> currentBlock = new List<byte>(thisBlockLength);
            Array.Copy(plainTextBytes.ToArray(), startChar, currentBlock.ToArray(), 0, thisBlockLength);
            List<byte> encryptedBlock = rsa.Encrypt(currentBlock.ToArray(), s_OAEP).ToList();
            vr.AddRange(encryptedBlock);
        }

        rsa.Clear();
        return vr;
        //return rsa.Encrypt(plainTextBytesBytes, false);
    }

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

    static Type type = typeof(CryptHelper2);

    // TODO: Umožnit export do key containery a v případě potřeby to z něho vytáhnout.
    public static List<byte> DecryptRSA(List<byte> cipherTextBytes, string passPhrase, List<byte> saltValueBytes, List<byte> initVectorBytes, string xmlSouborKlíče, int velikostKliče)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(velikostKliče, VratCspParameters(false));
        rsa.PersistKeyInCsp = false;
        rsa.FromXmlString(TF.ReadAllText(xmlSouborKlíče));
        //bool b = rsa.PublicOnly;
        if ((cipherTextBytes.Count % RSA_BLOCKSIZE) != 0)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.EncryptedTextIsAnInvalidLength));
        }

        //Calculate the number of blocks we will have to work on
        int blockCount = cipherTextBytes.Count / RSA_BLOCKSIZE;
        List<byte> vr = new List<byte>();
        for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
        {
            int startChar = blockIndex * RSA_BLOCKSIZE;
            //Define the block that we will be working on
            List<byte> currentBlockBytes = new List<byte>(RSA_BLOCKSIZE);
            Array.Copy(cipherTextBytes.ToArray(), startChar, currentBlockBytes.ToArray(), 0, RSA_BLOCKSIZE);
            List<byte> currentBlockDecrypted = rsa.Decrypt(currentBlockBytes.ToArray(), s_OAEP).ToList();
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