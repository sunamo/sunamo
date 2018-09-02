using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Indexing.TextContent
{
    public class TextIndexing
    {
        /// <summary>
        /// In key is full path to file
        /// </summary>
        public Dictionary<string, FileForSearching> files = null;
        private bool v1;
        private bool v2;
        private bool v3;

        public TextIndexing(bool v1, bool v2, bool v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public void ReloadFiles(List<string> list)
        {
            files.Clear();

            foreach (var item in list)
            {
                files.Add(item, new FileForSearching(item));
            }
        }
    }
}
