using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class SqlData
    {
    public static SqlData Empty = new SqlData();

    #region Is not implemented to SqlCommand
    public DateTime getIfNotFound;
    public bool signed = false;
    #endregion
    #region Top distinct
    public int limit = int.MaxValue;
    public bool distinct = false; 
    #endregion
    /// <summary>
    /// must be string.Empty
    /// is adding directly to SQL command
    /// </summary>
    public string orderBy = string.Empty;
    /// <summary>
    /// Is using only when is not specify hard-coded DESC
    /// </summary>
    public SortOrder sortOrder = SortOrder.Descending;
    public int? dnuPozpatku = null;
    /// <summary>
    /// Is used only when GroupByColumn is not only one column to get
    /// </summary>
    public string GroupByColumn = null;
    

    #region Use only in one method
    /// <summary>
    /// Used only in ReadValuesString
    /// value which is insert to output list string when db return DBNull
    /// </summary>
    public string stringUseWhenNull = string.Empty;
    #endregion

    #region Is used with getting columns
    /// <summary>
    /// could be passed into SqlData but actually is not
    /// </summary>
    public string table = string.Empty;
    public List<string> columns = new List<string>();
    public List<object> values = new List<object>(); 
    #endregion

    

    
}

