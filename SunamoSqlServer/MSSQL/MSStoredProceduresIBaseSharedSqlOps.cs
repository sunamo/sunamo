//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections;
//using System.Data;
//using System.Data.SqlClient;
//using sunamo;
//using sunamo.Values;

///// <summary>
///// 12-1-2019 refactoring:
///// 1)all methods here take ABC if take more than Where. otherwise is allowed params AB[] / object[]
///// 2) always is table - select / update column - where
///// 
///// </summary>
//public partial class MSStoredProceduresIBase : SqlServerHelper
//{
//    public static PpkOnDrive loggedCommands = null;

//    static MSStoredProceduresIBase()
//    {
//        //var f = AppData.ci.GetFile(AppFolders.Logs, "sqlCommands.txt");
//        //loggedCommands = new PpkOnDrive(f);
//    }

//    public DataTable SelectDateTableGroupBy(string table, string columns, string groupByColumns)
//    {
//        string sql = "select " + columns + " from " + table + " group by " + groupByColumns;
//        SqlCommand comm = new SqlCommand(sql);
//        //AddCommandParameter(comm, 0, IDColumnValue);
//        return SelectDataTable(comm);
//    }

//    /// <summary>
//    /// A1 NSN
//    /// </summary>
//    /// <param name="signed"></param>
//    /// <param name="tabulka"></param>
//    /// <param name="hledanySloupec"></param>
//    /// <param name="aB"></param>
//    
//    public int UpdateAppendStringValueCheckExistsOneRow(string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
//    {
//        return SqlOpsI.ci.UpdateAppendStringValueCheckExistsOneRow(d, tableName, sloupecAppend, hodnotaAppend, sloupecID, hodnotaID).affectedRows;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public int UpdateCutStringValue(string tableName, string sloupecCut, string hodnotaCut, string sloupecID, object hodnotaID)
//    {
//        return SqlOpsI.ci.UpdateCutStringValue(d, tableName, sloupecCut, hodnotaCut, sloupecID, hodnotaID).affectedRows;
//    }

//    /// <summary>
//    /// Conn přidá automaticky
//    /// Název metody je sice OneRow ale updatuje to libovolný počet řádků které to najde pomocí where - je to moje interní pojmenování aby mě to někdy trklo, možná později přijdu na způsob jak updatovat jen jeden řádek.
//    /// </summary>
//    public int Update(string table, string sloupecKUpdate, object n, string sloupecID, object id)
//    {
//        return SqlOpsI.ci.Update(d, table, sloupecKUpdate, n, sloupecID, id).affectedRows;
//    }

//    public int Update(string table, string sloupecKUpdate, object n, params AB[] ab)
//    {
//        return SqlOpsI.ci.Update(d, table, sloupecKUpdate, n, ab).affectedRows;
//    }

//    private int Update(string table, string sloupecKUpdate, int n, ABC abc)
//    {
//        return SqlOpsI.ci.Update(d, table, sloupecKUpdate, n, abc).affectedRows;
//    }

//    /// <summary>
//    /// Conn nastaví automaticky
//    /// </summary>
//    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params AB[] sets)
//    {
//        SqlOpsI.ci.UpdateValuesCombination(d, TableName, nameOfColumn, valueOfColumn, sets);
//    }

//    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params object[] setsNameValue)
//    {
//        // Dont use like idiot TwoDimensionParamsIntoOne where is not needed - just iterate. Must more use radio and less blindness
//        //var setsNameValue2 = CA.TwoDimensionParamsIntoOne(setsNameValue);
//        ABC abc = new ABC(setsNameValue);
//        UpdateValuesCombination(TableName, nameOfColumn, valueOfColumn, abc.ToArray());
//    }
//}