﻿using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;

public static class MySitesConverter //: ISimpleConverter<string, MySites>
{
    /// <summary>
    /// ignore case
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    public static MySites ConvertTo(string u)
    {
        var r = EnumHelper.Parse<MySites>(u);
        return r;
    }

    public static MySites ConvertTo(MySitesShort u)
    {
        return (MySites)((int)u);
    }

    public static byte ConvertFrom(MySites u)
    {
        return ConvertFrom(u.ToString().ToLower());
    }

    public static byte ConvertFrom(string t)
    {
        if (t.EndsWith("new"))
        {
            t = t.Substring(0, t.Length - 3);
        }
        switch (t)
        {
            case "kocicky":
                return 2;
            case "ggdag":
                return 0;
            case "bibleserver":
                return 1;
            case "geocaching":
                return 3;
            case "apps":
                return 4;
            case "lyrics":
                return 6;
            case "photos":
                return 5;
            case "casdmladez":
                return 11;
            case "shortener":
                return 12;
            case "build":
                return 9;
            case "thunderbrigade":
                return 10;
            case "developer":
                return 7;
            
            case "eurostrip":
                return 14;
            case "widgets":
                return 15;
            case "windowsmetrocontrols":
                return 16;
            case "chromedevicescompare":
                return 17;
            
        }
        //Mysites.Nope
        return 8;
    }
}
