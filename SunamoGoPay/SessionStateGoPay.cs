using System;
using System.Collections.Generic;
using System.Text;

public enum SessionStateGoPay : byte
{
    CREATED = 0,
    PAYMENT_METHOD_CHOSEN = 1,
    PAID = 2,
    AUTHORIZED=4,
    CANCELED=8,
    TIMEOUTED=16,
    REFUNDED=32,
    PARTIALLY_REFUNDED = 64
}