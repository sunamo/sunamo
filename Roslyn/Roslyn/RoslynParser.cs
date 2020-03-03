using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using sunamo;
using sunamo.Collections;
using sunamo.Constants;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using HtmlAgilityPack;
using System.Web;
using System.Text.RegularExpressions;

namespace Roslyn
{
    public class RoslynParser
    {
        // TODO: take also usings

        static Type type = null;

        public static bool IsCSharpCode(string input)
        {
            SyntaxTree d = null;
            try
            {
                d = CSharpSyntaxTree.ParseText(input);
            }
            catch (Exception ex)
            {
                // throwed Method not found: 'Boolean Microsoft.CodeAnalysis.StackGuard.IsInsufficientExecutionStackException(System.Exception)'.' for non cs code
                
            }
            var s = d.GetText().ToString();
            return d != null;
        }

        public static MethodDeclarationSyntax Method(string item)
        {
            item = item + "{}";
            //StringReader sr = new StringReader(item);
            //CSharpSyntaxNode.DeserializeFrom();
            var tree = CSharpSyntaxTree.ParseText(item);
            var root = tree.GetRoot();
            //return (MethodDeclarationSyntax)root.DescendantNodesAndTokensAndSelf().OfType<MethodDeclarationSyntax>().FirstOrNull();

            // Only root I cannot cast -> cannot cast CSU to MethodDeclSyntax

            var childNodes = root.ChildNodes();
            return (MethodDeclarationSyntax)childNodes.First();
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
                    if (project2 != "General")
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

        private static void AddPageMethods(StringBuilder sb, List<string> files)
        {
            SourceCodeIndexerRoslyn indexer = new SourceCodeIndexerRoslyn();

            foreach (var file in files)
            {
                indexer.ProcessFile(file, NamespaceCodeElementsType.Nope, ClassCodeElementsType.Method, false, false);
            }

            foreach (var file2 in indexer.classCodeElements)
            {
                sb.AppendLine(file2.Key);
                foreach (var method in file2.Value)
                {
                    if (method.Name.StartsWith("On") || method.Name.StartsWith("Page" + "_"))
                    {
                        sb.AppendLine(method.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Úplně nevím k čemu toto mělo sloužit. 
        /// Read comments inside
        /// </summary>
        /// <param name="folderFrom"></param>
        /// <param name="folderTo"></param>
        
        public static ABC GetVariablesInCsharp(SyntaxNode root, List<string> lines, out CollectionWithoutDuplicates<string> usings)
        {
            ABC result = new ABC();
            usings = Usings(lines);

            ClassDeclarationSyntax helloWorldDeclaration = null;
            helloWorldDeclaration = RoslynHelper.GetClass(root);

            var variableDeclarations = helloWorldDeclaration.DescendantNodes().OfType<FieldDeclarationSyntax>();

            foreach (var variableDeclaration in variableDeclarations)
            {
                //Console.WriteLine(variableDeclaration.Variables.First().Identifier.);
                //Console.WriteLine(variableDeclaration.Variables.First().Identifier.Value);
                string variableName = variableDeclaration.Declaration.Type.ToString();
                variableName = SH.ReplaceOnce(variableName, "global" + "::", "");
                int lastIndex = variableName.LastIndexOf(AllChars.dot);
                string ns, cn;
                SH.GetPartsByLocation(out ns, out cn, variableName, lastIndex);
                usings.Add(ns);
                // in key type, in value name
                result.Add(AB.Get(cn, variableDeclaration.Declaration.Variables.First().Identifier.Text));

            }

            return result;
        }

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
                    if (line.StartsWith("using" + " "))
                    {
                        removeLines.Add(i);
                        usings.Add(line);
                    }
                    else if (line.Contains(AllStrings.cbl))
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

        public static string GetAccessModifiers(SyntaxTokenList modifiers )
        {
            foreach (var item in modifiers)
            {
                switch (item.Kind())
                {
                    case SyntaxKind.PublicKeyword:
                        
                    case SyntaxKind.PrivateKeyword:
                        
                    case SyntaxKind.InternalKeyword:
                        
                    case SyntaxKind.ProtectedKeyword:
                        return item.WithoutTrivia().ToFullString();
                }

            }
            return string.Empty;
        }

        
    }
}