using System;
using System.Collections.Generic;
using System.Text;


public interface IStoredProceduresI : IStoredProcedures
    {
        int FindOutID(string tabulka, string nazevSloupce, object hodnotaSloupce);
        int FindOutNumberOfRows(string tabulka);
    }
