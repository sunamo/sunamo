using GoPay.Common;
using GoPay.Model.Payment;
using GoPay.Model.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoPay.Model.Payments.Target;

public class Program
{
    public static void Main(String[] s)
    {
        GoConsts2.Testing();

        var amount = 1 * 100;
        string name = "Item";
        var lang = "en";

        BasePayment payment = null;
        payment = new BasePayment()
        {
            Callback = new Callback
            {
                NotificationUrl = GoConsts2.notifyUri,
                ReturnUrl = GoConsts2.returnUri
            },
            // Will use ID of payment
            //OrderNumber = orderId,
            Amount = amount,
            OrderDescription = name,
            Currency = Currency.CZK,
            Lang = lang.ToString(),

            Target = new Target { GoId = GoConsts2.goID, Type = TargetType.ACCOUNT },
            Items = new List<OrderItem>
                {
                    new OrderItem{ Name =name, Count = 1, Amount = amount, ItemType= ItemType.ITEM }
                }
        };

        TranslateDictionary.returnXlfKey = true;

        GoPayData gpd = new GoPayData(GoConsts.clientID, GoConsts.ClientSecret, GoConsts.goID);

        SunamoGoPayHelper g = new SunamoGoPayHelper(gpd);
        var p = g.CreatePayment(payment);
        int s2 = 0;
    }
}