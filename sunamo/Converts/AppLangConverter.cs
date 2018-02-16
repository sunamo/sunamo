using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    /// <summary>
    /// AppLang/string
    /// </summary>
    public static class AppLangConverter //: ISimpleConverter<AppLang, string>
    {
        public static AppLang ConvertTo(string b)
        {
            return new AppLang(byte.Parse(b[0].ToString()), byte.Parse(b[1].ToString()));
        }

        public static string ConvertFrom(AppLang t)
        {
            return t.Type.ToString() + t.Language.ToString();
        }
    }
}
