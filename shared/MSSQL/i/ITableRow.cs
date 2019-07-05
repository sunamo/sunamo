using System.Collections.Generic;

public interface ITableRow
{
    int InsertToTable();
}

    /// <summary>
    /// Genericky I je typ ktery vraci metoda InsertToTable
    /// MYSLIM ZE TED UZ TU JE VSE POTREBNE, PROTO TU ZADNE JINE VECI JAKO NAPRIKLAD TA POSLEDNI ZAKOMENTOVANA METODA NEPRIDAVEJ
    /// ZAPRVE SI VAS SVEHO CASU A ZA DRUHE TO BUDE VETSINOU DUPLIKACE
    /// </summary>
public interface ITableRow<I>
    {
        /// <summary>
        /// Budes muset kontrolovat sam na UNIQUE, PRIMARY KEY atd.
        /// ID se vzdy dava az v teto metode, jinde neni povoleno ID zjistovat.
        /// </summary>
        /// <returns></returns>
        I InsertToTable();
        I InsertToTable2();
        void InsertToTable3(I i);
    }
