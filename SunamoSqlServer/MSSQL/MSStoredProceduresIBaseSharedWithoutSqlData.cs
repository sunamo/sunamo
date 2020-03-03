using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;

/// <summary>
/// usings
/// </summary>
public partial class MSStoredProceduresIBase2 : SqlServerHelper
{
    /// <summary>
    /// A1 NSN
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="aB"></param>
    
    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec)
    {
        //List<string> vr = new List<string>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesStringTrim(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, int limit, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + limit + ") {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTableLimit(string tableName, int limit)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") * FROM " + tableName);
        //AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }



}