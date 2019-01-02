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
using sunamo.Constants;

namespace Roslyn
{
    public class RoslynParser
    {
        // TODO: take also usings

        static Type type = null;

        public  void AspxCsToStandaloneAssembly(string from, string to)
        {
            var files = FS.GetFiles(from, FS.MascFromExtension(".aspx.cs"), SearchOption.TopDirectoryOnly);
            // Get namespace
            string ns = FS.GetFileName(from);

            foreach (var item in files)
            {
                string fnwoe = FS.GetFileNameWithoutExtensions(item);
                string designer = FS.Combine(from, fnwoe+ ".aspx.designer.cs");
                string fullPathTo = FS.Combine(to, fnwoe + "Cs.cs");

                #region Generate and save *Cs file
                CollectionWithoutDuplicates<string> usings;
                if (File.Exists(designer)  && !File.Exists(fullPathTo))
                {
                    var dict = GetVariablesInCsharp(GetSyntaxTree(TF.ReadFile( designer)), out usings);

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

                    string content = GetContentOfPageCsFile(ns, fnwoe, variables, usingsCode, ctorArgs, ctorInner, "GeneralPageCs", "Nope");
                    if (!File.Exists(fullPathTo))
                    {
                        TF.SaveFile(content, fullPathTo);
                    }
                }
                #endregion
            }
        }

        string GetContentOfPageCsFile(string ns, string className, string variables, string usings, string ctorArgs, string ctorInner, string baseClassCs, string nsBaseClassCs)
        {
            string template = SH.Format(@"using {7};
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
{3}
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

        public override void Page_Load(object sender, EventArgs e)
        [

        ]
    ]
]", AllStrings.lsf, AllStrings.rsf, ns, className, variables, usings, ctorArgs, ctorInner, baseClassCs, nsBaseClassCs);
            return template;
        }

        public static SyntaxTree GetSyntaxTree(string code)
        {
            return CSharpSyntaxTree.ParseText(code);
        }

        public ABC GetVariablesInCsharp(SyntaxTree tree, out CollectionWithoutDuplicates<string> usings)
        {
            usings = new CollectionWithoutDuplicates<string>();
            ABC result = new ABC();
            var root = (CompilationUnitSyntax)tree.GetRoot();

            var firstMember = root.Members[0];

            ClassDeclarationSyntax helloWorldDeclaration = null;

            if (firstMember is NamespaceDeclarationSyntax)
            {
                helloWorldDeclaration = (ClassDeclarationSyntax)((NamespaceDeclarationSyntax)firstMember).Members[0];
            }
            else if (firstMember is ClassDeclarationSyntax)
            {
                helloWorldDeclaration = (ClassDeclarationSyntax)firstMember;
            }
            else
            {
                ThrowExceptions.NotImplementedCase(type, "GetVariablesInCsharp");
            }

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
                result.Add(AB.Get( cn, variableDeclaration.Declaration.Variables.First().Identifier.Text));
                
            }

            return result;
        }
    }
}