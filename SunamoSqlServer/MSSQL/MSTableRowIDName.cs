using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




public class MSTableRowIDName : MSTableRowIDNameT<int> //: MSBaseRowTable//, ITableRow<int>
{
    public MSTableRowIDName(string tableName, string name) : base(tableName, name)
    {
    }

    public MSTableRowIDName(string tableName, MSColumnsDB columns, string name) : base(tableName, columns, name)
    {
    }

    protected override int GetT(object[] o, int i)
    {
        return MSTableRowParse.GetInt(o, i);
    }

    protected override void ParseRow(object[] o)
    {
        ID = MSTableRowParse.GetInt(o, 0);
        Name = MSTableRowParse.GetString(o, 1);
    }
}