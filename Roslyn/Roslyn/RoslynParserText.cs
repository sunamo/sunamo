using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// RoslynParser - use roslyn classes
/// RoslynParserText - no use roslyn classes, only text or indexer
/// </summary>
public class RoslynParserText
{
    public static CollectionWithoutDuplicates<string> Usings(List<string> lines, bool remove = false)
    {
        CollectionWithoutDuplicates<string> usings = new CollectionWithoutDuplicates<string>();
        List<int> removeLines = new List<int>();

        int i = -1;
        foreach (var item in lines)
        {
            i++;
            var line = item.Trim();
            if (line != string.Empty)
            {
                if (line.StartsWith("using "))
                {
                    removeLines.Add(i);
                    usings.Add(line);
                }
                else if (line.Contains(AllStrings.lcub))
                {
                    break;
                }
            }
        }

        if (remove)
        {
            CA.RemoveLines(lines, removeLines);
        }

        return usings;
    }

    private static void AddPageMethods(StringBuilder sb, List<string> files)
    {
        SourceCodeIndexerRoslyn Instance = SourceCodeIndexerRoslyn.Instance;

        foreach (var file in files)
        {
            Instance.ProcessFile(file, NamespaceCodeElementsType.Nope, ClassCodeElementsType.Method, false, false);
        }

        foreach (var file2 in Instance.classCodeElements)
        {
            sb.AppendLine(file2.Key);
            foreach (var method in file2.Value)
            {
                if (method.Name.StartsWith("On") || method.Name.StartsWith(sess.i18n(XlfKeys.Page) + "_"))
                {
                    sb.AppendLine(method.Name);
                }
            }
        }
    }

    public void FindPageMethod(string sczRootPath)
    {
        StringBuilder sb = new StringBuilder();

        List<string> project = new List<string>();

        var folders = FS.GetFolders(sczRootPath, SearchOption.TopDirectoryOnly);
        foreach (var item in folders)
        {
            string nameProject = FS.GetFileName(item);
            if (nameProject.EndsWith("X"))
            {
                string project2 = nameProject.Substring(0, nameProject.Length - 1);
                // General files is in Nope. GeneralX is only for pages in General folder
                if (project2 != XlfKeys.General)
                {
                    project.Add(project2);
                }

                var files = FS.GetFiles(item, FS.MascFromExtension(AllExtensions.cs), SearchOption.TopDirectoryOnly);
                AddPageMethods(sb, files);
            }
        }

        foreach (var item in project)
        {
            string path = FS.Combine(sczRootPath, item);
            var pages = FS.GetFiles(path, "*Page*.cs", SearchOption.TopDirectoryOnly);
            AddPageMethods(sb, pages);
        }

        ClipboardHelper.SetText(sb);
    }


}
