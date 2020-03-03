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
public partial class MSStoredProceduresIBase : SqlServerHelper
{
    public static PpkOnDrive loggedCommands = null;



    static MSStoredProceduresIBase()
    {
        //var f = AppData.ci.GetFile(AppFolders.Logs, "sqlCommands.txt");
        //loggedCommands = new PpkOnDrive(f);
    }

    /// <summary>
    /// A1 NSN
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="aB"></param>
    
    public int Update(string table, string columnToUpdate, object newValue, params AB[] abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        int vr = ExecuteNonQuery(comm);
        return vr;
    }

    public int UpdateMany(string table, string columnID, object valueID, params AB[] update)
    {
        int vr = 0;
        foreach (AB item in update)
        {

            string sql = string.Format("UPDATE {0} SET {1} = @p0 {2}", table, item.A, GeneratorMsSql.SimpleWhere("ID", 1));
            SqlCommand comm = new SqlCommand(sql);
            AddCommandParameter(comm, 0, item.B);
            AddCommandParameter(comm, 1, valueID);

            vr += ExecuteNonQuery(comm);
        }
        return vr;
    }

    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params object[] setsNameValue)
    {
        ABC abc = new ABC(setsNameValue);
        UpdateValuesCombination(TableName, nameOfColumn, valueOfColumn, abc.ToArray());
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params AB[] sets)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        //int pocetParametruSets = sets.Length;
        int indexParametrWhere = sets.Length;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}={3}", TableName, setString, nameOfColumn, "@p" + (indexParametrWhere).ToString()));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }
        AddCommandParameter(comm, indexParametrWhere, valueOfColumn);
        // NT-Při úpravách uprav i UpdateValuesCombinationCombinedWhere
        ExecuteNonQuery(comm);
    }


}