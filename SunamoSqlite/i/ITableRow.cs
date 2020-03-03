using System.Collections.Generic;
using System.Data;

    /// <summary>
    /// MYSLIM ŽE TED UŽ TU JE VŠE POTŘEBNÉ, PROTO TU XXDNX JINÉ VĚCI JAKO NAPŘIKLAD TA POSLEDNÍ ZAKOMENTOVANÁ METODA NEPŘIDÁVEJ
    /// ZAPRVÉ SI VAŽ SVÉHO ČASU A ZA DRUHÉ TO BUDE VXTXINOU DUPLIKACE
    /// </summary>
    public interface ITableRow
    {
        /// <summary>
        /// Budeš muset kontrolovat sám na UNIQUE, PRIMARY KEY atd.
        /// </summary>
        int InsertToTable();
        InsertedRows InsertToTableInsertedRows();
        /// <summary>
        /// Budeš muset kontrolovat sám na UNIQUE, PRIMARY KEY atd.
        /// </summary>
        ChangedRows UpdateInTable();
        ChangedRows UpdateInTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        ChangedRows UpdateInTable(string nazevSloupce, object hodnotaSloupce);
        ChangedRows UpdateInTableTest();
        ChangedRows UpdateInTableTest(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        ChangedRows UpdateInTableTest(string nazevSloupce, object hodnotaSloupce);
        void SelectInTable();
        /// <summary>
        /// Bere se jen První výsledek
        /// </summary>
        /// <param name="nazvySloupcu"></param>
        /// <param name="hodnotaSloupcu"></param>
        void SelectInTable(List<string> nazvySloupcu, List<object> hodnotaSloupcu);
        void SelectInTable(string nazevSloupce, object hodnotaSloupce);
        /// <summary>
        /// Vrací všechny nalezené řádky
        /// </summary>
        /// <param name="nazvySloupcu"></param>
        /// <param name="hodnotaSloupcu"></param>
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