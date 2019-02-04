using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roslyn.Data
{
    public class SourceFileTree
    {
        public SyntaxTree tree;
        public CompilationUnitSyntax root;
    }
}
