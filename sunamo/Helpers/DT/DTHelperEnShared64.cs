using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class DTHelperEn
{
    #region Date and time
    /// <summary>
    /// Its named ToString due to exactly same format return dt.ToString while is en-us localization
    /// 21.6.1989 / 6/21/1989 + " " + mm:ss tt
    /// </summary>
    public static string ToString(DateTime dt)
    {
        return ToShortDateString(dt) + " " + ToShortTimeString(dt);
    }
    #endregion

    #region ToString
    #region Only date
    /// <summary>
    /// 21.6.1989 / 6/21/1989
    /// </summary>
    /// <param name="today"></param>
    public static string ToShortDateString(DateTime today)
    {
        return ToShortDateString(today, DateTime.MinValue, DTHelperMulti.DateToString(today, Langs.en));
    }

    /// <summary>
    /// 21.6.1989 / 6/21/1989
    /// </summary>
    /// <param name="today"></param>
    /// <param name="_def"></param>
    /// <param name="returnWhenA1isA2"></param>
    public static string ToShortDateString(DateTime today, DateTime _def, string returnWhenA1isA2)
    {
        if (today == _def)
        {
            return returnWhenA1isA2;
        }
        return DTHelperMulti.DateToString(today, Langs.en);
    }
    #endregion



    #region Only time (without seconds)
    /// <summary>
    /// mm:ss tt
    /// </summary>
    /// <param name="dt"></param>
    public static string ToShortTimeString(DateTime dt)
    {
        return string.Format("{0:hh:mm tt}", dt);
    }
    #endregion
    #endregion
}
