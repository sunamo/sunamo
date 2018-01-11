using System;
public class MSStoredProceduresISql5 : MSStoredProceduresIBase
{
    static MSStoredProceduresIBase _ci = null;
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

    static string _databaseName = null;
    /// <summary>
    /// Název databáze z výčtu Databases
    /// </summary>
    public static string databaseName
    {
        get
        {
            return _databaseName;
        }
    }

    private MSStoredProceduresISql5()
    {

    }

    /// <summary>
    /// Má na konci "cs" aby se to nepletlo s CreateInstance která měla taky pouze 2 parametry
    /// </summary>
    /// <param name="cs"></param>
    /// <param name="databaseName"></param>
    public static void CreateInstanceCs(string cs, string databaseName)
    {
        if (MSDatabaseLayerSql5.conn != null)
        {
            throw new Exception("Třída MSDatabaseLayerCustom nemůže být inicializovana novým CS");
        }
        MSDatabaseLayerSql5.AssignConnectionString(cs);
        _databaseName = databaseName;
        ci = new MSStoredProceduresIBase(MSDatabaseLayerSql5.conn);
    }
    
    /// <summary>
    /// Toto se musí volat ručně před každým použitím této třídy.
    /// </summary>
    public static void CreateInstance(string dataSource, string database, string databaseName)
    {
                MSDatabaseLayerSql5.LoadNewConnectionFirst(dataSource, database);

        //}
        _databaseName = databaseName;
        ci = new MSStoredProceduresIBase(MSDatabaseLayerSql5.conn);
    }
}
