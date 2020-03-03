public partial class TableRowHelper{ 
/// <summary>
    /// 
    /// </summary>
    
    public static string BasicTestEmptyAllowed(string name, string value, int maxlenght)
    {
        if (value.Length > maxlenght)
        {
            return name + AllStrings.space + "musí být kratší než/rovno" + " " + maxlenght + " " + "znaků";
        }

        return "";
    }
}