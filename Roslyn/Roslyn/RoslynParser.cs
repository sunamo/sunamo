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

namespace Roslyn
{
    public class RoslynParser
    {
        // TODO: take also usings

        static Type type = null;

        public  void AspxCsToStandaloneAssembly(string from, string to, string baseClassCs, string nsBaseClassCs)
        {
            var files = FS.GetFiles(from, FS.MascFromExtension(".aspx.cs"), SearchOption.TopDirectoryOnly);
            // Get namespace
            string ns = FS.GetFileName(from);
            string nsX = FS.GetFileName(to);

            foreach (var file in files)
            {
                string fnwoe = FS.GetFileNameWithoutExtensions(file);
                string designer = FS.Combine(from, fnwoe+ ".aspx.designer.cs");

                string fullPathTo = FS.Combine(to, fnwoe + "Cs.cs");

                #region Generate and save *Cs file
                CollectionWithoutDuplicates<string> usings;
                if (File.Exists(designer)  && !File.Exists(fullPathTo))
                {
                    #region Get variables in designer
                    var designerContent = TF.ReadFile(designer);
                    var fileContent = TF.ReadFile(file);
                    var dict = GetVariablesInCsharp(GetSyntaxTree(designerContent), SH.GetLines(fileContent), out usings);
                    usings.Add(ns);
                    #endregion

                    #region Get all other code in .cs
                    SyntaxTree tree = CSharpSyntaxTree.ParseText(fileContent);
                    StringWriter swCode = new StringWriter();
                    var cl = GetClass(tree);

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
                            var firstTree = CSharpSyntaxTree.ParseText("            " + fnwoe + "Cs cs;");
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

                    string c = GetContentOfNewAspxCs(fnwoe + "Cs", SH.Join(AllStrings.comma, onlyB));
                    //SyntaxTree addedCode = CSharpSyntaxTree.ParseText(c);
                    //var syntaxNodes = addedCode.GetRoot().ChildNodes().ToList();
                    //var inserted = cl.SyntaxTree.GetRoot().InsertNodesAfter(firstNode, CA.ToList<SyntaxNode>( syntaxNodes[0]));

                    var contentFileNew = SH.GetLines(cl.SyntaxTree.ToString());
                    int classIndex = -1;
                    contentFileNew = CSharpGenerator.AddIntoClass(contentFileNew, SH.GetLines( c), out classIndex);

                    //string classLine = contentFileNew[classIndex];
                    //int colonIndex = classLine.IndexOf(AllChars.colon);
                    //string deriveFrom = classLine.Substring(colonIndex);
                    //var elements = SH.Split(deriveFrom, AllChars.comma);
                    //elements[0] = ": " + baseClassCs;
                    //contentFileNew[classIndex] = SH.Join(AllStrings.comma, elements);

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



                    string content = GetContentOfPageCsFile(nsX, fnwoe, variables, usingsCode, ctorArgs, ctorInner, baseClassCs, nsBaseClassCs, code);
                    if (!File.Exists(fullPathTo))
                    {

                        // save .cs file
                        TF.SaveLines(contentFileNew, file);
                        // save new file
                        TF.SaveFile(content, fullPathTo);
                    }
                }
                #endregion
            }
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

            //var m = node.DescendantNodes().OfType<IncompleteMemberSyntax>().First();
            //m.Type.Ide

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
            sb.AppendLine(formattedResult.ToFullString());
            //if (syntaxNodes.Count() == 0)
            //{
            //    sb.Append(format);
            //    //SyntaxNode syntaxNode = RoslynParser.GetNode(format);
            //    //    var formattedResult = Microsoft.CodeAnalysis.Formatting.Formatter.Format(syntaxNode, workspace);
            //    //sb.AppendLine(formattedResult.ToFullString());
            //}

            return sb.ToString();

            //var engine = FormattingEngine.Create();
            //engine.PreprocessorConfigurations = options.PreprocessorConfigurations;
            //engine.FileNames = options.FileNames;
            //engine.CopyrightHeader = options.CopyrightHeader;
            //engine.AllowTables = options.AllowTables;
            //engine.Verbose = false;

            //if (!SetRuleMap(engine, options.RuleMap))
            //{
            //    return 1;
            //}

            //foreach (var item in options.FormatTargets)
            //{
            //    await RunFormatItemAsync(engine, item, options.Language, cancellationToken);
            //}

            //return 0;
        }

       

        public List<string> GetCodeOfElementsClass(string folderFrom, string folderTo)
        {
            var files = FS.GetFiles(folderFrom, FS.MascFromExtension(".aspx.cs"), SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                

                SyntaxTree tree = CSharpSyntaxTree.ParseText(TF.ReadFile(file));

                //List<string> usings = new List<string>();
                

                List<string> result = new List<string>();
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
                sn = sn.TrackNodes(cl);
                root = root.TrackNodes(sn);
                
                var d = sn.SyntaxTree.ToString();
                int t = 0;
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

        public ABC GetVariablesInCsharp(SyntaxTree tree, List<string> lines, out CollectionWithoutDuplicates<string> usings)
        {
            usings = new CollectionWithoutDuplicates<string>();
            ABC result = new ABC();

            #region MyRegion
            //StringWriter swUsings = new StringWriter();
            //var usingsSn = tree.GetRoot().DescendantNodes().Where(node => node is UsingDirectiveSyntax);

            //foreach (var item in usingsSn)
            //{
            //    item.WriteTo(swUsings);
            //}

            //foreach (var item in SH.GetLines(swUsings.ToString()))
            //{
            //    usings.Add(item);
            //} 
            #endregion

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
                result.Add(AB.Get(cn, variableDeclaration.Declaration.Variables.First().Identifier.Text));

            }

            return result;
        }

        private static ClassDeclarationSyntax GetClass(SyntaxTree tree)
        {
            SyntaxNode sn;
            return GetClass(tree, out sn);
        }

        private static ClassDeclarationSyntax GetClass(SyntaxTree tree, out SyntaxNode ns)
        {
            ns = null;
            ClassDeclarationSyntax helloWorldDeclaration = null;

            var root = (CompilationUnitSyntax)tree.GetRoot();

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
                ns = helloWorldDeclaration;
            }
            else
            {
                ThrowExceptions.NotImplementedCase(type, "GetVariablesInCsharp");
            }

            return helloWorldDeclaration;
        }
    }
}