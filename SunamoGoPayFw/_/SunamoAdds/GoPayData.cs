using System;
using System.Collections.Generic;
using System.Text;

public class GoPayData
{
    public readonly string ClientID = null;
    public readonly string ClientSecret = null;
    public readonly long GoID = long.MaxValue;

    public GoPayData(string clientID, string clientSecret, long goID)
    {
        ClientID = clientID;
        ClientSecret = clientSecret;
        GoID = goID;
    }
}