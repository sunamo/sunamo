using System;
using System.Data;
using System.Text;
public class SloupecDBBase< MSSloupecDB, SqlDbType2>
{
    

    /// <summary>
    /// Must after call insert to static sloupecDb and databaseLayer
    /// </summary>
    /// <param name="sloupecDb"></param>
    static SloupecDBBase()
    {
        MSDatabaseLayer.SetFactoryColumnDb();
        // cant set up because its abstract
        //factoryColumnDB = MSFactoryColumnDB.Instance;
    }

    #region MyRegion
    public SqlDbType2 typ = default( SqlDbType2);
    string _nazev = "";
    public Signed _signed = Signed.Other;
    public bool _canBeNull = false;
    public bool canBeNull
    {
        get
        {
            return _canBeNull;
        }
        set
        {
            // 10-12-2019 origianlly there canBeNull return false and set was empty. But then I cant ActivateCs - trying save null to SessionID
            _canBeNull = value;
        }
    }
    public bool mustBeUnique = false;
    public bool primaryKey = false;
    public string referencesTable = null;
    public string referencesColumn = null;

    public SqlDbType Type2
    {
        get
        {
            return (SqlDbType)Enum.Parse(typeof(SqlDbType), Type.ToString());
        }
    }

    public SqlDbType2 Type
    {
        get
        {
            return typ;
        }
        set
        {
            typ = value;
        }
    }

     string delka = "";
     public string Delka
     {
         get
         {
             return delka;
         }
     }

    public string LengthWithoutBraces
    {
        get
        {
            return SH.TrimStartAndEnd(delka, AllStrings.lb, AllStrings.rb);
        }
    }
    /// <summary>
    /// Is without lenght
    /// </summary>
    public string Name
    {
        get
        {
            return _nazev;
        }
        set
        {
            int dex = value.IndexOf(AllChars.lb);
            if (dex != -1)
            {
                _nazev = value.Substring(0, dex);
                // Délka se zde zadává i se závorkami
                delka = value.Substring(dex);//, value.Length - _nazev.Length - 2);
            }
            else
            {
                _nazev = value;
            }
        }
    }

    /// <summary>
    /// IUN, SQL Server to nepodporuje
    /// </summary>
    public Signed IsSigned
    {
        get
        {
            return _signed;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CanBeNull
    {
        get
        {
            return canBeNull;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool MustBeUnique
    {
        get
        {
            return mustBeUnique;
        }
    }

    public bool PrimaryKey
    {
        get
        {
            return primaryKey;
        }
    }
    public bool isNewId = false;
    public bool IsNewId
    {
        get
        {
            return isNewId;
        }
    }
    #endregion



    #region d
    public string ReferencesTo()
    {
        return SH.Format2("{0}[{1}]", referencesTable, referencesColumn);
    }

    public string InfoToTextBox()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Datový typ" + ": " + databaseLayer.usedTa[ typ]);
        sb.AppendLine("Název" + ": " + _nazev);
        sb.AppendLine("Je primárním klíčem" + ": " + BTS.BoolToStringEn(primaryKey));
        sb.AppendLine("Nemusí být zadána" + ": " + BTS.BoolToStringEn(canBeNull));
        sb.AppendLine("Musí být jedinečná" + ": " + BTS.BoolToStringEn(mustBeUnique));
        sb.AppendLine();
        if (referencesTable != null)
        {
            sb.AppendLine("Odkazuje na tabulku[sloupec" + "]:");
            sb.AppendLine(ReferencesTo());
        }
        return sb.ToString();
    }
    
    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(databaseLayer.usedTa[ typ] + AllStrings.space + _nazev);
        if (referencesTable != null)
        {
            sb.Append(" " + "odkazuje na" + " " + ReferencesTo());
        }
        return sb.ToString();
    }

    #endregion
    
    public static IDatabaseLayer<SqlDbType2> databaseLayer = null;

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey)
    {
        
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, primaryKey);
    }

    public bool IsUnicode = false;

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, false);
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey, string referencesTable, string referencesColumn)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, referencesTable, referencesColumn, false);
    }

    /// <summary>
    /// Pokud použiji metodu bez A3/4, doplní se do obou false
    /// </summary>
    /// <param name="typ"></param>
    /// <param name="nazev"></param>
    /// <param name="canBeNull"></param>
    /// <param name="mustBeUnique"></param>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool canBeNull, bool mustBeUnique)
    {
        MSSloupecDB db = factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, canBeNull, mustBeUnique, null, null, false);
        return db;
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string name, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn)
    {
        return factoryColumnDB.CreateInstance(typ, name, Signed.Other, canBeNull, mustBeUnique, referencesTable, referencesColumn, false);
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, canBeNull, mustBeUnique, referencesTable, referencesColumn, primaryKey);
    }

    public static IFactoryColumnDB<MSSloupecDB, SqlDbType2> factoryColumnDB = null;



}