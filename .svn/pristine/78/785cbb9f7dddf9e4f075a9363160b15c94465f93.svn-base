using System;
using System.Collections.Generic;
using System.Globalization;

namespace sunamo
{
    public static class AppLangHelper
    {
        public static CultureInfo currentCulture = null;
        public static CultureInfo currentUICulture = null;
        /// <summary>
        /// Jazyky které si může zvolit sám uživatel
        /// V klíči je zkratka jazyku, v hodnotě pak její plný název
        /// </summary>
        static Dictionary<string, string> fixedLanguages = new Dictionary<string, string>();
        /// <summary>
        /// Texty, které jazyky 
        /// V klíči je dvou znakový název jazyku, v hodnotě pak texty "Podle nastaveného jazyka OS" a "Depending on the OS language" 
        /// </summary>
        static Dictionary<string, List<string>> systemLanguages = new Dictionary<string, List<string>>();
        /// <summary>
        /// V klíči je dvouznakový název jazyku, v hodnotě číslo tohoto jazyku, které je v třídě AppLang
        /// </summary>
        static Dictionary<string, byte> languageCodes = new Dictionary<string, byte>();
        /// <summary>
        /// Číslo 0
        /// </summary>
        const byte fixedC = 0;
        /// <summary>
        /// Číslo 1
        /// </summary>
        const byte systemC = 1;
        const byte dependingOnLanguage = 0;
        //const byte dependingOnCountry = 1;
        /// <summary>
        /// Text Podle nastaveného jazyka OS
        /// Ačkoliv má v názvu 0, používá se když typ jazyku není 0
        /// </summary>
        const string cs0 = "Podle nastaveného jazyka OS";
        //const string cs1 = "Podle nastaveného regionu";
        /// <summary>
        /// Text Depending on the OS language
        /// Ačkoliv má v názvu 0, používá se když typ jazyku není 0
        /// </summary>
        const string en0 = "Depending on the OS language";
        //const string en1 = "Depending on the country";
        public static AppLang selectedInCB = null;

        static AppLangHelper()
        {
            fixedLanguages.Add("cs", "Čeština");
            fixedLanguages.Add("en", "English");

            List<string> systemLanguageCS = new List<string>();
            systemLanguageCS.Add(cs0);
            List<string> systemLanguageEN = new List<string>();
            systemLanguageEN.Add(en0);
            systemLanguages.Add("cs", systemLanguageCS);
            systemLanguages.Add("en", systemLanguageEN);

        }

        /// <summary>
        /// Vrátí název jazyku například do ComboBoxu na změnu jazyka
        /// Vrací pokud A1.TYpe není 0 správný text podle jazyka OS 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="displayed"></param>
        /// <returns></returns>
        public static string ToString(AppLang actual)
        {
            string vr = "";
            //Langs l = Langs.cs;
            if (actual.Type == fixedC)
            {
                //l = (Langs)actual.Language;
                vr = fixedLanguages[((Langs)actual.Language).ToString()];
            }
            else
            {
                CultureInfo depending = null;
                if (actual.Language == dependingOnLanguage)
                {
                    depending = currentUICulture;
                }
                else
                {
                    depending = currentCulture;
                }
                //depending = CultureInfo.CurrentCulture;
                if (depending == null)
                {
                    if (actual.Language == dependingOnLanguage)
                    {
                        depending = CultureInfo.CurrentUICulture;
                    }
                    else
                    {
                        depending = CultureInfo.CurrentCulture;
                    }
                }

                if (depending.TwoLetterISOLanguageName == "cs")
                {
                    if (actual.Language == 0)
                    {
                        vr = cs0 + " - " + fixedLanguages[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];
                    }
                }
                else //if (depending.TwoLetterISOLanguageName == "en")
                {
                    if (actual.Language == 0)
                    {
                        vr = en0 + " - " + fixedLanguages[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];
                    }
                }
            }
            return vr;
        }

        /// <summary>
        /// Metoda která mi vrátí jazyk ve kterém se má obsah zobrazit
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Langs GetLang(string b)
        {
            Langs vr = Langs.cs;
            AppLang actual = AppLangConverter.ConvertTo(b);
            if (actual.Type == fixedC)
            {
                vr = (Langs)actual.Language;
            }
            else
            {
                if (actual.Language == 0)
                {
                    vr = GetLang2(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
                }
                else if (actual.Language == 1)
                {
                    vr = GetLang2(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
                }
            }
            return vr;
        }

        /// <summary>
        /// Používá se když chci vrátit jazyk ve výčtu Langs - A1 musí být obsah výčtu Langs, tedy cs nebo en
        /// 2 má v názvu proto že stejná metoda již existuje, ale ta mi vráti jazyk i podle OS
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Langs GetLang2(string p)
        {
            Langs vr = Langs.cs;
            if (Enum.TryParse<Langs>(p, out vr))
            {
                return vr;
            }
            return Langs.en;
        }

        /// <summary>
        /// Vrátí CultureInfo dané země bez specifikace jazyka pro jazyk A1
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static CultureInfo GetCi(Langs p)
        {
            CultureInfo ci = null;
            if (p == Langs.cs)
            {
                ci = new CultureInfo("cs");
            }
            else
            {
                ci = new CultureInfo("en");
            }

            return ci;
        }

        /// <summary>
        /// Vrátí mi vyčet Langs, tedy jazyk na základě A1
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static Langs GetLang(CultureInfo cultureInfo)
        {
            if (cultureInfo.TwoLetterISOLanguageName == "cs")
            {
                return Langs.cs;
            }
            else
            {
                return Langs.en;
            }
        }

        public static List<AppLang> ItemsToAddToComboBox(string settingsAl)
        {
            List<AppLang> vr = new List<AppLang>();
            selectedInCB = null;
            byte i = 0;
            foreach (var item in fixedLanguages)
            {
                AppLang al = new AppLang(fixedC, i);
                if (selectedInCB == null)
                {
                    if (AppLangConverter.ConvertFrom(al) == settingsAl)
                    {
                        selectedInCB = al;
                    }
                }
                vr.Add(al);
                i++;
            }
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
            {
                i = 0;
                foreach (var item in systemLanguages["cs"])
                {
                    AppLang al = new AppLang(systemC, i);
                    if (selectedInCB == null)
                    {
                        if (AppLangConverter.ConvertFrom(al) == settingsAl)
                        {
                            selectedInCB = al;
                        }
                    }
                    vr.Add(al);
                    i++;
                }
            }
            else
            {
                i = 0;
                foreach (var item in systemLanguages["en"])
                {
                    AppLang al = new AppLang(systemC, i);
                    if (selectedInCB == null)
                    {
                        if (AppLangConverter.ConvertFrom(al) == settingsAl)
                        {
                            selectedInCB = al;
                        }
                    }
                    vr.Add(al);

                    i++;
                }
            }
            return vr;
        }
    }
}
