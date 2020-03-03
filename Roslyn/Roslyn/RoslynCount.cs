using Microsoft.CodeAnalysis.CSharp.Syntax;
using sunamo.Essential;
using System;

namespace Roslyn
{
    public class RoslynCount 
    {
        public Type type = typeof(RoslynCount);

        /// <summary>
        /// Methods()
        /// </summary>
        public int  after2, before2;
        /// <summary>
        /// Members
        /// </summary>
        public int before, after;

        public void FillBefore(ClassDeclarationSyntax cl2_2)
        {
            before = cl2_2.Members.Count;
            before2 = ChildNodes.Methods(cl2_2).Count();
        }

        public void FillAfter(ClassDeclarationSyntax cl2_2)
        {
            after = cl2_2.Members.Count;
            after2 = ChildNodes.Methods(cl2_2).Count();
        }

        public void Log(string operation)
        {
            ThisApp.SetStatus(TypeOfMessage.Information, operation + $": " + "Before members {before}, methods {before2" + "}");
            ThisApp.SetStatus(TypeOfMessage.Information, operation + $": " + "After members {after}, methods {after2" + "}");
        }

        public void ThrowException()
        {
            //string methodName = "ThrowException";
            //// Members
            //ThrowExceptions.ElementWasntRemoved(type, methodName, "removing class members", before, after);
            //// Methods
            //ThrowExceptions.ElementWasntRemoved(type, methodName, "removing class methods", before2, after2);
        }
    }
}