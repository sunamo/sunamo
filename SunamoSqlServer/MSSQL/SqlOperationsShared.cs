using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;

/// <summary>
/// 12-1-2019 refactoring:
/// 1)all methods here take ABC if take more than Where. otherwise is allowed params AB[] / object[]
/// 2) always is table - select / update column - where
/// 
/// </summary>
public partial class SqlOperations : SqlServerHelper
{
    //public static PpkOnDrive loggedCommands = null;
    public static string table2 = null;
    public static string column2 = null;
    public static bool isNVarChar2 = false;
    public static Func<string, string, bool> IsNVarChar = null;
    public string _cs = null;
    string Cs
    {
        get
        {
            if (_cs != null)
            {
                return _cs;
            }

            return MSDatabaseLayer.cs;
        }
    }

    #region Inner classes
    public class Parse
    {
        public class DateTime
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            
    public SqlResult UpdateValuesCombinationCombinedWhere(SqlData d, string TableName, ABC sets, ABC where)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        string whereString = GeneratorMsSql.CombinedWhere(where);
        int indexParametrWhere = sets.Length + 1;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}", TableName, setString, whereString));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }
        indexParametrWhere--;
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, indexParametrWhere++, where[i].B);
        }
        // NT-Při úpravách uprav i UpdateValuesCombination
        var result = ExecuteNonQuery(d, comm);
        return result;
    }
    #endregion
    #endregion
}
