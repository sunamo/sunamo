using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

public partial class MSTSP // : IStoredProceduresI<SqlConnection, SqlCommand>
{
    public object[] SelectDataTableOneRow(SqlTransaction tran, string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        return SelectOneRow(tran, TableName, nazevSloupce, hodnotaSloupce);
    }

    // Duplikátní metoda
    /// <summary>
    /// G null pokud nenalezne
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="hledanySloupec"></param>
    
    public float UpdatePlusRealValue(SqlTransaction tran, string table, string sloupecKUpdate, float pridej, string sloupecID, int id)
    {
        float d = float.Parse(SelectCellDataTableStringOneRow(tran, table, sloupecID, id, sloupecKUpdate).ToString());
        if (d != 0)
        {
            pridej = (d + pridej) / 2;
        }
        Update(tran, table, sloupecKUpdate, pridej, sloupecID, id);
        return pridej;
    }


    public int SelectCellDataTableIntOneRow(SqlTransaction tran, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        DataTable dt = SelectDataTable(tran, comm);
        if (dt.Rows.Count == 0)
        {
            return -1;
        }
        return int.Parse(dt.Rows[0].ItemArray[0].ToString());
    }

    public object SelectCellDataTableObjectOneRow(SqlTransaction tran, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        DataTable dt = SelectDataTable(tran, comm);
        if (dt.Rows.Count == 0)
        {
            return null;
        }
        return dt.Rows[0].ItemArray[0];
    }



    public void InsertToTable3(SqlTransaction tran, string table, string sloupce, string valuesParams, object[] values)
    {
        SqlCommand comm = new SqlCommand("INSERT INTO" + " " + table + AllStrings.space + sloupce + " " + "VALUES" + " " + valuesParams);
        for (int i = 0; i < values.Length; i++)
        {
            AddCommandParameter(comm, i, values[i]);
        }
        ExecuteNonQuery(tran, comm);
        //InsertToTable2;
    }
}