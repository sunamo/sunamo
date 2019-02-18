﻿using System;
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
    public class RoslynHelper
    {
        static Type type = typeof(RoslynHelper);

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

            string result = sb.ToString();

            return result;
        }

        public static ClassDeclarationSyntax GetClass(SyntaxTree tree)
        {
            SyntaxNode sn;
            return GetClass(tree, out sn);
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

            var childType = child.GetType().FullName;
            var parentType = parent.GetType().FullName;

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
            else if (child is BaseTypeDeclarationSyntax && parent is NamespaceDeclarationSyntax)
            {
                var ns = (NamespaceDeclarationSyntax)parent;
                var method = (BaseTypeDeclarationSyntax)child;

                foreach (BaseTypeDeclarationSyntax item in ns.Members)
                {
                    if (method.Identifier.Value == item.Identifier.Value)
                    {
                        return method;
                    }
                }
            }
            else if (child is NamespaceDeclarationSyntax && parent is CompilationUnitSyntax)
            {
                var ns = (CompilationUnitSyntax)parent;
                var method = (NamespaceDeclarationSyntax)child;

                foreach (NamespaceDeclarationSyntax item in ns.Members)
                {
                    string fs1 = method.Name.ToFullString();
                    string fs2 = item.Name.ToFullString();
                    if (fs1 == fs2)
                    {
                        return method;
                    }
                }
            }
            else if (child is ClassDeclarationSyntax && parent is CompilationUnitSyntax)
            {
                var ns = (CompilationUnitSyntax)parent;
                var method = (ClassDeclarationSyntax)child;

                foreach (ClassDeclarationSyntax item in ns.Members)
                {
                    string fs1 = method.Identifier.ToFullString();
                    string fs2 = item.Identifier.ToFullString();
                    if (fs1 == fs2)
                    {
                        return method;
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
            #region MyRegion
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
            #endregion
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

            if (root.ChildNodes().OfType<ClassDeclarationSyntax>().Count() > 1)
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

        public static List<string> HeadersOfMethod(IEnumerable<SyntaxNode> enumerable)
        {
            List<string> clMethodsSharedNew = new List<string>();

            foreach (MethodDeclarationSyntax m in enumerable)
            {
                string h = RoslynHelper.GetHeaderOfMethod(m);
                clMethodsSharedNew.Add(h);
                int i = 0;
            }

            return clMethodsSharedNew;
        }

        public static string GetHeaderOfMethod(MethodDeclarationSyntax m)
        {
            InstantSB sb = new InstantSB(AllStrings.space);
            bool isStatic = RoslynHelper.IsStatic(m.Modifiers);
            if (isStatic)
            {
                sb.AddItem("static");
            }
            sb.AddItem(m.ReturnType.ToFullString());

            // in brackets, newline
            //string parameters = m.ParameterList.ToFullString(); 
            // only text
            string p2 = GetParameters(m.ParameterList);
            sb.AddItem("(" + p2 + ")");

            string s = sb.ToString();
            return s;
        }

        private static string GetParameters(ParameterListSyntax parameterList)
        {
            var c1 = parameterList.ChildNodes();
            //var c2 = parameterList.ChildNodesAndTokens();
            StringBuilder sb = new StringBuilder();
            foreach (var item in c1)
            {
                sb.Append(item.ToFullString()+ ", ");
            }
            string r = SH.RemoveLastLetters( sb.ToString(), 2);
            return r;
        }

        public static bool IsStatic(SyntaxTokenList modifiers)
        {
            return modifiers.Where(e => e.Value.ToString() == "static").Count() > 0;
        }
    }
}
