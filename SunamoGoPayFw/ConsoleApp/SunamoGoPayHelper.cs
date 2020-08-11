using GoPay;
using GoPay.Model.Payment;
using GoPay.Model.Payments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SunamoGoPayHelper
{
    /// <summary>
    /// Return string error or Payment if success
    /// have GwUrl Property
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
            Debugger.Break();
            //return RH.DumpAsString("e", e, DumpProvider.Reflection);
            return "";
        }
    }

    private GPConnector GetToken()
    {
        GPConnector token;
        GPConnector connector = new GPConnector(GoConsts2.apiUri, GoConsts2.clientID, GoConsts2.ClientSecret);

        // volá na /api/oauth2/token
        token = connector.GetAppToken();
        return token;
    }
}