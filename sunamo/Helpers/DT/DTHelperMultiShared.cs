﻿using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperMulti{ 

        

/// <summary>
    /// Vrátí datum a čas v českém formátu bez ms a s
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == Langs.cs)
            {
                return "Nebylo uvedeno";
            }
            else
            {
                return "Not indicated";
            }
        }

        if (l == Langs.cs)
        {
            return d.Day + "." + d.Month + "." + d.Year + " " + NH.MakeUpTo2NumbersToZero(d.Hour) + ":" + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
        else
        {
            return d.Month + "/" + d.Day + "/" + d.Year + " " + NH.MakeUpTo2NumbersToZero(d.Hour) + ":" + NH.MakeUpTo2NumbersToZero(d.Minute);
        }

    }



public static DateTime IsValidDateText(string r)
        {
            DateTime dt = DateTime.MinValue;
            r = r.Trim();
            if (r != "")
            {
                var indexTecky = r.IndexOf('.');
                if (indexTecky != -1)
                {
                    dt = DTHelperCs.ParseDateCzech(r);
                }
                else
                {
                    dt = DTHelperEn.ParseDateUSA(r);
                }
            }
            return dt;
        }


public static DateTime IsValidDateTimeText(string datum)
        {
            DateTime vr = DateTime.MinValue;
            int indexMezery = datum.IndexOf(' ');
            if (indexMezery != -1)
            {
                var datum2 = DateTime.Today;
                var cas2 = DateTime.Today;
                var datum3 = datum.Substring(0, indexMezery);
                var cas3 = datum.Substring(indexMezery + 1);
                if (datum3.IndexOf('.') != -1)
                {
                    datum2 = DTHelperCs.ParseDateCzech(datum3);
                }
                else
                {
                    datum2 = DTHelperEn.ParseDateUSA(datum3);
                }

                if (cas3.IndexOf(' ') == -1)
                {
                    cas2 = DTHelperCs.ParseTimeCzech(cas3);
                }
                else
                {
                    cas2 = DTHelperEn.ParseTimeUSA(cas3);
                }

                if (datum2 != DateTime.MinValue && cas2 != DateTime.MinValue)
                {
                    vr = new DateTime(datum2.Year, datum2.Month, datum2.Day, cas2.Hour, cas2.Minute, cas2.Second);
                }


            }

            return vr;
        }

public static DateTime IsValidTimeText(string r)
        {
            DateTime dt = DateTime.MinValue;
            r = r.Trim();
            if (r != "")
            {
                var indexMezery = r.IndexOf(' ');
                if (indexMezery == -1)
                {
                    dt = DTHelperCs.ParseTimeCzech(r);
                }
                else
                {
                    dt = DTHelperEn.ParseTimeUSA(r);
                }
            }
            return dt;
        }
}