using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface IXmlGenerator<T, Attr>
    {
        void WriteNonPairTagWithAttrs(T tag, params string[] args);
        void WriteTagWithAttr(T tag, Attr attr, string value);

        void TerminateTag(T tag);
        void WriteTag(T tag);
        void WriteNonPairTag(T tag);
        void WriteElement(T tag, string inner);
        void WriteTagWithAttrs(T tag, params string[] p_2);
        void WriteTagWith2Attrs(T tag, Attr attr1, string val1, Attr attr2, string val2);
        void WriteRaw(string content);
    }
}
