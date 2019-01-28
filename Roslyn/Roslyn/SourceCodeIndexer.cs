using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using sunamo;
using sunamo.Constants;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class SourceCodeIndexer
{
    Type type = typeof(SourceCodeIndexer);
    //public Dictionary<string, TU<string, int>> foundedLines = new Dictionary<string, TU<string, int>>();
    /// <summary>
    /// In A1 is full path, in A2 lines with letter content
    /// </summary>
    public Dictionary<string, List<string>> linesWithContent = new Dictionary<string, List<string>>();
    public Dictionary<string, List<int>> linesWithIndexes = new Dictionary<string, List<int>>();

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
    //ClassCodeElements allClassCodeElements = ClassCodeElements.
    /// <summary>
    /// Map NamespaceCodeElementsType to keywords used in C#
    /// </summary>
    public static Dictionary<NamespaceCodeElementsType, string> e2sNamespaceCodeElements = EnumHelper.EnumToString<NamespaceCodeElementsType>(namespaceCodeElementsType2);

    public bool IsIndexed(string pathFile)
    {
        return namespaceCodeElements.ContainsKey(pathFile);
    }

    public SourceCodeIndexer()
    {
        var arr = Enum.GetValues(typeof(NamespaceCodeElementsType));
        foreach (NamespaceCodeElementsType item in arr)
        {
            if (item != NamespaceCodeElementsType.Nope && item != NamespaceCodeElementsType.Class)
            {
                allNamespaceCodeElements |= item;
            }
        }
    }

    public void ProcessAllCodeElementsInFiles(string file)
    {
        ProcessFile(file, allNamespaceCodeElements, allClassCodeElements);
    }

    public void ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType)
    {
        if (!linesWithContent.ContainsKey(pathFile))
        {
            if (pathFile.Contains("Projects\\Projects"))
            {
                Debugger.Break();
            }

            IEnumerable<NamespaceCodeElementsType> namespaceCodeElementsAll = EnumHelper.GetValues<NamespaceCodeElementsType>();
            IEnumerable<ClassCodeElementsType> classodeElementsAll = EnumHelper.GetValues<ClassCodeElementsType>();

            List<string> namespaceCodeElementsKeywords = new List<string>();
            List<string> classCodeElementsKeywords = new List<string>();

            string fileContent = File.ReadAllText(pathFile);
            List<string> linesAll = SH.GetLines(fileContent);
            List<string> lines = CA.WrapWith(linesAll, AllStrings.space).ToList();

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

            //for (int i = 0; i < lines.Count; i++)
            //{
            //    foundedLines.Add(pathFile, GetTU(lines[i], FullFileIndex[i]));
            //}

            linesWithContent.Add(pathFile, lines);
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


            SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
            var root = (CompilationUnitSyntax)tree.GetRoot();

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
        }
    }

    private void AddMethodsFrom(CSharpSyntaxNode ancestor, string pathFile)
    {
        // ancestor.DescendantNodes() returns all recursive
        //var cls = ancestor.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
        var cls = ancestor.ChildNodes().OfType<ClassDeclarationSyntax>().ToList();
        foreach (var classEl in cls)
        {
            var methods = classEl.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
            foreach (MethodDeclarationSyntax method in methods)
            {
                var location = method.GetLocation();
                FileLinePositionSpan fileLinePositionSpan = location.GetLineSpan();

                ClassCodeElement element = new ClassCodeElement() { Index = fileLinePositionSpan.StartLinePosition.Line, Name = method.Identifier.ToString(), Type = ClassCodeElementsType.Method };

                DictionaryHelper.AddOrCreate<string, ClassCodeElement>(classCodeElements, pathFile, element);
            }
        }


    }

    public Dictionary<string, List<int>> SearchInContent(string term, bool includeEmpty)
    {
        Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();
        bool include = false;

        foreach (var item in linesWithContent)
        {
            var indexes = linesWithIndexes[item.Key];
            include = false;
            // return with zero elements - in item.Value is only lines with content. I need lines with exactly content of file to localize searched results
            List<int> founded = CA.ReturnWhichContainsIndexes(item.Value, term, false);

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

            var founded2 = new List<int>();
            foreach (var item2 in founded)
            {
                founded2.Add(indexes[item2]);
            }

            if (include)
            {
                result.Add(item.Key, founded2);
            }
        }
        return result;
    }

    public CodeElements FindNamespaceElement(string text, NamespaceCodeElementsType type, ClassCodeElementsType classType, bool fixedSpace = true)
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
                if (makeChecking)
                {
                    add = false;

                    if (item2.Type == type)
                    {
                        // Nope there cannot be passed
                        add = true;
                    }
                }

                if (add)
                {
                    if (SH.Contains(item2.Name, text, fixedSpace))
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
                }

                if (add)
                {
                    if (SH.Contains(item2.Name, text, fixedSpace))
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

