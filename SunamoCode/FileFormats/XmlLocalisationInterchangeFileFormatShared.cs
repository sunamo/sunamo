using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SunamoCode
{
    public static partial class XmlLocalisationInterchangeFileFormat
    {
        /// <summary>
        /// was collection with previously existed properties in SunamoStrings class like sess.i18n(XlfKeys.EditUserAccount) 
        /// </summary>
        static readonly List<string> sunamoStrings = SH.GetLines(@"sess.i18n(XlfKeys.AddAsRsvp)
sess.i18n(XlfKeys.EditUserAccount)
sess.i18n(XlfKeys.UserDetail)
sess.i18n(XlfKeys.ErrorSerie255)
sess.i18n(XlfKeys.ErrorSerie0)
sess.i18n(XlfKeys.ViewLastWeek)
sess.i18n(XlfKeys.YouAreNotLogged)
sess.i18n(XlfKeys.YouAreBlocked)
sess.i18n(XlfKeys.TurnOnSelectingPhotos)
sess.i18n(XlfKeys.TurnOffSelectingPhotos)
sess.i18n(XlfKeys.StringNotFound)
sess.i18n(XlfKeys.NoRightArgumentsToPage)
sess.i18n(XlfKeys.YouAreNotLoggedAsWebAdmin)
sess.i18n(XlfKeys.YouHaveNotValidIPv4Address)
sess.i18n(XlfKeys.UriTooShort)
sess.i18n(XlfKeys.UriTooLong)
sess.i18n(XlfKeys.CustomShortUriOccupatedYet)
sess.i18n(XlfKeys.LinkSuccessfullyShorted)
sess.i18n(XlfKeys.UnauthorizedOperation)
sess.i18n(XlfKeys.Error)
sess.i18n(XlfKeys.Success)
sess.i18n(XlfKeys.RemoveFromFavoritesSuccess)
sess.i18n(XlfKeys.AddToFavoritesSuccess)
sess.i18n(XlfKeys.RemoveFromFavorites)
sess.i18n(XlfKeys.AddToFavorites)
sess.i18n(XlfKeys.RemoveAsRsvpSuccess)
sess.i18n(XlfKeys.RemoveAsRsvp)
sess.i18n(XlfKeys.AddAsRsvp)
sess.i18n(XlfKeys.DetailsClickSurveyAspxLabel)
sess.i18n(XlfKeys.UnvalidSession)
sess.i18n(XlfKeys.ScIsNotTheSame)
sess.i18n(XlfKeys.NotImplementedPleaseContactWebAdmin)
sess.i18n(XlfKeys.IsNotInRange)");

   
        /// <summary>
        /// XmlLocalisationInterchangeFileFormatSunamo.removeSessI18nIfLineContains
        /// </summary>
        public static List<string> removeSessI18nIfLineContains = CA.ToList<string>("MSStoredProceduresI");

        /// <summary>
        /// Before is possible use ReplaceRlDataToSessionI18n
        /// Was earlier in sunamo, now in SunamoCode
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        //public static string RemoveSessI18nIfLineContains(string c, params string[] lineCont)
        //{
        //    return RemoveSessI18nIfLineContainsWorker(c, removeSessI18nIfLineContains.ToArray());
        //}

        public static string RemoveSessI18nIfLineContains(string c)
        {
            return RemoveSessI18nIfLineContains(c, removeSessI18nIfLineContains);
        }

        /// <summary>
        /// Was earlier in sunamo, now in SunamoCode
        /// </summary>
        /// <param name="c"></param>
        /// <param name="lineCont"></param>
        /// <returns></returns>
        public static string RemoveSessI18nIfLineContains(string c, IEnumerable<string> lineCont = null)
        {
            if (lineCont == null || lineCont.Count() == 0)
            {
                lineCont = removeSessI18nIfLineContains;
            }

            c = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(c);

            var l = SH.GetLines(c);
            bool cont = false;
            for (int i = l.Count - 1; i >= 0; i--)
            {
                var line = l[i];
                cont = false;
                foreach (var item in lineCont)
                {
                    if (line.Contains(item))
                    {
                        cont = true;
                        break;
                    }
                }

                if (cont)
                {
                    l[i] = RemoveAllSessI18n(l[i]);
                }
            }

            return SH.JoinNL(l);
        }

        /// <summary>
        /// Before is possible use ReplaceRlDataToSessionI18n
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RemoveAllSessI18n(string c)
        {
            var sb = new StringBuilder(c);

            var sessI18n = XmlLocalisationInterchangeFileFormatSunamo.SessI18nShort;

            var occ = SH.ReturnOccurencesOfString(c, sessI18n);
            var ending = new List<int>(occ.Count);

            foreach (var item in occ)
            {
                ending.Add(c.IndexOf(AllChars.rb, item));
            }

            var l = sessI18n.Length;

            for (int i = occ.Count - 1; i >= 0; i--)
            {
                sb = sb.Remove(ending[i], 1);
                sb = sb.Remove(occ[i], l);
            }

            var result = sb.ToString();
            return result;
        }
        public static Type type = typeof(XmlLocalisationInterchangeFileFormat);

        public static string ReplaceRlDataToSessionI18n(string text)
        {
            return ReplaceRlDataToSessionI18n(text, SunamoNotTranslateAble.RLDataEn, SunamoNotTranslateAble.SessI18nShort);
        }

        public static string ReplaceRlDataToSessionI18n(string content, string from, string to)
        {
            var RLDataEn = SunamoNotTranslateAble.RLDataEn;
            var SessI18n = SunamoNotTranslateAble.SessI18nShort;
            var RLDataCs = SunamoNotTranslateAble.RLDataCs;

            char endingChar = AllChars.rsqb;
            string newEndingChar = AllStrings.rb;
            if (from == SessI18n)
            {
                endingChar = AllChars.rb;
                newEndingChar = AllStrings.rsqb;
            }
            else if (from == RLDataCs || from == RLDataEn)
            {
                // keep as is
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), from);
            }

            string SunamoStringsDot = XmlLocalisationInterchangeFileFormatSunamo.SunamoStringsDot;

            int dx = -1;

            foreach (var item in sunamoStrings)
            {
                dx = content.IndexOf(item);
                if (dx != -1)
                {
                    var line = SH.GetLineFromCharIndex(content, SH.GetLines(content), dx);
                    if (line.Contains(SunamoStringsDot))
                    {
                        content = content.Insert(dx + item.Count()
                            , newEndingChar);
                        content = content.Remove(dx, SunamoStringsDot.Length);
                        content = content.Insert(dx, to + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);
                    }
                }
            }

            var l = from.Length;

            content = content.Replace(XmlLocalisationInterchangeFileFormatSunamo.RLDataEn2, from);

            var occ = SH.ReturnOccurencesOfString(content, from);
            List<int> ending = new List<int>();
            foreach (var item in occ)
            {
                var io = content.IndexOf(endingChar, item);
                ending.Add(io);
            }

            StringBuilder sb = new StringBuilder(content);

            occ.Reverse();
            ending.Reverse();

            for (int i = 0; i < occ.Count; i++)
            {
                sb.Remove(occ[i], l);
                sb.Insert(occ[i], to);

                var ending2 = ending[i];
                sb.Remove(ending2, 1);
                sb.Insert(ending2, newEndingChar);
            }

            var c = sb.ToString();
            //TF.SaveFile(c, )
            return c;
        }
        public static string Id(XElement item)
        {
            return XHelper.Attr(item, "id");
        }
    }
}