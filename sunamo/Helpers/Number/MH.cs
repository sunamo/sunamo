using System;
public class MH
{
    /// <summary>
    /// G nejv�t�� spole�n� d�litel/�initel/faktor (highest common factor)
    /// </summary>
    /// <param name="a">Number a to calculate</param>
    /// <param name="b">Number b to calculate</param>
    /// <returns>The Highest Common Factor of the two numbers</returns>
    /// <remarks>I explicitly checked for zero in the parameters
    /// but decided to not make it throw an exception because
    /// everyone seems to hate exceptions :).</remarks>
    public static int HCF(int a, int b)
    {
        if (a == 0 || b == 0)
            return 0;

        // declare our incremental variables
        int hcf = 1, counter = 1;

        while (counter <= Math.Min(a, b))
        {
            if ((a % counter == 0) && (b % counter == 0)) hcf = counter;

            // increment counter
            counter++;
        }

        // return the highest common factor
        return hcf;
    }

    /// <summary>
    /// Vr�t� nejmen�� spole�n� n�sobek. (lowest common factor)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int LCF(int a, int b)
    {
        for (int i = 2; ; i++)
        {
            if (a % i == 0 && b % i == 0)
            {
                return i;
            }
        }
        return 1;
    }
}
