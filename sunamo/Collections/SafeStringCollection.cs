using System.Collections.Generic;
using System.Text;

namespace sunamo.Collections
{
    public class SafeStringCollection
    {
        public List<string> safeStringCollection = new List<string>();
        private List<char> _unallowedChars = null;
        private char _replaceFor;

        public SafeStringCollection(List<char> unallowedChars, char replaceFor)
        {
            _unallowedChars = unallowedChars;
            _replaceFor = replaceFor;
        }

        public void Add(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in s)
            {
                char letter = item;

                if (_unallowedChars.Contains(item))
                {
                    letter = _replaceFor;
                }

                stringBuilder.Append(letter);
            }

            safeStringCollection.Add(stringBuilder.ToString());
        }
    }
}