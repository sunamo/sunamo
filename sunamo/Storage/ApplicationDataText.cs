using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Storage
{
    public class ApplicationDataText
    {
        static Type type = typeof(ApplicationDataText);

        /// <summary>
        /// If file conta
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> Parse(string file, List<string> sections)
        {
            Dictionary<string, List<string>> v = new Dictionary<string, List<string>>();
            List<string> lines = TF.GetLines(file);
            CA.Trim(lines);
            List<string> listString = new List<string>();
            int i = 0;
            foreach (var item in lines)
            {
                if (CA.IsSomethingTheSame(item, sections))
                {
                    CA.RemoveStringsEmpty(listString);
                    v.Add(sections[i++], listString);
                    listString = new List<string>();

                    continue;
                }
                listString.Add(item);
            }
            CA.RemoveStringsEmpty(listString);
            v.Add(sections[i++], listString);

            ThrowExceptions.DifferentCountInLists(type, "Parse", "sections", sections.Count, "v", v.Count);
            return v;
        }
    }
}
