using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;



/// <summary>
/// Třída musí být public, protože jinak se mi ji nepodaří zkompilovat
/// </summary>
public class MSStoredProceduresI : MSStoredProceduresIBase // : IStoredProceduresI<SqlConnection, SqlCommand>
{
    public static void SetVariable(SqlConnection ci, string databaseName)
    {
        _ci.conn = ci;
        _databaseName = databaseName;
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

    static MSStoredProceduresIBase _ci = new MSStoredProceduresIBase(null);
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
}
