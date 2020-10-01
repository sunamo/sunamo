using System;
using System.Collections.Generic;
using System.Text;


    public class UriConsts
    {
        public const string DevCz = "developer.sunamo.cz";
        public const string LyrCz = "lyrics.sunamo.cz";
    public const string AppCz = "apps.sunamo.cz";
    public const string GeoCz = "geocaching.sunamo.cz";
    public const string ErtCz = "dart.sunamo.cz";
    public const string RpsCz = "repairservice.sunamo.cz";
    public const string ShoCz = "shortener.sunamo.cz";
    public const string PhsCz = "photos.sunamo.cz";
    public const string HtpCz = "chytre-aplikace.cz";

    // miss acs
    public static List<string> All = CA.ToList<string>(DevCz, LyrCz, AppCz, GeoCz, ErtCz, RpsCz, ShoCz, PhsCz);
}