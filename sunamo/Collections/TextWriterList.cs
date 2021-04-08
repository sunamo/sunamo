using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// 
    /// Not working, tried with Microsoft.CodeAnalysis.SyntaxNode.WriteTo
    /// </summary>
    public class TextWriterList : TextWriter
    {
        private IList _list = null;
        public TextWriterList(IList list)
        {
            _list = list;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string value)
        {
            _list.Add(value);
        }
    }
}