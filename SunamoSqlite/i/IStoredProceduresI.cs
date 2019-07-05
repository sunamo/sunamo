using System;
using System.Collections.Generic;
using System.Text;

namespace DocArch.SqLite
{
    public interface IStoredProceduresI : IStoredProcedures
    {
        int FindOutID(string tabulka, string nazevSloupce, object hodnotaSloupce);
        int FindOutNumberOfRows(string tabulka);
    }
}
