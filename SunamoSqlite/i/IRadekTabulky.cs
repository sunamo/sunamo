using System.Collections.Generic;
using System.Data;
namespace DocArch.SqLite
{
    /// <summary>
    /// MYSLIM �E TE� U� TU JE V�E POT�EBN�, PROTO TU ��DN� JIN� V�CI JAKO NAP��KLAD TA POSLEDN� ZAKOMENTOVAN� METODA NEP�ID�VEJ
    /// ZAPRV� SI VA� SV�HO �ASU A ZA DRUH� TO BUDE V�T�INOU DUPLIKACE
    /// </summary>
    interface ITableRow
    {
        /// <summary>
        /// Bude� muset kontrolovat s�m na UNIQUE, PRIMARY KEY atd.
        /// </summary>
        /// <returns></returns>
        int InsertToTable();
        InsertedRows InsertToTableInsertedRows();
        /// <summary>
        /// Bude� muset kontrolovat s�m na UNIQUE, PRIMARY KEY atd.
        /// </summary>
        /// <returns></returns>
        ChangedRows UpdateInTable();
        ChangedRows UpdateInTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        ChangedRows UpdateInTable(string nazevSloupce, object hodnotaSloupce);
        ChangedRows UpdateInTableTest();
        ChangedRows UpdateInTableTest(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        ChangedRows UpdateInTableTest(string nazevSloupce, object hodnotaSloupce);
        void SelectInTable();
        /// <summary>
        /// Bere se jen Prvn� v�sledek
        /// </summary>
        /// <param name="nazvySloupcu"></param>
        /// <param name="hodnotaSloupcu"></param>
        void SelectInTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        void SelectInTable(string nazevSloupce, object hodnotaSloupce);
        /// <summary>
        /// Vrac� v�echny nalezen� ��dky
        /// </summary>
        /// <param name="nazvySloupcu"></param>
        /// <param name="hodnotaSloupcu"></param>
        /// <returns></returns>
        SelectedRows SelectInTableSelectedRows(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        SelectedRows SelectInTableSelectedRows(string nazevSloupce, object hodnotaSloupce);
        DataTable SelectInTableDataTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        DataTable SelectInTableDataTable(string nazevSloupce, object hodnotaSloupce);
        DeletedRows DeleteFromTable();
        DeletedRows DeleteFromTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        DeletedRows DeleteFromTable(string nazevSloupce, object hodnotaSloupce);
        DeletedRows DeleteFromTableTest();
        DeletedRows DeleteFromTableTest(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        DeletedRows DeleteFromTableTest(string nazevSloupce, object hodnotaSloupce);
        //bool ObsahujeTabulkaVSloupciHodnotu(string sloupec, object hodnota);
    }
}
