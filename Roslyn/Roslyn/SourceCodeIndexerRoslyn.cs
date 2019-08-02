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

public class SourceCodeIndexerRoslyn
{
    /// <summary>
    /// Syntax root is the same as root - contains all code (include usings)
    /// </summary>
    public Dictionary<string, SourceFileTree> sourceFileTrees = new Dictionary<string, SourceFileTree>();
    Type type = typeof(SourceCodeIndexerRoslyn);
    //public Dictionary<string, TU<string, int>> foundedLines = new Dictionary<string, TU<string, int>>();
    /// <summary>
    /// In A1 is full path, in A2 lines with letter content
    /// </summary>
    public Dictionary<string, List<string>> linesWithContent = new Dictionary<string, List<string>>();
    public Dictionary<string, List<int>> linesWithIndexes = new Dictionary<string, List<int>>();

    public void Nuke()
    {
        linesWithContent.Clear();
        linesWithIndexes.Clear();
        sourceFileTrees.Clear();
        namespaceCodeElements.Clear();
        classCodeElements.Clear();

    }

    /// <summary>
    /// Type of NamespaceCodeElementsType
    /// </summary>
    static Type namespaceCodeElementsType2 = typeof(NamespaceCodeElementsType);
    /// <summary>
    /// In key are full file path, in value parsed code elements
    /// </summary>
    public Dictionary<string, List<NamespaceCodeElement>> namespaceCodeElements = new Dictionary<string, List<NamespaceCodeElement>>();
    /// <summary>
    /// In key are full file path, in value parsed code elements
    /// </summary>
    public Dictionary<string, List<ClassCodeElement>> classCodeElements = new Dictionary<string, List<ClassCodeElement>>();
    /// <summary>
    /// All code elements
    /// </summary>
    NamespaceCodeElementsType allNamespaceCodeElements = NamespaceCodeElementsType.Class;
    ClassCodeElementsType allClassCodeElements = ClassCodeElementsType.Method;
    
    /// <summary>
    /// Map NamespaceCodeElementsType to keywords used in C#
    /// </summary>
    public static Dictionary<NamespaceCodeElementsType, string> e2sNamespaceCodeElements = EnumHelper.EnumToString<NamespaceCodeElementsType>(namespaceCodeElementsType2);
    /// <summary>
    /// After first init must set to true
    /// </summary>
    public bool init = false;
    public FileSystemWatchers watchers = null;

    public void RemoveFile(string t, bool fromFileSystemWatcher = false)
    {
        linesWithContent.Remove(t);
        linesWithIndexes.Remove(t);
        sourceFileTrees.Remove(t);
        classCodeElements.Remove(t);
        namespaceCodeElements.Remove(t);

        if (!fromFileSystemWatcher)
        {
            // will be raise in FileSystemWatchers
            watchers.Stop(t);
        }
    }

    public bool IsIndexed(string pathFile)
    {
        return namespaceCodeElements.ContainsKey(pathFile);
    }

    public SourceCodeIndexerRoslyn()
    {
        var arr = Enum.GetValues(typeof(NamespaceCodeElementsType));
        foreach (NamespaceCodeElementsType item in arr)
        {
            if (item != NamespaceCodeElementsType.Nope && item != NamespaceCodeElementsType.Class)
            {
                allNamespaceCodeElements |= item;
            }
        }

        watchers = new FileSystemWatchers(false, ProcessFile, RemoveFile);
    }

    public void ProcessFile(string file, bool fromFileSystemWatcher)
    {
        
            ProcessFile(file, NamespaceCodeElementsType.All, ClassCodeElementsType.All, false, fromFileSystemWatcher);
        
    }

    public void ProcessAllCodeElementsInFiles(string file, bool fromFileSystemWatcher, bool removeRegions = false)
    {
        ProcessFile(file, allNamespaceCodeElements, allClassCodeElements, removeRegions, fromFileSystemWatcher);
    }

    public void ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType, bool removeRegions, bool fromFileSystemWatcher)
    {
        if (init)
        {
            RemoveFile(pathFile);
        }
        
        SyntaxTree tree;
        CompilationUnitSyntax root;
        if (ProcessFile(pathFile, namespaceCodeElementsType, classCodeElementsType, out tree, out root, removeRegions, fromFileSystemWatcher))
        {
            if (sourceFileTrees.ContainsKey(pathFile))
            {
                sourceFileTrees.Remove(pathFile);
            }

            sourceFileTrees.Add(pathFile, new SourceFileTree { root = root, tree = tree });
        }
    }

    /// <summary>
    /// True if file wasnt indexed yet
    /// False is file was already indexed
    /// </summary>
    /// <param name="pathFile"></param>
    /// <param name="namespaceCodeElementsType"></param>
    /// <param name="classCodeElementsType"></param>
    /// <param name="tree"></param>
    /// <param name="root"></param>
    /// <param name="removeRegions"></param>
    public bool ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType, out SyntaxTree tree, out CompilationUnitSyntax root,  bool removeRegions, bool fromFileSystemWatcher)
    {
        tree = null;
        root = null;

        if (!RoslynHelper.AllowOnly(pathFile, false, true, true, false, false))
        {
            return false;
        }

        if (!RoslynHelper.AllowOnlyContains(pathFile, false, false))
        {
            return false;
        }

            if (!linesWithContent.ContainsKey(pathFile))
        {
            if (!fromFileSystemWatcher)
            {
                // only would call ProcessFile again
                watchers.Start(pathFile);
            }

            IEnumerable<NamespaceCodeElementsType> namespaceCodeElementsAll = EnumHelper.GetValues<NamespaceCodeElementsType>();
            IEnumerable<ClassCodeElementsType> classodeElementsAll = EnumHelper.GetValues<ClassCodeElementsType>();

            List<string> namespaceCodeElementsKeywords = new List<string>();
            List<string> classCodeElementsKeywords = new List<string>();

            string fileContent = string.Empty;
            List<string> lines = TF.ReadAllLines(pathFile);

            fileContent = SH.JoinNL(lines);

            List<string> linesAll = SH.GetLines(fileContent);
            lines = CA.WrapWith(linesAll, AllStrings.space).ToList();

            List<int> FullFileIndex = new List<int>();

            for (int i = lines.Count - 1; i >= 0; i--)
            {
                string item = lines[i];
                if (!SH.HasLetter(item))
                {
                    lines.RemoveAt(i);

                }
                else
                {
                    FullFileIndex.Add(i);
                }
            }
            FullFileIndex.Reverse();

            ThrowExceptions.DifferentCountInLists(type, "ProcessFile", "lines", lines.Count, "FullFileIndex", FullFileIndex.Count);

            // Probably was add on background again due to watch for changes

            if (linesWithContent.ContainsKey(pathFile))
            {
                linesWithContent.Remove(pathFile);
            }

            linesWithContent.Add(pathFile, lines);

            if (linesWithIndexes.ContainsKey(pathFile))
            {
                linesWithIndexes.Remove(pathFile);
            }
            linesWithIndexes.Add(pathFile, FullFileIndex);

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
                List<string> linesCodeElements = CA.ReturnWhichContains(lines, item, out indexes);
                for (int i = 0; i < linesCodeElements.Count; i++)
                {
                    var lineCodeElements = linesCodeElements[i];
                    string namespaceElementName = SH.WordAfter(lineCodeElements, e2sNamespaceCodeElements[namespaceCodeElementType]);
                    if (namespaceElementName.Length > 1)
                    {
                        if (char.IsUpper(namespaceElementName[0]))
                        {
                            NamespaceCodeElement element = new NamespaceCodeElement() { Index = FullFileIndex[indexes[i]], Name = namespaceElementName, Type = namespaceCodeElementType };

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

 

    private void AddMethodsFrom(CSharpSyntaxNode ancestor, string pathFile)
    {
        // ancestor.DescendantNodes() returns all recursive
        //var cls = ancestor.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
        var cls = ancestor.ChildNodes().OfType<ClassDeclarationSyntax>().ToList();
        foreach (var classEl in cls)
        {
            var methods = classEl.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
            foreach (MethodDeclarationSyntax method2 in methods)
            {
                // cant .WithoutTrailingTrivia().WithoutLeadingTrivia() - Specified argument was out of the range of valid values.'
                var method = method2;

                var s = method.Span;
                
                var location = method.GetLocation();
                FileLinePositionSpan fileLinePositionSpan = location.GetLineSpan();

                string methodName = method.Identifier.ToString();
                ClassCodeElement element = new ClassCodeElement() { Index = fileLinePositionSpan.StartLinePosition.Line, Name = methodName, Type = ClassCodeElementsType.Method,
                From = s.Start, To = s.End, Length = s.Length, Member = method };

                //if (methodName == "JoinSpace")
                //{
                //    //DebugLogger.Instance.WriteLine(RH.DumpAsString("During indexing:", method.FullSpan));
                //}

                DictionaryHelper.AddOrCreate<string, ClassCodeElement>(classCodeElements, pathFile, element);
            }
        }


    }

    public Dictionary<string, List<FoundedCodeElement>> SearchInContent(string term, bool includeEmpty)
    {
        Dictionary<string, List<FoundedCodeElement>> result = new Dictionary<string, List<FoundedCodeElement>>();
        bool include = false;

        foreach (var item in linesWithContent)
        {
#if DEBUG
            if (FS.GetFileName( item.Key) == "MainWindow.cs")
            {

            }
#endif

            var indexes = linesWithIndexes[item.Key];
            include = false;
            // return with zero elements - in item.Value is only lines with content. I need lines with exactly content of file to localize searched results
            List<int> founded = CA.ReturnWhichContainsIndexes(item.Value, term, SearchStrategy.AnySpaces);

            if (founded.Count == 0)
            {
                if (includeEmpty)
                {
                    include = true;
                }
            }
            else
            {
                include = true;
            }

            var founded2 = new List<FoundedCodeElement>();
            foreach (var item2 in founded)
            {
                founded2.Add(new FoundedCodeElement(indexes[item2], -1, 0));
            }

            if (include)
            {
                result.Add(item.Key, founded2);
            }
        }
        return result;
    }

    /// <summary>
    /// A4 = search for exact occur. otherwise split both to words
    /// </summary>
    /// <param name="text"></param>
    /// <param name="type"></param>
    /// <param name="classType"></param>
    /// <param name="searchStrategy"></param>
    /// <returns></returns>
    public CodeElements FindNamespaceElement(string text, NamespaceCodeElementsType type, ClassCodeElementsType classType, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        bool makeChecking = type != NamespaceCodeElementsType.All;
        Dictionary<string, NamespaceCodeElements> result = new Dictionary<string, NamespaceCodeElements>();
        Dictionary<string, ClassCodeElements> resultClass = new Dictionary<string, ClassCodeElements>();
        bool add = true;

        foreach (var item in namespaceCodeElements)
        {
            NamespaceCodeElements d = new NamespaceCodeElements();

            foreach (var item2 in item.Value)
            {
                if (item.Key.Contains("ItemWithCount"))
                {

                }

                if (makeChecking)
                {
                    add = false;

                    if (item2.Type == type)
                    {
                        // Nope there cannot be passed
                        add = true;
                    }
                    else if (classType == ClassCodeElementsType.All)
                    {
                        add = true;
                    }
                }

                if (add)
                {
                    if (SH.Contains(item2.NameWithoutGeneric, text, searchStrategy))
                    {
                        d.Add(item2);
                    }
                }
            }

            if (d.Count > 0)
            {
                result.Add(item.Key, d);
            }
        }

        foreach (var item in classCodeElements)
        {
            ClassCodeElements d = new ClassCodeElements();

            foreach (var item2 in item.Value)
            {
                if (makeChecking)
                {
                    add = false;

                    if (item2.Type == classType)
                    {
                        // Nope there cannot be passed
                        add = true;
                    }
                    else if (classType == ClassCodeElementsType.All)
                    {
                        add = true;
                    }
                }

                if (add)
                {
                    if (SH.Contains(item2.NameWithoutGeneric, text, searchStrategy))
                    {
                        d.Add(item2);
                    }
                }
            }

            if (d.Count > 0)
            {
                resultClass.Add(item.Key, d);
            }
        }

        return new CodeElements() { classes = resultClass, namespaces = result };
    }
}

