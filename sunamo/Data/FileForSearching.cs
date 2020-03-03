using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class FileForSearching
    {
        public bool surelyNo = false;
        public CollectionWithoutDuplicates<int> foundedLines = new CollectionWithoutDuplicates<int>();
        public List<string> linesLower = null;
        public List<string> lines = null;

        public FileForSearching(string item)
        {
            lines = TF.GetLines(item);
            linesLower = CA.ToLower(lines);
        }
    }
}