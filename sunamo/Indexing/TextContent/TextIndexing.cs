using AllProjectsSearch.Data;
using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace sunamo.Indexing.TextContent
{
    public class TextIndexing
    {
        //Dictionary<string, string> files = new Dictionary<string, string>();
        public Dictionary<string, FileForSearching> files = new Dictionary<string, FileForSearching>();
        bool alsoLower = false;
        bool removeEmptyLines = false;
        bool trim = false;

        public TextIndexing(bool alsoLower, bool removeEmptyLines, bool trim)
        {
            this.alsoLower = alsoLower;
            this.removeEmptyLines = removeEmptyLines;
            this.trim = trim;
        }

        List<string> ProcessFileContent(string file, out List<string> lower)
        {
            // I can't remove empty lines because preview of content
            var result = TF.GetLines(file);
            if (removeEmptyLines)
            {
                result = CA.RemoveStringsEmpty(result);
            }
            if (trim)
            {
                result = CA.Trim(result);
            }

            if (alsoLower)
            {
                lower = CA.ToLower(result.ToList());
            }
            else
            {
                lower = null;
            }
            
            return result;
        }

        public void ReloadFiles(List<string> input)
        {
            foreach (var item in input)
            {
                DateTime fileModified = File.GetLastWriteTime(item);
                if (files.ContainsKey(item))
                {
                    var t = files[item];
                    if (fileModified != t.date)
                    {
                        t.lines = ProcessFileContent(item, out t.linesLower);

                        t.date = fileModified;
                    }
                }
                else
                {
                    FileForSearching t = new FileForSearching();
                    t.lines = ProcessFileContent(item, out t.linesLower);
                    t.date = fileModified;
                    files.Add(item, t);
                }
            }


        }

        public void KeepOnlyOneFileWithSameContent()
        {
            Dictionary<string, List<string>> filesToDelete = new Dictionary<string, List<string>>();

            foreach (var item in files)
            {
                filesToDelete.Add(item.Key, item.Value.lines);
            }

            FS.DeleteFilesWithSameContent(filesToDelete);
        }
    }
}
