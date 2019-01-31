using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class CodeElement<T>
    {
        public string Name;
        public T Type;
        public int Index;
    /// <summary>
    /// Index in document with start
    /// </summary>
    public int From;
    /// <summary>
    /// Index in document with last char 
    /// 
    /// </summary>
    public int To;
    public int Length;
    /// <summary>
    /// Base classes of MemberDeclarationSyntax is only CSharpSyntaxNode and SyntaxNode
    /// </summary>
    public MemberDeclarationSyntax Member;
    }

