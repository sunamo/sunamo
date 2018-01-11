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
    /// </summary>
    public sealed class PluralConverter : ISimpleConverter
    {
        /// <summary>
        /// Store irregular plurals in a dictionary
        /// </summary>
        private static Dictionary<string, string> _dictionary = new Dictionary<string, string>();

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
            if (!_dictionary.ContainsKey("afterlife"))
            {
                _dictionary.Add("afterlife", "afterlives");
                _dictionary.Add("alga", "algae");
                _dictionary.Add("alumna", "alumnae");
                _dictionary.Add("alumnus", "alumni");
                _dictionary.Add("analysis", "analyses");
                _dictionary.Add("antenna", "antennae");
                _dictionary.Add("appendix", "appendices");
                _dictionary.Add("axis", "axes");
                _dictionary.Add("bacillus", "bacilli");
                _dictionary.Add("basis", "bases");
                _dictionary.Add("Bedouin", "Bedouin");
                _dictionary.Add("cactus", "cacti");
                _dictionary.Add("calf", "calves");
                _dictionary.Add("cherub", "cherubim");
                _dictionary.Add("child", "children");
                _dictionary.Add("cod", "cod");
                _dictionary.Add("cookie", "cookies");
                _dictionary.Add("criterion", "criteria");
                _dictionary.Add("curriculum", "curricula");
                _dictionary.Add("datum", "data");
                _dictionary.Add("deer", "deer");
                _dictionary.Add("diagnosis", "diagnoses");
                _dictionary.Add("die", "dice");
                _dictionary.Add("dormouse", "dormice");
                _dictionary.Add("elf", "elves");
                _dictionary.Add("elk", "elk");
                _dictionary.Add("erratum", "errata");
                _dictionary.Add("esophagus", "esophagi");
                _dictionary.Add("fauna", "faunae");
                _dictionary.Add("fish", "fish");
                _dictionary.Add("flora", "florae");
                _dictionary.Add("focus", "foci");
                _dictionary.Add("foot", "feet");
                _dictionary.Add("formula", "formulae");
                _dictionary.Add("fundus", "fundi");
                _dictionary.Add("fungus", "fungi");
                _dictionary.Add("genie", "genii");
                _dictionary.Add("genus", "genera");
                _dictionary.Add("goose", "geese");
                _dictionary.Add("grouse", "grouse");
                _dictionary.Add("hake", "hake");
                _dictionary.Add("half", "halves");
                _dictionary.Add("headquarters", "headquarters");
                _dictionary.Add("hippo", "hippos");
                _dictionary.Add("hippopotamus", "hippopotami");
                _dictionary.Add("hoof", "hooves");
                _dictionary.Add("housewife", "housewives");
                _dictionary.Add("hypothesis", "hypotheses");
                _dictionary.Add("index", "indices");
                _dictionary.Add("jackknife", "jackknives");
                _dictionary.Add("knife", "knives");
                _dictionary.Add("labium", "labia");
                _dictionary.Add("larva", "larvae");
                _dictionary.Add("leaf", "leaves");
                _dictionary.Add("life", "lives");
                _dictionary.Add("loaf", "loaves");
                _dictionary.Add("louse", "lice");
                _dictionary.Add("magus", "magi");
                _dictionary.Add("man", "men");
                _dictionary.Add("memorandum", "memoranda");
                _dictionary.Add("midwife", "midwives");
                _dictionary.Add("millennium", "millennia");
                _dictionary.Add("moose", "moose");
                _dictionary.Add("mouse", "mice");
                _dictionary.Add("nebula", "nebulae");
                _dictionary.Add("neurosis", "neuroses");
                _dictionary.Add("nova", "novas");
                _dictionary.Add("nucleus", "nuclei");
                _dictionary.Add("oesophagus", "oesophagi");
                _dictionary.Add("offspring", "offspring");
                _dictionary.Add("ovum", "ova");
                _dictionary.Add("ox", "oxen");
                _dictionary.Add("papyrus", "papyri");
                _dictionary.Add("passerby", "passersby");
                _dictionary.Add("penknife", "penknives");
                _dictionary.Add("person", "people");
                _dictionary.Add("phenomenon", "phenomena");
                _dictionary.Add("placenta", "placentae");
                _dictionary.Add("pocketknife", "pocketknives");
                _dictionary.Add("pupa", "pupae");
                _dictionary.Add("radius", "radii");
                _dictionary.Add("reindeer", "reindeer");
                _dictionary.Add("retina", "retinae");
                _dictionary.Add("rhinoceros", "rhinoceros");
                _dictionary.Add("roe", "roe");
                _dictionary.Add("salmon", "salmon");
                _dictionary.Add("scarf", "scarves");
                _dictionary.Add("self", "selves");
                _dictionary.Add("seraph", "seraphim");
                _dictionary.Add("series", "series");
                _dictionary.Add("sheaf", "sheaves");
                _dictionary.Add("sheep", "sheep");
                _dictionary.Add("shelf", "shelves");
                _dictionary.Add("species", "species");
                _dictionary.Add("spectrum", "spectra");
                _dictionary.Add("stimulus", "stimuli");
                _dictionary.Add("stratum", "strata");
                _dictionary.Add("supernova", "supernovas");
                _dictionary.Add("swine", "swine");
                _dictionary.Add("terminus", "termini");
                _dictionary.Add("thesaurus", "thesauri");
                _dictionary.Add("thesis", "theses");
                _dictionary.Add("thief", "thieves");
                _dictionary.Add("trout", "trout");
                _dictionary.Add("vulva", "vulvae");
                _dictionary.Add("wife", "wives");
                _dictionary.Add("wildebeest", "wildebeest");
                _dictionary.Add("wolf", "wolves");
                _dictionary.Add("woman", "women");
                _dictionary.Add("yen", "yen");
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
            else if (_dictionary.ContainsKey(word.ToLower()))
            //it's an irregular plural, use the word from the dictionary
            {
                return _dictionary[word.ToLower()];
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
            if (_dictionary.ContainsValue(word))
            {
                foreach (KeyValuePair<string, string> kvp in _dictionary)
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
        static public bool TestIsPlural(string word)
        {
            word = word.ToLower();
            if (word.Length <= 2)
            {
                return false; // not a word that can be made singular if only two letters!
            }
            if (_dictionary.ContainsValue(word.ToLower()))
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
