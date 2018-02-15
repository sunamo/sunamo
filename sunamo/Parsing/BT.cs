/// <summary>
/// Nesmí obsahovat žádné statické metody, od toho tu je třída BTS
/// </summary>
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace sunamo
{
    public class ParseDefault
    {
        public class Byte
        {
            public byte ParseByte(string p, byte def)
            {
                byte p2;
                if (byte.TryParse(p, out p2))
                {
                    return p2;
                }
                return def;
            }
        }

        public class Integer
        {
            /// <summary>
            /// Vrátí A2 pokud se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            /// <param name="def"></param>
            /// <returns></returns>
            public int ParseInt(string p, int def)
            {
                int p2;
                if (int.TryParse(p, out p2))
                {
                    return p2;
                }
                return def;
            }
        }
    }



    public class Parse
    {
        public class Byte
        {
            public byte ParseByte(string p)
            {
                byte b;
                if (byte.TryParse(p, out b))
                {
                    return b;
                }
                return 0;
            }
        }



        public class Double
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            public double ParseDouble(string p)
            {
                double p2;
                if (double.TryParse(p, out p2))
                {
                    return p2;
                }
                return 0;
            }
        }

        public class Integer
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            public int ParseInt(string p)
            {
                int p2;
                if (int.TryParse(p, out p2))
                {
                    return p2;
                }
                return -1;
            }

            /// <summary>
            /// Vrátí int.MaxValue v případě že se nepodaří vyparsovat
            /// </summary>
            public int ParseIntMaxValue(string p)
            {
                int p2;
                if (int.TryParse(p, out p2))
                {
                    return p2;
                }
                return int.MaxValue;
            }
        }

        public class Short
        {


            /// <summary>
            /// Vrátí -1 pokud se nepodaří vyparsovat
            /// </summary>
            /// <param name="d"></param>
            /// <returns></returns>
            public short ParseShort(string d)
            {
                short s = 0;
                if (short.TryParse(d, out s))
                {
                    return s;
                }
                return -1;
            }

        }
    }

    public class BT
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Invert(bool b, bool really)
        {
            if (really)
            {
                return !b;
            }
            return b;
        }

        public static DateTime TryParseDateTime(string v, CultureInfo ciForParse, DateTime defaultValue)
        {
            DateTime vr = defaultValue;

            if (DateTime.TryParse(v, ciForParse, DateTimeStyles.None, out vr))
            {
                return vr;
            }
            return defaultValue;
        }

        public static bool GetValueOfNullable(bool? t)
        {
            if (t.HasValue)
            {
                return t.Value;
            }
            return false;
        }

        public const int MinValue = -2147483648;
        public const int MaxValue = 2147483647;

        public static List<int> CastToIntList(object[] d)
        {
            List<int> vr = new List<int>();
            foreach (string item in d)
            {
                vr.Add(int.Parse(item.ToString()));
            }
            return vr;
        }



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

        public static bool IsDateTime(string dt)
        {
            if (dt == null)
            {
                return false;
            }
            return DateTime.TryParse(dt, out lastDateTime);
        }


        /// <summary>
        /// 0 - false, all other - 1
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool IntToBool(object v)
        {
            return Convert.ToBoolean(int.Parse(v.ToString()));
        }

        public static bool EqualDateWithoutTime(DateTime dt1, DateTime dt2)
        {
            if (dt1.Day == dt2.Day && dt1.Month == dt2.Month && dt1.Year == dt2.Year)
            {
                return true;
            }
            return false;
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

        private const string Yes = "Yes";
        private const string No = "No";

        /// <summary>
        /// G bool repr. A1. Pro Yes true, JF.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool StringToBool(string s)
        {
            if (s == Yes) return true;
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

        public static string BoolToStringCs(bool p)
        {
            if (p) return "Ano";
            return "Ne";
        }

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

        public static bool IsInRange(int od, int to, int index)
        {
            return od >= index && to <= index;
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

        public object MakeUpTo3NumbersToZero(int p)
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

        public object MakeUpTo2NumbersToZero(int p)
        {
            if (p.ToString().Length == 1)
            {
                return "0" + p;
            }
            return p;
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

        public static int? ParseInt(string entry, int? _default)
        {
            int lastInt2 = 0;
            if (int.TryParse(entry, out lastInt2))
            {
                return lastInt2;
            }
            return _default;
        }

        /// <summary>
        /// POkud bude A1 null, vrátí int.MinValue
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

        public static bool lastBool = false;

        public static bool TryParseBool(string trim)
        {
            return bool.TryParse(trim, out lastBool);
        }

        public static bool TryParseInt(string entry)
        {
            // Pokud bude A1 null, výsledek bude false
            return int.TryParse(entry, out lastInt);

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

        public static byte[] ConvertFromUtf8ToBytes(string vstup)
        {
            return Encoding.UTF8.GetBytes(vstup);
        }

        public static string ConvertFromBytesToUtf8(byte[] bajty)
        {
            return Encoding.UTF8.GetString(bajty);
        }

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
            return day + "." + month + "." + year + " " + hour + ":" + minutes + ":" + seconds;// +":" + miliseconds;
        }

        public static string SameLenghtAllDates(DateTime dateTime)
        {
            string year = dateTime.Year.ToString();
            string month = SH.MakeUpToXChars(dateTime.Month, 2);
            string day = SH.MakeUpToXChars(dateTime.Day, 2);
            return day + "." + month + "." + year; // +" " + hour + ":" + minutes + ":" + seconds;// +":" + miliseconds;
        }

        public static string SameLenghtAllTimes(DateTime dateTime)
        {
            string hour = SH.MakeUpToXChars(dateTime.Hour, 2);
            string minutes = SH.MakeUpToXChars(dateTime.Minute, 2);
            string seconds = SH.MakeUpToXChars(dateTime.Second, 2);
            return hour + ":" + minutes + ":" + seconds;// +":" + miliseconds;
        }

        public static string UsaDateTimeToString(DateTime d)
        {
            return d.Month + "/" + d.Day + "/" + d.Year + " " + d.Hour + ":" + d.Minute + ":" + d.Second;// +":" + miliseconds;
        }

        public static byte[] ClearEndingsBytes(byte[] plainTextBytes)
        {
            List<byte> bytes = new List<byte>();
            bool pridavat = false;
            for (int i = plainTextBytes.Length - 1; i >= 0; i--)
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
                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextBytes[i] = 0;
                }
                return plainTextBytes;
            }
            return bytes.ToArray();
        }


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

        public static int TryParseInt(string entry, int def)
        {
            int lastInt = 0;
            if (int.TryParse(entry, out lastInt))
            {
                return lastInt;
            }
            return def;
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

        /// <summary>
        /// Pokud se bool nepodaří vyparsovat, vrátí false.
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

        public static int[] CastArrayStringToInt(string[] plemena)
        {
            int[] vr = new int[plemena.Length];
            for (int i = 0; i < plemena.Length; i++)
            {
                vr[i] = int.Parse(plemena[i]);
            }
            return vr;
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





        #region MyRegion
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


    }
}
