public class InternetProtocolsHelper
{
    public static string GetInternetProtocolFromEnum(bool https)
    {
        if (https)
        {
            return GetInternetProtocolFromEnum(InternetProtocols.https);
        }
        return GetInternetProtocolFromEnum(InternetProtocols.http);
    }

    public static string GetInternetProtocolFromEnum(InternetProtocols ip)
    {
        return ip.ToString() + "://";
    }

    /// <summary>
    /// DO A2 se zadává bez ://
    /// </summary>
    /// <param name="protocol"></param>
    /// <returns></returns>
    public static bool? GetInternetProtocolFromString(string protocol)
    {
        if (protocol == "http")
        {
            return false;
        }
        if (protocol == "https")
        {
            return true;
        }
        return null;
    }


}
