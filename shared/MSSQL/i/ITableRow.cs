using System.Collections.Generic;

public interface ITableRow
{
    int InsertToTable();
}

    /// <summary>
    /// Generick� I je typ kter� vrac� metoda InsertToTable
    /// MYSL�M �E TE� U� TU JE V�E POT�EBN�, PROTO TU ��DN� JIN� V�CI JAKO NAP��KLAD TA POSLEDN� ZAKOMENTOVAN� METODA NEP�ID�VEJ
    /// ZAPRV� SI VA� SV�HO �ASU A ZA DRUH� TO BUDE V�T�INOU DUPLIKACE
    /// </summary>
public interface ITableRow<I>
    {
        /// <summary>
        /// Bude� muset kontrolovat s�m na UNIQUE, PRIMARY KEY atd.
        /// ID se v�dy d�v� a� v t�to metod�, jinde nen� povoleno ID zji��ovat.
        /// </summary>
        /// <returns></returns>
        I InsertToTable();
        I InsertToTable2();
        void InsertToTable3(I i);
    }
