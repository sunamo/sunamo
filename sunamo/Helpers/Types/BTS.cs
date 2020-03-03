/// <summary>
/// Base Types Static
/// </summary>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public static partial class BTS
{
    public static Stream StreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public static string StringFromStream(Stream stream)
    {
        StreamReader reader = new StreamReader(stream);
        string text = reader.ReadToEnd();
        return text;
    }

    #region Parse*
    public static bool lastBool = false;

    public static bool TryParseBool(string trim)
    {
        return bool.TryParse(trim, out lastBool);
    }




    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Invert(bool b, bool really)
    {
        if (really)
        {
            return !b;
        }
        return b;
    }

    /// <summary>
    /// Check for null in A2
    /// </summary>
    /// <param name="tag2"></param>
    /// <param name="tag"></param>
    
    public static string SameLenghtAllDateTimes(DateTime dateTime)
    {
        string year = dateTime.Year.ToString();
        string month = SH.MakeUpToXChars(dateTime.Month, 2);
        string day = SH.MakeUpToXChars(dateTime.Day, 2);
        string hour = SH.MakeUpToXChars(dateTime.Hour, 2);
        string minutes = SH.MakeUpToXChars(dateTime.Minute, 2);
        string seconds = SH.MakeUpToXChars(dateTime.Second, 2);
        return day + AllStrings.dot + month + AllStrings.dot + year + AllStrings.space + hour + AllStrings.colon + minutes + AllStrings.colon + seconds;// +AllStrings.colon + miliseconds;
    }

    public static string SameLenghtAllDates(DateTime dateTime)
    {
        string year = dateTime.Year.ToString();
        string month = SH.MakeUpToXChars(dateTime.Month, 2);
        string day = SH.MakeUpToXChars(dateTime.Day, 2);
        return day + AllStrings.dot + month + AllStrings.dot + year; // +AllStrings.space + hour + AllStrings.colon + minutes + AllStrings.colon + seconds;// +AllStrings.colon + miliseconds;
    }

    public static string SameLenghtAllTimes(DateTime dateTime)
    {
        string hour = SH.MakeUpToXChars(dateTime.Hour, 2);
        string minutes = SH.MakeUpToXChars(dateTime.Minute, 2);
        string seconds = SH.MakeUpToXChars(dateTime.Second, 2);
        return hour + AllStrings.colon + minutes + AllStrings.colon + seconds;// +AllStrings.colon + miliseconds;
    }

    public static string UsaDateTimeToString(DateTime d)
    {
        return d.Month + AllStrings.slash + d.Day + AllStrings.slash + d.Year + AllStrings.space + d.Hour + AllStrings.colon + d.Minute + AllStrings.colon + d.Second;// +AllStrings.colon + miliseconds;
    }

    public static bool EqualDateWithoutTime(DateTime dt1, DateTime dt2)
    {
        if (dt1.Day == dt2.Day && dt1.Month == dt2.Month && dt1.Year == dt2.Year)
        {
            return true;
        }
        return false;
    }
    #endregion

    public static List<string> GetOnlyNonNullValues(params string[] args)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            string text = args[i];
            object hodnota = args[++i];
            if (hodnota != null)
            {
                vr.Add(text);
                vr.Add(hodnota.ToString());
            }
        }
        return vr;
    }

    #region Get*ValueForType
    public static object GetMaxValueForType(Type id)
    {
        if (id == typeof(Byte))
        {
            return Byte.MaxValue;
        }
        else if (id == typeof(Decimal))
        {
            return Decimal.MaxValue;
        }
        else if (id == typeof(Double))
        {
            return Double.MaxValue;
        }
        else if (id == typeof(Int16))
        {
            return Int16.MaxValue;
        }
        else if (id == typeof(Int32))
        {
            return Int32.MaxValue;
        }
        else if (id == typeof(Int64))
        {
            return Int64.MaxValue;
        }
        else if (id == typeof(Single))
        {
            return Single.MaxValue;
        }
        else if (id == typeof(SByte))
        {
            return SByte.MaxValue;
        }
        else if (id == typeof(UInt16))
        {
            return UInt16.MaxValue;
        }
        else if (id == typeof(UInt32))
        {
            return UInt32.MaxValue;
        }
        else if (id == typeof(UInt64))
        {
            return UInt64.MaxValue;
        }
        throw new Exception("Nepovolen\u00FD nehodnotov\u00FD typ v metod\u011B GetMaxValueForType");
    }

    public static object GetMinValueForType(Type idt)
    {
        if (idt == typeof(Byte))
        {
            return 1;
        }
        else if (idt == typeof(Int16))
        {
            return Int16.MinValue;
        }
        else if (idt == typeof(Int32))
        {
            return Int32.MinValue;
        }
        else if (idt == typeof(Int64))
        {
            return Int64.MinValue;
        }
        else if (idt == typeof(SByte))
        {
            return SByte.MinValue;
        }
        else if (idt == typeof(UInt16))
        {
            return UInt16.MinValue;
        }
        else if (idt == typeof(UInt32))
        {
            return UInt32.MinValue;
        }
        else if (idt == typeof(UInt64))
        {
            return UInt64.MinValue;
        }
        throw new Exception("Nepovolen\u00FD nehodnotov\u00FD typ v metod\u011B GetMinValueForType");
    }
    #endregion


    public static List<byte> ClearEndingsBytes(List<byte> plainTextBytes)
    {
        List<byte> bytes = new List<byte>();
        bool pridavat = false;
        for (int i = plainTextBytes.Length() - 1; i >= 0; i--)
        {
            if (!pridavat && plainTextBytes[i] != 0)
            {
                pridavat = true;
                byte pridat = plainTextBytes[i];
                bytes.Insert(0, pridat);
            }
            else if (pridavat)
            {
                byte pridat = plainTextBytes[i];
                bytes.Insert(0, pridat);
            }
        }
        if (bytes.Count == 0)
        {
            for (int i = 0; i < plainTextBytes.Length(); i++)
            {
                plainTextBytes[i] = 0;
            }
            return plainTextBytes;
        }
        return bytes;
    }
}
