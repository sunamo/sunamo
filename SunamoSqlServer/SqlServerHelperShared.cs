using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;

public partial class SqlServerHelper
{
    

    public static T EmptyNonSigned<T>() where T : struct
    {
        var t = typeof(T);

        if (t == Types.tShort)
        {
            short v = -1;
            return (T)(dynamic)v;
        }
        if (t == Types.tInt)
        {
            short v = -1;
            return (T)(dynamic)v;
        }
        ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), t);
        return default(T);
    }


    

    


    

    

    
}