using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class NumConsts
{
    public const int defaultPortIfCannotBeParsed = 587;
    /// <summary>
    /// Min age is 18 due to GDPR - below 18 is needed parent agreement of child
    /// </summary>
    public const int MinAge = 18;
    public static short nDtMinVal = 10101;
    public static short nDtMaxVal = 32271;
    public const long kB = 1024;
    public const double zeroDouble = 0;
    public const int zeroInt = 0;
    public const float zeroFloat = 0;
    /// <summary>
    /// at int should be no postfix
    /// </summary>
    public const int one = 1;
    public const int mOne = -1;


}
