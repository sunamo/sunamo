using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlResult<T> : SqlResult
{
    public T result = default(T);

    public SqlResult()
    {
    }

    public SqlResult(SqlResult v)
    {
        base.sqlExceptions = v.sqlExceptions;
    }

}

/// <summary>
/// Can be null but only in case whether no operation was processed with db / no rows was returned
/// </summary>
public class SqlResult
{
    public SqlExceptions sqlExceptions = SqlExceptions.None;
    public string duplicated1 = string.Empty;
    //public string duplicated2 = string.Empty;

    //public string column = string.Empty;

    #region Passed from SqlData but actually is used in SunamoCzAdmin.SqlExceptionResolver
    public string table = string.Empty;
    public List<string> columns = new List<string>();
    public List<object> values = new List<object>();
    #endregion

    public int affectedRows = 0;

    /// <summary>
    /// Is always filled with result.exc = Exceptions.TextOfExceptions(ex);
    /// After calling SqlExceptionResolver.
    /// </summary>
    public string exc;
   
    public Exception ex
    {
        set
        {
            exc = Exceptions.TextOfExceptions(value);
        }
    }

    //public long resultLong;
    //public Guid resultGuid;
    //public int resultInt;
    //public short resultShort;
    //public byte resultByte;
    //public bool resultBool;
    //public float resultFloat;
    //public object resultObject;
    //public bool? resultNullableBool;

    public SqlResult()
    {

    }

    public SqlResult(SqlData d)
    {
        //this.table = d.table;
        //this.columns = d.columns;
        //this.values = d.values;
    }
}

