using System;

public static partial class NormalizeNumbers{ 
public static uint NormalizeInt(int p)
    {
        long p2 = (long)p;
        long sm = (long)int.MaxValue;
        uint nt = (uint)(p2 + sm + 1);
        //nt++;
        return nt;
    }
}