using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static partial class BTS
    {

        #region Parse*
        public static bool lastBool = false;

        public static bool TryParseBool(string trim)
        {
            return bool.TryParse(trim, out lastBool);
        }


        #endregion

        #region TryParse*
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

        #region Parse*
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


        #endregion

    }

