/// <summary>
/// 
/// </summary>
using System.Collections.Generic;

/// <summary>
/// Toto se d� pou��t, pokud chc i vracet Ppk. Je to trochu nel, ale d� se to.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISearching<T>
{
    T Search(string co);
}



/// <summary>
/// Nen� typov�, vrac� string. Mohl by jsem sice object, ale byly by obr. probl�my s p�et.
/// Pokud je ve t��d� v�ce t��d s int. stejn�m hled�n�m..
/// To by m� zaj�mlo, pro� jsem to tak debiln� pod�dil
/// </summary>
public interface ISearching : ISearching<string>
{
    
}

