using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace sunamo
{
    /// <summary>
    /// Tato třída není statická jako ostatní convertery z důvodu že by se zbytečně využívali prostředky při startu aplikace, i když tuto třídu bych nakonec vůbec nevyužil.
    /// Snaž se prosím tuto třídu vytvářet jen jednou
    /// 
    /// 
    /// </summary>
    public sealed class PluralConverter : ISimpleConverter
    {
        /// <summary>
        /// Store irregular plurals in a dictionary
        /// </summary>
        private static Dictionary<string, string> s_dictionary = new Dictionary<string, string>();

        #region Constructors
        /// <summary>
        /// Run initialization on this singleton class
        /// </summary>
        public PluralConverter()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!s_dictionary.ContainsKey("afterlife"))
            {
                s_dictionary.Add("afterlife", "afterlives");
                s_dictionary.Add("alga", "algae");
                s_dictionary.Add("alumna", "alumnae");
                s_dictionary.Add("alumnus", "alumni");
                s_dictionary.Add("analysis", "analyses");
                s_dictionary.Add("antenna", "antennae");
                s_dictionary.Add("appendix", "appendices");
                s_dictionary.Add("axis", "axes");
                s_dictionary.Add("bacillus", "bacilli");
                s_dictionary.Add("basis", "bases");
                s_dictionary.Add(sess.i18n(XlfKeys.Bedouin), sess.i18n(XlfKeys.Bedouin));
                s_dictionary.Add("cactus", "cacti");
                s_dictionary.Add("calf", "calves");
                s_dictionary.Add("cherub", "cherubim");
                s_dictionary.Add("child", "children");
                s_dictionary.Add("cod", "cod");
                s_dictionary.Add("cookie", "cookies");
                s_dictionary.Add("criterion", "criteria");
                s_dictionary.Add("curriculum", "curricula");
                s_dictionary.Add("datum", "data");
                s_dictionary.Add("deer", "deer");
                s_dictionary.Add("diagnosis", "diagnoses");
                s_dictionary.Add("die", "dice");
                s_dictionary.Add("dormouse", "dormice");
                s_dictionary.Add("elf", "elves");
                s_dictionary.Add("elk", "elk");
                s_dictionary.Add("erratum", "errata");
                s_dictionary.Add("esophagus", "esophagi");
                s_dictionary.Add("fauna", "faunae");
                s_dictionary.Add("fish", "fish");
                s_dictionary.Add("flora", "florae");
                s_dictionary.Add("focus", "foci");
                s_dictionary.Add("foot", "feet");
                s_dictionary.Add("formula", "formulae");
                s_dictionary.Add("fundus", "fundi");
                s_dictionary.Add("fungus", "fungi");
                s_dictionary.Add("genie", "genii");
                s_dictionary.Add("genus", "genera");
                s_dictionary.Add("goose", "geese");
                s_dictionary.Add("grouse", "grouse");
                s_dictionary.Add("hake", "hake");
                s_dictionary.Add("half", "halves");
                s_dictionary.Add("headquarters", "headquarters");
                s_dictionary.Add("hippo", "hippos");
                s_dictionary.Add("hippopotamus", "hippopotami");
                s_dictionary.Add("hoof", "hooves");
                s_dictionary.Add("housewife", "housewives");
                s_dictionary.Add("hypothesis", "hypotheses");
                s_dictionary.Add("index", "indices");
                s_dictionary.Add("jackknife", "jackknives");
                s_dictionary.Add("knife", "knives");
                s_dictionary.Add("labium", "labia");
                s_dictionary.Add("larva", "larvae");
                s_dictionary.Add("leaf", "leaves");
                s_dictionary.Add("life", "lives");
                s_dictionary.Add("loaf", "loaves");
                s_dictionary.Add("louse", "lice");
                s_dictionary.Add("magus", "magi");
                s_dictionary.Add("man", "men");
                s_dictionary.Add("memorandum", "memoranda");
                s_dictionary.Add("midwife", "midwives");
                s_dictionary.Add("millennium", "millennia");
                s_dictionary.Add("moose", "moose");
                s_dictionary.Add("mouse", "mice");
                s_dictionary.Add("nebula", "nebulae");
                s_dictionary.Add("neurosis", "neuroses");
                s_dictionary.Add("nova", "novas");
                s_dictionary.Add("nucleus", "nuclei");
                s_dictionary.Add("oesophagus", "oesophagi");
                s_dictionary.Add("offspring", "offspring");
                s_dictionary.Add("ovum", "ova");
                s_dictionary.Add("ox", "oxen");
                s_dictionary.Add("papyrus", "papyri");
                s_dictionary.Add("passerby", "passersby");
                s_dictionary.Add("penknife", "penknives");
                s_dictionary.Add("person", "people");
                s_dictionary.Add("phenomenon", "phenomena");
                s_dictionary.Add("placenta", "placentae");
                s_dictionary.Add("pocketknife", "pocketknives");
                s_dictionary.Add("pupa", "pupae");
                s_dictionary.Add("radius", "radii");
                s_dictionary.Add("reindeer", "reindeer");
                s_dictionary.Add("retina", "retinae");
                s_dictionary.Add("rhinoceros", "rhinoceros");
                s_dictionary.Add("roe", "roe");
                s_dictionary.Add("salmon", "salmon");
                s_dictionary.Add("scarf", "scarves");
                s_dictionary.Add("self", "selves");
                s_dictionary.Add("seraph", "seraphim");
                s_dictionary.Add("series", "series");
                s_dictionary.Add("sheaf", "sheaves");
                s_dictionary.Add("sheep", "sheep");
                s_dictionary.Add("shelf", "shelves");
                s_dictionary.Add("species", "species");
                s_dictionary.Add("spectrum", "spectra");
                s_dictionary.Add("stimulus", "stimuli");
                s_dictionary.Add("stratum", "strata");
                s_dictionary.Add("supernova", "supernovas");
                s_dictionary.Add("swine", "swine");
                s_dictionary.Add("terminus", "termini");
                s_dictionary.Add("thesaurus", "thesauri");
                s_dictionary.Add("thesis", "theses");
                s_dictionary.Add("thief", "thieves");
                s_dictionary.Add("trout", "trout");
                s_dictionary.Add("vulva", "vulvae");
                s_dictionary.Add("wife", "wives");
                s_dictionary.Add("wildebeest", "wildebeest");
                s_dictionary.Add("wolf", "wolves");
                s_dictionary.Add("woman", "women");
                s_dictionary.Add("yen", "yen");
            }
        }
        #endregion //Constructors

        #region Methods
        /// <summary>
        /// Call this method to get the properly pluralized 
        /// English version of the word.
        /// </summary>
        /// <param name="word">The word needing conditional pluralization.</param>
        /// <param name="count">The number of items the word refers to.</param>
        /// <returns>The pluralized word</returns>
        public string ConvertTo(string word)
        {
            if (TestIsPlural(word) == true)
            {
                return word; //it's already a plural
            }
            else if (s_dictionary.ContainsKey(word.ToLower()))
            //it's an irregular plural, use the word from the dictionary
            {
                return s_dictionary[word.ToLower()];
            }
            if (word.Length <= 2)
            {
                return word; //not a word that can be pluralised!
            }
            ////1. If the word ends in a consonant plus -y, change the -y into
            /// ie and add an -s to form the plural 
            ///e.g. enemy--enemies baby--babies
            switch (word.Substring(word.Length - 2))
            {
                case "by":
                case "cy":
                case "dy":
                case "fy":
                case "gy":
                case "hy":
                case "jy":
                case "ky":
                case "ly":
                case "my":
                case "ny":
                case "py":
                case "ry":
                case "sy":
                case "ty":
                case "vy":
                case "wy":
                case "xy":
                case "zy":
                    {
                        return word.Substring(0, word.Length - 1) + "ies";
                    }
                case "is":
                    {
                        return word.Substring(0, word.Length - 1) + "es";
                    }
                case "ch":
                case "sh":
                    {
                        return word + "es";
                    }
                default:
                    {
                        switch (word.Substring(word.Length - 1))
                        {
                            case "s":
                            case "z":
                            case "x":
                                {
                                    return word + "es";
                                }
                            default:
                                {
                                    //4. Assume add an -s to form the plural of most words.
                                    return word + "s";
                                }
                        }
                    }
            }
        }

        /// <summary>
        /// Call this method to get the singular 
        /// version of a plural English word.
        /// </summary>
        /// <param name="word">The word to turn into a singular</param>
        /// <returns>The singular word</returns>
        public string ConvertFrom(string word)
        {
            word = word.ToLower();
            if (s_dictionary.ContainsValue(word))
            {
                foreach (KeyValuePair<string, string> kvp in s_dictionary)
                {
                    if (kvp.Value.ToLower() == word) return kvp.Key;
                }
            }
            if (word.Substring(word.Length - 1) != "s")
            {
                return word; // not a plural word if it doesn't end in S
            }
            if (word.Length <= 2)
            {
                return word; // not a word that can be made singular if only two letters!
            }
            if (word.Length >= 4)
            {
                switch (word.Substring(word.Length - 4))
                {
                    case "bies":
                    case "cies":
                    case "dies":
                    case "fies":
                    case "gies":
                    case "hies":
                    case "jies":
                    case "kies":
                    case "lies":
                    case "mies":
                    case "nies":
                    case "pies":
                    case "ries":
                    case "sies":
                    case "ties":
                    case "vies":
                    case "wies":
                    case "xies":
                    case "zies":
                        {
                            return word.Substring(0, word.Length - 3) + "y";
                        }
                    case "ches":
                    case "shes":
                        {
                            return word.Substring(0, word.Length - 2);
                        }
                }
            }

            if (word.Length >= 3)
            {
                switch (word.Substring(word.Length - 3))
                {
                    //box--boxes 
                    case "ses":
                    case "zes":
                    case "xes":
                        {
                            return word.Substring(0, word.Length - 2);
                        }
                }
            }
            if (word.Length >= 3)
            {
                switch (word.Substring(word.Length - 2))
                {
                    case "es":
                        {
                            return word.Substring(0, word.Length - 1) + "is";
                        }
                    //4. Assume add an -s to form the plural of most words.
                    default:
                        {
                            return word.Substring(0, word.Length - 1);
                        }
                }
            }
            return word;
        }
        /// <summary>
        /// test if a word is plural
        /// </summary>
        /// <param name="word">word to test</param>
        /// <returns>true if a word is plural</returns>
        private static bool TestIsPlural(string word)
        {
            word = word.ToLower();
            if (word.Length <= 2)
            {
                return false; // not a word that can be made singular if only two letters!
            }
            if (s_dictionary.ContainsValue(word.ToLower()))
            {
                return true; //it's definitely already a plural
            }
            if (word.Length >= 4)
            {
                switch (word.Substring(word.Length - 4))
                {
                    case "bies":
                    case "cies":
                    case "dies":
                    case "fies":
                    case "gies":
                    case "hies":
                    case "jies":
                    case "kies":
                    case "lies":
                    case "mies":
                    case "nies":
                    case "pies":
                    case "ries":
                    case "sies":
                    case "ties":
                    case "vies":
                    case "wies":
                    case "xies":
                    case "zies":
                    case "ches":
                    case "shes":
                        {
                            return true;
                        }
                }
            }

            if (word.Length >= 3)
            {
                switch (word.Substring(word.Length - 3))
                {
                    //box--boxes 
                    case "ses":
                    case "zes":
                    case "xes":
                        {
                            return true;
                        }
                }
            }
            if (word.Length >= 3)
            {
                switch (word.Substring(word.Length - 2))
                {
                    case "es":
                        {
                            return true;
                        }
                }
            }
            if (word.Substring(word.Length - 1) != "s")
            {
                return false; // not a plural word if it doesn't end in S
            }
            return true;
        }
        #endregion


    }
}