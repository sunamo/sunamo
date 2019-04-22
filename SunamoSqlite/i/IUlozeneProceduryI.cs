using System;
using System.Collections.Generic;
using System.Text;

namespace DocArch.SqLite
{
    interface IUlozeneProceduryI : IUlozeneProcedury
    {
        int FindOutID(string tabulka, string nazevSloupce, object hodnotaSloupce);
        int FindOutNumberOfRows(string tabulka);
    }
}
