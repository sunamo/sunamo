using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn;
using Roslyn.Data;
using sunamo;
using sunamo.Constants;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static CsFileFilter;

public partial class SourceCodeIndexerRoslyn{ 

public void ProcessFile(string file, bool fromFileSystemWatcher)
    {
        ProcessFile(file, NamespaceCodeElementsType.All, ClassCodeElementsType.All, false, fromFileSystemWatcher);
    }

    public bool IsToIndexedFolder(string pathFile, bool alsoEnds)
    {
        var uf = UnindexableFiles.uf;
        bool end2 = false;

        if (pathFile.Contains(@"\_\"))
        {

        }

        if (!CsFileFilter.AllowOnly(pathFile, endArgs, containsArgs, ref end2, alsoEnds))
        {
            if (end2)
            {
                uf.unindexablePathEndsFiles.Add(pathFile);
            }
            else
            {
                uf.unindexablePathPartsFiles.Add(pathFile);
            }

            return false;
        }

        if (CA.StartWith(pathStarts, pathFile) != null)
        {
            uf.unindexablePathStartsFiles.Add(pathFile);

            return false;
        }

        return true;
    }

    public bool IsToIndexed(string pathFile)
    {
        #region All 4 for which is checked
        if (CA.ReturnWhichContainsIndexes(endsOther, pathFile, SearchStrategy.FixedSpace).Count > 0)
        {
            return false;
        }

        var uf = UnindexableFiles.uf;

        var fn = FS.GetFileName(pathFile);
        if (CA.ReturnWhichContainsIndexes(fileNames, fn, SearchStrategy.FixedSpace).Count > 0)
        {
            uf.unindexableFileNamesFiles.Add(pathFile);

            return false;
        }

        #endregion

        return IsToIndexedFolder(pathFile, true);
    }

    public bool isCallingIsToIndexed = false;

    /// <summary>
    /// SourceCodeIndexerRoslyn.ProcessFile
    /// 
    /// True if file wasnt indexed yet
    /// False is file was already indexed
    /// </summary>
    /// <param name = "pathFile"></param>
    /// <param name = "namespaceCodeElementsType"></param>
    /// <param name = "classCodeElementsType"></param>
    /// <param name = "tree"></param>
    /// <param name = "root"></param>
    /// <param name = "removeRegions"></param>
    public bool ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType, out SyntaxTree tree, out CompilationUnitSyntax root, bool removeRegions, bool fromFileSystemWatcher)
    {
        tree = null;
        root = null;

        // A2 must be false otherwise read file twice
        if (!FS.ExistsFile(pathFile, false))
        {
            return false;
        }

        if (!isCallingIsToIndexed)
        {
            if (!IsToIndexed(pathFile))
            {
                return false;
            }
        }

        // is checking a little above
        //if (CA.EndsWith(pathFile, endsOther))
        //{
        //    return false;
        //}



        //if (!CsFileFilter.AllowOnlyContains(pathFile, new CsFileFilter.ContainsArgs( false, false, false)))
        //{
        //    return false;
        //}

        if (!linesWithContent.ContainsKey(pathFile))
        {
            //if (!fromFileSystemWatcher)
            //{
            //    // only would call ProcessFile again
            //    watchers.Start(pathFile);
            //}
            IEnumerable<NamespaceCodeElementsType> namespaceCodeElementsAll = EnumHelper.GetValues<NamespaceCodeElementsType>();
            IEnumerable<ClassCodeElementsType> classodeElementsAll = EnumHelper.GetValues<ClassCodeElementsType>();
            List<string> namespaceCodeElementsKeywords = new List<string>();
            List<string> classCodeElementsKeywords = new List<string>();
            string fileContent = string.Empty;

            IEnumerable<string> lines = null;
            if (fromFileSystemWatcher)
            {
                if (File.Exists(pathFile))
                {
                    lines = File.ReadAllLines(pathFile).ToList();
                }
            }
            else
            {
                lines = TF.ReadAllLines(pathFile);
            }

            fileContent = SH.JoinNL(lines);
            List<string> linesAll = SH.GetLines(fileContent);
            linesAll = CA.WrapWith(linesAll, AllStrings.space).ToList();
            List<int> FullFileIndex = new List<int>();
            for (int i = linesAll.Count - 1; i >= 0; i--)
            {
                string item = linesAll[i];
                if (!SH.HasLetter(item))
                {
                    linesAll.RemoveAt(i);
                }
                else
                {
                    FullFileIndex.Add(i);
                }
            }
            FullFileIndex.Reverse();
            ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(),type, "ProcessFile", "lines", linesAll.Count, "FullFileIndex", FullFileIndex.Count);
            // Probably was add on background again due to watch for changes
            if (linesWithContent.ContainsKey(pathFile))
            {
                linesWithContent.Remove(pathFile);
            }
            linesWithContent.Add(pathFile, linesAll);
            if (linesWithIndexes.ContainsKey(pathFile))
            {
                linesWithIndexes.Remove(pathFile);
            }

             linesWithIndexes.AddIfNotExists(pathFile, FullFileIndex);
            foreach (var item in namespaceCodeElementsAll)
            {
                if (namespaceCodeElementsType.HasFlag(item))
                {
                    namespaceCodeElementsKeywords.Add(SH.WrapWith(item.ToString().ToLower(), AllChars.space));
                }
            }

            foreach (var item in namespaceCodeElementsKeywords)
            {
                string elementTypeString = item.Trim();
                NamespaceCodeElementsType namespaceCodeElementType = (NamespaceCodeElementsType)Enum.Parse(namespaceCodeElementsType2, item, true);
                List<int> indexes;
                List<string> linesCodeElements = CA.ReturnWhichContains(linesAll, item, out indexes);
                for (int i = 0; i < linesCodeElements.Count; i++)
                {
                    var lineCodeElements = linesCodeElements[i];
                    string namespaceElementName = SH.WordAfter(lineCodeElements, e2sNamespaceCodeElements[namespaceCodeElementType]);
                    if (namespaceElementName.Length > 1)
                    {
                        if (char.IsUpper(namespaceElementName[0]))
                        {
                            NamespaceCodeElement element = new NamespaceCodeElement()
                            {Index = FullFileIndex[indexes[i]], Name = namespaceElementName, Type = namespaceCodeElementType};
                            DictionaryHelper.AddOrCreate<string, NamespaceCodeElement>(namespaceCodeElements, pathFile, element);
                        }
                    }
                }
            }

            ClassCodeElementsType classCodeElementsTypeToFind = ClassCodeElementsType.All;
            if (classCodeElementsType.HasFlag(ClassCodeElementsType.All))
            {
                classCodeElementsTypeToFind |= ClassCodeElementsType.Method;
            }

            tree = CSharpSyntaxTree.ParseText(fileContent);
            root = (CompilationUnitSyntax)tree.GetRoot();
            var c = classCodeElements;
            var ns = root.DescendantNodes();
            IEnumerable<NamespaceDeclarationSyntax> namespaces = ns.OfType<NamespaceDeclarationSyntax>().ToList();
            foreach (var nameSpace in namespaces)
            {
                if (classCodeElementsTypeToFind.HasFlag(ClassCodeElementsType.Method))
                {
                    var ancestor = nameSpace;
                    AddMethodsFrom(ancestor, pathFile);
                }
            }
            AddMethodsFrom(root, pathFile);
            return true;
        }
        return false;
    }
}