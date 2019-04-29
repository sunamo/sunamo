using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Roslyn
{
    public class WebToStandaloneAssembly
    {
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

            return SH.Format2(template, AllStrings.lsf, AllStrings.rsf, csClass, ctorArgs);
        }

        string GetContentOfPageCsFile(string nsX, string className, string variables, string usings, string ctorArgs, string ctorInner, string baseClassCs, string nsBaseClassCs, string code)
        {
            string template = SH.Format2(@"{3}
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
                    var dict = RoslynParser.GetVariablesInCsharp(RoslynHelper.GetSyntaxTree(designerContent).GetRoot(), SH.GetLines(fileAspxCsContent), out usings);
                    usings.Add(ns);
                    #endregion

                    #region Get all other members in .cs
                    SyntaxTree tree = CSharpSyntaxTree.ParseText(fileAspxCsContent);
                    StringWriter swCode = new StringWriter();
                    var cl = RoslynHelper.GetClass(tree.GetRoot());
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
                    contentFileNew = CSharpGenerator.AddIntoClass(contentFileNew, SH.GetLines(c), out classIndex, ns);

                    List<string> us = CA.ToList(ns, ns + "X", "System", "System.Web.UI");
                    CSharpGenerator genUs = new CSharpGenerator();
                    foreach (var item in us)
                    {
                        genUs.Using(item);
                    }
                    genUs.AppendLine();
                    genUs.Namespace(0, ("sunamo.cz." + ns).TrimEnd(AllChars.dot));


                    contentFileNew.Insert(0, genUs.ToString());
                    contentFileNew.Add(AllStrings.cbr);

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
    }
}
