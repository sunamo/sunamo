using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface IXmlGeneratorShared
    {
        void WriteCData(string innerCData);
        void StartComment();
        void EndComment();
        void WriteRaw(string p);
        void WriteXmlDeclaration();
        int Length();
        void Insert(int index, string text);

         


    }
}
