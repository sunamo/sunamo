public partial class TableRowHelper
{
    /// <summary>
    /// Kontroluje jen na horní hranici, 
    /// </summary>
    /// <returns></returns>
    public static string BasicTestEmptyAllowed(string name, string value, int maxlenght)
    {
        if (value.Length > maxlenght)
        {
            return name + AllStrings.space + "musí být kratší než/rovno " + maxlenght + " znaků";
        }

        return "";
    }
}