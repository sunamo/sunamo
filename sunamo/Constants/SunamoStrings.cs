using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Because much of consts here was czech is in xlf cz but not in en and xlfkeys
/// In SunamoCzAdmin I have created WhichIsInCzechButIsntInEnglish
/// 
/// </summary>
public class SunamoStrings
{
    static SunamoStrings()
    {
        messageIfEmpty = MessageIfEmpty("data");
    }

    /// <summary>
    /// Kód, kterým se kontroluje pravost uživatele různých služeb, hlavně u těch kde se já nemusím přihlašovat(jednoduché desktopové apps, atd.)
    /// </summary>
    /// <summary>
    /// Toto nikdy nepoužívat, je to tu jen abych mohl ukončit metodu returnem.
    /// </summary>
    /// <summary>
    /// Nebyly nalezeny žádné data k zobrazení
    /// </summary>
    public static string messageIfEmpty = null;
    public static string IsNotInRange = "is not in range";

    public static string MessageIfEmpty(string p)
    {
        return "Wasn't found any data to show";
    }
}