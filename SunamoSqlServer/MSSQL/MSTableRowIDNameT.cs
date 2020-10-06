using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class MSTableRowIDNameT<T> where T : struct //: MSBaseRowTable//, ITableRow<int>
{
    MSColumnsDB _columns = null;
    public T ID = SqlServerHelper.EmptyNonSigned<T>();
    /// <summary>
    /// Protože je NChar, musím zde uchovávat i maxLenght
    /// </summary>
    public string Name = null;
    string _tableName = null;

    protected abstract void ParseRow(object[] o);

    /// <summary>
    /// Tento konstruktor byl zakomentovaný - proč, to nevím
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="name"></param>
    public MSTableRowIDNameT(string tableName, string name)
    {
        _tableName = tableName;
        this.Name = name;
    }

    public MSTableRowIDNameT(string tableName, MSColumnsDB columns, string name)
    {
        _tableName = tableName;
        _columns = columns;
        this.Name = name;
    }

    public string TableName
    {
        get { return _tableName; }
    }

    public void SelectInTable()
    {
        object[] o = MSStoredProceduresI.ci.SelectRowReader(TableName, "ID,Name", "ID", ID);
        if (o != null)
        {
            ID = GetT(o, 0);
            Name = MSTableRowParse.GetString(o, 1);
        }
    }

    protected abstract T GetT(object[] o, int i);

    public T InsertToTable()
    {
        ID = (T)(dynamic)MSStoredProceduresI.ci.Insert(TableName, typeof(T), "ID", Name);
        return ID;
    }

    public T InsertToTable2()
    {
        ID = (T)(dynamic)MSStoredProceduresI.ci.Insert2(TableName, ColumnNamesWeb.ID, typeof(T),  Name);
        return ID;
    }

    public void UpdateInTable()
    {
        MSStoredProceduresI.ci.Update(TableName, "ID", ID, "Name", Name);

    }

    public void DeleteFromTable()
    {
        MSStoredProceduresI.ci.Delete(TableName, "ID", ID);
    }

}