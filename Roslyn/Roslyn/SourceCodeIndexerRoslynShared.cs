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

public partial class SourceCodeIndexerRoslyn{ 
public void ProcessFile(string file, bool fromFileSystemWatcher)
    {
        ProcessFile(file, NamespaceCodeElementsType.All, ClassCodeElementsType.All, false, fromFileSystemWatcher);
    }
public void ProcessFile(string pathFile, NamespaceCodeElementsType namespaceCodeElementsType, ClassCodeElementsType classCodeElementsType, bool removeRegions, bool fromFileSystemWatcher)
    {


        SyntaxTree tree;
        CompilationUnitSyntax root;
        if (ProcessFile(pathFile, namespaceCodeElementsType, classCodeElementsType, out tree, out root, removeRegions, fromFileSystemWatcher))
        {
            if (sourceFileTrees.ContainsKey(pathFile))
            {
                sourceFileTrees.Remove(pathFile);
            }

            sourceFileTrees.Add(pathFile, new SourceFileTree{root = root, tree = tree});
        }
    }
/// <summary>
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