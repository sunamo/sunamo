using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Essential
{

    public class ThisApp
    {

        public static Langs l = Langs.en;
        public static ResourcesHelper Resources;
        public static string Name;
        public static string _Name
        {
            get
            {
                return "_" + Name;
            }
        }

        public static readonly bool initialized = false;
        public static string Namespace = "";
        public static event SetStatusDelegate StatusSetted;

        public static void SetStatus(TypeOfMessage st, string status, params object[] args)
            {
            var format = SH.Format2(status, args);
            if (format.Trim() != string.Empty)
            {
                StatusSetted(st, format);
            }
         }

        
    }
}
