using System.IO;


using System.Collections.Generic;
using System;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Cant be derived from MSDatabaseLayer because its inherit many method with same name (like LoadNewConnectionFirst) and VS call method from MSDatabaseLayer, not desired MSDatabaseLayerSql5
/// </summary>
public class MSDatabaseLayerSql5 : MSDatabaseLayerBase
{
    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static SqlConnection conn = null;
    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static string dataSource2 = "";
    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static string database2 = "";

    public static bool LoadNewConnection(string dataSource, string database)
    {
        string cs = null;
        cs = "Data Source=" + dataSource;
        if (!string.IsNullOrEmpty(database))
        {
            cs += ";Database=" + database;
        }
        //;TransparentNetworkIPResolution=False is not supported in .NET5
        cs += ";" + "Integrated Security=True;MultipleActiveResultSets=True" + ";Max Pool Size=50000;Pooling=True;";
        //_conn = new SqlConnection(cs);

        //OpenWhenIsNotOpen();
        //conn.Open();

        return LoadNewConnection(cs);


    }

    public static bool LoadNewConnection(string cs)
    {
        MSStoredProceduresISql5.ci._cs = cs;

        //conn = new SqlConnection(cs);
        //try
        //{
        //    conn.Open();
        //    //conn.Close();
        //    //conn.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    return false;
        //}
        return true;
        //if (!string.IsNullOrEmpty(_conn.ConnectionString))
        //{
        //    //OpenWhenIsNotOpen();
        //    conn.Open();
        //}

    }


}