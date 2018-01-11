using System;
public class ConvertTypeNameTypeNumbers
{
    /// <summary>
    /// Vrátí null pokud A1 nebude číselný typ
    /// Pokud budeš porovnávat na získaný typ, porovnávej typeof(int, string, byte) ne typeof(Int32, String, Byte)
    /// DO A1 musíš zadávat bez "System." - metoda to sama nenahradí
    /// </summary>
    /// <param name="idt"></param>
    /// <returns></returns>
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
