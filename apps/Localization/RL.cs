using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using sunamo.Enums;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using apps;
using sunamo;
using Windows.ApplicationModel.Resources;

namespace apps
{
    public static class RL 
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
        static ResourceLoader loader = ResourceLoader.GetForCurrentView("apps/Resources");
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
}
