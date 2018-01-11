using System;
public static class NormalizeNumbers
{
        public static ushort NormalizeShort(short p)
        {
            int p2 = (int)p;
            int sm = (int)short.MaxValue;
            ushort nt = (ushort)(p2 + sm + 1);
            //nt++;
            return nt;
        }

        public static uint NormalizeInt(int p)
        {
            long p2 = (long)p;
            long sm = (long)int.MaxValue;
            uint nt = (uint)(p2 + sm + 1);
            //nt++;
            return nt;
        }

        public static ulong NormalizeLong(long p)
        {
            decimal p2 = (decimal)p;
            decimal sm = (decimal)long.MaxValue;
            ulong nt = (ulong)(p2 + sm + 1m);
            //nt++;
            return nt;
        }

    public static uint BytesToMegabytes(int size)
    {
        uint normalized = NormalizeInt(size);
        normalized /= 1024;
        normalized /= 1024;
        return normalized;
    }
}
