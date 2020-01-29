using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CastSqlResult{ 
public static SqlResult<object[]> FirstRowToArrayObject(SqlResult<DataTable> dt)
    {
        var result = new SqlResult<object[]>();
        result.result = dt.result.Rows[0].ItemArray;
        return result;
    }
}