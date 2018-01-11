using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




public class MSTableRowIDName //: MSBaseRowTable//, ITableRow<int>
{
    MSColumnsDB _columns = null;
    public int ID = -1;
    /// <summary>
    /// Protože je NChar, musím zde uchovávat i maxLenght
    /// </summary>
    public string Name = null;
    string _tableName = null;

    private void ParseRow(object[] o)
    {
        ID = MSTableRowParse.GetInt(o, 0);
        Name = MSTableRowParse.GetString(o, 1);
    }

    /// <summary>
    /// Tento konstruktor byl zakomentovaný - proč, to nevím
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="name"></param>
    public MSTableRowIDName(string tableName, string name)
    {
        _tableName = tableName;
        this.Name = name;
    }

    public MSTableRowIDName(string tableName, MSColumnsDB columns, string name)
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
        object[] o = MSStoredProceduresI.ci.SelectRowReader(TableName, "ID", ID, "ID,Name");
        if (o != null)
        {
            ID = MSTableRowParse.GetInt(o, 0);
            Name = MSTableRowParse.GetString(o, 1);
        }
    }

    public int InsertToTable()
    {
        ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int), "ID", Name);
        return ID;
    }

    public void UpdateInTable()
    {
        MSStoredProceduresI.ci.UpdateOneRow(TableName, "ID", ID, "Name", Name);
        
    }

    public void DeleteFromTable()
    {
        MSStoredProceduresI.ci.Delete(TableName, "ID", ID);
    }

}

