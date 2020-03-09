﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;



/// <summary>
/// Třída musí být public, protože jinak se mi ji nepodaří zkompilovat
/// </summary>
public class MSStoredProceduresI : MSStoredProceduresIBase // : IStoredProceduresI<SqlConnection, SqlCommand>
{
    static Type type = typeof(MSStoredProceduresI);

    public static void SetVariable(SqlConnection ci, string databaseName)
    {
        ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(),type, RH.CallingMethod(), "Commented due to new approach - create new db conn with every request");
        //_ci.conn = ci;
        //MSDatabaseLayer._conn = ci;
        //_databaseName = databaseName;
    }

    static string _databaseName = null;
    /// <summary>
    /// Název databáze z výčtu Databases
    /// </summary>
    public static string databaseName
    {
        get
        {
            return _databaseName;
        }
    }

    static MSStoredProceduresIBase _ci = new MSStoredProceduresIBase();

    public static MSStoredProceduresIBase ci
    {
        get
        {
            return _ci;
        }
        private set
        {
            _ci = value;
        }
    }

    public static string ConvertToVarChar(string item)
    {
        return SqlServerHelper.ConvertToVarChar(item);
    }
}