using System;

public partial class DTHelperFormalized
{
    #region Parse
    /// <summary>
    /// Is used in GpxTrackFile
    /// 2018-08-10T11:33:19Z
    /// 
    /// </summary>
    /// <param name="p"></param>
    public static DateTime StringToDateTimeFormalizeDate(string p)
    {
        if (string.IsNullOrEmpty(p))
        {
            return DateTime.MinValue;
        }

        return DateTime.Parse(p, null, System.Globalization.DateTimeStyles.None);
    } 
    #endregion
}