using System.Collections.Generic;
using System.Text;

namespace sunamo.Collections
{
    public class SafeStringCollection
    {
        public List<string> safeStringCollection = new List<string>();
        List<char> unallowedChars = null;
        char replaceFor;

        public SafeStringCollection(List<char> unallowedChars, char replaceFor)
        {
            this.unallowedChars = unallowedChars;
            this.replaceFor = replaceFor;
        }

        public void Add(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in s)
            {
                char letter = item;

                if (unallowedChars.Contains(item))
                {
                    letter = replaceFor;
                }

                stringBuilder.Append(letter);
            }

            safeStringCollection.Add(stringBuilder.ToString());
        }
    }
}
