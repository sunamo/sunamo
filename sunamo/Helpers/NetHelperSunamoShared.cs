using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public partial class NetHelperSunamo
{
    //[Obsolete("Do not use this in Production code!!!", true)]
    public static void NEVER_EAT_POISON_Disable_CertificateValidation()
    {
        // Disabling certificate validation can expose you to a man-in-the-middle attack
        // which may allow your encrypted message to be read by an attacker
        // https://stackoverflow.com/a/14907718/740639
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (
                object s,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors
            ) {
                return true;
            };
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        //client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //new X509ServiceCertificateAuthentication()
        //{
        //    CertificateValidationMode = X509CertificateValidationMode.None,
        //    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
        //};

    }

    // callback used to validate the certificate in an SSL conversation
    private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
    {
        //object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors
        bool result = cert.Subject.Contains("YourServerName");
        return result;
    }
}