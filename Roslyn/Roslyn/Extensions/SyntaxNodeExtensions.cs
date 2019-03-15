using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roslyn.Extensions
{
    public static class SyntaxNodeExtensions
    {
        public static SyntaxNode NoTrivia(this SyntaxNode n)
        {
            return RoslynHelper.WithoutAllTrivia(n);
        }
    }
}
