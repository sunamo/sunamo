using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProjectsSearch.Data
{
    public class FileForSearching
    {
        public List<string> linesLower = null;
        public List<string> lines = null;
        public DateTime date = DateTime.MinValue;
        public bool surelyNo = false;
        public CollectionWithoutDuplicates<int> foundedLines = new CollectionWithoutDuplicates<int>();
    }
}
