using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Must be in sunamo because is shared between MSSQl and SQL Server project
/// </summary>
/// <typeparam name="MSSloupecDB"></typeparam>
/// <typeparam name="SqlDbType2"></typeparam>
public interface IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    MSSloupecDB CreateInstance(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey);
}