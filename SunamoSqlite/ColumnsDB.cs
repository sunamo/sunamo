﻿using DocArch.SqLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;


public class ColumnsDB : List<SloupecDB>
{
    bool signed = false;
    string derived = null;

    /// <summary>
    /// A1 od jakých rozhraní a tříd by měla být odvozena třída TableRow
    /// </summary>
    /// <param name="derived"></param>
    /// <param name="signed"></param>
    /// <param name="p"></param>
    public ColumnsDB(string derived, bool signed, params SloupecDB[] p)
    {
        this.derived = derived;
        this.signed = signed;
        this.AddRange(p);
    }

    public ColumnsDB(bool signed, params SloupecDB[] p)
    {
        this.signed = signed;
        this.AddRange(p);
    }

    public ColumnsDB(params SloupecDB[] p)
    {
        this.AddRange(p);
    }

    /// <summary>
    /// Can return null in Command property
    /// A2 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    public SQLiteCommand GetSqlCreateTable(string table, bool dynamicTables, SQLiteConnection conn)
    {
        string sql = GeneratorSqLite.CreateTable(table, this, dynamicTables, conn);
        SQLiteCommand comm = new SQLiteCommand(sql, conn);
        return comm;
    }
    /// <summary>
    /// Can return null in Command property
    /// 
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public SQLiteCommand GetSqlCreateTable(string tableName)
    {
        return GetSqlCreateTable(tableName, false);
    }

    /// <summary>
    /// Can return null in Command property
    /// A2 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="dynamicTables"></param>
    /// <returns></returns>
    public SQLiteCommand GetSqlCreateTable(string table, bool dynamicTables)
    {
        return GetSqlCreateTable(table, dynamicTables, DatabaseLayer.conn);
    }

    //public ColumnsDB(bool signed, string replaceinStoredProceduresI, params SloupecDB[] p)
    //{
    //    this.signed = signed;
    //    this.replaceinStoredProceduresI = replaceinStoredProceduresI;
    //    this.AddRange(p);
    //}
}

