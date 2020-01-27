using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InstancesSqlResult
{
    static Type type = typeof(InstancesSqlResult);
    //public static SqlResult<string> emptyString = Create<string>();
    //internal static SqlResult<List<long>> listLong = Create<List<long>>();
    //internal static SqlResult<IList> list = Create<IList>();
    //internal static SqlResult<object[]> arrayObjectNull = null;
    //internal static SqlResult<short> shortMinValue = ValueMinValue<short>();

    //private static SqlResult<T> ValueMinValue<T>()
    //{

    //}

    //internal static SqlResult<short> shortZero = ValueZero<short>();
    //internal static SqlResult<int> intMinValue = ValueZero<int>();
    //internal static SqlResult<int> intZero = ValueZero<int>();
    //internal static SqlResult<byte> byteZero = ValueZero<byte>();

    //private static SqlResult<T> ValueZero<T>()
    //{
    //    var t = typeof(T);
    //    object r = null;
    //    if (t == Types.tFloat)
    //    {
    //        r = string.Empty;
    //    }


    //}

    //internal static SqlResult<long> longMinusOne = ;
    //internal static SqlResult<uint> uintZero;

    /// <summary>
    /// Into A2 of methods in InstancesSqlResult cant be inserted null, but Empty value. Its for easy finding where is passed empty value
    /// </summary>
    internal static SqlResult Empty = new SqlResult();

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
            ThrowExceptions.NotImplementedCase(type, RH.CallingMethod());
        }

        return RuntimeHelper.CastToGeneric<T>(r);
    }

    internal static SqlResult<short> Short(short odeber, SqlResult result)
    {
        var resultShort = new SqlResult<short>(result);
        resultShort.result = odeber;
        return resultShort;
    }

    internal static SqlResult<ushort> Ushort(ushort odeber, SqlResult result)
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

    internal static SqlResult<string> String(string save, SqlResult update)
    {
        var resultString = new SqlResult<string>();
        resultString.result = save;
        return resultString;
    }












    internal static SqlResult<DateTime> DateTime(DateTime getIfNotFound, SqlResult o)
    {
        var resultString = new SqlResult<DateTime>();
        resultString.result = getIfNotFound;
        return resultString;
    }

    internal static SqlResult<bool> Bool(bool v, SqlResult o)
    {
        var resultString = new SqlResult<bool>();
        resultString.result = v;
        return resultString;
    }

    internal static SqlResult<float> Float(float maxValue, SqlResult o)
    {
        var resultString = new SqlResult<float>();
        resultString.result = maxValue;
        return resultString;
    }

    internal static SqlResult<Guid> Guid(Guid v, SqlResult result)
    {
        var resultString = new SqlResult<Guid>();
        resultString.result = v;
        return resultString;
    }

    internal static SqlResult<Guid> Guid(string v, SqlResult result)
    {
        var resultString = new SqlResult<Guid>();
        resultString.result = System. Guid. Parse( v);
        return resultString;
    }

    internal static SqlResult<List<string>> ListString(List<string> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<string>>();
        resultString.result = vr;
        return resultString;
    }

    internal static SqlResult<SqlDataReader> SqlDataReader(SqlDataReader result, SqlResult empty)
    {
        var resultString = new SqlResult<SqlDataReader>();
        resultString.result = result;
        return resultString;
    }

    internal static SqlResult<bool?> NullableBool(bool? p, SqlResult o)
    {
        var resultString = new SqlResult<bool?>();
        resultString.result = p;
        return resultString;
    }

    internal static SqlResult<List<int>> ListInt(List<int> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<int>>();
        resultString.result = vr;
        return resultString;
    }

    internal static SqlResult<List<byte>> ListByte(List<byte> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<byte>>();
        resultString.result = vr;
        return resultString;
    }

    internal static SqlResult<List<long>> ListLong(List<long> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<long>>();
        resultString.result = vr;
        return resultString;
    }

    internal static SqlResult<DataTable> DataTable(DataTable dt)
    {
        var resultString = new SqlResult<DataTable>();
        resultString.result = dt;
        return resultString;
    }

    internal static SqlResult<List<DateTime>> ListDateTime(List<DateTime> vr, SqlResult r2)
    {
        var resultString = new SqlResult<List<DateTime>>();
        resultString.result = vr;
        return resultString;
    }

    internal static SqlResult<object[]> ArrayObject(object[] itemArray, SqlResult dt)
    {
        var resultString = new SqlResult<object[]>();
        resultString.result = itemArray;
        return resultString;
    }

    internal static SqlResult<object> Object(long i, SqlResult all2)
    {
        var resultString = new SqlResult<object>();
        resultString.result = i;
        return resultString;
    }

    internal static SqlResult<uint> Uint(uint v, SqlResult o)
    {
        var resultString = new SqlResult<uint>();
        resultString.result = v;
        return resultString;
    }

    internal static SqlResult<IList> List(object[] o)
    {
        throw new NotImplementedException();
    }

    internal static SqlResult<List<short>> ListShort(List<short> vr, SqlResult<SqlDataReader> r2)
    {
        throw new NotImplementedException();
    }
}

