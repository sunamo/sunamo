using System;
using System.Data;
using System.Text;
public class MSSloupecDB
{
    #region MyRegion
    SqlDbType typ = SqlDbType.Real;
    string _nazev = "";
    Signed _signed = Signed.Other;
    bool canBeNull
    {
        get
        {
            return false;
        }
        set
        {

        }
    }
    bool mustBeUnique = false;
    bool primaryKey = false;
    public string referencesTable = null;
    public string referencesColumn = null;

    public SqlDbType Type
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
    public string Name
    {
        get
        {
            return _nazev;
        }
        set
        {
            int dex = value.IndexOf('(');
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
    /// NSN, SQL Server to nepodporuje
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
    bool isNewId = false;
    public bool IsNewId
    {
        get
        {
            return isNewId;
        }
    }
    #endregion

    public MSSloupecDB(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        this.typ = ConvertSqlDbType.ToSqlDbType( typ, out isNewId);
        this.Name = nazev;
        this._signed = signed;
        this.canBeNull = canBeNull;
        this.mustBeUnique = mustBeUnique;
        this.referencesTable = referencesTable;
        this.referencesColumn = referencesColumn;
        this.primaryKey = primaryKey;
    }

    #region d
    public string ReferencesTo()
    {
        return string.Format("{0}[{1}]", referencesTable, referencesColumn);
    }

    public string InfoToTextBox()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Datový typ: " + MSDatabaseLayer.usedTa[ typ]);
        sb.AppendLine("Název: " + _nazev);
        sb.AppendLine("Je primárním klíčem: " + BTS.BoolToStringCs(primaryKey));
        sb.AppendLine("Nemusí být zadána: " + BTS.BoolToStringCs(canBeNull));
        sb.AppendLine("Musí být jedinečná: " + BTS.BoolToStringCs(mustBeUnique));
        sb.AppendLine();
        if (referencesTable != null)
        {
            sb.AppendLine("Odkazuje na tabulku[sloupec]:");
            sb.AppendLine(ReferencesTo());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(MSDatabaseLayer.usedTa[ typ] + " " + _nazev);
        if (referencesTable != null)
        {
            sb.Append(" odkazuje na " + ReferencesTo());
        }
        return sb.ToString();
    }

    public static MSSloupecDB[] CI(SqlDbType2 varChar, object p, bool v1, bool v2)
    {
        throw new NotImplementedException();
    }
    #endregion

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey)
    {
        return new MSSloupecDB(typ, nazev, Signed.Other, false, false, null, null, primaryKey);
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev)
    {
        return new MSSloupecDB(typ, nazev, Signed.Other, false, false, null, null, false);
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey, string referencesTable, string referencesColumn)
    {
        return new MSSloupecDB(typ, nazev, Signed.Other, false, false, referencesTable, referencesColumn, false);
    }

    /// <summary>
    /// Pokud použiji metodu bez A3/4, doplní se do obou false
    /// </summary>
    /// <param name="typ"></param>
    /// <param name="nazev"></param>
    /// <param name="canBeNull"></param>
    /// <param name="mustBeUnique"></param>
    /// <returns></returns>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool canBeNull, bool mustBeUnique)
    {
        MSSloupecDB db = new MSSloupecDB(typ, nazev, Signed.Other, canBeNull, mustBeUnique, null, null, false);
        return db;
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string name, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn)
    {
        return new MSSloupecDB(typ, name, Signed.Other, canBeNull, mustBeUnique, referencesTable, referencesColumn, false);
    }

    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        return new MSSloupecDB(typ, nazev, Signed.Other, canBeNull, mustBeUnique, referencesTable, referencesColumn, primaryKey);
    }




    #region Metody ci společně se Signed
    #endregion
}
