public interface ISunamoPaymentGateway<BasePayment, Payment, PaymentSessionId, SessionState>
{
    /// <summary>
    /// Return string error or Payment if success
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    object CreatePayment(string orderId, BasePayment payment);
    SessionState IsPayed(PaymentSessionId paymentSessionId);
    Payment Status(PaymentSessionId paymentSessionId);
}