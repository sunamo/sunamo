using System;
using System.Collections.Generic;
using System.Text;


public class UriShortConsts
{
    public const string DevCz = "dev.localhost";
    public const string AppCz = "app.localhost";
    public const string GeoCz = "geo.localhost";
    public const string ErtCz = "var.localhost";
    public const string ShoCz = "sho.localhost";
    public const string RpsCz = "rps.localhost";
    public const string PhsCz = "phs.localhost";
    public const string HtpCz = "htp.localhost";
    public const string LyrCz = "lyr.localhost";

    // miss acs
    public static List<string> All = CA.ToList<string>(DevCz, LyrCz, AppCz, GeoCz, ErtCz, RpsCz, ShoCz, PhsCz);
}