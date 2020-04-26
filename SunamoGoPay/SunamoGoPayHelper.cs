using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GoPay;
using GoPay.Common;
using GoPay.Model.Payment;
using GoPay.Model.Payments;
using static GoPay.Model.Payments.Target;

public class SunamoGoPayHelper
    {
    GoPayData goPayData = null;

    public SunamoGoPayHelper(GoPayData goPayData)
    {
        this.goPayData = goPayData;
    }

    /// <summary>
    /// Return error or uri if success
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
        public  string CreatePayment(BasePayment payment)
        {
            GPConnector connector = new GPConnector("https://gw.sandbox.gopay.com/api", goPayData.ClientID, goPayData.ClientSecret);

            // volá na /api/oauth2/token
            GPConnector token = connector.GetAppToken();

            try
            {
                Payment result = token.CreatePayment(payment);
                return result.GwUrl;
            }
            catch (GPClientException e)
            {
            return YamlHelper.DumpAsYaml(e);
            }

        
        }

    #region MyRegion
    public static string Encrypt(string data, string secureKey)
    {
        // Vytvorime instanci 3DES algoritmu, nastavime parametry
        TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
        alg.Padding = PaddingMode.Zeros;
        alg.Mode = CipherMode.ECB;
        alg.GenerateIV();
        try
        {
            alg.Key = new ASCIIEncoding().GetBytes(secureKey);

        }
        catch (Exception ex)
        {
            throw new GopayException(ex.ToString());
        }

        // Vytvorime encryptor
        ICryptoTransform encryptor = alg.CreateEncryptor(alg.Key, alg.IV);

        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(cs);
        byte[] encryptedByte;

        try
        {
            sw.Write(data);
            sw.Flush();
            cs.FlushFinalBlock();

            encryptedByte = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(encryptedByte, 0, (int)ms.Length);

        }
        catch (Exception ex)
        {
            throw new GopayException(ex.ToString());

        }
        finally
        {
            ms.Close();
            ms.Dispose();

            cs.Close();
            cs.Dispose();

            sw.Close();
            sw.Dispose();
        }

        StringBuilder encryptedData = new StringBuilder();
        foreach (byte b in encryptedByte)
        {
            try
            {
                encryptedData.Append(String.Format("{0:x2}", b));

            }
            catch (Exception ex)
            {
                throw new GopayException(ex.ToString());
            }
        }

        return encryptedData.ToString();
    }

    public static string Hash(string data)
    {
        byte[] dataToHash = ASCIIEncoding.UTF8.GetBytes(data);
        byte[] hashValue = new SHA1Managed().ComputeHash(dataToHash);

        StringBuilder hashData = new StringBuilder();
        foreach (byte b in hashValue)
        {
            hashData.Append(String.Format("{0:x2}", b));
        }

        return hashData.ToString();
    }

    public static string ConcatPaymentSession(
           long gopayId,
           long paymentSessionId,
           string secureKey)
    {

        return gopayId.ToString() + "|" +
               paymentSessionId.ToString() + "|" +
               secureKey;
    }

    public static string PaymentSignature(long paymentSessionId, long targetGoId)
    {
        string secureKey = GoConsts.secureKey;

        var hash = Hash(
                            ConcatPaymentSession(
                                targetGoId,
                                paymentSessionId,
                                secureKey));

        string sessionEncryptedSignature = Encrypt(
                       hash ,
                             secureKey);

        return hash;

    }
    #endregion
}

