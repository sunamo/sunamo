using System.Collections.Generic;/// <summary>
/// Pokud chci n�co aplikovat na v�. dT, mus�m to vlo�it zde
/// </summary>
/// <typeparam name="T"></typeparam>


public interface ISearchingAll<T>
{
    List<T> Search(string co);
    /// <summary>
    /// Kontroluje zda n�co nen� null
    /// </summary>
    void JeVseOK();
    /// <summary>
    /// K snadn�mu z�sk�n� prvvku a jeho hodnocen�.
    /// </summary>
    List<string> Names();
    List<T> Ppk();
}
