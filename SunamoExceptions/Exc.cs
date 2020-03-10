using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Reflection;

namespace SunamoExceptions
{
    public class Exc
    {
        #region For easy copy in SunamoException project
        public static bool aspnet = false;
        static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Remove GetStackTrace (first line
        /// </summary>
        /// <returns></returns>
        public static string GetStackTrace()
        {
            StackTrace st = new StackTrace();

            var v = st.ToString();
            var l = GetLines(v);
            Trim(l);
            l.RemoveAt(0);

            return JoinNL(l);
        }

        /// <summary>
        /// Print name of calling method, not GetCurrentMethod
        /// If is on end Test, will trim
        /// </summary>
        public static string CallingMethod(int v = 1)
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(v).GetMethod();
            var methodName = methodBase.Name;
            methodName = TrimEnd(methodName, "Test");
            return methodName;
        }

        #region MyRegion
        public static string TrimEnd(string name, string ext)
        {
            while (name.EndsWith(ext))
            {
                return name.Substring(0, name.Length - ext.Length);
            }
            return name;
        }

        public static string JoinNL(List<string> l)
        {
            return Join(Environment.NewLine, l);
        }

        public static string Join(string s, IList<string> l)
        {
            sb.Clear();
            foreach (var item in l)
            {
                sb.Append(item + Environment.NewLine);
            }
            return sb.ToString();
        }

        public static List<string> Trim(List<string> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                l[i] = l[i].Trim();
            }
            return l;
        }

        public static List<string> GetLines(string v)
        {
            var l = v.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return l;
        }
        #endregion
        #endregion
    }
}