using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class SqlData
    {
    public static SqlData Empty = new SqlData();
    public bool distinct = false;
    public bool signed = false;
    public string stringUseWhenNull = string.Empty;
    /// <summary>
    /// could be passed into SqlData but actually is not
    /// </summary>
    public string table = string.Empty;
    public List<string> columns = new List<string>();
    public List<object> values = new List<object>();
    internal DateTime getIfNotFound;
    public string GroupByColumn = null;
    public int limit = int.MaxValue;

    /// <summary>
    /// must be string.E
    /// </summary>
    public string orderBy = string.Empty;
    public SortOrder sortOrder = SortOrder.Descending;
    public  int? dnuPozpatku = null;
}

