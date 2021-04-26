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
    /// <summary>
    /// RoslynParser - use roslyn classes
    /// RoslynParserText - no use roslyn classes, only text or indexer
    /// </summary>
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

        

        

        /// <summary>
        /// Úplně nevím k čemu toto mělo sloužit. 
        /// Read comments inside
        /// </summary>
        /// <param name="folderFrom"></param>
        /// <param name="folderTo"></param>
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
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="wrapIntoClass"></param>
        public static ABC GetVariablesInCsharp(SyntaxNode root)
        {
            List<string> lines = new List<string>();
            CollectionWithoutDuplicates<string> usings;

            return GetVariablesInCsharp(root, lines, out usings);
        }

        /// <summary>
        /// </summary>
        /// <param name="root"></param>
        /// <param name="lines"></param>
        /// <param name="usings"></param>
        public static ABC GetVariablesInCsharp(SyntaxNode root, List<string> lines, out CollectionWithoutDuplicates<string> usings)
        {
            ABC result = new ABC();
            usings = RoslynParserText.Usings(lines);

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

        /// <summary>
        /// return declaredVariables, assignedVariables
        /// A1 can be string or CompilationUnitSyntax
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Tuple<List<string>, List<string>> ParseVariables(object code)
        {
            SyntaxNode root = null;
            string code2 = null;

            //MethodDeclarationSyntax;

            root =  SyntaxNodeFromObjectOrString(code);

            var variableDeclarations = root.DescendantNodes().OfType<VariableDeclarationSyntax>();
            var variableAssignments = root.DescendantNodes().OfType<AssignmentExpressionSyntax>();

            List<string> declaredVariables = new List<string>(variableDeclarations.Count());
            List<string> assignedVariables = new List<string>(variableAssignments.Count());

            foreach (var variableDeclaration in variableDeclarations)
                declaredVariables.Add(variableDeclaration.Variables.First().Identifier.Value.ToString());

            foreach (var variableAssignment in variableAssignments)
                assignedVariables.Add(variableAssignment.Left.ToString());

            return new Tuple<List<string>, List<string>>(declaredVariables, assignedVariables);
        }

        public static SyntaxNode SyntaxNodeFromObjectOrString(object code)
        {
            SyntaxNode root = null;

            if (code is SyntaxNode)
            {
                root = (SyntaxNode)code;
            }
            else if (code is string)
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code.ToString());
                root = (SyntaxNode)tree.GetRoot();
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), "else");
            }

            return root;
        }

        public static Dictionary<string, List<string>> GetVariablesInEveryMethod(string s)
        {
            Dictionary<string, List<string>> m = new Dictionary<string, List<string>>();

            var tree = CSharpSyntaxTree.ParseText(s);
            var root = tree.GetRoot();

            IEnumerable<MethodDeclarationSyntax> methods = root
              .DescendantNodes()
              .OfType<MethodDeclarationSyntax>().ToList();

            foreach (var method in methods)
            {
                var v = RoslynParser.ParseVariables(method);
                m.Add(method.Identifier.Text, v.Item2);
            }

            return m;
        }
    }
}