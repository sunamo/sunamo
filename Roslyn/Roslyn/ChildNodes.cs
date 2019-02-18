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
    }
}
