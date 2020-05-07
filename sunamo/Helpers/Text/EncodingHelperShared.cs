using System;
using System.Collections.Generic;

public partial class EncodingHelper{ 
public static string PureBytesOperation(Func<List<byte>, List<byte>> b, string s)
    {
        var bytes = BTS.ConvertFromUtf8ToBytes(s);
        bytes = b.Invoke(bytes);
        return BTS.ConvertFromBytesToUtf8(bytes);
    }
}