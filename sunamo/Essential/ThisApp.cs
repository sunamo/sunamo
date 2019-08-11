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
        public static bool check = false;
        public static Langs l = Langs.en;
        public static ResourcesHelper Resources;
        /// <summary>
        /// Name = Solution
        /// Project = Project
        /// </summary>
        public static string Name;
        /// <summary>
        /// Name = Solution
        /// Project = Project
        /// </summary>
        public static string Project;
        public static string _Name
        {
            get
            {
                return AllStrings.us + Name;
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
                if (StatusSetted == null)
                {
                    // For unit tests
                    //DebugLogger.Instance.WriteLine(st + ": " + format);
                }
                else
                {
                    StatusSetted(st, format);
                }
            }
        }

        public static void StatusFromText(string v)
        {
            var tom = AspNet.IsStatusMessage(ref v);
            SetStatus(tom, v);
        }
    }
}
