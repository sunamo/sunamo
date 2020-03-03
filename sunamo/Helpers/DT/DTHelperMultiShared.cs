using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperMulti
{
    #region ToString
    /// <summary>
    /// 21.6.1989 11:22 (fill zero)
    /// 6/21/1989 11:22 (fill zero)
    /// Vrátí datum a čas v českém formátu bez ms a s
    /// </summary>
    /// <param name="d"></param>
    
    public static DateTime IsValidDateTimeText(string datum)
    {
        DateTime vr = DateTime.MinValue;
        int indexMezery = datum.IndexOf(AllChars.space);
        if (indexMezery != -1)
        {
            var datum2 = DateTime.Today;
            var cas2 = DateTime.Today;
            var datum3 = datum.Substring(0, indexMezery);
            var cas3 = datum.Substring(indexMezery + 1);

            if (datum3.IndexOf(AllChars.dot) != -1)
            {
                datum2 = DTHelperCs.ParseDateCzech(datum3);
            }
            else
            {
                datum2 = DTHelperEn.ParseDateUSA(datum3);
            }

            if (cas3.IndexOf(AllChars.space) == -1)
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
            var indexMezery = r.IndexOf(AllChars.space);
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
    #endregion
}