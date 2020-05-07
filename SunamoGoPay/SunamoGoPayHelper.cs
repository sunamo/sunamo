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
    static Type type = typeof(SunamoGoPayHelper);
    public static Dictionary<SessionStateGoPay, string> stateToString = new Dictionary<SessionStateGoPay, string>();

    static SunamoGoPayHelper()
    {
        var v = EnumHelper.GetValues<SessionStateGoPay>();
        foreach (var item in v)
        {
            stateToString.Add(item, ConvertSnakeConvention.FromConvention(item.ToString()));
        }
    }

    public SunamoGoPayHelper(GoPayData goPayData)
    {
       

        this.goPayData = goPayData;
    }



    /// <summary>
    /// Return string error or Payment if success
    /// have GwUrl Property
    /// 
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    public object CreatePayment(BasePayment payment)
    {
        GPConnector token;

        token = GetToken();

        try
        {
            Payment result = token.CreatePayment(payment);
            return result;
        }
        catch (GPClientException e)
        {
            return YamlHelper.DumpAsYaml(e);
        }
    }

    private  GPConnector GetToken()
    {
        GPConnector token;
        GPConnector connector = new GPConnector("https://gw.sandbox.gopay.com/api", goPayData.ClientID, goPayData.ClientSecret);

        // volá na /api/oauth2/token
        token = connector.GetAppToken();
        return token;
    }

    public  Payment Status(long paymentSessionId)
    {
        var token = GetToken();

        /*
Id: 3101079696
OrderNumber: 128
State: PAID
PaymentInstrument: PAYMENT_CARD
Amount: 10000
Payer:
AllowedPaymentInstruments: []
AllowedSwifts: []
Contact:
Email: sunamocz@gmail.com
CountryCode: CZE
PaymendCard:
CardNumber: 418803******0003
CardExpiration: 2009
CardBrand: VISA Electron
CardIssuerCountry: CZE
CardIssuerBank: KOMERCNI BANKA, A.S.
Target:
Type: ACCOUNT
GoId: 8700421323
AdditionalParams: []
Lang: en
GwUrl: https://gw.sandbox.gopay.com/gw/v3/2857a4bc36190819ec74b34f81b914d1
EetCode:
Fik: 30de9ba1-86db-4a70-a4f9-46bcf8efed5a-fa
Bkp: 05403C98-9690E74B-F75AA761-E34D22EB-CB82D02E
Pkp: SUEW0onGqv1mkOhfaxqkNR+880XrX1yPC9f3LDhJK2Bd+oKTD+axM/YDhLhwRj+5Cd10JrokKkD5Ls0DMPVoPdATZLYVQwrKBpI5GxvuWkUeXCWjYfi/5nQoyyFI4wqRFA9ZwnK+sfTssTnXuWWUK6dU50hwWsqpTP9PbbDhJixkD01qEKXJkPfigcboOWB+I7ng0if+odActpG021OSvkpjuDyK1RIMxPWPuA2wqBb2DB21AXUt+E37ztupwE5YIYOzx3zq4KMeIwNocXYrayez5qoIpwUc3r/Onez0/xNze7d9GrfDv+6Mnn51x/ggNYplKlFS7jEHnK/BkwAeBw==
 */
        var status = token.PaymentStatus(paymentSessionId);
        return status;
    }

    public  Payment.SessionState IsPayed(long paymentSessionId)
    {
        var payment = Status(paymentSessionId);
        var state = payment.State;
        if (state.HasValue)
        {
            return state.Value;// == Payment.SessionState.PAID;
        }

        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        return Payment.SessionState.AUTHORIZED; //false;
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

