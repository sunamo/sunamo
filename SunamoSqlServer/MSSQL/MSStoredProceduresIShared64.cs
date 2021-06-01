using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MSStoredProceduresI : MSStoredProceduresIBase
{
    public static bool forceIsVps = false;
    static MSStoredProceduresIBase _ci = new MSStoredProceduresIBase();
    public static MSStoredProceduresIBase ci
    {
        get
        {
            return _ci;
        }
        private set
        {
            _ci = value;
        }
    }


    public static string ConvertToVarChar(string item)
    {
        return SqlServerHelper.ConvertToVarChar(item);
    }
}
