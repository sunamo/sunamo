using System;
using System.Security;
public class SecureStringHelper
{
    public static SecureString ToSecureString(string input)
    {
        SecureString secure = new SecureString();
        foreach (char c in input)
        {
            secure.AppendChar(c);
        }
        secure.MakeReadOnly();
        return secure;
    }

    public static string ToInsecureString(SecureString input)
    {
        string returnValue = string.Empty;
        IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
        try
        {
            returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
        }
        return returnValue;
    }
}
