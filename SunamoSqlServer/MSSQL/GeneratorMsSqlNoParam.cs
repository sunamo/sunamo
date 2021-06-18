using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GeneratorMsSqlNoParam
{
    public static string CombinedWhere(ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, ABC whereOr = null)
    {
        return GeneratorMsSqlWorker.CombinedWhere(false, where, isNotWhere, greaterThanWhere, lowerThanWhere, whereOr);
    }

    public static SqlCommand CombinedWhereCommand(string commandBeforeWhere, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, string orderBy)
    {
        return GeneratorMsSqlWorker.CombinedWhereCommand(false, commandBeforeWhere, where, isNotWhere, greaterThanWhere, lowerThanWhere, orderBy);
    }

    public static string DeleteOneRow(string tn, ABC where)
    {
        InstantSB sb = new InstantSB(AllStrings.space);
        sb.AddItem(SqlConsts.delete);
        sb.AddItem(SqlConsts.top1);
        sb.AddItem(SqlConsts.from);
        sb.AddItem(tn);
        sb.AddItem(CombinedWhere(where, null, null, null));
        return sb.ToString();
    }
}
