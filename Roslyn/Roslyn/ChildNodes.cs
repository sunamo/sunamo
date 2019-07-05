using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roslyn
{
    public class ChildNodes
    {
        public static IEnumerable<MethodDeclarationSyntax> Methods(SyntaxNode n)
        {
            return n.ChildNodes().OfType<MethodDeclarationSyntax>();
        }

        public static MethodDeclarationSyntax Method(ClassDeclarationSyntax cl, string item)
        {
            var methodToFind = RoslynParser.Method(item);

            var founded = RoslynHelper.FindNode(cl, methodToFind, true);
            //var methods =  Methods(cl);
            return (MethodDeclarationSyntax)founded;
        }

        public static NamespaceDeclarationSyntax Namespace(SyntaxNode n)
        {
            if (n is NamespaceDeclarationSyntax)
            {
                return (NamespaceDeclarationSyntax)n;
            }
            return (NamespaceDeclarationSyntax)n.ChildNodes().OfType<NamespaceDeclarationSyntax>().FirstOrNull();
        }

        public static ClassDeclarationSyntax Class(SyntaxNode n)
        {
            if (n is ClassDeclarationSyntax)
            {
                return (ClassDeclarationSyntax)n;
            }
            return (ClassDeclarationSyntax)n.ChildNodes().OfType<ClassDeclarationSyntax>().FirstOrNull();
        }

        public static SyntaxNode NamespaceOrClass(SyntaxNode root)
        {
            var ns = Namespace(root);
            if (ns != null)
            {
                return ns;
            }
            return Class(root);
        }
    }
}
