﻿using System.IO;


using System.Collections.Generic;
using System;
using System.Text;
using System.Data.SqlClient;

public class MSDatabaseLayerSql5 : MSDatabaseLayer
{


    static string cs = null;
    static bool closing = false;

    static string dataSource2 = "";
    static string database2 = "";
    /// <summary>
    /// Není veřejná, místo ní používej pro otevírání databáze metodu LoadNewConnectionFirst
    /// Používá se když chci otevřít nějakou DB která nenese jen jméno aplikace
    /// </summary>
    /// <param name="file"></param>
    private static bool LoadNewConnection(string dataSource, string database)
    {
        string cs = null;
        cs = "Data Source=" + dataSource;
        if (!string.IsNullOrEmpty(database))
        {
            cs += ";Database=" + database;
        }
        cs += ";" + "Integrated Security=True;MultipleActiveResultSets=True" + ";TransparentNetworkIPResolution=False;Max Pool Size=50000;Pooling=True;";
        conn = new SqlConnection(cs);
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;

    }
    /// <summary>
    /// Používá se ve desktopových aplikacích
    /// Používá se když chci otevřít nějakou DB která nenese jen jméno aplikace
    /// </summary>
    /// <param name="file"></param>
    public static bool LoadNewConnectionFirst(string dataSource, string database)
    {
        dataSource2 = dataSource;
        database2 = database;
        bool vr = LoadNewConnection(dataSource, database);

        conn.Disposed += new EventHandler(conn_Disposed);
        conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
        conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
        return vr;
    }

    //public static void AssignConnectionString(string cs2)
    //{
    //    cs = cs2;
    //        if (_conn != null)
    //        {
    //            closing = true;
    //            _conn.Close();
    //            _conn.Dispose();
    //            closing = false;
    //        }
    //        LoadNewConnectionFirst(cs);
    //}



    #region Prace s vlastni DB


    /// <summary>
    /// Může se používat pouze v programu SqLiteTest nebo v jiných používající více DB souborů !!!
    /// </summary>
    public static void CloseDbFile()
    {
        if (conn != null)
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        //customDB = false;
    }

    #endregion

    static void LoadNewConnectionFirst(string cs2)
    {
        LoadNewConnection(cs2);
        if (conn != null)
        {
            conn.Disposed += new EventHandler(conn_Disposed);
            conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
            conn.StateChange += new System.Data.StateChangeEventHandler(conn_StateChange);
        }
    }

    static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    {
        // TODO: Později implementovat
    }

    static void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
    {
        if (e.CurrentState == System.Data.ConnectionState.Broken)
        {
            if (conn != null)
            {
                if (!closing)
                {
                    conn.Open();
                }

            }
        }
        else if (e.CurrentState == System.Data.ConnectionState.Closed)
        {
            if (conn != null)
            {
                if (!closing)
                {
                    conn.Open();
                }

            }
        }
    }

    static void conn_Disposed(object sender, EventArgs e)
    {
        if (!closing)
        {
            LoadNewConnection(cs);
        }
    }

    public static void LoadNewConnection(string cs)
    {
        conn = new SqlConnection(cs);

        conn.Open();
    }





}
