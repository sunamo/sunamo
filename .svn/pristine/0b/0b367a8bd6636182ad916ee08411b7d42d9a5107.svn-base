/// <summary>
/// Base Types Static
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;

    public static partial class BTS
    {
        #region Castint to Array
        public static int[] CastArrayStringToInt(string[] plemena)
        {
            int[] vr = new int[plemena.Length];
            for (int i = 0; i < plemena.Length; i++)
            {
                vr[i] = int.Parse(plemena[i]);
            }
            return vr;
        }

        public static short[] CastArrayStringToShort(string[] plemena)
        {
            short[] vr = new short[plemena.Length];
            for (int i = 0; i < plemena.Length; i++)
            {
                vr[i] = short.Parse(plemena[i]);
            }
            return vr;
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
    }
