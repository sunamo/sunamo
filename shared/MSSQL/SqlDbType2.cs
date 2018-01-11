public enum SqlDbType2
{
    #region Nikdy nepoužívat
    
    /// <summary>
    /// Používá se pouze v desktopových aplikacích, kde není tak nutné dbát na velikost
    /// Pokud specifikuješ délku/precison 0-2, velikost bude 6 bajtů. Velikost je počet číslic pro nanosekundy.
    /// </summary>
    DateTime2,
    #endregion
    /// <summary>
    ///  
    /// </summary>
    UniqueIdentifier,
    /// <summary>
    /// Použij tento datový sloupec pokud chceš aby byl DEFAULT(newid()) a jedinečné GUID se tak generovali v DB
    /// </summary>
    UniqueIdentifierAutoNewId,
    /// <summary>
    /// Velikost 3 bajty. Pouze datum od 1.1.0001 do 31.12.9999
    /// </summary>
    Date,
    /// <summary>
    /// Velikost 3 bajty. Datum a čas
    /// </summary>
    SmallDateTime,
    /// <summary>
    /// Real je stejný jako v C# float, pokud chci použít v SQL Serveru datový typ double(C#), musím použít Float(SQL Server typ)
    /// Velikost 4 bajty. Číslo s desetinnou čárkou obsahující vždy 7 číslic, navíc může obsahovat - a , ale počet číslic zůstavá stejný = 7. 
    /// POZOR: Pokud do DB zadám desetinné číslo, které bude mít více než 7 čísel(vč. těch před čárkou), bude číslo zaokrouhleno na celé číslo.
    /// POZOR: Pokud do DB zapíšu větší celé číslo než 9999999 nebo menší než -9999999, do DB se to uloží ve exponenciálním formátu, pro číslo 99999998 na 1E+08
    /// </summary>
    Real,
    /// <summary>
    /// Velikost 4 bajty. Číslo bez desetinné čárky v rozsahu od -2,147,483,648 do 2,147,483,647
    /// </summary>
    Int,
    /// <summary>
    /// Velikost 2*počet_znaků bajtů + 2 bajty. Omezený řetězec na max. 4000 znaků(8000 bajtů). Uvážlivě dávej NVarChar tam kde skutečně člověk neví jak dlouhý bude text a může být dlouhý a tam kde se z tabulky nebere jen 1 určitý sloupec(nebo více) - pro takové případy je lepší NChar. Obecně se snaž používat více NVarChar z důvodu úspory místa v DB, ta rychlost není zase až tak podstatná a navíc uživatelé mohou zadat větší množství dat bez toho aby to spolklo větší množství míst. NVarChar se používá stejně jako NText vždy ve samostatných Tabulkách.
    /// Lze zadat NVarChar(MAX) pro delší texty než 4000 znaků.
    /// </summary>
    NVarChar,
    /// <summary>
    /// Velikost 2*počet_znaků bajtů. Omezený řetězec na max. 4000 znaků(8000 bajtů). NChar se hodí tam kde všechny hodnoty v řádku budou víceméně stejně dlouhé a nechce se ti zakládat nová tabulka pro NVarChar. Jinak je lepší NVarChar
    /// </summary>
    NChar,
    /// <summary>
    /// Velikost 1 bit. Duální hodnota pravda/nepravda
    /// </summary>
    Bit,
    /// <summary>
    /// Velikost 1 bajt. Číslo bez desetinné čárky v rozsahu od 0 do 255
    /// </summary>
    TinyInt,
    /// <summary>
    /// Velikost 2 bajty. Číslo bez desetinné čárky v rozsahu od -32,768 do 32,767
    /// </summary>
    SmallInt,
    /// <summary>
    /// 1-8000 bajtů v pevné velikosti(ve variabilní tu je VarBinary)
    /// </summary>
    Binary,
    /// <summary>
    /// REMEMBER: Už nepoužívat a odstranit, nebo spíše všechny proměnné v MSStoredProcedures mít na long
    /// Velikost 8 bajtů. Hodnota od -9,223,372,036,854,775,808 do 9,223,372,036,854,775,807. Snažit se používat jen v naprosto vyjímečných případech. Já to tu mám pro zachování kompatibility pro GeneralLayer a tabulky Comments, LastVisits aj. 
    /// </summary>
    BigInt,
    /// <summary>
    /// Nepoužívat, minimální velikost je 5 bajtů při max. počtu číslic 9 - do Real se vleze 7 číslic a zabírá v DB jen 4 bajty
    /// Hlavní výhoda tohoto typu je možnost nakonfigurovat celkový počet číslic a počet číslic za desetinnou čárkou. 
    /// První číslo určuje maximální celkový počet číslic.Druhé číslo určuje kolik je z toho za desetinnou čárkou. 
    /// Pro více informací se koukni na http://jepsano.net/2015/03/jak-v-sql-serveru-udelat-sloupec-s-nastavitelnym-plovoucim-typem/
    /// </summary>
    Decimal,
    /// <summary>
    /// Velikost délka textu + 2; Omezený řetězec na maximálně 8000 znaků. Lze zadat VarChar(MAX) pro texty delší než 8000 znaků. Používej jako NVarChar s tím, že se zde nesmí vyskytnout žádný Unicode znak. Lépe řečeno snaž se toto nepoužívat vůbec, mám to tu jen proto, že se používali například v CasdMladezLayer.
    /// Zásadne se může používat pozue pro texty bez diakritiky a jiných spec. znaků, jinak všude NVarChar
    /// </summary>
    VarChar,
    /// <summary>
    /// Velikost délka textu. Omezený řetězec na maximálně 8000 znaků. Používej jako NChar s tím, že se zde nesmí vyskytnout žádný Unicode znak. Lépe řečeno snaž se toto nepoužívat vůbec, mám to tu jen proto, že se používali například v CasdMladezLayer.
    /// Zásadně se může používat pouze pro texty bez diakritiky a jiných spec. znaků, jinak všude NVarChar
    /// </summary>
    Char
}
