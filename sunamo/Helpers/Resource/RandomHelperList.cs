using System;
using System.Collections.Generic;
using System.Text;


public class RandomHelperList
{
    public static IEnumerable<int> GenerateNumbers(int length, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return RandomHelper.RandomInt(NH.MinForLength(length), NH.MaxForLength(length));
        }
    }
}