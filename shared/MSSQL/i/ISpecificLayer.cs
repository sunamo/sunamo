using System;
using System.Collections.Generic;

using System.Web;

/// <summary>
/// Ručně je nutno skrýt veřejný konstruktor a zavolat v něm metodu CreateStaticTables() a vytvořit ci
/// </summary>
public interface ISpecificLayer
{
    /// <summary>
    /// Smaže tabulky a vytvoří úplně nové
    /// </summary>
    void ClearAndCreateTables();
    /// <summary>
    /// Vytvoří do statické pp Dictionary<string, ColumnsDB> Tables sloupce které mám v DB
    /// 
    /// </summary>
    void CreateStaticTables();
}
