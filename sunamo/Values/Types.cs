using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Types
{
    // must be in Types.desktop
    //
    public static readonly Type tVoidString = typeof(VoidString);
    public static readonly Type tObject = typeof(object);
    
    public static readonly Type tStringBuilder = typeof(StringBuilder);

    public static readonly Type tIEnumerable = typeof(IEnumerable);

    #region Same seria as in DefaultValueForTypeT
    public static readonly Type tString = typeof(string);
    public static readonly Type tBool = typeof(bool);

    #region Signed numbers
    public static readonly Type tFloat = typeof(float);
    public static readonly Type tDouble = typeof(double);
    public static readonly Type tInt = typeof(int);
    public static readonly Type tLong = typeof(long);
    public static readonly Type tShort = typeof(short);
    public static readonly Type tDecimal = typeof(decimal);
    public static readonly Type tSbyte = typeof(sbyte);
    #endregion

    #region Unsigned numbers
    public static readonly Type tByte = typeof(byte);
    public static readonly Type tUshort = typeof(ushort);
    public static readonly Type tUint = typeof(uint);
    public static readonly Type tUlong = typeof(ulong);
    #endregion

    public static readonly Type tDateTime = typeof(DateTime);
    public static readonly Type tBinary = typeof(byte[]);
    public static readonly Type tGuid = typeof(Guid);
    public static readonly Type tChar = typeof(char);
    #endregion

    public static readonly List<Type> allBasicTypes = CA.ToList<Type>(tObject, tString, tStringBuilder, tInt, tDateTime,
        tDouble, tFloat, tChar, tBinary, tByte, tShort, tBinary, tLong, tDecimal, tSbyte, tUshort, tUint, tUlong);
    public static readonly Type tListLong = typeof(List<long>);
    public static readonly Type list = typeof(IList);
}