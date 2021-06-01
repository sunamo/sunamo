using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MSDatabaseLayer
{
    public static bool LoadNewConnection(string cs)
    {
        MSDatabaseLayer.cs = cs;

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

    /// <summary>
    /// Cant be in MSDatabaseLayerBase because it was shared between MSDatabaseLayer and MSDatabaseLayerSql5
    /// </summary>
    public static SqlConnection conn = null;



}
