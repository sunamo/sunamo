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
public partial class SourceCodeIndexerRoslyn
{
    #region Working method
    public void ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType, bool removeRegions, bool fromFileSystemWatcher)
    {
        bool removed = false;

        SyntaxTree _tree;
        CompilationUnitSyntax root;
        if (ProcessFile(pathFile, namespaceCodeElementsType, classCodeElementsType, out _tree, out root, removeRegions, fromFileSystemWatcher))
        {
            if (sourceFileTrees.ContainsKey(pathFile))
            {
                removed = true;
                sourceFileTrees.Remove(pathFile);
            }
            else
            {
                // Watcher.Start I can call after loading all files - otherwise there are million of events and app never start
                // Check whether folder is already indexing
                //if (!watchers.IsIndexindDirectory(pathFile))
                //{
                //watchers.Start(FS.GetDirectoryName( pathFile));
                //}
            }

            if (!sourceFileTrees.ContainsKey(pathFile))
            {
                sourceFileTrees.Add(pathFile, new SourceFileTree { root = root, tree = _tree });
            }
            else
            {
                sourceFileTrees[pathFile] = new SourceFileTree { root = root, tree = _tree };
            }
        }
    }

    public void RemoveFile(string t, bool fromFileSystemWatcher = false)
    {
        linesWithContent.Remove(t);
        linesWithIndexes.Remove(t);
        sourceFileTrees.Remove(t);
        classCodeElements.Remove(t);
        namespaceCodeElements.Remove(t);

        // watcher I cant stop, its one for all!!
        //if (!fromFileSystemWatcher)
        //{
        //    // will be raise in FileSystemWatchers
        //    watchers.Stop(t);
        //}
    } 
    #endregion

    /// <summary>
    /// Syntax root is the same as root - contains all code (include usings)
    /// </summary>
    public FsWatcherDictionary<string, SourceFileTree> sourceFileTrees = new FsWatcherDictionary<string, SourceFileTree>();
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
    public FsWatcherDictionary<string, List<NamespaceCodeElement>> namespaceCodeElements = new FsWatcherDictionary<string, List<NamespaceCodeElement>>();
    /// <summary>
    /// In key are full file path, in value parsed code elements
    /// </summary>
    public FsWatcherDictionary<string, List<ClassCodeElement>> classCodeElements = new FsWatcherDictionary<string, List<ClassCodeElement>>();
    /// <summary>
    /// All code elements
    /// </summary>
    NamespaceCodeElementsType allNamespaceCodeElements = NamespaceCodeElementsType.Class;
    ClassCodeElementsType allClassCodeElements = ClassCodeElementsType.Method;
    /// <summary>
    /// Map NamespaceCodeElementsType to keywords used in C#
    /// </summary>
    public static Dictionary<NamespaceCodeElementsType, string> e2sNamespaceCodeElements = EnumHelper.EnumToString<NamespaceCodeElementsType>(namespaceCodeElementsType2);
    
    public FileSystemWatchers watchers = null;

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

        watchers = new FileSystemWatchers( ProcessFile, RemoveFile);
    }

    public void ProcessAllCodeElementsInFiles(string file, bool fromFileSystemWatcher, bool removeRegions = false)
    {
        ProcessFile(file, allNamespaceCodeElements, allClassCodeElements, removeRegions, fromFileSystemWatcher);
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
                ClassCodeElement element = new ClassCodeElement()
                {Index = fileLinePositionSpan.StartLinePosition.Line, Name = methodName, Type = ClassCodeElementsType.Method, From = s.Start, To = s.End, Length = s.Length, Member = method};
                //if (methodName == "JoinSpace")
                //{
                //    ////DebugLogger.Instance.WriteLine(RH.DumpAsString("During indexing:", method.FullSpan));
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
    /// <param name = "text"></param>
    /// <param name = "type"></param>
    /// <param name = "classType"></param>
    /// <param name = "searchStrategy"></param>
    /// <returns></returns>
    public CodeElements FindNamespaceElement(string text, NamespaceCodeElementsType type, ClassCodeElementsType classType, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        bool makeChecking = type != NamespaceCodeElementsType.All;
        Dictionary<string, NamespaceCodeElements> result = new Dictionary<string, NamespaceCodeElements>();
        Dictionary<string, ClassCodeElements> resultClass = new Dictionary<string, ClassCodeElements>();
        bool add = true;

        if (type != NamespaceCodeElementsType.Nope)
        {

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
        }

        if (classType != ClassCodeElementsType.Nope)
        {
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
        }

        return new CodeElements()
        {classes = resultClass, namespaces = result};
    }
}