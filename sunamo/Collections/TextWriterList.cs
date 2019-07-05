using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// Not working, tried with Microsoft.CodeAnalysis.SyntaxNode.WriteTo
    /// </summary>
    public class TextWriterList : TextWriter
    {
        IList list = null;
        public TextWriterList(IList list)
        {
            this.list = list;

            
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string value)
        {
            list.Add(value);

        }
    }
}
