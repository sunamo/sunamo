using System;

public static partial class NormalizeNumbers
{
    static long intMax = (long) int.MaxValue;
    static long one = 1;
    public static uint NormalizeInt(int p)
    {
        //long p2 = (long)p;
        
        uint nt = (uint)(p + intMax + one);
        //nt++;
        return nt;
    }



    public static ushort NormalizeShort(short p)
    {
        int p2 = (int)p;
        int sm = (int)short.MaxValue;
        ushort nt = (ushort)(p2 + sm + 1);
        //nt++;
        return nt;
    }
}