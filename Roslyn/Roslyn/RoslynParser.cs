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
                indexer.ProcessFile(file, sunamo.Enums.NamespaceCodeElementsType.Nope, ClassCodeElementsType.Method);
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

        public void AspxCsToStandaloneAssembly(string from, string to, string baseClassCs, string nsBaseClassCs)
        {
            var files = FS.GetFiles(from, FS.MascFromExtension(".aspx.cs"), SearchOption.TopDirectoryOnly);
            // Get namespace
            string ns = FS.GetFileName(from);
            string nsX = FS.GetFileName(to);

            foreach (var fileAspxCs in files)
            {
                string fnwoeAspxCs = FS.GetFileNameWithoutExtensions(fileAspxCs);
                string designer = FS.Combine(from, fnwoeAspxCs + ".aspx.designer.cs");

                string fullPathTo = FS.Combine(to, fnwoeAspxCs + "Cs.cs");

                #region Generate and save *Cs file
                CollectionWithoutDuplicates<string> usings;
                if (FS.ExistsFile(designer) && !FS.ExistsFile(fullPathTo))
                {
                    #region Get variables in designer
                    var designerContent = TF.ReadFile(designer);
                    var fileAspxCsContent = TF.ReadFile(fileAspxCs);
                    var dict = GetVariablesInCsharp(GetSyntaxTree(designerContent), SH.GetLines(fileAspxCsContent), out usings);
                    usings.Add(ns);
                    #endregion

                    #region Get all other members in .cs
                    SyntaxTree tree = CSharpSyntaxTree.ParseText(fileAspxCsContent);
                    StringWriter swCode = new StringWriter();
                    var cl = GetClass(tree);
                    if (cl == null)
                    {
                        Console.WriteLine(fnwoeAspxCs + " contains more classes");
                        continue;
                    }

                    SyntaxNode firstNode = null;

                    int count = cl.Members.Count;
                    for (int i = count - 1; i >= 0; i--)
                    {
                        var item = cl.Members[i];
                        item.WriteTo(swCode);
                        //cl.Members.RemoveAt(i);
                        //cl.Members.Remove(item);
                        if (i == 0)
                        {
                            var firstTree = CSharpSyntaxTree.ParseText("            public " + fnwoeAspxCs + "Cs cs;");
                            firstNode = firstTree.GetRoot().ChildNodes().First();
                            cl = cl.ReplaceNode(item, firstNode);
                        }
                        else
                        {
                            cl = cl.RemoveNode(item, SyntaxRemoveOptions.KeepEndOfLine);
                        }
                    }

                    swCode.Flush();
                    
                    #endregion

                    CSharpGenerator genVariables = new CSharpGenerator();
                    foreach (var item2 in dict)
                    {
                        genVariables.Field(2, AccessModifiers.Private, false, VariableModifiers.None, item2.A, item2.B.ToString(), false);
                    }

                    CSharpGenerator genUsings = new CSharpGenerator();
                    foreach (var item2 in usings.c)
                    {
                        genUsings.Using(item2);
                    }

                    var onlyB = dict.OnlyBs();
                    var variables = genVariables.ToString();
                    var usingsCode = genUsings.ToString();

                    var ctorArgs = SH.JoinKeyValueCollection(dict.OnlyAs(), onlyB, AllStrings.space, AllStrings.comma);
                    var ctorInner = CSharpHelper.GetCtorInner(3, onlyB);
                    var code = swCode.ToString();
                    code = SH.ReplaceOnce(code, "protected void Page_Load", "public override void Page_Load");

                    string c = GetContentOfNewAspxCs(fnwoeAspxCs + "Cs", SH.Join(AllStrings.comma, onlyB));
                    //SyntaxTree addedCode = CSharpSyntaxTree.ParseText(c);
                    //var syntaxNodes = addedCode.GetRoot().ChildNodes().ToList();
                    //var inserted = cl.SyntaxTree.GetRoot().InsertNodesAfter(firstNode, CA.ToList<SyntaxNode>( syntaxNodes[0]));

                    var contentFileNew = SH.GetLines(cl.SyntaxTree.ToString());
                    int classIndex = -1;
                    contentFileNew = CSharpGenerator.AddIntoClass(contentFileNew, SH.GetLines( c), out classIndex, ns);

                    List<string> us = CA.ToList(ns, ns + "X", "System", "System.Web.UI");
                    CSharpGenerator genUs = new CSharpGenerator();
                    foreach (var item in us)
                    {
                        genUs.Using(item);
                    }
                    genUs.AppendLine();
                    genUs.Namespace(0, ("sunamo.cz." + ns).TrimEnd('.'));
                    

                    contentFileNew.Insert(0, genUs.ToString());
                    contentFileNew.Add("}");

                    string content = GetContentOfPageCsFile(nsX, fnwoeAspxCs, variables, usingsCode, ctorArgs, ctorInner, baseClassCs, nsBaseClassCs, code);
                    content = SH.ReplaceAll(content, string.Empty, "CreateEmpty();");
                   
                        // save .cs file
                        TF.SaveLines(contentFileNew, fileAspxCs);
                        // save new file
                        TF.SaveFile(content, fullPathTo);
                    
                }
                #endregion
            }
        }

        public static SyntaxNode FindNode(SyntaxNode parent, SyntaxNode child, bool onlyDirectSub)
        {
            int dx;
            return FindNode(parent, child, onlyDirectSub, out dx);
        }

        /// <summary>
        /// Because of searching is very unreliable
        /// Into A1 I have to insert class when I search in classes. If I insert root/ns/etc, method will be return to me whole class, because its contain method
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static SyntaxNode FindNode(SyntaxNode parent, SyntaxNode child, bool onlyDirectSub, out int dx)
        {
            dx = -1;

            #region MyRegion
            if (false)
            {
                if (onlyDirectSub)
                {
                    // toto mi vratí např. jen public, nikoliv celou stránku
                    //var ss = cl.ChildThatContainsPosition(cl.GetLocation().SourceSpan.Start);
                    foreach (var item in parent.ChildNodes())
                    {
                        // Má tu lokaci trochu dál protože obsahuje zároveň celou třídu
                        string l1 = item.GetLocation().ToString();
                        string l2 = child.GetLocation().ToString();

                        var s = child.Span;
                        var s2 = child.FullSpan;
                        var s3 = child.GetReference();

                        if (l1 == l2)
                        {
                            return item;
                        }
                    }
                }
                else
                {
                    return parent.FindNode(child.FullSpan, false, true).WithoutLeadingTrivia().WithoutTrailingTrivia();
                }

                return null;
            }
            #endregion

            if (child is MethodDeclarationSyntax && parent is ClassDeclarationSyntax)
            {
                ClassDeclarationSyntax cl = (ClassDeclarationSyntax)parent;
                MethodDeclarationSyntax method = (MethodDeclarationSyntax)child;

                
                foreach (var item in cl.Members)
                {
                    dx++;
                    if (item is MethodDeclarationSyntax)
                    {
                        var method2 = (MethodDeclarationSyntax)item;
                        bool same = true;

                        if (method.Identifier.Text != method2.Identifier.Text)
                        {
                            same = false;
                        }

                        if (same)
                        {
                            if (!RoslynComparer.Modifiers(method.Modifiers, method2.Modifiers))
                            {
                                same = false;
                            }
                        }

                        if (same)
                        {
                            return method2;
                        }
                    }
                }
            }
            else
            {
                ThrowExceptions.NotImplementedCase(type, "FindNode");
            }

            return null;
            //return nsShared.FindNode(cl.FullSpan, false, true).WithoutLeadingTrivia().WithoutTrailingTrivia();
        }

        public static ClassDeclarationSyntax RemoveNode(ClassDeclarationSyntax cl2, SyntaxNode method, SyntaxRemoveOptions keepDirectives)
        {
            //var children = method.ChildNodesAndTokens().ToList();
            //for (int i = children.Count() - 1; i >= 0; i--)
            //{
            //    var t = children[i].GetType().FullName;
            //    if (!(children[i] is MethodDeclarationSyntax))
            //    {
            //        int i2 = 0;
            //    }
            //    else
            //    {
            //        children.RemoveAt(i);
            //    }
            //}
            //return null;

            //FindNode()
            //cl2.Members.
            return null;
        }

        string GetContentOfNewAspxCs(string csClass, string ctorArgs)
        {
            //CSharpGenerator genAspxCs = new CSharpGenerator();
            //genAspxCs.Field(2, AccessModifiers.Private, false, VariableModifiers.None, fnwoe + "Cs", "cs", false);

            const string template = @"    
            protected void Page_Init(object sender, EventArgs e)
            [
                cs = new {0}({1});
            ]

            protected void Page_Load(object sender, EventArgs e)
            [
                cs.Page = ((Page)this).Page;
                cs.Page_Load(sender, e);
                cs.CreateTitle();
            ]";

            return SH.Format(template, AllStrings.lsf, AllStrings.rsf, csClass, ctorArgs);
        }

        string GetContentOfPageCsFile(string nsX, string className, string variables, string usings, string ctorArgs, string ctorInner, string baseClassCs, string nsBaseClassCs, string code)
        {
            string template = SH.Format(@"{3}
namespace {0}
[
    public partial class {1}Cs : {6}
    [
        #region Variables & ctor
{2}

        public {1}Cs({4})
        [
            {5}
        ]
        #endregion

        [0]
    ]
]", AllStrings.lsf, AllStrings.rsf, nsX, className, variables, usings, ctorArgs, ctorInner, baseClassCs, nsBaseClassCs);
            template = SH.Format2(template, code);
            return template;
        }

        public static SyntaxTree GetSyntaxTree(string code)
        {
            return CSharpSyntaxTree.ParseText(code);
        }

        public static string Format(string format)
        {
            format = HttpUtility.HtmlDecode(format);
            format = Regex.Replace(format, RegexHelper.rBrTagCaseInsensitive.ToString(), string.Empty);

            var workspace = MSBuildWorkspace.Create();
            StringBuilder sb = new StringBuilder();

            SyntaxTree firstTree = CSharpSyntaxTree.ParseText(format);

            //SourceText text = firstTree.GetText();
            var firstRoot = firstTree.GetRoot();
            
            var syntaxNodes = firstRoot.ChildNodesAndTokens();

            bool token = false;
            bool node2 = false;

            SyntaxNode node = null;

            //List<SyntaxNodeOrToken> syntaxNodes2 = new List<SyntaxNodeOrToken>();
            //syntaxNodes2.AddRange(syntaxNodes.Where(d => d.Kind() == SyntaxKind.IncompleteMember));
            List<SyntaxNode> syntaxNodes2 = new List<SyntaxNode>();


            foreach (var item in syntaxNodes.Where(d => d.Kind() == SyntaxKind.IncompleteMember))
            {
                var node3 = item.AsNode();
                //string name = node3.TryGetInferredMemberName();
                //string name = node3.De
                //HtmlNode.ElementsFlags.Add(name, HtmlElementFlag.CanOverlap);
                syntaxNodes2.Add(node3);
            }

            //SyntaxNode node = CSharpSyntaxNode.DeserializeFrom()
            syntaxNodes = firstRoot.ChildNodesAndTokens();
            foreach (var syntaxNode in syntaxNodes)
            {
                string s = syntaxNode.GetType().FullName.ToString();
                if (syntaxNode.GetType() == typeof(SyntaxNodeOrToken))
                {
                    token = true;
                    node = ((SyntaxNodeOrToken)syntaxNode).Parent;
                    break;
                }
                else if (syntaxNode.GetType() == typeof(SyntaxNode))
                {
                    node2 = true;
                    node = (SyntaxNode)syntaxNode;
                    
                }
                else
                {
                    ThrowExceptions.NotImplementedCase(type, "Format");
                }
            }

            if (node2 && token)
            {
                throw new Exception("Cant process token and SyntaxNode - output could be duplicated");
            }

            var node4 = node.Parent;
            if (token)
            {
                node4 = node;
            }

            node = node4.ReplaceNode(node, node.RemoveNodes(syntaxNodes2, SyntaxRemoveOptions.AddElasticMarker));
            var nodesAndTokens = node.ChildNodesAndTokens();
            var formattedResult = Microsoft.CodeAnalysis.Formatting.Formatter.Format(node, workspace);
            
            sb.AppendLine(formattedResult.ToFullString().Trim());

            string result =  sb.ToString();

            return result;
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
                var cl = GetClass(tree,out sn);

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
        /// <param name="tree"></param>
        /// <param name="lines"></param>
        /// <param name="usings"></param>
        /// <returns></returns>
        public ABC GetVariablesInCsharp(SyntaxTree tree, List<string> lines, out CollectionWithoutDuplicates<string> usings)
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
            helloWorldDeclaration = GetClass(tree);

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

        public static ClassDeclarationSyntax GetClass(SyntaxTree tree)
        {
            SyntaxNode sn;
            return GetClass(tree, out sn);
        }

        /// <summary>
        /// Return null if 
        /// Into A2 insert first member of A1 - Namespace/Class
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax GetClass(SyntaxTree tree, out SyntaxNode ns)
        {
            ns = null;
            ClassDeclarationSyntax helloWorldDeclaration = null;

            var root = (CompilationUnitSyntax)tree.GetRoot();

            if(root.ChildNodes().OfType<ClassDeclarationSyntax>().Count() > 1)
            {
                return null;
            }
            var firstMember = root.Members[0];

            if (firstMember is NamespaceDeclarationSyntax)
            {
                ns = (NamespaceDeclarationSyntax)firstMember;
                int i = 0;
                var fm = ((NamespaceDeclarationSyntax)ns).Members[i++];
                while (fm.GetType() != typeof(ClassDeclarationSyntax))
                {
                    fm = ((NamespaceDeclarationSyntax)ns).Members[i++];
                }
                helloWorldDeclaration = (ClassDeclarationSyntax)fm;
            }
            else if (firstMember is ClassDeclarationSyntax)
            {
                helloWorldDeclaration = (ClassDeclarationSyntax)firstMember;
                // keep ns as null
                //ns = nu;
            }
            else
            {
                ThrowExceptions.NotImplementedCase(type, "GetClass");
            }

            return helloWorldDeclaration;
        }
    }
}