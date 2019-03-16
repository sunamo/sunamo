using sunamo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


    /// <summary>
    /// Whole class copied from apps - want to use RL in any type of my apps
    /// </summary>
    public class RL
    {
        /// <summary>
        /// Pokud chceš používat tuto třídu, musíš zároveň prvně zavolat RL.Initialize()
        /// </summary>
        private static class Xaml
        {
            public static byte lid = 1;

            public static void Initialize(Langs l)
            {
                RL.Xaml.lid = (byte)l;
            }

        }

        public static string GetString(string k)
        {
            return loader.GetString(k);
        }

        private static class XmlLocalisationInterchangeFileFormat
        {
        }

        /// <summary>
        /// Globální proměnná pro nastavení jazyka celé app
        /// Musí to být výčet protože aplikace může mít více jazyků
        /// </summary>
        public static Langs l = Langs.en;
        static Dictionary<string, string> en = new Dictionary<string, string>();
        static Dictionary<string, string> cs = new Dictionary<string, string>();
        public static IResourceHelper loader = null;
        static int langsLength = 0;

        static RL()
        {
            langsLength = Enum.GetValues(typeof(Langs)).Length;
        }


        public static void Initialize(Langs l)
        {
            RL.l = l;
            AppLangHelper.currentCulture = new CultureInfo(l.ToString());
            AppLangHelper.currentUICulture = new CultureInfo(l.ToString());
        }
    }

