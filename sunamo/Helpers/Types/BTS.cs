/// <summary>
/// Base Types Static
/// </summary>
using System;
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
    /// <returns></returns>
    public static bool CompareAsObjectAndString(object tag2, object tag)
    {
        bool same = false;
        if (tag2 != null)
        {
            if (tag == tag2)
            {
                same = true;
            }
            else if (tag.ToString() == tag2.ToString())
            {
                same = true;
            }
        }
        return same;

    }

    /// <summary>
    ///  G zda  prvky A2 - Ax jsou hodnoty A1.
    /// </summary>
    /// <param name="hodnota"></param>
    /// <param name="paramy"></param>
    /// <returns></returns>
    public static bool IsAllEquals(bool hodnota, params bool[] paramy)
    {
        for (int i = 0; i < paramy.Length; i++)
        {
            if (hodnota != paramy[i])
            {
                return false;
            }
        }
        return true;

    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsInRange(int od, int to, int value)
    {
        return od >= value && to <= value;
    }

    /// <summary>
    /// If has value true, return true. Otherwise return false
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool GetValueOfNullable(bool? t)
    {
        if (t.HasValue)
        {
            return t.Value;
        }
        return false;
    }

    #region TryParse*
    public static DateTime TryParseDateTime(string v, CultureInfo ciForParse, DateTime defaultValue)
    {
        DateTime vr = defaultValue;

        if (DateTime.TryParse(v, ciForParse, DateTimeStyles.None, out vr))
        {
            return vr;
        }
        return defaultValue;
    }

    public static uint lastUint = 0;

    public static bool TryParseUint(string entry)
    {
        // Pokud bude A1 null, výsledek bude false
        return uint.TryParse(entry, out lastUint);
    }

    public static bool TryParseDateTime(string entry)
    {
        if (DateTime.TryParse(entry, out lastDateTime))
        {
            return true;
        }
        return false;
    }

    public static byte TryParseByte(string p1, byte _def)
    {
        byte vr = _def;
        if (byte.TryParse(p1, out vr))
        {
            return vr;

        }
        return _def;
    }

    

    /// <summary>
    /// Vrací vyparsovanou hodnotu pokud se podaří vyparsovat, jinak A2
    /// </summary>
    /// <param name="p"></param>
    /// <param name="_default"></param>
    /// <returns></returns>
    public static bool TryParseBool(string p, bool _default)
    {
        bool vr = _default;

        if (bool.TryParse(p, out vr))
        {
            return vr;
        }
        return _default;
    }

    public static int TryParseIntCheckNull(string entry, int def)
    {
        int lastInt = 0;
        if (entry == null)
        {
            return lastInt;
        }
        if (int.TryParse(entry, out lastInt))
        {
            return lastInt;
        }
        return def;
    }

    public static int TryParseInt(string entry, int def)
    {
        int lastInt = 0;
        if (int.TryParse(entry, out lastInt))
        {
            return lastInt;
        }
        return def;
    }
    #endregion

    #region int <> bool
    public static int BoolToInt(bool v)
    {
        return Convert.ToInt32(v);
    }

    /// <summary>
    /// 0 - false, all other - 1
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static bool IntToBool(int v)
    {
        return Convert.ToBoolean(v);
    }
    #endregion

    #region Parse*
    public static float ParseFloat(string ratingS)
    {
        float vr = float.MinValue;
        ratingS = ratingS.Replace(AllChars.comma, AllChars.dot);
        if (float.TryParse(ratingS, out vr))
        {
            return vr;
        }
        return vr;
    }

    /// <summary>
    /// Vrátí false v případě že se nepodaří vyparsovat
    /// </summary>
    /// <param name="displayAnchors"></param>
    /// <returns></returns>
    public static bool ParseBool(string displayAnchors)
    {
        bool vr = false;
        if (bool.TryParse(displayAnchors, out vr))
        {
            return vr;
        }
        return false;
    }

    /// <summary>
    /// Vrátí A2 v případě že se nepodaří vyparsovat
    /// </summary>
    /// <param name="displayAnchors"></param>
    /// <returns></returns>
    public static bool ParseBool(string displayAnchors, bool def)
    {
        bool vr = false;
        if (bool.TryParse(displayAnchors, out vr))
        {
            return vr;
        }
        return def;
    }

    public static int ParseInt(string entry, bool mustBeAllNumbers)
    {
        int d;
        if (!int.TryParse(entry, out d))
        {
            if (mustBeAllNumbers)
            {
                return int.MinValue;
            }
        }

        return d;
    }

    public static int ParseInt(string entry, int _default)
    {
        int lastInt2 = 0;
        if (int.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return _default;
    }

    /// <summary>
    /// POkud bude A1 nevyparsovatelné, vrátí int.MinValue
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static int ParseInt(string entry)
    {
        int lastInt2 = 0;
        if (int.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return int.MinValue;
    }

    public static short ParseShort(string entry)
    {
        short lastInt2 = 0;
        if (short.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return short.MinValue;
    }

    public static byte ParseByte(string entry)
    {
        byte lastInt2 = 0;
        if (byte.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return byte.MinValue;
    }

    public static byte ParseByte(string entry, byte def)
    {
        byte lastInt2 = 0;
        if (byte.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return def;
    }

    public static int? ParseInt(string entry, int? _default)
    {
        int lastInt2 = 0;
        if (int.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return _default;
    }
    #endregion

    #region Is*
    public static int lastInt = -1;
    public static DateTime lastDateTime = DateTime.MinValue;

    public static bool IsInt(string id)
    {
        if (id == null)
        {
            return false;
        }
        return int.TryParse(id, out lastInt);
    }

    public static bool IsDateTime(string dt)
    {
        if (dt == null)
        {
            return false;
        }
        return DateTime.TryParse(dt, out lastDateTime);
    }


    public static bool IsByte(string id, out byte b)
    {
        if (id == null)
        {
            b = 0;
            return false;
        }
        //byte b2 = 0;
        bool vr = byte.TryParse(id, out b);
        //b = b2;
        return vr;
    }


    public static bool IsBool(string trim)
    {
        if (trim == null)
        {
            return false;
        }
        return bool.TryParse(trim, out lastBool);
    }

    #endregion

    #region *To*
    /// <summary>
    /// 0 - false, all other - 1
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static bool IntToBool(object v)
    {
        return Convert.ToBoolean(int.Parse(v.ToString()));
    }

    private const string Yes = "Yes";
    private const string No = "No";

    /// <summary>
    /// G bool repr. A1. Pro Yes true, JF.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool StringToBool(string s)
    {
        if (s == Yes || s == bool.TrueString) return true;
        return false;
    }

    /// <summary>
    /// G str rep. pro A1 - Yes/No.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string BoolToString(bool p)
    {
        if (p) return Yes;
        return No;
    }

    public static string BoolToString(bool p, bool lower = false)
    {
        string vr = null;
        if (p)
            vr = Yes;
        else
        {
            vr = No;
        }

        return vr.ToLower();
    }


    #endregion

    #region byte[] <> string
    public static List<byte> ConvertFromUtf8ToBytes(string vstup)
    {
        return Encoding.UTF8.GetBytes(vstup).ToList();
    }

    public static string ConvertFromBytesToUtf8(List< byte> bajty)
    {
        NH.RemoveEndingZeroPadding(bajty);
        return Encoding.UTF8.GetString(bajty.ToArray());
    }
    #endregion

    #region Casting between array - cant commented because it wasnt visible between 
    public static string[] CastArrayObjectToString(object[] args)
    {
        string[] vr = new string[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            vr[i] = args[i].ToString();
        }
        return vr;
    }

    public static string[] CastArrayIntToString(int[] args)
    {
        string[] vr = new string[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            vr[i] = args[i].ToString();
        }
        return vr;
    }
    #endregion

    #region Castint to Array - commented, its in used only List
    //public static int[] CastArrayStringToInt(string[] plemena)
    //    {
    //        int[] vr = new int[plemena.Length];
    //        for (int i = 0; i < plemena.Length; i++)
    //        {
    //            vr[i] = int.Parse(plemena[i]);
    //        }
    //        return vr;
    //    }

    //    public static short[] CastArrayStringToShort(List<string> plemena)
    //    {
    //        short[] vr = new short[plemena.Count];
    //        for (int i = 0; i < plemena.Count; i++)
    //        {
    //            vr[i] = short.Parse(plemena[i]);
    //        }
    //        return vr;
    //    }

    //    public static string[] CastArrayObjectToString(object[] args)
    //    {
    //        string[] vr = new string[args.Length];
    //        for (int i = 0; i < args.Length; i++)
    //        {
    //            vr[i] = args[i].ToString();
    //        }
    //        return vr;
    //    }



    //public static string[] CastArrayIntToString(int[] args)
    //    {
    //        string[] vr = new string[args.Length];
    //        for (int i = 0; i < args.Length; i++)
    //        {
    //            vr[i] = args[i].ToString();
    //        }
    //        return vr;
    //    }
    #endregion

    #region Casting to List
    public static List<int> CastToIntList(object[] d)
        {
            List<int> vr = new List<int>();
            foreach (string item in d)
            {
                vr.Add(int.Parse(item.ToString()));
            }
            return vr;
        }

        /// <summary>
        /// Pokud se cokoliv nepodaří přetypovat, vyhodí výjimku
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<int> CastCollectionStringToInt(string[] p)
        {
            List<int> vr = new List<int>();
            foreach (string item in p)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    int nt;
                    if (int.TryParse(item, out nt))
                    {
                        vr.Add(int.Parse(item));
                    }
                }
            }
            return vr;
        }

        public static List<int> CastCollectionShortToInt(List<short> n)
        {
            List<int> vr = new List<int>();
            for (int i = 0; i < n.Count; i++)
            {
                vr.Add((int)n[i]);
            }
            return vr;
        }

        public static List<short> CastCollectionIntToShort(List<int> n)
        {
            List<short> vr = new List<short>(n.Count);
            for (int i = 0; i < n.Count; i++)
            {
                vr.Add((short)n[i]);
            }
            return vr;
        }

        public static List<int> CastListShortToListInt(List<short> n)
        {
            List<int> vr = new List<int>(n.Count);
            for (int i = 0; i < n.Count; i++)
            {
                vr.Add((short)n[i]);
            }
            return vr;
        }
        #endregion

        #region MakeUpTo*NumbersToZero
        public static object MakeUpTo3NumbersToZero(int p)
        {
            int d = p.ToString().Length;
            if (d == 1)
            {
                return "0" + p;
            }
            else if (d == 2)
            {
                return "00" + p;
            }
            return p;
        }

        public static object MakeUpTo2NumbersToZero(int p)
        {
            if (p.ToString().Length == 1)
            {
                return "0" + p;
            }
            return p;
        }


        #endregion

        #region GetNumberedList*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="max"></param>
    /// <param name="postfix"></param>
    /// <returns></returns>
        public static object[] GetNumberedListFromTo(int p, int max)
        {
            max++;
            List<object> vr = new List<object>();
            for (int i = 0; i < max; i++)
            {
                vr.Add(i);
            }
            return vr.ToArray();
        }

    public static List<string> GetNumberedListFromTo(int p, int max, string postfix = ". ")
    {
        max++;
        List<string> vr = new List<string>();
        for (int i = 0; i < max; i++)
        {
            vr.Add(i + postfix);
        }
        return vr;
    }

    private static List<string> GetNumberedListFromToList(int p, int indexOdNext)
        {
            List<string> vr = new List<string>();
            object[] o = GetNumberedListFromTo(p, indexOdNext);
            foreach (object item in o)
            {
                vr.Add(item.ToString());
            }
            return vr;
        }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string BoolToStringCs(bool p, bool lower = false)
    {
        string vr = null;
        if (p)
            vr = "Ano";
        else
        {
            vr = "Ne";
        }

        return vr.ToLower();
    }

    

    #region Ostatní
    /// <summary>
    /// Rok nezkracuje, počítá se standardním 4 místným
    /// Produkuje formát standardní s metodou DateTime.ToString()
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
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

        public static string[] GetOnlyNonNullValues(params string[] args)
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
            return vr.ToArray();
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
            throw new Exception("Nepovolený nehodnotový typ v metodě GetMaxValueForType");
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
            throw new Exception("Nepovolený nehodnotový typ v metodě GetMinValueForType");
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
