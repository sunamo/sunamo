using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using System;
using System.Collections.Generic;
using System.Text;


public class RoslynServicesHelper
{
    public static string Format(string formattedResult)
    {
        var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);

        var tree = SyntaxTree.ParseText(formattedResult);

        CompilationUnitSyntax root;
        tree.TryGetRoot(out root);

        //CompilationUnitSyntax cus = (CompilationUnitSyntax)formattedResult;
        var def = Roslyn.Services.Formatting.FormattingOptions.GetDefaultOptions();
        var formatted = root.Format(def);
        var formatted2 = formatted.GetFormattedRoot();
        var formattedText = formatted2.GetText();
        var result = formattedText.ToString();

        return result;
    }
}

