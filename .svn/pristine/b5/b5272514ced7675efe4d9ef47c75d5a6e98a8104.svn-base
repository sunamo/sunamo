using System;
using System.Security;
using System.Security.Cryptography;
public static class ProtectedDataHelper
{
    public static string EncryptString(string salt, System.Security.SecureString input)
    {
        byte[] entropy = System.Text.Encoding.Unicode.GetBytes(salt);
        byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
            System.Text.Encoding.Unicode.GetBytes(Unsafe.ToInsecureString(input)),
            entropy,
            System.Security.Cryptography.DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(encryptedData);
    }

    public static SecureString DecryptString(string salt, string encryptedData)
    {
        try
        {
            byte[] entropy = System.Text.Encoding.Unicode.GetBytes(salt);
            byte[] decryptedData = null;
            try
            {
                decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            }
            catch (Exception)
            {
                return new SecureString();
            }
            return Unsafe.ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
        }
        catch
        {
            return new SecureString();
        }
    }
}
