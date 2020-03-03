using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using sunamo;
using sunamo.Collections;
using sunamo.Constants;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using HtmlAgilityPack;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Formatting;

using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn
{
    public class RoslynHelper
    {
        static Type type = typeof(RoslynHelper);

        /// <summary>
        /// Return also referenced projects (sunamo return also duo and Resources, although is not in sunamo)
        /// If I want what is only in sln, use APSH.GetProjectsInSlnFile
        /// </summary>
        /// <param name="slnPath"></param>
        /// <param name="SkipUnrecognizedProjects"></param>
        
        public static T ReplaceNode<T>(SyntaxNode cl, SyntaxNode cl2, out SyntaxNode root) where T : SyntaxNode
        {
            bool first = true;
            T result = default(T);
            while (cl is SyntaxNode)
            {
                if (cl.Parent == null)
                {
                    break;
                }
                cl = cl.Parent.ReplaceNode(cl, cl2);
                
                if (first)
                {
                    result = (T)cl2;
                    first = false;
                }
                cl2 = cl;
                cl = cl.Parent;
            }
            root = cl2;
            if (result== null)
            {

            }
            return result;
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

        public static string NameWithoutGeneric(string name)
        {
            
            return SH.RemoveAfterFirst(name, AllStrings.lt);
        }
    }
}
