using System;

public class ConvertTypeNameTypeNumbers
{
    /// <summary>
    /// Return null if won't be number
    /// If you will be compared to obtained type, do comparing like typeof(int, string, byte) not typeof(Int32, String, Byte)
    /// Into A1 must be without "System."
    /// </summary>
    /// <param name="idt"></param>
    public static Type ToType(string idt)
    {
        switch (idt)
        {
            case "int":
                return typeof(int);
            case "Int32":
                return typeof(int);
            case "float":
                return typeof(float);
            case "Single":
                return typeof(float);
            case "double":
                return typeof(double);
            case "Double":
                return typeof(double);
            case "decimal":
                return typeof(decimal);
            case "Decimal":
                return typeof(decimal);
            case "byte":
                return typeof(byte);
            case "Byte":
                return typeof(byte);
            case "sbyte":
                return typeof(sbyte);
            case "SByte":
                return typeof(sbyte);
            case "short":
                return typeof(short);
            case "Int16":
                return typeof(short);
            case "long":
                return typeof(long);
            case "Int64":
                return typeof(long);
            case "ushort":
                return typeof(ushort);
            case "UInt16":
                return typeof(ushort);
            case "uint":
                return typeof(uint);
            case "UInt32":
                return typeof(uint);
            case "ulong":
                return typeof(ulong);
            case "UInt64":
                return typeof(ulong);
        }
        return null;
    }
}