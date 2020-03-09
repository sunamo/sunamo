using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A2 in any method (SqlResult dt) cant be never null
/// </summary>
public class InstancesSqlResult
{
    static Type type = typeof(InstancesSqlResult);
    //public static SqlResult<string> emptyString = Create<string>();
    //public static SqlResult<List<long>> listLong = Create<List<long>>();
    //public static SqlResult<IList> list = Create<IList>();
    //public static SqlResult<object[]> arrayObjectNull = null;
    //public static SqlResult<short> shortMinValue = ValueMinValue<short>();

    //private static SqlResult<T> ValueMinValue<T>()
    //{

    //}

    //public static SqlResult<short> shortZero = ValueZero<short>();
    //public static SqlResult<int> intMinValue = ValueZero<int>();
    //public static SqlResult<int> intZero = ValueZero<int>();
    //public static SqlResult<byte> byteZero = ValueZero<byte>();

    //private static SqlResult<T> ValueZero<T>()
    //{
    //    var t = typeof(T);
    //    object r = null;
    //    if (t == Types.tFloat)
    //    {
    //        r = string.Empty;
    //    }


    //}

    //public static SqlResult<long> longMinusOne = ;
    //public static SqlResult<uint> uintZero;

    /// <summary>
    /// Into A2 of methods in InstancesSqlResult cant be inserted null, but Empty value. Its for easy finding where is passed empty value
    /// </summary>
    public static SqlResult Empty = new SqlResult();

    private static SqlResult<T> Create<T>()
    {
        SqlResult<T> result = new SqlResult<T>();
        result.result = Value<T>();
        return result;
    }

    private static T Value<T>()
    {
        var t = typeof(T);
        object r = null;
        if (t == Types.tString)
        {
            r = string.Empty;
        }
        else if (t == Types.tListLong)
        {
            r = new List<long>();
        }
        else if (t == Types.list)
        {
            r = new List<object>();
        }
        else
        {
            ThrowExceptions.NotImplementedCase(RuntimeHelper.GetStackTrace(),type, RH.CallingMethod(), t);
        }

        return RuntimeHelper.CastToGeneric<T>(r);
    }

    public static SqlResult<short> Short(short odeber, SqlResult result)
    {
        var resultShort = new SqlResult<short>(result);
        resultShort.result = odeber;
        return resultShort;
    }

    public static SqlResult<ushort> Ushort(ushort odeber, SqlResult result)
    {
        var resultUshort = new SqlResult<ushort>(result);
        resultUshort.result = odeber;
        return resultUshort;
    }

    public static SqlResult<long> Long(long n, SqlResult result2)
    {
        var result = new SqlResult<long>(result2);
        result.result = n;
        return result;
    }

    public static SqlResult<int> Int(int n, SqlResult result)
    {
        var resultInt = new SqlResult<int>(result);
        resultInt.result = n;
        return resultInt;
    }

    public static SqlResult<byte> Byte(byte pridej,  SqlResult result)
    {
        var resultByte = new SqlResult<byte>();
        resultByte.result = pridej;
        return resultByte;
    }

    public static SqlResult<string> String(string save, SqlResult update)
    {
        var resultString = new SqlResult<string>();
        resultString.result = save;
        return resultString;
    }












    public static SqlResult<DateTime> DateTime(DateTime getIfNotFound, SqlResult o)
    {
        var resultString = new SqlResult<DateTime>();
        resultString.result = getIfNotFound;
        return resultString;
    }

    public static SqlResult<bool> Bool(bool v, SqlResult o)
    {
        var resultString = new SqlResult<bool>();
        resultString.result = v;
        return resultString;
    }

    public static SqlResult<float> Float(float maxValue, SqlResult o)
    {
        var resultString = new SqlResult<float>();
        resultString.result = maxValue;
        return resultString;
    }

    public static SqlResult<Guid> Guid(Guid v, SqlResult result)
    {
        var resultString = new SqlResult<Guid>();
        resultString.result = v;
        return resultString;
    }

    public static SqlResult<Guid> Guid(string v, SqlResult result)
    {
        var resultString = new SqlResult<Guid>();
        resultString.result = System. Guid. Parse( v);
        return resultString;
    }

    public static SqlResult<List<string>> ListString(List<string> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<string>>();
        resultString.result = vr;
        return resultString;
    }

    public static SqlResult<SqlDataReader> SqlDataReader(SqlDataReader result, SqlResult empty)
    {
        var resultString = new SqlResult<SqlDataReader>();
        resultString.result = result;
        return resultString;
    }

    public static SqlResult<bool?> NullableBool(bool? p, SqlResult o)
    {
        var resultString = new SqlResult<bool?>();
        resultString.result = p;
        return resultString;
    }

    public static SqlResult<List<int>> ListInt(List<int> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<int>>();
        resultString.result = vr;
        return resultString;
    }

    public static SqlResult<List<byte>> ListByte(List<byte> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<byte>>();
        resultString.result = vr;
        return resultString;
    }

    public static SqlResult<List<long>> ListLong(List<long> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<long>>();
        resultString.result = vr;
        return resultString;
    }

    public static SqlResult<DataTable> DataTable(DataTable dt)
    {
        var resultString = new SqlResult<DataTable>();
        resultString.result = dt;
        return resultString;
    }

    public static SqlResult<List<DateTime>> ListDateTime(List<DateTime> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<DateTime>>();
        resultString.result = vr;
        return resultString;
    }

    public static SqlResult<object[]> ArrayObject(object[] itemArray, SqlResult dt)
    {
        var resultString = new SqlResult<object[]>();
        resultString.result = itemArray;
        return resultString;
    }

    public static SqlResult<object> Object(long i, SqlResult all2)
    {
        var resultString = new SqlResult<object>();
        resultString.result = i;
        return resultString;
    }

    public static SqlResult<uint> Uint(uint v, SqlResult o)
    {
        var resultString = new SqlResult<uint>();
        resultString.result = v;
        return resultString;
    }

    public static SqlResult<IList> List(IList o)
    {
        var resultString = new SqlResult<IList>();
        resultString.result = o;
        return resultString;
    }

    public static SqlResult<List<short>> ListShort(List<short> vr, SqlResult<SqlDataReader> r2)
    {
        var resultString = new SqlResult<List<short>>();
        resultString.result = vr;
        return resultString;
    }
}