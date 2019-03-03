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
                    if (method.Name.StartsWith("On") || method.Name.StartsWith("Page_"))
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
        /// <returns></returns>
        public List<string> GetCodeOfElementsClass(string folderFrom, string folderTo)
        {
            FS.WithEndSlash(ref folderFrom);
            FS.WithEndSlash(ref folderTo);

            var files = FS.GetFiles(folderFrom, FS.MascFromExtension(".aspx.cs"), SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(TF.ReadFile(file));

                List<string> result = new List<string>();
                // Here probable it mean SpaceName, ale když není namespace, uloží třídu 
                SyntaxNode sn;
                var cl = RoslynHelper.GetClass(tree.GetRoot(),out sn);

                SyntaxAnnotation saSn = new SyntaxAnnotation();
                sn = sn.WithAdditionalAnnotations(saSn);
                
                SyntaxAnnotation saCl = new SyntaxAnnotation();
                cl = cl.WithAdditionalAnnotations(saCl);
                //ClassDeclarationSyntax cl2 = cl.Parent.)

                var root = tree.GetRoot();

                int count = cl.Members.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    var item = cl.Members[i];
                    //cl.Members.RemoveAt(i);
                    //cl.Members.Remove(item);
                    cl = cl.RemoveNode(item, SyntaxRemoveOptions.KeepEndOfLine);
                }
                // záměna namespace za class pak dělá problémy tady
                sn = sn.TrackNodes(cl);
                root = root.TrackNodes(sn);
                
                var d = sn.SyntaxTree.ToString();
                var fileTo = SH.Replace(file, folderFrom, folderTo);
                File.WriteAllText(fileTo, d);
            }

            return null;
        }

        private SyntaxNode FindTopParent(SyntaxNode cl)
        {
            var result = cl;
            while (result.Parent != null)
            {
                result = result.Parent;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="root"></param>
        /// <param name="lines"></param>
        /// <param name="usings"></param>
        /// <returns></returns>
        public static ABC GetVariablesInCsharp(SyntaxNode root, List<string> lines, out CollectionWithoutDuplicates<string> usings)
        {
            usings = new CollectionWithoutDuplicates<string>();
            ABC result = new ABC();

            foreach (var item in lines)
            {
                var line = item.Trim();
                if (line != string.Empty)
                {
                    if (line.StartsWith("using "))
                    {
                        usings.Add(line);
                    }
                    else if (line.Contains(AllStrings.lsf))
                    {
                        break;
                    }   
                }
            }

            ClassDeclarationSyntax helloWorldDeclaration = null;
            helloWorldDeclaration = RoslynHelper.GetClass(root);

            var variableDeclarations = helloWorldDeclaration.DescendantNodes().OfType<FieldDeclarationSyntax>();

            foreach (var variableDeclaration in variableDeclarations)
            {
                //Console.WriteLine(variableDeclaration.Variables.First().Identifier.);
                //Console.WriteLine(variableDeclaration.Variables.First().Identifier.Value);
                string variableName = variableDeclaration.Declaration.Type.ToString();
                variableName = SH.ReplaceOnce(variableName, "global::", "");
                int lastIndex = variableName.LastIndexOf(AllChars.dot);
                string ns, cn;
                SH.GetPartsByLocation(out ns, out cn, variableName, lastIndex);
                usings.Add(ns);
                // in key type, in value name
                result.Add(AB.Get(cn, variableDeclaration.Declaration.Variables.First().Identifier.Text));

            }

            return result;
        }

        
    }
}